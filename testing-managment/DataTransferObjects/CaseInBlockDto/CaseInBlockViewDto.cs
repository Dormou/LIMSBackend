namespace testing_managment.DataTransferObjects.CaseInBlockDto
{
    public class CaseInBlockViewDto
    {
        public Guid Case { get; set; }

        public Guid Block { get; set; }

        public bool Applicable { get; set; }

        public int Verdict { get; set; }

        public string TestReportComment { get; set; }
    }
}
