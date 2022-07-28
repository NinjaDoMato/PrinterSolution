using Bogus;
using PrinterSolution.Common.Utils.Enum;
using PrinterSolution.Tests.Faker;
using System.Collections.Generic;
using System.Linq;

namespace MaterialSolution.Tests.Faker
{
    public class MaterialFaker : IEntityFaker<Material>
    {
        private readonly Faker<Material> _faker;
        public MaterialFaker()
        {
            _faker = new Faker<Material>()
            .RuleFor(x => x.Id, f => f.IndexGlobal)
            .RuleFor(x => x.Code, f => f.Lorem.Word())
            .RuleFor(x => x.Name, f => f.Lorem.Word())
            .RuleFor(x => x.DateCreated, f => f.Date.Past())
            .RuleFor(x => x.LastUpdate, f => f.Date.Recent())
            .RuleFor(x => x.PricePerKilo, f => f.Random.Decimal(100, 200))
            .RuleFor(x => x.Type, f => f.Random.Enum<MaterialType>())
            .RuleFor(x => x.WeightLeft, f => f.Random.Decimal(0, 1000))
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
