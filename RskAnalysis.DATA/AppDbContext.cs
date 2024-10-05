using Microsoft.EntityFrameworkCore;
using RskAnalysis.CORE.Models;

namespace RskAnalysis.DATA
{
    public class AppDbContext : DbContext
    {
        public DbSet<Sectors> Sectors { get; set; }
        public DbSet<Partners> Partners { get; set; }
        public DbSet<Contracts> Contracts { get; set; }
        public DbSet<Businesses> Businesses { get; set; }
        public DbSet<Cities> Cities { get; set; }
        

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=(localDB)\\MSSQLLocalDB;Database=RiskAnalysis;Trusted_Connection=True;MultipleActiveResultSets=true");
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=RiskAnalysis;Trusted_Connection=True;MultipleActiveResultSets=true");
                
            }
        }

        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primary key definitions
            modelBuilder.Entity<Cities>().HasKey(p => p.CityId);
            modelBuilder.Entity<Partners>().HasKey(p => p.PartnerId);
            modelBuilder.Entity<Contracts>().HasKey(c => c.ContractId);
            modelBuilder.Entity<Sectors>().HasKey(c => c.SectorId);
            modelBuilder.Entity<Businesses>().HasKey(c => c.BusinessId);
            
           

            // Business - Sector 1-1 relationship
            //modelBuilder.Entity<Businesses>()
            //    .HasOne(b => b.Sector)
            //    .WithOne(s => s.Business)
            //    .HasForeignKey<Businesses>(b => b.SectorId);

            // Contracts - Risk: Many-to-One relationship (N-1)   Not:1-1 olmayacak mı
            //modelBuilder.Entity<Contracts>()
            //    .HasOne(c => c.Risk)  // Her Contract bir tane Risk'e sahiptir
            //    .WithMany(r => r.ContractsList)  // Bir Risk birden fazla Contract ile ilişkilidir
            //    .HasForeignKey(c => c.RiskId);

            //Business - Contracts: 1 - N relationship
            //modelBuilder.Entity<Businesses>()
            //    .HasMany(b => b.BusinessId)
            //    .WithOne(c => c.ContractsList)
            //    .HasForeignKey(c => c.BusinessId);

            // Partners - Contracts: 1-N relationship
            //modelBuilder.Entity<Partners>()
            // .HasMany(p => p.ContractsList)
            // .WithOne(c => c.Partner)
            // .HasForeignKey(c => c.PartnerId)
            // .OnDelete(DeleteBehavior.NoAction);  // Cascade silme davranışını kaldırarak NoAction kullanıyoruz



            // Cities - Partners: 1-N relationship
            //modelBuilder.Entity<Partners>()
            //    .HasMany(c => c.City)
            //    .WithOne(p => p.Partner)
            //    .HasForeignKey(p => p.CityId);
        }
    }
}
