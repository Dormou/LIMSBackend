using Middleware.Controllers;
using Middleware.Interfaces;
using References.Models;
using References.Models.ViewModels;

namespace References.Controllers
{
    public class TestGroupController : BaseCRUDController<TestGroup, TestGroupCreate, TestGroupUpdate>
    {
        public TestGroupController(IBaseEntityRepository<TestGroup> repository) : base(repository)
        {
        }
    }
}
