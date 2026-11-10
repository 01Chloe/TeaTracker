namespace TeaTracker.Api.Models;

public class Tasting(int id, int rate, DateTime date)
{
    public int Id { get; } = id;
    public int? Rate { get; set; } = rate;
    public DateTime Date { get; set; } = date;
}
