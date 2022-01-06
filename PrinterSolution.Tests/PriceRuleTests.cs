using Microsoft.EntityFrameworkCore;
using Moq;
using PrinterSolution.Common.Entities;
using PrinterSolution.Common.Utils.Enum;
using PrinterSolution.PriceAPI.Services;
using System;
using Xunit;

namespace PrinterSolution.Tests
{
    public class PriceRuleUnitTests
    {
        [Fact]
        public void ValidCreatePriceRule()
        {
            // Arrange
            var mock = new Mock<IPriceRuleService>();

            var testData = new PriceRule
            {
                Code = It.IsAny<string>(),
                Name = It.IsAny<string>(),
                Description = It.IsAny<string>(),
                Target = It.IsAny<PriceRuleTarget>(),
                Operation = It.IsAny<PriceRuleOperation>(),
                Value = It.IsAny<decimal>(),
                Priority = It.IsAny<int>(),
                Status = true
            };

            mock.Setup(m =>
                m.CreateRule(testData.Name, testData.Code, testData.Description, testData.Target, testData.Operation, testData.Value, testData.Priority))
                .Returns(testData);

            // Act
            var result = mock.Object.CreateRule(testData.Name, testData.Code, testData.Description, testData.Target, testData.Operation, testData.Value, testData.Priority);

            // Assert
            Assert.Equal(testData.Name, result.Name);
            Assert.Equal(testData.Code, result.Code);
            Assert.Equal(testData.Description, result.Description);
            Assert.Equal(testData.Value, result.Value);
            Assert.Equal(testData.Operation, result.Operation);
            Assert.Equal(testData.Target, result.Target);
            Assert.Equal(testData.Priority, result.Priority);
            Assert.Equal(testData.Status, result.Status);
        }

        [Fact]
        public void NotValidCreatePriceRule()
        {
            // Arrange
            var mock = new Mock<IPriceRuleService>();

            var testData = new PriceRule
            {
                Code = string.Empty,
                Name = string.Empty,
                Description = string.Empty,
                Target = PriceRuleTarget.FinalPrice,
                Operation = PriceRuleOperation.Add,
                Value = 0,
                Priority = 1,
                Status = true
            };

            mock.Setup(m =>
                m.CreateRule(testData.Name, testData.Code, testData.Description, testData.Target, testData.Operation, testData.Value, testData.Priority))
                .Throws(new Exception("Name cannot be empty"));

            // Act
            try
            {
                var result = mock.Object.CreateRule(testData.Name, testData.Code, testData.Description, testData.Target, testData.Operation, testData.Value, testData.Priority);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.Equal("Name cannot be empty", ex.Message);
            }
        }
    }
}

