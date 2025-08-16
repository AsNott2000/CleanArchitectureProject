using System;

namespace CleanArchitectureProject.Application.Features.Users.Commands;

public class LoginUserCommand
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
}
