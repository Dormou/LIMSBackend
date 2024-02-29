namespace testing_managment.Models.Entities
{
    public class TestProgram
    {
        public Guid Id { get; set; }

        public int ProjectNumber { get; set; }

        public Guid Dut { get; set; }

        public Guid TestInitiator { get; set; }

        public DateOnly StartDate { get; set; }
        
        public DateOnly EndDate { get; set; }
    }
}
