using Microsoft.EntityFrameworkCore;
using Moq;
using PrinterSolution.Common.Entities;
using PrinterSolution.Common.Services;
using PrinterSolution.Common.Utils.Enum;
using System;
using Xunit;

namespace PrinterSolution.Tests
{
    public class PrinterUnitTests
    {
        [Fact]
        public void ValidCreatePrinter()
        {
            // Arrange
            var mock = new Mock<IPrinterService>();

            var testData = new Printer
            {
                Name = It.IsAny<string>(),
                Address = It.IsAny<string>(),
                Width = It.IsAny<int>(),
                Height = It.IsAny<int>(),
                Depth = It.IsAny<int>(),
                HasHeatedBed = It.IsAny<bool>(),
                Status = It.IsAny<PrinterStatus>()
            };

            mock.Setup(m =>
                m.CreatePrinter(testData.Name, testData.Address, testData.Type, testData.Height, testData.Width, testData.Depth, testData.HasHeatedBed))
                .Returns(testData);

            // Act
            var result = mock.Object.CreatePrinter(testData.Name, testData.Address, testData.Type, testData.Height, testData.Width, testData.Depth, testData.HasHeatedBed);

            // Assert
            Assert.Equal(testData.Name, result.Name);
            Assert.Equal(testData.Address, result.Address);
            Assert.Equal(testData.HasHeatedBed, result.HasHeatedBed);
            Assert.Equal(testData.Height, result.Height);
            Assert.Equal(testData.Width, result.Width);
            Assert.Equal(testData.Depth, result.Depth);
        }

        [Fact]
        public void NotValidCreatePrinter()
        {
            // Arrange
            var mock = new Mock<IPrinterService>();

            var testData = new Printer
            {
                Name = It.IsAny<string>(),
                Address = It.IsAny<string>(),
                Width = It.IsAny<int>(),
                Height = It.IsAny<int>(),
                Depth = It.IsAny<int>(),
                HasHeatedBed = It.IsAny<bool>(),
                Status = It.IsAny<PrinterStatus>()
            };

            mock.Setup(m =>
                m.CreatePrinter(testData.Name, testData.Address, testData.Type, testData.Height, testData.Width, testData.Depth, testData.HasHeatedBed))
                .Throws(new Exception("Name cannot be empty"));

            // Act
            try
            {
                var result = mock.Object.CreatePrinter(testData.Name, testData.Address, testData.Type, testData.Height, testData.Width, testData.Depth, testData.HasHeatedBed);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.Equal("Name cannot be empty", ex.Message);
            }
        }
    }
}

