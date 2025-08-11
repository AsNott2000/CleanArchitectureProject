using System;
using CleanArchitectureProject.Application.DTOs;
using CleanArchitectureProject.Domain.Interfaces;

namespace CleanArchitectureProject.Application.Features.Cars.Handlers;

public class GetAllCarsHandler
{
    private readonly ICarsRepository _carsRepository;

    public GetAllCarsHandler(ICarsRepository carsRepository)
    {
        _carsRepository = carsRepository;
    }

    public async Task<List<CarDto>> HandleAsync(CancellationToken ct = default)
    {
        var cars = await _carsRepository.GetAllAsync();
        return cars.Select(c => new CarDto { Id = c.Id, CarName = c.CarName }).ToList();
    }
}
