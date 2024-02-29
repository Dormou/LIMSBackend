namespace testing_managment.DataTransferObjects.DutDto
{
    public class DutViewDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid Type { get; set; }

        public Guid Company { get; set; }

        public int Firmware_Version { get; set; }

        public int Hardmware_Version { get; set; }
    }
}
