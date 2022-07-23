using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrinterSolution.Common.Entities;
using PrinterSolution.Common.Services;
using PrinterSolution.Common.Utils.Enum;
using PrinterSolution.Tests.Context;
using System;

namespace PrinterSolution.Tests
{
    [TestClass]
    public class PriceRuleUnitTests
    {
        private readonly IPriceRuleService _service;

        public PriceRuleUnitTests()
        {
            var context = new InMemoryDatabaseContext();

            _service = new PriceRuleService(context.Context);
        }


        [TestMethod]
        public void ValidCreatePriceRule()
        {
            // Arrange
            var testData = new PriceRule
            {
                Code = "TST_PRICE",
                Name = "Test price rule",
                Description = "Price rule used for tests",
                Target = It.IsAny<PriceRuleTarget>(),
                Operation = It.IsAny<PriceRuleOperation>(),
                Value = It.IsAny<decimal>(),
                Priority = It.IsAny<int>(),
                Status = true
            };

            // Act
            var result = _service.CreateRule(testData.Name, testData.Code, testData.Description, testData.Target, testData.Operation, testData.Value, testData.Priority);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PriceRule));
            Assert.IsTrue(testData.Name.Equals(result.Name));
            Assert.IsTrue(testData.Code.Equals(result.Code));
            Assert.IsTrue(testData.Description.Equals(result.Description));
            Assert.IsTrue(testData.Value.Equals(result.Value));
            Assert.IsTrue(testData.Operation.Equals(result.Operation));
            Assert.IsTrue(testData.Target.Equals(result.Target));
            Assert.IsTrue(testData.Priority.Equals(result.Priority));
            Assert.IsTrue(testData.Status.Equals(result.Status));
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void NotValidCreatePriceRule()
        {
            // Arrange
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

            _ = _service.CreateRule(testData.Name, testData.Code, testData.Description, testData.Target, testData.Operation, testData.Value, testData.Priority);
        }
    }
}

