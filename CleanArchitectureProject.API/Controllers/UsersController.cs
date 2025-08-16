using System;
using CleanArchitectureProject.Application.DTOs;
using CleanArchitectureProject.Application.Features.Users.Commands;
using CleanArchitectureProject.Application.Features.Users.Handlers;
using CleanArchitectureProject.Application.Features.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly GetUserByIdHandler _byId;
    private readonly GetAllUsersHandler _all;

    public UsersController(CreateUserHandler create, LoginUserHandler login, GetUserByIdHandler byId, GetAllUsersHandler all)
    {
        
        _byId = byId;
        _all = all;
    }
    

    [Authorize]
    [HttpGet("{id:guid}", Name = "GetUserById")]
    public async Task<ActionResult<UserDto>> GetById(Guid id)
        => Ok(await _byId.HandleAsync(new GetUserByIdQuery { UserId = id }));

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAll()
        => Ok(await _all.HandleAsync());
}
