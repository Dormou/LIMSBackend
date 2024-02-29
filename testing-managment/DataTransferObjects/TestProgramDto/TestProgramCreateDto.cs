namespace testing_managment.DataTransferObjects.TestProgramDto
{
    public class TestProgramCreateDto
    {
        public int ProjectNumber { get; set; }

        public Guid Dut { get; set; }

        public Guid TestInitiator { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }
    }
}
