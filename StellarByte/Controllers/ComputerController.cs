using Microsoft.AspNetCore.Mvc;
using Domain.Request;
using Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Adm")]
public class ComputerController : ControllerBase
{
    private readonly IComputerService _service;

    public ComputerController(IComputerService service)
    {
        _service = service;
    }
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult List()
    {
        var computers = _service.List();
        return Ok(computers);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
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
