using PrinterSolution.Common.DTOs.Requests;
using PrinterSolution.Common.Utils.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PrinterSolution.Tests
{
    [TestClass]
    public class PriceRuleUnitTests : TestBase
    {
        [TestMethod]
        public void ValidCreatePriceRule()
        {
            // Arrange
            var testData = new CreatePriceRuleModel
            {
                Code = "TST_PRICE",
                Name = "Test price rule",
                Description = "Price rule used for tests",
                Target = It.IsAny<PriceRuleTarget>(),
                Type = It.IsAny<PriceRuleOperation>(),
                Value = It.IsAny<decimal>(),
                Priority = It.IsAny<int>()
            };

            // Act
            var result = priceRuleService.CreateRule(testData);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PriceRule));
            Assert.IsTrue(testData.Name.Equals(result.Name));
            Assert.IsTrue(testData.Code.Equals(result.Code));
            Assert.IsTrue(testData.Description.Equals(result.Description));
            Assert.IsTrue(testData.Value.Equals(result.Value));
            Assert.IsTrue(testData.Type.Equals(result.Operation));
            Assert.IsTrue(testData.Target.Equals(result.Target));
            Assert.IsTrue(testData.Priority.Equals(result.Priority));
            Assert.IsTrue(result.Status);
        }

        [TestMethod]
        public void NotValidCreatePriceRule()
        {
            // Arrange
            var testData = new CreatePriceRuleModel
            {
                Code = string.Empty,
                Name = string.Empty,
                Description = string.Empty,
                Target = PriceRuleTarget.FinalPrice,
                Type = PriceRuleOperation.Add,
                Value = 0,
                Priority = 1
            };

            IList<ValidationResult> validationResult= ValidateModel(testData);

            Assert.IsNotNull(validationResult);
            Assert.IsTrue(validationResult.Count > 0);
            Assert.IsTrue(validationResult.Any(v => v.MemberNames.Contains("Name")));
            Assert.IsTrue(validationResult.Any(v => v.MemberNames.Contains("Code")));
            Assert.IsTrue(validationResult.Any(v => v.MemberNames.Contains("Description")));
            Assert.IsTrue(validationResult.Any(v => v.MemberNames.Contains("Value")));
        }       
    }
}

