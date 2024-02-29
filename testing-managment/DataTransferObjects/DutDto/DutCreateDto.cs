using System.ComponentModel.DataAnnotations;

namespace testing_managment.DataTransferObjects.DutDto
{
    public class DutCreateDto
    {
        public string Name { get; set; }

        public Guid Type { get; set; }

        public Guid Company { get; set; }

        public int Firmware_Version { get; set; }

        public int Hardmware_Version { get; set; }
    }
}
