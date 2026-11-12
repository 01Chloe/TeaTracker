namespace TeaTracker.Api.Models;

public class Tasting
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public DateTime Date { get; set; }
    public int TeaId { get; set; }
    public Tea Tea { get; set; } = null!;
}
