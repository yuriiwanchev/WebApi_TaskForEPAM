using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess.Models;
using Domain;
using Domain.Helpers;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    internal class StudentsRepository : ICrudRepository<Student>
    {
        private readonly StudentsDbContext context;
        private readonly IMapper mapper;

        public StudentsRepository(StudentsDbContext DbContext, IMapper mapper)
        {
            context = DbContext;
            this.mapper = mapper;
        }

        public IEnumerable<Student> GetAll()
        {
            var personsDb = context.Students.ToList();
            return mapper.Map<IReadOnlyCollection<Student>>(personsDb);
        }

        public Student? Get(int id)
        {
            var personDb = context.Students.FirstOrDefault(x => x.Id == id);
            return mapper.Map<Student?>(personDb);
        }

        public int New(Student person)
        {
            var personDb = mapper.Map<StudentDb>(person);
            var result = context.Students.Add(personDb);
            context.SaveChanges();
            return result.Entity.Id;
        }

        public void Edit(Student person)
        {
            if (context.Students.Find(person.Id) is StudentDb personInDb)
            {
                personInDb.Name = person.Name;
                personInDb.Email = person.Email;
                personInDb.PhoneNumber = person.PhoneNumber;
                context.Entry(personInDb).State = EntityState.Modified;
                context.SaveChanges();
            }
            else
            {
                throw new InvalidUserInputException("There is no student with that id");
            }
        }

        public void Delete(int id)
        {
            var personToDelete = context.Students.Find(id);
            context.Entry(personToDelete).State = EntityState.Deleted;
            context.SaveChanges();
        }
    }
}