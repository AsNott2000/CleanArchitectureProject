using System;
using CleanArchitectureProject.Application.DTOs;
using CleanArchitectureProject.Application.Features.Users.Commands;
using CleanArchitectureProject.Domain.Entities;
using CleanArchitectureProject.Domain.Interfaces;
using CleanArchitectureProject.Domain.Services;

namespace CleanArchitectureProject.Application.Features.Users.Handlers;

public class CreateUserHandler
{
    private readonly IUsersRepository _userRepository;
    private readonly ICarsRepository _carsRepository;
    private readonly IPasswordHasher _passwordHasher;

    //DI, Dependency Injection
    public CreateUserHandler(
        IUsersRepository userRepository,
        ICarsRepository carRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _carsRepository = carRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserDto> HandleAsync(CreateUserCommand command)
    {
        // Kullanıcı adı benzersiz mi?
        if (await _userRepository.IsUserNameExistsAsync(command.UserName))
            throw new InvalidOperationException("Kullanici adi bulunamadi");

        if (string.IsNullOrEmpty(command.UserName) || string.IsNullOrEmpty(command.Password))
            throw new ArgumentException("Kullanici adi ve sifre bos olamaz");


        var car = await _carsRepository.GetByIdAsync(command.CarId);
        if (car == null)
            throw new InvalidOperationException("Araba bulunamadi");

        var hashedPassword = _passwordHasher.Hash(command.Password);

        var user = new UsersModel
        {
            UserName = command.UserName,
            Password = hashedPassword,
            Name = command.Name,
            Surname = command.Surname,
            CarId = command.CarId
        };

        await _userRepository.AddAsync(user);
        
        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Name = user.Name,
            Surname = user.Surname,
            CarId = user.CarId
        };
    }
}
