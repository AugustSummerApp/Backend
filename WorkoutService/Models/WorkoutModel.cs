using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WorkoutService.Models;

public class WorkoutModel
{
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required] 
    public string Name { get; set; } = null!;

    [Required]
    public int ExerciseId { get; set; }
    public ExerciseModel Exercise { get; set; } = null!;

    [Range(1, 30)]
    public int Sets { get; set; }

    [Range(1, 30)]
    public int Reps { get; set; }

    [Precision(18, 2), Range(0, 1000)]
    public decimal Weight { get; set; }

    [Required]
    public string Equipment { get; set; } = string.Empty;

}
