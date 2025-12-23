using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StudentManagementApi.Data
{
    public class StudentDbContext(DbContextOptions<StudentDbContext> options) : DbContext(options) 
    {
        public DbSet<Student> Students => Set<Student>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Email)
                .IsUnique();

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FirstName = "Nino", LastName = "Ramishvili", Email = "nin.ramishvili4@gmail.com", Age = 21 },
                new Student { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "1234@mail.com", Age = 22 },
                new Student { Id = 4, FirstName = "Alice", LastName = "ramishvili", Email = "abc@gmail.com", Age = 19 }
            );
        }
    }
    
}
