using System.Collections.Generic;
using Domain.Models;

namespace Domain.Services
{
    public interface ICrudService<TEntity> where TEntity : IBaseEntity
    {
        TEntity? Get(int id);
        IReadOnlyCollection<TEntity> GetAll();
        int New(TEntity entity);
        int Edit(TEntity entity);
        void Delete(int id);
    }
}