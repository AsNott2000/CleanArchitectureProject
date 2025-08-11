using System;
using CleanArchitectureProject.Application.DTOs;
using CleanArchitectureProject.Application.Features.Cars.Commands;
using CleanArchitectureProject.Domain.Entities;
using CleanArchitectureProject.Domain.Interfaces;

namespace CleanArchitectureProject.Application.Features.Cars.Handlers;

public class CreateCarHandler
{
    private readonly ICarsRepository _carsRepository;

    public CreateCarHandler(ICarsRepository carsRepository)
    {
        _carsRepository = carsRepository;
    }

    public async Task<CarDto> HandleAsync(CreateCarCommand command, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(command.CarName))
            throw new ArgumentException("CarName zorunludur.");

        if (await _carsRepository.IsCarNameExistsAsync(command.CarName))
            throw new InvalidOperationException("Aynı isimde bir araba zaten mevcut.");

        var car = new CarsModel
        {
            Id = Guid.NewGuid(),
            CarName = command.CarName
        };

        await _carsRepository.AddAsync(car);

        return new CarDto { Id = car.Id, CarName = car.CarName };
    }
}
