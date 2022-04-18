using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    internal class LectionLogDb
    {
        public int LectionId { get; set; } 
        public int StudentId { get; set; } 
        
        [Required]
        public bool Attendance { get; set; } 
        public int HomeworkId { get; set; }
        
        [Required]
        public int Score { get; set; }

        public List<LectionDb> Lections { get; set; } = new List<LectionDb>();
        public List<StudentDb> Students { get; set; } = new List<StudentDb>();
        public List<HomeworkDb> Homeworks { get; set; } = new List<HomeworkDb>();

        // public LectionDb Lection { get; set; }
        // public StudentDb Student { get; set; }
        // public HomeworkDb Homework { get; set; }
    }
}