using Middleware.Controllers;
using Middleware.Interfaces;
using References.Models;
using References.Models.ViewModels;

namespace References.Controllers
{
    public class CompanyController : BaseCRUDController<Company, CompanyCreate, CompanyUpdate>
    {
        public CompanyController(IBaseEntityRepository<Company> repository) : base(repository)
        {
        }
    }
}
