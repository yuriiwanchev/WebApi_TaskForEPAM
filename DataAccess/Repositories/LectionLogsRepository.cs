using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess.Models;
using Domain;
using Domain.Helpers;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    internal class LectionLogsRepository : ILectionLogRepository
    {
        private readonly StudentsDbContext context;
        private readonly IMapper mapper;

        public LectionLogsRepository(StudentsDbContext DbContext, IMapper mapper)
        {
            context = DbContext;
            this.mapper = mapper;
        }

        public IEnumerable<LectionLog> GetAll()
        {
            var personsDb = context.LectionLogs.ToList();
            return mapper.Map<IReadOnlyCollection<LectionLog>>(personsDb);
        }

        public LectionLog? Get(int lectionId, int studentId, int homeworkId)
        {
            var personDb = context.LectionLogs.FirstOrDefault(x => 
                x.LectionId == lectionId &&
                x.StudentId == studentId &&
                x.HomeworkId == homeworkId);
            return mapper.Map<LectionLog?>(personDb);
        }

        public int[] New(LectionLog person)
        {
            var personDb = mapper.Map<LectionLogDb>(person);
            var result = context.LectionLogs.Add(personDb);
            context.SaveChanges();
            return new[] { result.Entity.LectionId, result.Entity.StudentId, result.Entity.HomeworkId };
        }

        public void Edit(LectionLog log)
        {
            //var keyValues = new[] {log.LectionId, log.StudentId, log.HomeworkId};
            if (context.LectionLogs.Find(log.LectionId, log.StudentId, log.HomeworkId) is LectionLogDb logInDb)
            {
                logInDb.Attendance = log.Attendance;
                logInDb.Score = log.Score;
                context.Entry(logInDb).State = EntityState.Modified;
                context.SaveChanges();
            }
            else
            {
                throw new InvalidUserInputException("There is no lection log with that ids");
            }
        }

        public void Delete(int lectionId, int studentId, int homeworkId)
        {
            //var keyValues = new[] {lectionId, studentId, homeworkId};
            // var personToDelete = context.LectionLogs.Find(keyValues);
            var personToDelete = context.LectionLogs.Find(lectionId, studentId, homeworkId);
            context.Entry(personToDelete).State = EntityState.Deleted;
            context.SaveChanges();
        }
    }
}