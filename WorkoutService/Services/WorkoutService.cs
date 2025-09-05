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
            .Include(w => w.Exercise) 
            .OrderBy(w => w.Date)
            .ThenBy(w => w.Id)
            .ToListAsync();
    }

    public async Task<WorkoutModel?> GetByIdAsync(int id)
    {
        return await _context.WorkoutSession
            .AsNoTracking()
            .Include(w => w.Exercise)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<WorkoutModel> CreateAsync(WorkoutModel model)
    {
        var exists = await _context.Exercises
            .AsNoTracking()
            .AnyAsync(e => e.Id == model.ExerciseId);

        if (!exists)
            throw new ArgumentException($"ExerciseId {model.ExerciseId} does not exist");

        _context.WorkoutSession.Add(model);
        await _context.SaveChangesAsync();

        await _context.Entry(model).Reference(m => m.Exercise).LoadAsync();
        return model;
    }

    public async Task<IEnumerable<WorkoutModel>> GetByDateAsync(DateOnly date)
    {
        var start = date.ToDateTime(TimeOnly.MinValue);
        var end = date.ToDateTime(TimeOnly.MaxValue);

        return await _context.WorkoutSession
            .AsNoTracking()
            .Include(w => w.Exercise)
            .Where(w => w.Date >= start && w.Date < end)
            .OrderBy(w => w.Id)
            .ToListAsync();
    }
}
