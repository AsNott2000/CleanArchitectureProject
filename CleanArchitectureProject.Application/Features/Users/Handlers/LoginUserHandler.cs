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
    private readonly IJwtTokenService _jwt;

    public LoginUserHandler(IUsersRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenService jwt)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwt = jwt;
    }
    
    public async Task<LoginResponseDto> HandleAsync(LoginUserCommand command, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByUserNameAsync(command.UserName);
        if (user is null)
            throw new InvalidOperationException("Kullanıcı bulunamadı.");

        if (!_passwordHasher.Verify(command.Password, user.Password))
            throw new InvalidOperationException("Şifre hatalı.");

        var token = _jwt.GenerateToken(user.Id, user.UserName);
        return new LoginResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            UserName = user.UserName,
            Token = token
        };
    }

}
