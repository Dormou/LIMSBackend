using System.Numerics;

namespace testing_managment.Models.Entities
{
    public class Dut
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid Type { get; set; }

        public Guid Company { get; set; }

        public int Firmware_Version { get; set; }

        public int Hardmware_Version { get; set; }
    }
}
