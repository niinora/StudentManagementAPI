using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementApi.Data;
using StudentManagementApi.Dtos;
using StudentManagementApi.Mappers;
using System.Threading.Tasks;

namespace StudentManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController(StudentDbContext context) : ControllerBase
    {
        private readonly StudentDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<StudentResponseDto>>> GetStudents()
        {
            var students = await _context.Students.Select(s => s.ToStudentResponseDto()).ToListAsync();
            return Ok(students);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<StudentResponseDto>> GetStudentById(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student.ToStudentResponseDto());
        }



        [HttpPost]
        public async Task<ActionResult<StudentResponseDto>> CreateStudent(StudentRequestDto newStudentDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _context.Students.AnyAsync(s => s.Email == newStudentDto.Email))
            {
                return Conflict("A student with the same email already exists.");
            }
            var newStudent = newStudentDto.ToStudent();
            newStudent.CreatedAt = DateTime.UtcNow.ToLocalTime();
            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudentById), new { id = newStudent.Id }, newStudent.ToStudentResponseDto());
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentRequestDto updatedStudentDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } 

            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            if (await _context.Students.AnyAsync(s => s.Email == updatedStudentDto.Email && s.Id != id))
            {
                return Conflict("A student with the same email already exists.");
            }

            student.FirstName = updatedStudentDto.FirstName;
            student.LastName = updatedStudentDto.LastName;
            student.Email = updatedStudentDto.Email;
            student.Age = updatedStudentDto.Age;

            await _context.SaveChangesAsync();  
            return Ok(student.ToStudentResponseDto());
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
