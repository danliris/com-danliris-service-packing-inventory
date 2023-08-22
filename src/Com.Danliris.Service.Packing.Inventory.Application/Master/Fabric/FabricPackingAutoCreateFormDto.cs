namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric
{
    public class FabricPackingAutoCreateFormDto
    {
        public int FabricSKUId { get; set; }
        public int ProductSKUId { get; set; }
        public int Quantity { get; set; }
        public string PackingType { get; set; }
        public double Length { get; set; }
        public string Description { get; set; }
        public int MaterialConstructionId { get; set; }
        public string MaterialConstructionName { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public int YarnMaterialId { get; set; }
        public string YarnMaterialName { get; set; }
        public string FinishWidth { get; set; }
    }
}