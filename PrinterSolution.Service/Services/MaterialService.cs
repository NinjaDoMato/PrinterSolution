namespace PrinterSolution.Service.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialService service;

        public MaterialService(IMaterialService service)
        {
            this.service = service;
        }

        public Material CreateMaterial(string name, string code, decimal pricePerKilo, MaterialType type)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Name cannot be empty.");

            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException("Code cannot be empty.");

            if (pricePerKilo <= 0)
                throw new ArgumentException("Price per kilo must be greater than 0.");

            if (_ctx.Materials.Any(m => m.Name == name))
                throw new Exception("This name is already used.");

            if (_ctx.Materials.Any(m => m.Code == code))
                throw new Exception("This code is already used.");

            var newMaterial = new Material
            {
                Name = name,
                Code = code,
                PricePerKilo = pricePerKilo,
                Type = type
            };

            _ctx.Materials.Add(newMaterial);
            _ctx.SaveChanges();

            return newMaterial;
        }

        public bool DeleteMaterial(int id)
        {
            var material = _ctx.Materials.FirstOrDefault(m => m.Id == id);

            if (material == null)
                throw new KeyNotFoundException("Material not found.");

            _ctx.Materials.Remove(material);

            return true;
        }

        public Material GetMaterialById(int id)
        {
            return _ctx.Materials.FirstOrDefault(m => m.Id == id);
        }

        public List<Material> GetMaterials()
        {
            return _ctx.Materials.ToList();
        }

        public Material UpdateMaterial(Material material)
        {
            if (!_ctx.Materials.Any(m => m.Id == material.Id))
                throw new KeyNotFoundException("Material not found.");

            if (_ctx.Materials.Any(m => m.Name == material.Name && m.Id != material.Id))
                throw new Exception("This name is already used.");

            if (_ctx.Materials.Any(m => m.Code == material.Code && m.Id != material.Id))
                throw new Exception("This code is already used.");

            _ctx.Materials.Update(material);

            return material;
        }
    }
}
