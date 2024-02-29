using Middleware.Controllers;
using Middleware.Interfaces;
using References.Models;
using References.Models.ViewModels;

namespace References.Controllers
{
    public class TestCaseController : BaseCRUDController<TestCase, TestCaseCreate, TestCaseUpdate>
    {
        public TestCaseController(IBaseEntityRepository<TestCase> repository) : base(repository)
        {
        }
    }
}
