namespace WorkoutService.Models;

public class WorkoutModel
{
    public int Id { get; set; } 
    public DateTime Date { get; set; } 
    public string Name { get; set; } = null!;
    public string ExerciseType { get; set; } = string.Empty;
    public string Sets { get; set; } = null!;
    public string Reps { get; set; } = null!;
    public decimal Weight { get; set; } 
    public string Equipment { get; set; } = string.Empty;

}
