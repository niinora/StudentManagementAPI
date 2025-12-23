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
        [Range(16, int.MaxValue, ErrorMessage = "Age must be at least 16.")]
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.ToLocalTime();

    }
}
