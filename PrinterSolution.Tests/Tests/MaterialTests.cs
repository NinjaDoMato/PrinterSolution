using PrinterSolution.Common.Utils.Enum;
using System.Collections.Generic;
using System.Linq;

namespace PrinterSolution.Tests
{
    [TestClass]
    public class MaterialUnitTests : TestBase
    {
        [TestMethod]
        public void ValidGetMaterial()
        {
            var material = materialService.GetMaterials().First();

            Assert.IsNotNull(material);

            var result = materialService.GetMaterialById(material.Id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Material));
            Assert.AreEqual(result.Id, material.Id);
        }

        [TestMethod]
        public void ValidGetMaterials()
        {
            var result = materialService.GetMaterials();

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
            var result = materialService.CreateMaterial(testData.Name, testData.Code, testData.PricePerKilo, testData.Type);

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
            var material = materialService.GetMaterials().First();

            material.Name = "Updated Name";
            material.Code = "Updated Code";
            material.PricePerKilo = 200.0m;

            // Act
            var result = materialService.UpdateMaterial(material);

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
            var material = materialService.GetMaterials().First();

            var result = materialService.DeleteMaterial(material.Id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsTrue(result);
        }
    }
}

