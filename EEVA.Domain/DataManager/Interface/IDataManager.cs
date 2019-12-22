using System;
using System.Collections.Generic;
using System.Text;

namespace EEVA.Domain.Models.Repository
{
    public interface IDataManager<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        void Add(TEntity entity);
        void Update(TEntity dbEntity, TEntity entity);
        void Delete(TEntity entity);
        IEnumerable<TEntity> Search(string keyword);
    }
}
