using System;
using CleanArchitectureProject.Application.DTOs;
using CleanArchitectureProject.Application.Features.Cars.Queries;
using CleanArchitectureProject.Domain.Interfaces;

namespace CleanArchitectureProject.Application.Features.Cars.Handlers;

public class GetCarByIdHandler
{
    private readonly ICarsRepository _carsRepository;

    public GetCarByIdHandler(ICarsRepository carsRepository)
    {
        _carsRepository = carsRepository;
    }

    public async Task<CarDto> HandleAsync(GetCarByIdQuery query, CancellationToken ct = default)
    {
        var car = await _carsRepository.GetByIdAsync(query.CarId);
        if (car is null)
            throw new InvalidOperationException("Araba bulunamadı.");

        return new CarDto { Id = car.Id, CarName = car.CarName };
    }

}
