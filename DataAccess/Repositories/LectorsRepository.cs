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
    internal class LectorsRepository : ICrudRepository<Lector>
    {
        private readonly StudentsDbContext context;
        private readonly IMapper mapper;

        public LectorsRepository(StudentsDbContext personsDbContext, IMapper mapper)
        {
            context = personsDbContext;
            this.mapper = mapper;
        }

        public IEnumerable<Lector> GetAll()
        {
            var personsDb = context.Lectors.ToList();
            return mapper.Map<IReadOnlyCollection<Lector>>(personsDb);
        }

        public Lector? Get(int id)
        {
            var personDb = context.Lectors.FirstOrDefault(x => x.Id == id);
            return mapper.Map<Lector?>(personDb);
        }

        public int New(Lector person)
        {
            var personDb = mapper.Map<LectorDb>(person);
            var result = context.Lectors.Add(personDb);
            context.SaveChanges();
            return result.Entity.Id;
        }

        public void Edit(Lector person)
        {
            if (context.Lectors.Find(person.Id) is LectorDb personInDb)
            {
                personInDb.Name = person.Name;
                personInDb.Email = person.Email;
                context.Entry(personInDb).State = EntityState.Modified;
                context.SaveChanges();
            }
            else
            {
                throw new InvalidUserInputException("There is no lector with that id");
            }
        }

        public void Delete(int id)
        {
            var personToDelete = context.Lectors.Find(id);
            context.Entry(personToDelete).State = EntityState.Deleted;
            context.SaveChanges();
        }
    }
}