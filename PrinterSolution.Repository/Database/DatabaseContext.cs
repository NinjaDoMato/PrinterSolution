using Microsoft.EntityFrameworkCore;
using PrinterSolution.Repository.Entities;

namespace PrinterSolution.Repository.Database
{
    public class DatabaseContext : DbContext
    {
        private string _connectionString = "Server=localhost;Database=PrinterSolution;Trusted_Connection=True;";

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PriceRule>()
                .Property(b => b.Name)
                .HasMaxLength(25);

            modelBuilder.Entity<PriceRule>()
               .Property(b => b.Description)
               .HasMaxLength(140);

            modelBuilder.Entity<Material>()
               .Property(b => b.Name)
               .HasMaxLength(25);

            modelBuilder.Entity<Printer>()
              .Property(b => b.Name)
              .HasMaxLength(25);

            modelBuilder.Entity<OrderHistory>()
              .HasOne<Order>(b => b.Order)
              .WithMany(b => b.History)
              .HasForeignKey(b => b.OrderId);
        }

        public DbSet<PriceRule> PriceRules { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Printer> Printers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }
    }
}
