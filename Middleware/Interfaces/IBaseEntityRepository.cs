using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Interfaces
{
    public interface IBaseEntityRepository<T> where T : IBaseEntity
    {
        T? Get(Guid id);
        List<T> GetExisting();
        List<T> GetAll();
        IQueryable<T> FindAll(bool withDeleted = false);
        Guid Create(T entity);
        bool Update(T entity);
        bool Delete(Guid id);
    }
}
