using System.ComponentModel.DataAnnotations;

namespace TeaTracker.Web.Models;

public class CreateTeaRequest
{
    [Required, StringLength(100, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(100, MinimumLength = 1)]
    public string Brand { get; set; } = string.Empty;

    [Range(0, 10000)]
    public decimal Price { get; set; }

    [Range(0, 10000)]
    public int TeaBagCount { get; set; }

    public TeaType TeaType { get; set; }
}
