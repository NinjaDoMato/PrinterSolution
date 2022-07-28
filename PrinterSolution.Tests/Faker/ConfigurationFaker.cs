using Bogus;
using PrinterSolution.Common.Utils.Enum;
using PrinterSolution.Tests.Faker;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConfigurationSolution.Tests.Faker
{
    public class ConfigurationFaker : IEntityFaker<Configuration>
    {
        private readonly Faker<Configuration> _faker;
        private readonly List<string> _configurationCodes = new List<string>
        {
            "kWh",
            "AvgkWh",
            "MWC"
        };

        private readonly List<string> _configurationNames = new List<string>
        {
            "Energy cost per Watt",
            "Average power consumption",
            "Manual Work Cost"
        };

        public ConfigurationFaker()
        {
            var index = 0;
            _faker = new Faker<Configuration>()
            .RuleFor(x => x.Id, f => ++index)
            .RuleFor(x => x.Name, f => f.Lorem.Word())
            .RuleFor(x => x.DateCreated, f => f.Date.Past())
            .RuleFor(x => x.LastUpdate, f => f.Date.Recent())
            .RuleFor(x => x.Type, f => f.Random.Enum<ConfigurationType>())
            .RuleFor(x => x.Value, f => Math.Round(f.Random.Decimal(1.0m, 10.0m), 2).ToString().Replace(",", "."))
            .RuleFor(x => x.Code, f => _configurationCodes[index - 1])
            .RuleFor(x => x.Name, f => _configurationNames[index - 1]);
        }

        public IList<Configuration> Generate(int count = 5)
        {
            return _faker.Generate(count);
        }

        public Configuration Generate()
        {
            return _faker.Generate(1).First();
        }
    }
}
