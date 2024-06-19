using System.ComponentModel.DataAnnotations;

namespace Library1.Models
{
    public class Publisher
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public long Phone { get; set; }
        public string Address { get; set; }
    }
}
