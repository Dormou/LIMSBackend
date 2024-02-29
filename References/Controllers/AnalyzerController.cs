using Middleware.Controllers;
using Middleware.Interfaces;
using References.Models;
using References.Models.ViewModels;

namespace References.Controllers
{
    public class AnalyzerController : BaseCRUDController<Analyzer, AnalyzerCreate, AnalyzerUpdate>
    {
        public AnalyzerController(IBaseEntityRepository<Analyzer> repository) : base(repository)
        {
        }
    }
}
