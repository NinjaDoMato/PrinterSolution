namespace PrinterSolution.Service.Interfaces
{
    public interface IMaterialService
    {
        public Material CreateMaterial(string name, string code, decimal pricePerKilo, MaterialType type);
        public Material UpdateMaterial(Material material);
        public Material GetMaterialById(int id);

        public bool DeleteMaterial(long id);
        public List<Material> GetMaterials();
    }
}
