using System;

namespace CleanArchitectureProject.Application.Features.Cars.Commands;

public class CreateCarCommand
{
    public string CarName { get; set; } = default!;
}
