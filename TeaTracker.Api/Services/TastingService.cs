using Microsoft.EntityFrameworkCore;
using TeaTracker.Api.Data;
using TeaTracker.Api.Dtos;
using TeaTracker.Api.Models;

namespace TeaTracker.Api.Services;

public class TastingService(TeaTrackerDbContext db)
{
    public async Task<List<TastingResponse>?> GetByTeaAsync(int teaId)
    {
        bool teaExists = await db.Teas.AnyAsync(t => t.Id == teaId);

        if (!teaExists)
        {
            return null;
        }

        return await db.Tastings
            .AsNoTracking()
            .Where(t => t.TeaId == teaId)
            .OrderByDescending(t => t.Date)
            .Select(t => new TastingResponse(t.Id, t.Rating, t.Date, t.TeaId))
            .ToListAsync();
    }

    public async Task<CreateTastingResult> CreateAsync(int teaId, CreateTastingRequest request)
    {
        Tea? tea = await db.Teas.FirstOrDefaultAsync(t => t.Id == teaId);

        if (tea is null)
        {
            return new CreateTastingResult(CreateTastingStatus.TeaNotFound);
        }

        if (tea.TeaBagCount <= 0)
        {
            return new CreateTastingResult(CreateTastingStatus.OutOfStock);
        }

        tea.TeaBagCount--;

        Tasting tasting = new()
        {
            TeaId = teaId,
            Rating = request.Rating,
            Date = request.Date ?? DateTime.UtcNow
        };

        db.Tastings.Add(tasting);

        await db.SaveChangesAsync();

        TastingResponse response = new(tasting.Id, tasting.Rating, tasting.Date, tasting.TeaId);

        return new CreateTastingResult(CreateTastingStatus.Created, response);
    }
}
