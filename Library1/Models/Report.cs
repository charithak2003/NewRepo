using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library1.Models
{
    public class Report
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int Student_Id { get; set; }
        [ForeignKey(nameof(Student_Id))]
        public int Book_Id { get; set; }
        [ForeignKey(nameof(Book_Id))]
        public virtual Book Book { get; set; }
        public virtual Student Student { get; set; }
        public DateOnly Issue_date { get; set; }
        public DateOnly Return_date { get; set; }
        public float Penalty {  get; set; }

    }
}
