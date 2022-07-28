using Microsoft.EntityFrameworkCore;

namespace PrinterSolution.Repository.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly string _connectionString = "Server=localhost;Database=PrinterSolution;Trusted_Connection=True;";

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
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
