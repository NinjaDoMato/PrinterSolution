using Moq;
using PrinterSolution.PriceAPI.Services;
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
        public void ValidEstimateProductionCost()
        {
            // Arrange
            var mock = new Mock<IPriceService>();

            var material = Math.Abs(It.IsAny<decimal>());
            var hoursPrinting = Math.Abs(It.IsAny<decimal>());
            var postWork = Math.Abs(It.IsAny<decimal>());

            mock.Setup(m =>
                m.EstimateProductionCost(material, "PLA", hoursPrinting, postWork));

            // Act
            var result = mock.Object.EstimateProductionCost(material, "PLA", hoursPrinting, postWork);

            // Assert
            Assert.True(result > 0);
        }
    }
}
