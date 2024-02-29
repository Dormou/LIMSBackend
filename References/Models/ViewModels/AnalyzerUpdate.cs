using System.Text.Json.Serialization;

namespace References.Models.ViewModels
{
    public class AnalyzerUpdate : Analyzer
    {
        [JsonIgnore]
        public bool IsDeleted { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public DateTime ModifiedDate { get; set; }
    }
}
