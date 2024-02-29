using Middleware.Controllers;
using Middleware.Interfaces;
using References.Models;
using References.Models.ViewModels;

namespace References.Controllers
{
    public class DeviceTypeController : BaseCRUDController<DeviceType, DeviceTypeCreate, DeviceTypeUpdate>
    {
        public DeviceTypeController(IBaseEntityRepository<DeviceType> repository) : base(repository)
        {
        }
    }
}
