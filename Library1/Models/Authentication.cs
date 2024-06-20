using System.ComponentModel.DataAnnotations;

namespace Library1.Models
{
    public class Authentication
    {
      
            [Key]
            [Required]
            public int Id { get; set; }
            public int UserId { get; set; }
            public string Password { get; set; }
        }
    }
