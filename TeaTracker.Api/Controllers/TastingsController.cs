using Microsoft.AspNetCore.Mvc;
using TeaTracker.Api.Dtos;
using TeaTracker.Api.Services;

namespace TeaTracker.Api.Controllers;

[ApiController]
[Route("api/teas/{teaId:int}/tastings")]
public class TastingsController(TastingService tastingService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TastingResponse>>> GetByTea(int teaId)
    {
        List<TastingResponse>? tastings = await tastingService.GetByTeaAsync(teaId);

        if (tastings == null)
        {
            return NotFound();
        }
        return Ok(tastings);
    }

    [HttpPost]
    public async Task<ActionResult<TastingResponse>> Create(int teaId, CreateTastingRequest request)
    {
        CreateTastingResult result = await tastingService.CreateAsync(teaId, request);

        switch (result.Status)
        {
            case CreateTastingStatus.TeaNotFound:
                return NotFound();
            case CreateTastingStatus.OutOfStock:
                return Problem(
                    statusCode: StatusCodes.Status409Conflict,
                    title: "Stock insuffisant",
                    detail: "Ce thé n'a plus de sachets disponibles."
                );
            case CreateTastingStatus.Created:
                return CreatedAtAction(nameof(GetByTea), new { teaId }, result.Tasting);
            default:
                throw new InvalidOperationException($"Statut inattendu : {result.Status}");
        }
    }
}
