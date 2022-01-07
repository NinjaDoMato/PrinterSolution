using Microsoft.EntityFrameworkCore;
using PrinterSolution.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Common.Database
{
    public class DatabaseContext : DbContext
    {
        private string _connectionString = "Server=localhost;Database=PrinterSolution;Trusted_Connection=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
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
        }


        #region PriceEntities
        public DbSet<PriceRule> PriceRule { get; set; }
        public DbSet<Configuration> Configuration{ get; set; }
        public DbSet<Material> Material{ get; set; }
        public DbSet<Printer> Printer{ get; set; }

        #endregion
    }
}
