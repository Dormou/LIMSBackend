using Middleware.Interfaces;
using System.ComponentModel;

namespace Middleware.Models
{
    public class BaseEntity: IBaseEntity
    {
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        [ReadOnly(true)]
        public DateTime CreatedDate { get; set; }

        [ReadOnly(true)]
        public DateTime ModifiedDate { get; set; }

        public BaseEntity()
        {
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
        }

        public void UpdateModifiedDate()
        {
            ModifiedDate = DateTime.UtcNow;
        }

    }
}