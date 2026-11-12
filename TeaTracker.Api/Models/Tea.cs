namespace TeaTracker.Api.Models;

public class Tea
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Brand { get; set; }
    public decimal Price { get; set; }
    public int TeaBagCount { get; set; }
    public TeaType TeaType { get; set; }
    public List<Tasting> Tastings { get; set; } = [];
    
}
