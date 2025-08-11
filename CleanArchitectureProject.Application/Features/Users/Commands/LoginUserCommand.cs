using System;

namespace CleanArchitectureProject.Application.Features.Users.Commands;

public class LoginUserCommand
{
    public Guid UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
}
