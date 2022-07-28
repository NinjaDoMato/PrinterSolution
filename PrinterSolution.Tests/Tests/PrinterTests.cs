using PrinterSolution.Common.DTOs.Requests;
using PrinterSolution.Common.Utils.Enum;
using PrinterSolution.Tests.Faker;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PrinterSolution.Tests
{
    [TestClass]
    public class PrinterUnitTests : TestBase
    {
        [TestMethod]
        public void ValidCreatePrinter()
        {
            // Arrange
            var testData = new CreatePrinterModel
            {
                Name = "Test Printer",
                Depth = 250,
                Height = 250,
                Width = 250,
                HeatBed = true,
                Address = "127.0.0.1",
                Type = PrinterType.FDM
            };

            // Act
            var result = printerService.CreatePrinter(testData);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Printer));
            Assert.IsTrue(testData.Name.Equals(result.Name));
            Assert.IsTrue(testData.Address.Equals(result.Address));
            Assert.IsTrue(testData.HeatBed.Equals(result.HasHeatedBed));
            Assert.IsTrue(testData.Height.Equals(result.Height));
            Assert.IsTrue(testData.Width.Equals(result.Width));
            Assert.IsTrue(testData.Depth.Equals(result.Depth));
        }

        [TestMethod]
        public void NotValidCreatePrinter()
        {
            // Arrange
            var testData = new CreatePrinterModel
            {
                Name = string.Empty,
                Address = string.Empty,
                Width = 200,
                Height = -110,
                Depth = 200,
                HeatBed = It.IsAny<bool>(),
                Type = It.IsAny<PrinterType>()
            };

            IList<ValidationResult> validationResult = ValidateModel(testData);

            Assert.IsNotNull(validationResult);
            Assert.IsTrue(validationResult.Count > 0);
            Assert.IsTrue(validationResult.Any(v => v.MemberNames.Contains("Name")));
            Assert.IsTrue(validationResult.Any(v => v.MemberNames.Contains("Address")));
            Assert.IsTrue(validationResult.Any(v => v.MemberNames.Contains("Height")));
        }
    }
}

