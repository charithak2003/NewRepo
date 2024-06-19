using System.ComponentModel.DataAnnotations;

namespace Library1.Models
{
    public class Book
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Title { get; set; }
        public string PUB_YEAR { get; set; }
        public string Publisher_Name { get; set; }
        public Report Report { get; set; }
    }
}

