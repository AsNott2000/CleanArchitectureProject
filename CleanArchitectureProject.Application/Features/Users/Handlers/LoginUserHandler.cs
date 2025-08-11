using System;
using CleanArchitectureProject.Application.DTOs;
using CleanArchitectureProject.Application.Features.Users.Commands;
using CleanArchitectureProject.Domain.Interfaces;
using CleanArchitectureProject.Domain.Services;

namespace CleanArchitectureProject.Application.Features.Users.Handlers;

public class LoginUserHandler
{
    private readonly IUsersRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public LoginUserHandler(IUsersRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<UserDto> HandleAsync(LoginUserCommand command, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(command.UserName);
        if (user is null)
            throw new InvalidOperationException("Kullanıcı bulunamadı.");

        if (!_passwordHasher.Verify(command.Password, user.Password))
            throw new InvalidOperationException("Şifre hatalı.");

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            UserName = user.UserName,
            CarId = user.CarId
        };
    }

}
