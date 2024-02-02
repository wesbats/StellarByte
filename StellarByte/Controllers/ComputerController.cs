﻿using Microsoft.AspNetCore.Mvc;
using Domain.Request;
using Application.Services;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComputerController : ControllerBase
{
    private readonly IComputerService _service;

    public ComputerController(IComputerService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public IActionResult List()
    {
        var computers = _service.List();
        return Ok(computers);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var computer = _service.GetById(id);
        return computer is null ? NotFound() : Ok(computer);
    }

    [HttpPost]
    public IActionResult Post([FromBody]BaseComputerRequest request)
    {
        var newComputer = _service.Create(request);
        return Ok(newComputer);
    }

    [HttpPost("{id}")]
    public IActionResult Put(int id, [FromBody]UpdateComputerRequest requestComputer)
    {
        requestComputer.Id = id;
        var updatedComputer = _service.Update(requestComputer);
        return Ok(updatedComputer);
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return NoContent();
    }
}
