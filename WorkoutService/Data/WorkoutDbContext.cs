using Microsoft.EntityFrameworkCore;
using WorkoutService.Models;

namespace WorkoutService.Data;

public class WorkoutDbContext : DbContext
{
    public WorkoutDbContext(DbContextOptions<WorkoutDbContext> options)
        : base(options)
    {
    }

    public DbSet <WorkoutModel> WorkoutSession { get; set; }
    public DbSet<ExerciseModel> Exercises { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WorkoutModel>().ToTable("WorkoutSession");
        modelBuilder.Entity<ExerciseModel>().ToTable("ExerciseModel");

        modelBuilder.Entity<ExerciseModel>()
            .HasIndex(e => e.Name)
            .IsUnique();

        modelBuilder.Entity<WorkoutModel>()
            .HasIndex(w => new { w.ExerciseId, w.Date });
    }
}
