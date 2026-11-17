namespace TeaTracker.Web.Models;

public static class TeaTypeExtensions
{
    public static string ToLabel(this TeaType type) => type switch
    {
        TeaType.Green => "Thé vert",
        TeaType.Black => "Thé noir",
        TeaType.HerbalTea => "Tisane",
        _ => type.ToString()
    };
}
