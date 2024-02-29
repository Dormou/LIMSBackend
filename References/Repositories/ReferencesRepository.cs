using Middleware.Interfaces;
using Middleware.Repositories;

namespace References.Repository
{
    public class ReferencesRepository<T> : BaseRepository<T> where T: class, IBaseEntity
    {
        public ReferencesRepository(ReferencesContext context) : base(context) { }
    }
}
