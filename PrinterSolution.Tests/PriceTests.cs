using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrinterSolution.Common.DTOs;
using PrinterSolution.Common.Services;
using PrinterSolution.Tests.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Tests
{
    [TestClass]
    public class PriceTests
    {
        private readonly IPriceService _service;
        public PriceTests()
        {
            var context = new InMemoryDatabaseContext();

            _service = new PriceService(context.Context);
        }

        [TestMethod]
        public void ValidEstimateDetailedCosts()
        {
            var result = _service.EstimateDetailedCosts(100, "PLA", 10, 0.5m);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DetailedPriceEstimation));
            Assert.IsTrue(result.FinalPrice >= 0);
            Assert.IsTrue(result.TotalProductionCost >= 0);
            Assert.IsTrue(result.TotalEnergyCost >= 0);
            Assert.IsTrue(result.TotalPreparationCost >= 0);
            Assert.IsTrue(result.TotalAditionalCost >= 0);
        }
    }
}
