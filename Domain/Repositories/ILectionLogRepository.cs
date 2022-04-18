using System.Collections.Generic;
using Domain.Models;

namespace Domain.Repositories
{
    public interface ILectionLogRepository
    {
        int[] New(LectionLog person); 
        LectionLog? Get(int lectionId, int studentId, int homeworkId); 
        IEnumerable<LectionLog> GetAll();
        void Edit(LectionLog person); 
        void Delete(int lectionId, int studentId, int homeworkId);
    }
}