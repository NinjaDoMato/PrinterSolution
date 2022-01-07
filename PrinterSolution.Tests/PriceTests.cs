using Moq;
using PrinterSolution.Common.DTOs;
using PrinterSolution.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PrinterSolution.Tests
{
    public class PriceTests
    {
        [Fact]
        public void ValidEstimateDetailedCosts()
        {
            // Arrange
            var mock = new Mock<IPriceService>();

            var returnData = new DetailedPriceEstimation();

            mock.Setup(m =>
                m.EstimateDetailedCosts(Math.Abs(It.IsAny<decimal>()), "PLA", Math.Abs(It.IsAny<decimal>()), Math.Abs(It.IsAny<decimal>())))
                .Returns(returnData);

            // Act
            var result = mock.Object.EstimateDetailedCosts(Math.Abs(It.IsAny<decimal>()), "PLA", Math.Abs(It.IsAny<decimal>()), Math.Abs(It.IsAny<decimal>()));

            // Assert
            Assert.True(result.FinalPrice >= 0);
            Assert.True(result.TotalProductionCost >= 0);
            Assert.True(result.TotalEnergyCost >= 0);
            Assert.True(result.TotalPreparationCost >= 0);
            Assert.True(result.TotalAditionalCost >= 0);
        }
    }
}
