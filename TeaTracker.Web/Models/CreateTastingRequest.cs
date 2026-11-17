using System.ComponentModel.DataAnnotations;

namespace TeaTracker.Web.Models;

public class CreateTastingRequest
{
    [Range(0, 5)]
    public int Rating { get; set; }

    public DateTime? Date { get; set; }
}
