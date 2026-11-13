using Microsoft.AspNetCore.Mvc;
using TeaTracker.Api.Dtos;
using TeaTracker.Api.Models;
using TeaTracker.Api.Services;

namespace TeaTracker.Api.Controllers;

[ApiController]
[Route("api/teas")]
public class TeasController(TeaService teaService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TeaResponse>>> GetAll([FromQuery] TeaType? type, [FromQuery] string? search)
    {
        List<TeaResponse> teas = await teaService.GetAllAsync(type, search);
        return Ok(teas);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TeaResponse>> GetById(int id)
    {
        TeaResponse? tea = await teaService.GetByIdAsync(id);

        if (tea is null)
        {
            return NotFound();
        }

        return Ok(tea);
    }

    [HttpPost]
    public async Task<ActionResult<TeaResponse>> Create(CreateTeaRequest request)
    {
        TeaResponse created = await teaService.CreateAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}
