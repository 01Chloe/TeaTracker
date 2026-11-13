using System.ComponentModel.DataAnnotations;

namespace TeaTracker.Api.Dtos;

public class CreateTastingRequest
{
    [Range(0, 5)]
    public required int Rating { get; set; }

    public DateTime? Date { get; set; }
}
