using Microsoft.EntityFrameworkCore;
using References.Models;

namespace References
{
    public class ReferencesContext : DbContext
    {
        public ReferencesContext(DbContextOptions<ReferencesContext> options) : base(options)
        {
        }
        public DbSet<TestGroup> TestGroup { get; set; }
        public DbSet<TestCase> TestCase { get; set; }
        public DbSet<Analyzer> Analyzer { get; set; }
        public DbSet<ClientSimulator> ClientSimulator { get; set; }
        public DbSet<EquipmentSimulator> EquipmentSimulator { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<DeviceType> DeviceType { get; set; }
    }
}
