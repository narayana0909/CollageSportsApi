using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CollageSportsApi.Data;
using CollageSportsApi.Models;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentsController(AppDbContext context)
    {
        _context = context;
    }

    // Get all students
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _context.Students.ToListAsync();
        return Ok(students);
    }

    // Get student by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) 
            return NotFound();
        return Ok(student);
    }

    // Add new student
    [HttpPost]
    public async Task<IActionResult> Add(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
    }

    // Update existing student
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Student student)
    {
        if (id != student.Id) return BadRequest();

        _context.Entry(student).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // Delete student
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
