using Application.Services;
using Domain.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using Domain.Requests;
using System.Collections.Generic;
using Domain.Exceptions;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize]
    public IActionResult List()
    {
        var isAdm = User.FindFirst(ClaimTypes.Role)!.Value == "Adm";
        List<UserResponse> users;
        if (isAdm)
        {
            users = _service.List();
        }
        else
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            users = new List<UserResponse> { _service.GetById(userId)! };
        }
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var isAdm = User.FindFirst(ClaimTypes.Role)!.Value == "Adm";
        if (!isAdm && userId != id)
            throw new BadRequestException("UserId", "UserID must be the same as requested");
        var user = _service.GetById(id);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Post([FromBody] BaseUserRequest user)
    {
        var newUser = _service.Create(user);
        return Ok(newUser);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] UpdateUserRequest user)
    {
        var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var isAdm = User.FindFirst(ClaimTypes.Role)!.Value == "Adm";
        if (!isAdm && userId != id)
            throw new BadRequestException("UserId", "UserID must be the same as requested");
        user.Id = id;
        var updatedUser = _service.Update(user);
        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Adm")]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return NoContent();
    }
}
