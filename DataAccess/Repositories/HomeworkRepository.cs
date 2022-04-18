using System;
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
    internal class HomeworkRepository : ICrudRepository<Homework>
    {
        private readonly StudentsDbContext context;
        private readonly IMapper mapper;

        public HomeworkRepository(StudentsDbContext DbContext, IMapper mapper)
        {
            context = DbContext;
            this.mapper = mapper;
        }

        public IEnumerable<Homework> GetAll()
        {
            var personsDb = context.Homeworks.ToList();
            return mapper.Map<IReadOnlyCollection<Homework>>(personsDb);
        }

        public Homework? Get(int id)
        {
            var personDb = context.Homeworks.FirstOrDefault(x => x.Id == id);
            return mapper.Map<Homework?>(personDb);
        }

        public int New(Homework person)
        {
            var personDb = mapper.Map<HomeworkDb>(person);
            var result = context.Homeworks.Add(personDb);
            context.SaveChanges();
            return result.Entity.Id;
        }

        public void Edit(Homework person)
        {
            if (context.Homeworks.Find(person.Id) is HomeworkDb personInDb)
            {
                personInDb.Name = person.Name;
                context.Entry(personInDb).State = EntityState.Modified;
                context.SaveChanges();
            }
            else
            {
                throw new InvalidUserInputException("There is no homework with that id");
            }
        }

        public void Delete(int id)
        {
            var personToDelete = context.Homeworks.Find(id);
            context.Entry(personToDelete).State = EntityState.Deleted;
            context.SaveChanges();
        }
    }
}