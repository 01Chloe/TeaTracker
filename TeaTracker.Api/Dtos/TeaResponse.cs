using TeaTracker.Api.Models;

namespace TeaTracker.Api.Dtos;

public record TeaResponse(int Id, string Title, string Brand, decimal Price, int TeaBagCount, TeaType TeaType);

