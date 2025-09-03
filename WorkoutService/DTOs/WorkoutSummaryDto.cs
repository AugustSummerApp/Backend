using WorkoutService.Models;

namespace WorkoutService.DTOs;

public class WorkoutSummaryDto
{
    public DateOnly Date {  get; set; }
    public DateOnly PrevDate { get; set; }
    public DateOnly NextDate { get; set; }
    public int TotalWorkouts { get; set; }
    public IEnumerable<WorkoutModel> Workouts { get; set; } = [];
}
