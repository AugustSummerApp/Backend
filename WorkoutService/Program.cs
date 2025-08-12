using Microsoft.EntityFrameworkCore;
using WorkoutService.Data;
using IWorkoutService = WorkoutService.Interface.IWorkoutService;
using WorkoutSvc = WorkoutService.Services.WorkoutService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WorkoutDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IWorkoutService, WorkoutSvc>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy => policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());
});


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowReact");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
