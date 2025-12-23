using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementApi.Data;
using System.Threading.Tasks;

namespace StudentManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController(StudentDbContext context) : ControllerBase
    {
        private readonly StudentDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            return Ok(await _context.Students.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student newStudent)
        {
            if (newStudent is null) return BadRequest();
            if (await _context.Students.AnyAsync(s => s.Email == newStudent.Email))
            {
                return Conflict("A student with the same email already exists.");
            }
            newStudent.CreatedAt = DateTime.UtcNow.ToLocalTime();
            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudentById), new { id = newStudent.Id }, newStudent);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student updatedStudent)
        {
            if (updatedStudent is null) return BadRequest();

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            if (await _context.Students.AnyAsync(s => s.Email == updatedStudent.Email && s.Id != id))
            {
                return Conflict("A student with the same email already exists.");
            }

            student.FirstName = updatedStudent.FirstName;
            student.LastName = updatedStudent.LastName;
            student.Email = updatedStudent.Email;
            student.Age = updatedStudent.Age;

            await _context.SaveChangesAsync();  
            return Ok(student);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
