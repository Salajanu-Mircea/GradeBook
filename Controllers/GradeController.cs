using Microsoft.AspNetCore.Mvc;
using Siemens.Internship2026.GradeBook.Interfaces;

namespace Siemens.Internship2026.GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
// Renamed from ItemController to GradeController
public class GradeController : ControllerBase
{
    // OLD: private readonly IItemReader _reader;  <- controller talked directly to the repository
    private readonly IGradeService _service;
    private readonly ILogger<GradeController> _logger; // replaced Console.WriteLine with ILogger

    // OLD: public ItemController(IItemReader reader)
    public GradeController(IGradeService service, ILogger<GradeController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // OLD: Console.WriteLine($"[LOG] {DateTime.UtcNow}: GET api/item called");
        _logger.LogInformation("GET api/grade called at {Time}", DateTime.UtcNow);

        var grades = await _service.GetAllAsync();

        // OLD (business logic that was here — removed) and moved to the service layer
        // Controller just returns data. Business logic lives in the Service layer.

        return Ok(grades);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        // OLD: Console.WriteLine($"[LOG] {DateTime.UtcNow}: GET api/item/{id} called");
        _logger.LogInformation("GET api/grade/{Id} called at {Time}", id, DateTime.UtcNow);

        if (id <= 0)
        {
            // OLD: Console.WriteLine($"[LOG] Invalid id: {id}");
            _logger.LogWarning("Invalid id: {Id}", id);
            return BadRequest("Id must be a positive integer.");
        }

        var grade = await _service.GetByIdAsync(id);
        if (grade == null)
        {
            // OLD: Console.WriteLine($"[LOG] Item {id} not found");
            _logger.LogWarning("Grade {Id} not found", id);
            return NotFound($"Grade with Id {id} was not found.");
        }
        
        return Ok(grade);
    }
    
    // New endpoint for III:
    // Get api/grade/passing-active?count=N
    // Returns the first N grades that are active and passing(>=5)
    // the count is provided via query parameter
    // Valid URL "https://localhost:44361/api/grade/passing-active?count=1"
    [HttpGet("passing-active")]
    public async Task<IActionResult> GetPassingActiveGrades([FromQuery] int count)
    {
        _logger.LogInformation("GET api/grade/passing-active?count={Count} called at {Time}", count, DateTime.UtcNow);

        if (count <= 0)
        {
            _logger.LogWarning("You must provide a valid number(>0): {Count}", count);
            return BadRequest("Count must be a positive integer.");
        }
        
        var grades = await _service.GetNPassingActiveGradesAsync(count);
        return Ok(grades);
    }
}
