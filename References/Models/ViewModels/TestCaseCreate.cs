using System.Text.Json.Serialization;

namespace References.Models.ViewModels
{
    public class TestCaseCreate : TestCase
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public DateTime ModifiedDate { get; set; }
    }
}
