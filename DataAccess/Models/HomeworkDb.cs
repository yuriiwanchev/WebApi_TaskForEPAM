using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    internal class HomeworkDb
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(1000)]
        public string Name { get; set; }

        public List<LectionLogDb> LectionLog { get; set; } = new List<LectionLogDb>();
    }
}