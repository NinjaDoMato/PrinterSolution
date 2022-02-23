using Bogus;
using PrinterSolution.Common.Entities;
using PrinterSolution.Common.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Tests.Faker
{
    public class PrinterFaker : IEntityFaker<Printer>
    {
        private Faker<Printer> _faker;
        public PrinterFaker()
        {
            var index = 0;
            _faker = new Faker<Printer>()
            .RuleFor(x => x.Id, f => ++index)
            .RuleFor(x => x.Name, f => f.Lorem.Word())
            .RuleFor(x => x.Address, f => f.Internet.IpAddress().ToString())
            .RuleFor(x => x.DateCreated, f => f.Date.Past())
            .RuleFor(x => x.LastUpdate, f => f.Date.Recent())
            .RuleFor(x => x.LastMaintenance, f => f.Date.Recent())
            .RuleFor(x => x.Depth, f => f.Random.Number(150, 300))
            .RuleFor(x => x.Width, f => f.Random.Number(150, 300))
            .RuleFor(x => x.Height, f => f.Random.Number(150, 300))
            .RuleFor(x => x.HasHeatedBed, f => f.Random.Bool())
            .RuleFor(x => x.Type, f => f.Random.Enum<PrinterType>())
            .RuleFor(x => x.Status, f => f.Random.Enum<PrinterStatus>());
        }

        public IList<Printer> Generate(int count = 5)
        {
            return _faker.Generate(count);
        }

        public Printer Generate()
        {
            return _faker.Generate(1).First();
        }
    }
}
