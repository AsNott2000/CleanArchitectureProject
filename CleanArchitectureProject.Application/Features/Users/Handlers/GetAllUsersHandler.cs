using System;
using CleanArchitectureProject.Application.DTOs;
using CleanArchitectureProject.Domain.Interfaces;

namespace CleanArchitectureProject.Application.Features.Users.Handlers;

public class GetAllUsersHandler
{
    private readonly IUsersRepository _userRepository;

    public GetAllUsersHandler(IUsersRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> HandleAsync(CancellationToken ct = default)
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name,
            Surname = u.Surname,
            UserName = u.UserName,
            CarId = u.CarId
        }).ToList();
    }
}
