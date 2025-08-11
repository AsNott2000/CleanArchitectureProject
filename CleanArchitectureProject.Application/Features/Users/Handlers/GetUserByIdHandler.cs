using System;
using CleanArchitectureProject.Application.DTOs;
using CleanArchitectureProject.Application.Features.Users.Queries;
using CleanArchitectureProject.Domain.Interfaces;

namespace CleanArchitectureProject.Application.Features.Users.Handlers;

public class GetUserByIdHandler
{
    private readonly IUsersRepository _userRepository;

    public GetUserByIdHandler(IUsersRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> HandleAsync(GetUserByIdQuery query, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId);
        if (user is null)
            throw new InvalidOperationException("Kullanıcı bulunamadı.");

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
