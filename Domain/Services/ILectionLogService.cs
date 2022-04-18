using System.Collections.Generic;
using Domain.Models;

namespace Domain.Services
{
    public interface ILectionLogService
    {
        LectionLog? Get(int lectionId, int studentId, int homeworkId);
        IReadOnlyCollection<LectionLog> GetAll();
        int[] New(LectionLog student);
        int[] Edit(LectionLog student);
        void Delete(int lectionId, int studentId, int homeworkId);
    }
}