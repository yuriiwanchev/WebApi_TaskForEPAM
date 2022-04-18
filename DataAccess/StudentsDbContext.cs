using System;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    internal class StudentsDbContext : DbContext
    {
        public DbSet<StudentDb> Students { get; set; }
        public DbSet<LectorDb> Lectors { get; set; }
        public DbSet<LectionDb> Lections { get; set; }
        public DbSet<HomeworkDb> Homeworks { get; set; }
        public DbSet<LectionLogDb> LectionLogs { get; set; }

        public StudentsDbContext()
        {
            // Database.EnsureDeleted();
            // Database.EnsureCreated();

            // Database.Migrate();
        }

        public StudentsDbContext(DbContextOptions<StudentsDbContext> options) : base(options)
        {
            // bool isDeleted = Database.EnsureDeleted();
            // if (isDeleted) Console.WriteLine("База данных была удалена");
            
            // bool isCreated = Database.EnsureCreated();
            // if (isCreated) Console.WriteLine("База данных была создана");
            // else Console.WriteLine("База данных уже существует");
        }

        public void Init()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LectionLogDb>()
                .HasKey(c => new { c.LectionId, c.StudentId, c.HomeworkId });


            modelBuilder.Entity<StudentDb>().HasData(
                new StudentDb[] 
                {
                    new StudentDb { Id=1, Name="Tom Stone", Email = "tomS@mail.com", PhoneNumber = "+7 924 257-93-12"},
                    new StudentDb { Id=2, Name="Alice Film",Email = "aliceF@mail.com", PhoneNumber = "8 912 257-93-12"},
                    new StudentDb { Id=3, Name="Sam O'Nil", Email = "samsam@ya.ru", PhoneNumber = "+7 (924) 224-53-12"},
                    new StudentDb { Id=4, Name="Yura Ivanchev", Email = "iwi@mail.com", PhoneNumber = "+7 924 257-93-55"},
                    new StudentDb { Id=5, Name="Denis Ognev", Email = "dodood@gmail.com", PhoneNumber = "+7 924 257-93-66"}
                });
            
            modelBuilder.Entity<LectorDb>().HasData(
                new LectorDb[] 
                {
                    new LectorDb { Id=1, Name="John Hemming", Email = "john_hemming@mail.com"},
                    new LectorDb { Id=2, Name="Howard Lovecraft",Email = "howard_lovecraft@mail.com"},
                    new LectorDb { Id=3, Name="Jim Fars",Email = "jim_fars@mail.com"},
                });
            
            modelBuilder.Entity<LectionDb>().HasData(
                new LectionDb[] 
                {
                    new LectionDb { Id=1, Name="Introduction to the C# Language, Basic Coding in C#", LectorId = 3, Date = new DateTime(2021, 5, 1, 8, 30, 0)},
                    new LectionDb { Id=2, Name="Creating types in C#", LectorId = 2, Date = new DateTime(2021, 5, 1, 10, 30, 0)},
                    new LectionDb { Id=3, Name="Strings Overview. Formatting, Parsing, Comparing", LectorId = 1, Date = new DateTime(2021, 5, 2, 8, 30, 0)},
                });
            
            modelBuilder.Entity<HomeworkDb>().HasData(
                new HomeworkDb[] 
                {
                    new HomeworkDb { Id=1, Name="Task 1"},
                    new HomeworkDb { Id=2, Name="Task 2"},
                    new HomeworkDb { Id=3, Name="Task 3"},
                });
            
            modelBuilder.Entity<LectionLogDb>().HasData(
                new LectionLogDb[] 
                {
                    new LectionLogDb { LectionId = 1, StudentId = 1, Attendance = true, HomeworkId = 1, Score = 4 },
                    new LectionLogDb { LectionId = 1, StudentId = 2, Attendance = true, HomeworkId = 1, Score = 3 },
                    new LectionLogDb { LectionId = 1, StudentId = 3, Attendance = false, HomeworkId = 1, Score = 0 },
                    new LectionLogDb { LectionId = 1, StudentId = 4, Attendance = true, HomeworkId = 1, Score = 5 },
                    new LectionLogDb { LectionId = 1, StudentId = 5, Attendance = true, HomeworkId = 1, Score = 5 }, //
                    new LectionLogDb { LectionId = 2, StudentId = 1, Attendance = true, HomeworkId = 2, Score = 3 },
                    new LectionLogDb { LectionId = 2, StudentId = 2, Attendance = true, HomeworkId = 2, Score = 3 },
                    new LectionLogDb { LectionId = 2, StudentId = 3, Attendance = true, HomeworkId = 2, Score = 5 },
                    new LectionLogDb { LectionId = 2, StudentId = 4, Attendance = true, HomeworkId = 2, Score = 4 },
                    new LectionLogDb { LectionId = 2, StudentId = 5, Attendance = true, HomeworkId = 2, Score = 5 }  //
                });
            
            // var students = new List<Student>
            // { new Student{
            // students.ForEach(s => context.Students.Add(s));
            // context.SaveChanges();
        }
    }
}