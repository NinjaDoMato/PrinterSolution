﻿using ConfigurationSolution.Tests.Faker;
using MaterialSolution.Tests.Faker;
using Microsoft.EntityFrameworkCore;
using PrinterSolution.Repository.Database;
using PrinterSolution.Tests.Faker;
using System;

namespace PrinterSolution.Tests.Context
{
    public class InMemoryDatabaseContext : IDisposable
    {
        public DbContextOptions<DatabaseContext> ContextOptions { get; }
        public DatabaseContext Context { get; }

        public InMemoryDatabaseContext()
        {
            ContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                     .UseInMemoryDatabase("in_memory")
                     .EnableSensitiveDataLogging()
                     .EnableDetailedErrors()
                     .Options;

            Context = new DatabaseContext(ContextOptions);
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

            var printerFaker = new PrinterFaker();
            var materialFaker = new MaterialFaker();
            var configurationFaker = new ConfigurationFaker();

            Context.Printers.AddRange(printerFaker.Generate(5));
            Context.Materials.AddRange(materialFaker.Generate(7));
            Context.Configurations.AddRange(configurationFaker.Generate(3));

            Context.SaveChanges();
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
        }

    }

}
