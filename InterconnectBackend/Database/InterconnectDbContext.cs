using Microsoft.EntityFrameworkCore;
using Models.Database;

namespace Database
{
    /// <summary>
    /// Database context for the Interconnect application.
    /// </summary>
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

        /// <summary>
        /// Collection of virtual machine entities.
        /// </summary>
        public DbSet<VirtualMachineEntityModel> VirtualMachineEntityModels { get; set; }
        
        /// <summary>
        /// Collection of bootable disks.
        /// </summary>
        public DbSet<BootableDiskModel> BootableDiskModels { get; set; }
        
        /// <summary>
        /// Collection of entity connections in virtual network.
        /// </summary>
        public DbSet<VirtualNetworkEntityConnectionModel> VirtualNetworkEntityConnectionModels { get; set; }
        
        /// <summary>
        /// Collection of virtual network nodes.
        /// </summary>
        public DbSet<VirtualNetworkNodeEntityModel> VirtualNetworkNodeEntityModels { get; set; }
        
        /// <summary>
        /// Collection of Internet entities.
        /// </summary>
        public DbSet<InternetEntityModel> InternetEntityModels { get; set; }
        
        /// <summary>
        /// Collection of virtual networks.
        /// </summary>
        public DbSet<VirtualNetworkModel> VirtualNetworkModels { get; set; }
        
        /// <summary>
        /// Collection of virtual machine network interfaces.
        /// </summary>
        public DbSet<VirtualMachineEntityNetworkInterfaceModel> VirtualMachineEntityNetworkInterfaceModels { get; set; }

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
