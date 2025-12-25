using StudentManagementApi.Dtos;

namespace StudentManagementApi.Mappers
{
    public static class StudentMappers
    {
        public static StudentResponseDto ToStudentResponseDto(this Student student)
        {
            return new StudentResponseDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Age = student.Age
            };
        }
        public static StudentRequestDto ToStudentRequestDto(this Student student)
        {
            return new StudentRequestDto
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Age = student.Age
            };
        }
        public static Student ToStudent(this StudentRequestDto studentDto)
        {
            return new Student
            {
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                Email = studentDto.Email,
                Age = studentDto.Age
            };
        }
    }
}
