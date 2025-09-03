using Microsoft.AspNetCore.Mvc;
using WorkoutService.DTOs;
using WorkoutService.Interface;
using WorkoutService.Models;

namespace WorkoutService.Controllers;


[ApiController]
[Route("api/[controller]")]
public class WorkoutsController : ControllerBase
{
    private readonly IWorkoutService _service;
    private readonly TimeZoneInfo SweTz = TimeZoneInfo.FindSystemTimeZoneById("Europe/Stockholm");

    public WorkoutsController(IWorkoutService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}", Name = "GetWorkoutById")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var workout = await _service.GetByIdAsync(id);
        if (workout is null) return NotFound();
        return Ok(workout);
    }

    [HttpGet("summary")]
    public async Task<ActionResult<WorkoutSummaryDto>> GetSummary([FromQuery] DateOnly? date)
    {
        var todaySwe = DateOnly.FromDateTime(TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, SweTz));
        var target = date ?? todaySwe;

        var workouts = await _service.GetByDateAsync(target);

        var dto = new WorkoutSummaryDto
        {
            Date = target,
            PrevDate = target.AddDays(-1),
            NextDate = target.AddDays(+1),
            TotalWorkouts = workouts.Count(),
            Workouts = workouts
        };
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] WorkoutModel model)
    {
        var created = await _service.CreateAsync(model);
        return CreatedAtRoute("GetWorkoutById", new { id = created.Id }, created);
    }

}



