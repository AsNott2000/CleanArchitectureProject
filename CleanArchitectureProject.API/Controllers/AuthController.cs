using System;
using CleanArchitectureProject.Application.DTOs;
using CleanArchitectureProject.Application.Features.Users.Commands;
using CleanArchitectureProject.Application.Features.Users.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureProject.API.Controllers;

public class AuthController : ControllerBase
{
    private readonly CreateUserHandler _create;
    private readonly LoginUserHandler _login;

    public AuthController(CreateUserHandler create, LoginUserHandler login)
    {

        _create = create;
        _login = login;
    }
    
    [HttpPost("Register")]
    public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserCommand cmd)
    {
        var dto = await _create.HandleAsync(cmd);
        return CreatedAtRoute(
            routeName: "GetUserById",
            routeValues: new { id = dto.Id },
            value: dto);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginUserCommand cmd)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var dto = await _login.HandleAsync(cmd);
        return Ok(dto);
    }
}
