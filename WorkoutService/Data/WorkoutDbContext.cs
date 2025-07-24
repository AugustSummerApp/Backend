﻿using Microsoft.EntityFrameworkCore;
using WorkoutService.Models;

namespace WorkoutService.Data;

public class WorkoutDbContext : DbContext
{
    public WorkoutDbContext(DbContextOptions<WorkoutDbContext> options)
        : base(options)
    {
    }

    public DbSet <WorkoutModel> WorkoutSession { get; set; }
}
