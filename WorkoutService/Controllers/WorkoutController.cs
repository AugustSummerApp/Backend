using Microsoft.AspNetCore.Mvc;
using WorkoutService.Interface;
using WorkoutService.Models;

namespace WorkoutService.Controllers;


[ApiController]
[Route("api/[controller]")]
public class WorkoutsController : ControllerBase
{
    private readonly IWorkoutService _service;

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

    [HttpGet("bydate")]
    public async Task<ActionResult<IEnumerable<WorkoutModel>>> GetByDateAsync()
    {
        var list = await _service.GetByDateAsync();
        return Ok(list);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] WorkoutModel model)
    {
        var created = await _service.CreateAsync(model);
        return CreatedAtRoute("GetWorkoutById", new { id = created.Id }, created);
    }


}



