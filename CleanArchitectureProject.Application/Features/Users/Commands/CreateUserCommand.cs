using System;

namespace CleanArchitectureProject.Application.Features.Users.Commands;

public class CreateUserCommand
{
    public Guid CarId { get; set; }
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Surname { get; set; } = default!;
}
