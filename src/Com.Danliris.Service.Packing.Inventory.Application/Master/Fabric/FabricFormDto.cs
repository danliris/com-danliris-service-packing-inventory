namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric
{
    public class FabricFormDto
    {
        public string Composition { get; set; }
        public string Construction { get; set; }
        public string Width { get; set; }
        public string Design { get; set; }
        public string Grade { get; set; }
        public string UOMUnit { get; set; }
        public string PackingType { get; set; }
        public double? PackingSize { get; set; }
    }
}