using CleanArchitectureProject.Application.DTOs;
using CleanArchitectureProject.Application.Features.Cars.Commands;
using CleanArchitectureProject.Application.Features.Cars.Handlers;
using CleanArchitectureProject.Application.Features.Cars.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly CreateCarHandler _create;
    private readonly GetCarByIdHandler _byId;
    private readonly GetAllCarsHandler _all;

    public CarsController(CreateCarHandler create, GetCarByIdHandler byId, GetAllCarsHandler all)
    {
        _create = create;
        _byId = byId;
        _all = all;
    }

    [HttpPost]
    public async Task<ActionResult<CarDto>> Create([FromBody] CreateCarCommand cmd)
    {
        var dto = await _create.HandleAsync(cmd);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CarDto>> GetById(Guid id)
        => Ok(await _byId.HandleAsync(new GetCarByIdQuery { CarId = id }));

    [HttpGet]
    public async Task<ActionResult<List<CarDto>>> GetAll()
        => Ok(await _all.HandleAsync());
}
