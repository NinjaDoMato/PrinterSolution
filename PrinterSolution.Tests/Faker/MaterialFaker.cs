using Bogus;
using PrinterSolution.Common.Entities;
using PrinterSolution.Common.Utils.Enum;
using PrinterSolution.Tests.Faker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialSolution.Tests.Faker
{
    public class MaterialFaker : IEntityFaker<Material>
    {
        private Faker<Material> _faker;
        public MaterialFaker()
        {
            var index = 0;
            _faker = new Faker<Material>()
            .RuleFor(x => x.Id, f => ++index)
            .RuleFor(x => x.Code, f => ((MaterialType)index-1).ToString())
            .RuleFor(x => x.Name, f => f.Lorem.Word())
            .RuleFor(x => x.DateCreated, f => f.Date.Past())
            .RuleFor(x => x.LastUpdate, f => f.Date.Recent())
            .RuleFor(x => x.PricePerKilo, f => f.Random.Decimal(100, 200))
            .RuleFor(x => x.Type, f => (MaterialType)index-1)
            .RuleFor(x => x.WeightLeft, f => f.Random.Decimal(100, 1000))
            .RuleFor(x => x.Weight, f => 1000);
        }

        public IList<Material> Generate(int count = 5)
        {
            return _faker.Generate(count);
        }

        public Material Generate()
        {
            return _faker.Generate(1).First();
        }
    }
}
