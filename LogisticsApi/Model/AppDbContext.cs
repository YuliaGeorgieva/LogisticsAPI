using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApi.Model
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-QP657VL5;Database=LogisticsDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //   builder.ApplyConfiguration(new ServiceTypeSeedingConfiguration());

            //builder.Entity<Shipment>()
            //  .HasMany(e => e.ShipmentDetails)
            //  .WithOne(e => e.Shipment)
            //  .HasForeignKey(e => e.ShipmentId)
            //  .IsRequired();

            //builder.Entity<Company>()
            //.HasMany(e => e.CompanyBranches)
            //.WithOne(e => e.Company)
            //.HasForeignKey(e => e.CompanyId)
            //.IsRequired();

            //ABOVE is SAME as BELOW

            //     builder.Entity<ShipmentDetail>()
            //.HasOne(e => e.Shipment)
            //.WithMany(e => e.ShipmentDetails)
            //.HasForeignKey(e => e.ShipmentId)
            //.IsRequired();

            base.OnModelCreating(builder);
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyBranch> CompanyBranches { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentDetail> ShipmentDetails { get; set; }
    }
}