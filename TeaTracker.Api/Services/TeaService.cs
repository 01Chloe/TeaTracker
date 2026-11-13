using Microsoft.EntityFrameworkCore;
using TeaTracker.Api.Data;
using TeaTracker.Api.Dtos;
using TeaTracker.Api.Models;

namespace TeaTracker.Api.Services;

public class TeaService(TeaTrackerDbContext db)
{
    public async Task<List<TeaResponse>> GetAllAsync(TeaType? type, string? search)
    {
        IQueryable<Tea> query = db.Teas.AsNoTracking();

        if (type is not null)
        {
            query = query.Where(t => t.TeaType == type.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(t => t.Title.Contains(search) || t.Brand.Contains(search));
        }

        return await query
            .OrderBy(t => t.Title)
            .Select(t => new TeaResponse(
                t.Id,
                t.Title,
                t.Brand,
                t.Price,
                t.TeaBagCount,
                t.TeaType
            ))
            .ToListAsync();
    }

    public async Task<TeaResponse?> GetByIdAsync(int id)
    {
        return await db.Teas
            .AsNoTracking()
            .Where(t => t.Id == id)
            .Select(t => new TeaResponse(
                t.Id,
                t.Title,
                t.Brand,
                t.Price,
                t.TeaBagCount,
                t.TeaType
            ))
            .FirstOrDefaultAsync();
    }

    public async Task<TeaResponse> CreateAsync(CreateTeaRequest request)
    {
        Tea tea = new()
        {
            Title = request.Title,
            Brand = request.Brand,
            Price = request.Price,
            TeaBagCount = request.TeaBagCount,
            TeaType = request.TeaType
        };

        db.Teas.Add(tea);

        await db.SaveChangesAsync();

        return new TeaResponse(
            tea.Id,
            tea.Title,
            tea.Brand,
            tea.Price,
            tea.TeaBagCount,
            tea.TeaType
        );
    }
}
