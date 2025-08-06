using Microsoft.AspNetCore.Mvc;                        // For ControllerBase, ApiController, etc.
using Microsoft.EntityFrameworkCore;                   // For EF Core features
using CollageSportsApi.Models;                         // To use Result model
using CollageSportsApi.Data;                           // To access AppDbContext

// Marks the class as an API controller
[ApiController]

// Sets route like: api/results
[Route("api/[controller]")]
public class ResultsController : ControllerBase
{
    // Private variable for database context
    private readonly AppDbContext _context;

    // Constructor that gets AppDbContext through dependency injection
    public ResultsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/results
    // Returns all results with student details
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Result>>> GetResults()
    {
        return await _context.Results
                             .Include(r => r.StudentId)  // Include student info with result
                             .ToListAsync();           // Convert to list
    }

    // GET: api/results/5
    // Get result by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Result>> GetResult(int id)
    {
        var result = await _context.Results
                                   .Include(r => r.StudentId)
                                   .FirstOrDefaultAsync(r => r.Id == id);

        if (result == null)
        {
            return NotFound(); // 404
        }

        return result;
    }

    // POST: api/results
    // Create a new result
    [HttpPost]
    public async Task<ActionResult<Result>> CreateResult(Result result)
    {
        _context.Results.Add(result);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetResult), new { id = result.Id }, result);
    }

    // PUT: api/results/5
    // Update result details
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateResult(int id, Result result)
    {
        if (id != result.Id)
        {
            return BadRequest(); // 400
        }

        _context.Entry(result).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Results.Any(r => r.Id == id))
            {
                return NotFound(); // 404
            }
            else
            {
                throw;
            }
        }

        return NoContent(); // 204
    }

    // DELETE: api/results/5
    // Delete a result
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteResult(int id)
    {
        var result = await _context.Results.FindAsync(id);

        if (result == null)
        {
            return NotFound();
        }

        _context.Results.Remove(result);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
