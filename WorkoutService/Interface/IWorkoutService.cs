using WorkoutService.Models;

namespace WorkoutService.Interface
{
    public interface IWorkoutService
    {
        Task<WorkoutModel> CreateAsync(WorkoutModel model);
        Task<IEnumerable<WorkoutModel>> GetAllAsync();
        Task<WorkoutModel?> GetByIdAsync(int id);
        Task<IEnumerable<WorkoutModel>> GetByDateAsync(DateOnly Date);
    }
}