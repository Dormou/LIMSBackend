using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Middleware.Interfaces
{
    public interface IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public void UpdateModifiedDate();
    }
}
