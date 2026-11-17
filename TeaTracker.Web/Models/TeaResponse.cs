namespace TeaTracker.Web.Models;

public record TeaResponse(int Id, string Title, string Brand, decimal Price, int TeaBagCount, TeaType TeaType);