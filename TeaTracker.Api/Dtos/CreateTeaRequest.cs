using System.ComponentModel.DataAnnotations;
using TeaTracker.Api.Models;

namespace TeaTracker.Api.Dtos;

public class CreateTeaRequest
{
    [Required, StringLength(100)]
    public required string Title { get; set; }

    [Required, StringLength(100)]
    public required string Brand { get; set; }

    [Range(0, 10000)]
    public decimal Price { get; set; }

    [Range(0, 10000)]
    public int TeaBagCount { get; set; }

    [EnumDataType(typeof(TeaType))]
    public TeaType TeaType { get; set; }
}
