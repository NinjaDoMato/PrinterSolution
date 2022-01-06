using Microsoft.EntityFrameworkCore;
using Moq;
using PrinterSolution.Common.Entities;
using PrinterSolution.Common.Services;
using PrinterSolution.Common.Utils.Enum;
using System;
using Xunit;

namespace PrinterSolution.Tests
{
    public class MaterialUnitTests
    {
        [Fact]
        public void ValidCreateMaterial()
        {
            // Arrange
            var mock = new Mock<IMaterialService>();

            var testData = new Material
            {
                Code = It.IsAny<string>(),
                Name = It.IsAny<string>(),
                PricePerKilo = Math.Abs(It.IsAny<decimal>()),
                Type = It.IsAny<MaterialType>()
            };

            mock.Setup(m =>
                m.CreateMaterial(testData.Name, testData.Code, testData.PricePerKilo, testData.Type))
                .Returns(testData);

            // Act
            var result = mock.Object.CreateMaterial(testData.Name, testData.Code, testData.PricePerKilo, testData.Type);

            // Assert
            Assert.Equal(testData.Name, result.Name);
            Assert.Equal(testData.Code, result.Code);
            Assert.Equal(testData.PricePerKilo, result.PricePerKilo);
            Assert.Equal(testData.Type, result.Type);
        }
    }
}

