using System;

namespace CleanArchitectureProject.Application.DTOs;

public class LoginResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Token { get; set; } = default!;
}
