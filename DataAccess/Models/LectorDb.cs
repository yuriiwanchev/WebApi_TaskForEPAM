using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    internal record LectorDb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(40)]
        public string Email { get; set; }
        
        public LectionDb Lection { get; set; }
    }
}