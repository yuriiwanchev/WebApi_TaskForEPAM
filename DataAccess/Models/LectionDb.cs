using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    internal record LectionDb
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public int LectorId { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
        public LectorDb Lector { get; set; }
        
        public List<LectionLogDb> LectionLog { get; set; } = new List<LectionLogDb>();
    }
}