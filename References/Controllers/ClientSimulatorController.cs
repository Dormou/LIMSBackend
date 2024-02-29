using Middleware.Controllers;
using Middleware.Interfaces;
using References.Models;
using References.Models.ViewModels;

namespace References.Controllers
{
    public class ClientSimulatorController : BaseCRUDController<ClientSimulator, ClientSimulatorCreate, ClientSimulatorUpdate>
    {
        public ClientSimulatorController(IBaseEntityRepository<ClientSimulator> repository) : base(repository)
        {
        }
    }
}
