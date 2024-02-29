namespace testing_managment.Models.Entities
{
    public class CaseInBlock
    {
        public Guid Case { get; set; }

        public Guid Block { get; set; }

        public bool Applicable { get; set; }

        public int Verdict { get; set; }

        public string TestReportComment { get; set; }
    }
}
