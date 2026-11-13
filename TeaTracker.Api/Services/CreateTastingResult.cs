using TeaTracker.Api.Dtos;

namespace TeaTracker.Api.Services;

public enum CreateTastingStatus
{
    Created,
    TeaNotFound,
    OutOfStock
}

public record CreateTastingResult(CreateTastingStatus Status, TastingResponse? Tasting = null);