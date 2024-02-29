using Middleware.Controllers;
using Middleware.Interfaces;
using References.Models;
using References.Models.ViewModels;

namespace References.Controllers
{
    public class EquipmentSimulatorController : BaseCRUDController<EquipmentSimulator, EquipmentSimulatorCreate, EquipmentSimulatorUpdate>
    {
        public EquipmentSimulatorController(IBaseEntityRepository<EquipmentSimulator> repository) : base(repository) 
        {
        }
    }
}
