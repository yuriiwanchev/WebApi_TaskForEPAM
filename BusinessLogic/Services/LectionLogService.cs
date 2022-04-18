using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;

namespace BusinessLogic.Services
{
    internal class LectionLogService : ILectionLogService
    {
        private readonly ILectionLogRepository personsRepository;

        public LectionLogService(ILectionLogRepository personsRepository)
        {
            this.personsRepository = personsRepository;
        }

        public void Delete(int lectionId, int studentId, int homeworkId)
        {
            personsRepository.Delete(lectionId, studentId, homeworkId);
        }

        public int[] Edit(LectionLog person)
        {
            personsRepository.Edit(person);
            return new []{person.LectionId, person.StudentId, person.HomeworkId};
        }

        public LectionLog? Get(int lectionId, int studentId, int homeworkId)
        {
            return personsRepository.Get(lectionId, studentId, homeworkId);
        }

        public IReadOnlyCollection<LectionLog> GetAll()
        {
            return personsRepository.GetAll().ToArray();
        }

        public int[] New(LectionLog person)
        {
            return personsRepository.New(person);
        }
    }
}