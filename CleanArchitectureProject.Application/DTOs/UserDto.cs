using System;

namespace CleanArchitectureProject.Application.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public Guid CarId { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}
