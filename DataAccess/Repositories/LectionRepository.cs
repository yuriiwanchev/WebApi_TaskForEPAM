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
    internal class LectionRepository : ICrudRepository<Lection>
    {
        private readonly StudentsDbContext context;
        private readonly IMapper mapper;

        public LectionRepository(StudentsDbContext DbContext, IMapper mapper)
        {
            context = DbContext;
            this.mapper = mapper;
        }

        public IEnumerable<Lection> GetAll()
        {
            var personsDb = context.Lections.ToList();
            return mapper.Map<IReadOnlyCollection<Lection>>(personsDb);
        }

        public Lection? Get(int id)
        {
            var personDb = context.Lections.FirstOrDefault(x => x.Id == id);
            return mapper.Map<Lection?>(personDb);
        }

        public int New(Lection person)
        {
            var personDb = mapper.Map<LectionDb>(person);
            var result = context.Lections.Add(personDb);
            context.SaveChanges();
            return result.Entity.Id;
        }

        public void Edit(Lection person)
        {
            if (context.Lections.Find(person.Id) is LectionDb personInDb)
            {
                personInDb.Name = person.Name;
                personInDb.LectorId = person.LectorId;
                personInDb.Date = person.Date;
                context.Entry(personInDb).State = EntityState.Modified;
                context.SaveChanges();
            }
            else
            {
                throw new InvalidUserInputException("There is no lection with that id");
            }
        }

        public void Delete(int id)
        {
            var personToDelete = context.Lections.Find(id);
            context.Entry(personToDelete).State = EntityState.Deleted;
            context.SaveChanges();
        }
    }
}