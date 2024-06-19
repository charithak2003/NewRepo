using System.ComponentModel.DataAnnotations;

namespace Library1.Models
{
    public class Author
    {

        [Key]
        [Required]
        public int Id { get; set; }
        public string Author_Name { get; set; }

    }
}
