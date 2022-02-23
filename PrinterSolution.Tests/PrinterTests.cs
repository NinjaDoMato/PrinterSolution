using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrinterSolution.Common.Entities;
using PrinterSolution.Common.Services;
using PrinterSolution.Common.Utils.Enum;
using PrinterSolution.Tests.Context;
using PrinterSolution.Tests.Faker;
using System;

namespace PrinterSolution.Tests
{
    [TestClass]
    public class PrinterUnitTests
    {
        private readonly IPrinterService _service;
        private readonly PrinterFaker _faker = new PrinterFaker();

        public PrinterUnitTests()
        {
            var context = new InMemoryDatabaseContext();

            _service = new PrinterService(context.Context);
        }


        [TestMethod]
        public void ValidCreatePrinter()
        {
            // Arrange
            var testData = _faker.Generate();

            // Act
            var result = _service.CreatePrinter(testData.Name, testData.Address, testData.Type, testData.Height, testData.Width, testData.Depth, testData.HasHeatedBed);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Printer));
            Assert.IsTrue(testData.Name.Equals(result.Name));
            Assert.IsTrue(testData.Address.Equals(result.Address));
            Assert.IsTrue(testData.HasHeatedBed.Equals(result.HasHeatedBed));
            Assert.IsTrue(testData.Height.Equals(result.Height));
            Assert.IsTrue(testData.Width.Equals(result.Width));
            Assert.IsTrue(testData.Depth.Equals(result.Depth));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void NotValidCreatePrinter()
        {
            // Arrange
            var testData = new Printer
            {
                Name = string.Empty,
                Address = It.IsAny<string>(),
                Width = It.IsAny<int>(),
                Height = It.IsAny<int>(),
                Depth = It.IsAny<int>(),
                HasHeatedBed = It.IsAny<bool>(),
                Status = It.IsAny<PrinterStatus>()
            };

            _ = _service.CreatePrinter(testData.Name, testData.Address, testData.Type, testData.Height, testData.Width, testData.Depth, testData.HasHeatedBed);

        }
    }
}

