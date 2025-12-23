using Microsoft.AspNetCore.Mvc;

namespace StudentManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        static private List<Student> students = new List<Student>
        {
            new Student { Id = 1, FirstName = "John", LastName = "Doe", Email = "123@gmail.com", Age = 20 },
            new Student { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "1234@mail.com", Age = 22 },
            new Student { Id = 3, FirstName = "Alice", LastName = "ramishvili", Email = "abc@gmail.com", Age = 19 }
        };
        [HttpGet]
        public ActionResult<List<Student>> GetStudents()
        {
            return Ok(students);
        }
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudentById(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        [HttpPost]
        public ActionResult<Student> CreateStudent(Student newStudent)
        {
            if(newStudent is null) return BadRequest();
            if(students.Any(s => s.Email == newStudent.Email))
            {
                return Conflict("A student with the same email already exists.");
            }
            newStudent.Id = students.Any() ?students.Max(s => s.Id) + 1 :1;
            newStudent.CreatedAt = DateTime.UtcNow.ToLocalTime();
            students.Add(newStudent);
            return CreatedAtAction(nameof(GetStudentById), new { id = newStudent.Id }, newStudent);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, Student updatedStudent)
        {
            if (updatedStudent is null) return BadRequest();
            var student = students.FirstOrDefault(s => s.Id == id);
            if(student == null) return NotFound();
            if(students.Any(s => s.Email == updatedStudent.Email && s.Id != id))
            {
                return Conflict("A student with the same email already exists.");
            }
            student.FirstName = updatedStudent.FirstName;
            student.LastName = updatedStudent.LastName;
            student.Email = updatedStudent.Email;
            student.Age = updatedStudent.Age;
            return Ok(student);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if(student == null) return NotFound();
            students.Remove(student);
            return NoContent();
        }
    }
}
