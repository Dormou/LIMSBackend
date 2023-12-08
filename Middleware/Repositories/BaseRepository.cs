using Microsoft.EntityFrameworkCore;
using Middleware.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Repositories
{
    public class BaseRepository<T> : IBaseEntityRepository<T> where T : class, IBaseEntity
    {
        protected readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public virtual IQueryable<T> FindAll(bool withDeleted = false)
        {
            return _context.Set<T>().AsNoTracking().Where(e => withDeleted || !e.IsDeleted).AsQueryable();
        }

        public virtual Guid Create(T entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Creating record error: {ex}");
            }

            return entity.Id;
        }

        public virtual T? Get(Guid id)
        {
            return FindAll().FirstOrDefault(e => e.Id == id);
        }

        public virtual List<T> GetAll()
        {
            return FindAll(true).ToList();
        }

        public virtual List<T> GetExisting()
        {
            return FindAll().ToList();
        }

        public virtual bool Update(T entity)
        {
            try
            {
                entity.UpdateModifiedDate();
                _context.Update(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Updating record error: {ex}");
            }

            return true;
        }

        public virtual bool Delete(Guid id)
        {
            var entity = FindAll().FirstOrDefault(e => e.Id == id);

            if (entity == null)
                return false;

            try
            {
                entity.UpdateModifiedDate();
                entity.IsDeleted = true;
                _context.Update(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Deleting record error: {ex}");
            }

            return true;
        }
    }
}
