namespace TeaTracker.Api.Models;

public class Tea(int id, string title, string brand, int price, int teaCount)
{
    public int Id { get; } = id;
    public string Title { get; set; } = title;
    public string Brand { get; set; } = brand;
    public int Price { get; set; } = price;
    public int TeaCount { get; set; } = teaCount;
}
