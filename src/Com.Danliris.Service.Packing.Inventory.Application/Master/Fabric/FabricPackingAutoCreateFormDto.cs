namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric
{
    public class FabricPackingAutoCreateFormDto
    {
        public int FabricSKUId { get; set; }
        public int ProductSKUId { get; set; }
        public int Quantity { get; set; }
        public string PackingType { get; set; }
        public double Length { get; set; }
    }
}