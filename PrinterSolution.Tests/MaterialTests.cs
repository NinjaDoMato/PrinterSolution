using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrinterSolution.Common.Entities;
using PrinterSolution.Common.Services;
using PrinterSolution.Common.Utils.Enum;
using PrinterSolution.Tests.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrinterSolution.Tests
{
    [TestClass]
    public class MaterialUnitTests
    {
        private readonly IMaterialService _service;
        public MaterialUnitTests()
        {
            var context = new InMemoryDatabaseContext();

            _service = new MaterialService(context.Context);
        }

        [TestMethod]
        public void ValidGetMaterial()
        {
            var result = _service.GetMaterialById(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Material));
            Assert.AreEqual(result.Id, 1);
        }

        [TestMethod]
        public void ValidGetMaterials()
        {
            var result = _service.GetMaterials();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Material>));
        }

        [TestMethod]
        public void ValidCreateMaterial()
        {
            // Arrange
            var testData = new Material
            {
                Code = "TST_MAT",
                Name = "Test material",
                PricePerKilo = 100.0m,
                Type = It.IsAny<MaterialType>()
            };

            // Act
            var result = _service.CreateMaterial(testData.Name, testData.Code, testData.PricePerKilo, testData.Type);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Material));
            Assert.IsTrue(testData.Name.Equals(result.Name));
            Assert.IsTrue(testData.Code.Equals(result.Code));
            Assert.IsTrue(testData.PricePerKilo.Equals(result.PricePerKilo));
            Assert.IsTrue(testData.Weight.Equals(result.Weight));
            Assert.IsTrue(testData.WeightLeft.Equals(result.WeightLeft));
            Assert.IsTrue(testData.Type.Equals(result.Type));
        }

        [TestMethod]
        public void ValidUpdateMaterial()
        {
            // Arrange
            var material = _service.GetMaterialById(1);

            material.Name = "Updated Name";
            material.Code = "Updated Code";
            material.PricePerKilo = 200.0m;

            // Act
            var result = _service.UpdateMaterial(material);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Material));
            Assert.IsTrue(material.Name.Equals(result.Name));
            Assert.IsTrue(material.Code.Equals(result.Code));
            Assert.IsTrue(material.PricePerKilo.Equals(result.PricePerKilo));
            Assert.IsTrue(material.Weight.Equals(result.Weight));
            Assert.IsTrue(material.WeightLeft.Equals(result.WeightLeft));
            Assert.IsTrue(material.Type.Equals(result.Type));
        }

        [TestMethod]
        public void ValidDeleteMaterial()
        {
            var result   = _service.DeleteMaterial(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsTrue(result);
        }
    }
}

