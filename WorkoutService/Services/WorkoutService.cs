using Microsoft.EntityFrameworkCore;
using WorkoutService.Data;
using WorkoutService.Interface;
using WorkoutService.Models;

namespace WorkoutService.Services;

public class WorkoutService : IWorkoutService
{
    private readonly WorkoutDbContext _context;

    public WorkoutService(WorkoutDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WorkoutModel>> GetAllAsync()
    {
        return await _context.WorkoutSession
                             .AsNoTracking()
                             .ToListAsync();
    }

    public async Task<WorkoutModel?> GetByIdAsync(int id)
    {
        return await _context.WorkoutSession
                             .AsNoTracking()
                             .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<WorkoutModel> CreateAsync(WorkoutModel model)
    {
        _context.WorkoutSession.Add(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<IEnumerable<WorkoutModel>> GetByDateAsync(DateOnly Date)
    {
        var start = Date.ToDateTime(TimeOnly.MinValue);
        var end = Date.ToDateTime(TimeOnly.MaxValue);

        return await _context.WorkoutSession
            .AsNoTracking()
            .Where(w => w.Date >= start && w.Date < end)
            .OrderBy(w => w.Id)
            .ToListAsync();
    }
}
