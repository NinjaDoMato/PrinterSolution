using PrinterSolution.Repository.Interfaces;

namespace PrinterSolution.Service.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IRepository<Material> repository;

        public MaterialService(IRepository<Material> repository)
        {
            this.repository = repository;
        }

        public Material CreateMaterial(string name, string code, decimal pricePerKilo, MaterialType type)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Name cannot be empty.");

            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException("Code cannot be empty.");

            if (pricePerKilo <= 0)
                throw new ArgumentException("Price per kilo must be greater than 0.");

            if (repository.FirstOrDefault(m => m.Name == name) != null)
            {
                throw new Exception("This name is already used.");
            }

            if (repository.FirstOrDefault(m => m.Code == code) != null)
            {
                throw new Exception("This code is already used.");
            }

            var newMaterial = new Material
            {
                Name = name,
                Code = code,
                PricePerKilo = pricePerKilo,
                Type = type
            };

            newMaterial = repository.Insert(newMaterial);

            return newMaterial;
        }

        public bool DeleteMaterial(long id)
        {
            var material = repository.FirstOrDefault(m => m.Id == id);

            if (material == null)
                throw new KeyNotFoundException("Material not found.");

            repository.Delete(material);

            return true;
        }

        public Material GetMaterialById(long id)
        {
            var material = repository.FirstOrDefault(m => m.Id == id);

            if (material == null)
            {
                throw new KeyNotFoundException("Material not found.");
            }

            return material;
        }

        public List<Material> GetMaterials()
        {
            return repository.Where(m => m.Id > 0).ToList();
        }

        public Material UpdateMaterial(Material material)
        {
            if (repository.FirstOrDefault(m => m.Id == material.Id) == null)
                throw new KeyNotFoundException("Material not found.");

            if (repository.FirstOrDefault(m => m.Name == material.Name && m.Id != material.Id) != null)
                throw new Exception("This name is already used.");

            if (repository.FirstOrDefault(m => m.Code == material.Code && m.Id != material.Id) != null)
                throw new Exception("This code is already used.");

            repository.Update(material);

            return material;
        }
    }
}
