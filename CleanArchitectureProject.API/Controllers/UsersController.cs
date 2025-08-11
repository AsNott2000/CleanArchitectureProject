using System;
using CleanArchitectureProject.Application.DTOs;
using CleanArchitectureProject.Application.Features.Users.Commands;
using CleanArchitectureProject.Application.Features.Users.Handlers;
using CleanArchitectureProject.Application.Features.Users.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly CreateUserHandler _create;
    private readonly LoginUserHandler _login;
    private readonly GetUserByIdHandler _byId;
    private readonly GetAllUsersHandler _all;

    public UsersController(CreateUserHandler create, LoginUserHandler login, GetUserByIdHandler byId, GetAllUsersHandler all)
    {
        _create = create;
        _login = login;
        _byId = byId;
        _all = all;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserCommand cmd)
    {
        var dto = await _create.HandleAsync(cmd);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginUserCommand cmd)
    {
        var dto = await _login.HandleAsync(cmd);
        return Ok(dto);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> GetById(Guid id)
        => Ok(await _byId.HandleAsync(new GetUserByIdQuery { UserId = id }));

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAll()
        => Ok(await _all.HandleAsync());
}
