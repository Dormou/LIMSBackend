using Middleware.Models;

namespace References.Models
{
    public class TestCase : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Mandatory { get; set; }
        public Guid TestGroupId { get; set; }
        public TestGroup TestGroup { get; set; }
    }
}
