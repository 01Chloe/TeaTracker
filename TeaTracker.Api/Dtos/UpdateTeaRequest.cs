using System.ComponentModel.DataAnnotations;
using TeaTracker.Api.Models;

namespace TeaTracker.Api.Dtos;

public class UpdateTeaRequest
{
    [StringLength(100, MinimumLength = 1)]
    public string? Title { get; set; }

    [StringLength(100, MinimumLength = 1)]
    public string? Brand { get; set; }

    [Range(0, 10000)]
    public decimal? Price { get; set; }

    [Range(0, 10000)]
    public int? TeaBagCount { get; set; }

    [EnumDataType(typeof(TeaType))]
    public TeaType? TeaType { get; set; }
}
