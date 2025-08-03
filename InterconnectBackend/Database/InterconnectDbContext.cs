using Microsoft.EntityFrameworkCore;
using Models.Database;

namespace Database
{
    public class InterconnectDbContext : DbContext
    {
        private readonly string _connectionString;

        public InterconnectDbContext()
        {
            _connectionString = @"Host=localhost:8899;Username=interconnect;Password=inter;Database=interconnect";
        }

        public InterconnectDbContext(DbContextOptions<InterconnectDbContext> options) : base(options)
        {
            _connectionString = "";
        }

        public DbSet<VirtualMachineEntityModel> VirtualMachineEntityModels { get; set; }
        public DbSet<BootableDiskModel> BootableDiskModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            optionsBuilder.UseNpgsql(_connectionString);
        }
    }

}
