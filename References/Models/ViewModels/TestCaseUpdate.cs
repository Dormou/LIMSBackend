using System.Text.Json.Serialization;

namespace References.Models.ViewModels
{
    public class TestCaseUpdate : TestCase
    {
        [JsonIgnore]
        public bool IsDeleted { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public DateTime ModifiedDate { get; set; }
    }
}
