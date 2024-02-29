namespace testing_managment.Models.Entities
{
    public class TestBlock
    {
        public Guid Id { get; set; }

        public Guid TestProgram { get; set; }

        public Guid TestGroup { get; set; }

        public Guid TestEngineer { get; set; }

        public DateOnly Deadline { get; set; }

        public string Recommendation { get; set; }
    }
}
