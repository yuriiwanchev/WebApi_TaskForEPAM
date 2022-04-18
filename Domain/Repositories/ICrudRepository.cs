using System.Collections.Generic;
using Domain.Models;

namespace Domain.Repositories
{
    public interface ICrudRepository<TEntity> where TEntity : IBaseEntity
    {
        int New(TEntity person); // Create
        TEntity? Get(int id); // Read
        IEnumerable<TEntity> GetAll();
        void Edit(TEntity person); // Update
        void Delete(int id); // Delete
    }
}