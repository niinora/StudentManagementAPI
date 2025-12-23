using System.ComponentModel.DataAnnotations;

namespace StudentManagementApi
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Range(16, 100, ErrorMessage = "Age must be between 16 and 100.")]
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
