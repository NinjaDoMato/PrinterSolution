using PrinterSolution.Common.Database;
using PrinterSolution.Common.Entities;
using PrinterSolution.Common.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterSolution.Common.Services
{
    public interface IMaterialService
    {
        public Material CreateMaterial(string name, string code, decimal pricePerKilo, MaterialType type);
        public Material UpdateMaterial(Material material);
        public Material GetMaterialById(int id);

        public bool DeleteMaterial(int id);
        public List<Material> GetMaterials();
    }

    public class MaterialService : IMaterialService
    {
        private readonly DatabaseContext _ctx;

        public MaterialService(DatabaseContext context)
        {
            _ctx = context;
        }

        public Material CreateMaterial(string name, string code, decimal pricePerKilo, MaterialType type)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Name cannot be empty.");

            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException("Code cannot be empty.");

            if (pricePerKilo <= 0)
                throw new ArgumentException("Price per kilo must be greater than 0.");

            if (_ctx.Material.Any(m => m.Name == name))
                throw new Exception("This name is already used.");

            if (_ctx.Material.Any(m => m.Code == code))
                throw new Exception("This code is already used.");

            var newMaterial = new Material
            {
                Name = name,
                Code = code,
                PricePerKilo = pricePerKilo,
                Type = type
            };

            _ctx.Material.Add(newMaterial);
            _ctx.SaveChanges();

            return newMaterial;
        }

        public bool DeleteMaterial(int id)
        {
            var material = _ctx.Material.FirstOrDefault(m => m.Id == id);

            if (material == null)
                throw new KeyNotFoundException("Material not found.");

            _ctx.Material.Remove(material);

            return true;
        }

        public Material GetMaterialById(int id)
        {
            return _ctx.Material.FirstOrDefault(m => m.Id == id);
        }

        public List<Material> GetMaterials()
        {
            return _ctx.Material.ToList();
        }

        public Material UpdateMaterial(Material material)
        {
            if (!_ctx.Material.Any(m => m.Id == material.Id))
                throw new KeyNotFoundException("Material not found.");

            if (_ctx.Material.Any(m => m.Name == material.Name && m.Id != material.Id))
                throw new Exception("This name is already used.");

            if (_ctx.Material.Any(m => m.Code == material.Code && m.Id != material.Id))
                throw new Exception("This code is already used.");

            _ctx.Material.Update(material);

            return material;
        }
    }
}
