using PrinterSolution.Common.DTOs;
using PrinterSolution.Repository.Entities;
using PrinterSolution.Tests.Context;
using System.Linq;

namespace PrinterSolution.Tests
{
    [TestClass]
    public class PriceTests : TestBase
    {
        [TestMethod]
        public void ValidEstimateDetailedCosts()
        {
            var material = materialService.GetMaterials().First();

            var result = priceService.EstimateDetailedCosts(100, material.Code, 10, 0.5m);

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
