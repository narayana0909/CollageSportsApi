using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CollageSportsApi.Models;
using CollageSportsApi.Data;

[ApiController]
[Route("api/[controller]")]
public class RegistrationsController : ControllerBase
{
    private readonly AppDbContext _context;

    public RegistrationsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/registrations
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Registration>>> GetRegistrations()
    {
        return await _context.Registrations.Include(r => r.StudentId).ToListAsync();
    }

    // GET: api/registrations/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Registration>> GetRegistration(int id)
    {
        var registration = await _context.Registrations.Include(r => r.StudentId).FirstOrDefaultAsync(r => r.Id == id);

        if (registration == null)
            return NotFound();

        return registration;
    }

    // POST: api/registrations
    [HttpPost]
    public async Task<ActionResult<Registration>> CreateRegistration(Registration registration)
    {
        registration.RegisteredOn = DateTime.Now;

        _context.Registrations.Add(registration);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRegistration), new { id = registration.Id }, registration);
    }

    // DELETE: api/registrations/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegistration(int id)
    {
        var registration = await _context.Registrations.FindAsync(id);

        if (registration == null)
            return NotFound();

        _context.Registrations.Remove(registration);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
