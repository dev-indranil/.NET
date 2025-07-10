namespace StudentApi.Models
{
    public class Student
    {
        public int Id { get; set; }     // Primary key
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string Email { get; set; } = null!;
    }
}
