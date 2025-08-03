using WorkoutService.Models;

namespace WorkoutService.Interface
{
    public interface IWorkoutService
    {
        Task<WorkoutModel> CreateAsync(WorkoutModel model);
        Task<IEnumerable<WorkoutModel>> GetAllAsync();
        Task<IEnumerable<WorkoutModel>> GetByDateAsync();
        Task<WorkoutModel?> GetByIdAsync(int id);
    }
}