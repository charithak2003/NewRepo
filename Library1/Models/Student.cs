using System.ComponentModel.DataAnnotations;

namespace Library1.Models
{
    public class Student
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string usn { get; set; }
        public string Department { get; set; }
        public string sem { get; set; }
        public Report Report { get; set; }
    }
}

