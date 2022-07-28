using AutoMapper;
using PrinterSolution.Repository.Database;
using PrinterSolution.Tests.Context;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrinterSolution.Tests
{
    public abstract class TestBase
    {
        protected readonly DatabaseContext context;

        protected readonly IPriceService priceService;
        protected readonly IPriceRuleService priceRuleService;
        protected readonly IMaterialService materialService;
        protected readonly IPrinterService printerService;
        private IMapper mapper;

        public TestBase()
        {
            var config = new AutomapperConfig();
            mapper = config.Mapper;

            context = new InMemoryDatabaseContext().Context;

            IRepository<PriceRule> priceRuleRepository = new Repository<PriceRule>(context);
            IRepository<Printer> printerRepository = new Repository<Printer>(context);
            IRepository<Material> materialRepository = new Repository<Material>(context);
            IRepository<Configuration> configurationRepository = new Repository<Configuration>(context);

            priceService = new PriceService(priceRuleRepository, materialRepository, configurationRepository);
            materialService = new MaterialService(materialRepository);
            printerService = new PrinterService(printerRepository, mapper);
            priceRuleService = new PriceRuleService(priceRuleRepository, mapper);
        }

        protected IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
