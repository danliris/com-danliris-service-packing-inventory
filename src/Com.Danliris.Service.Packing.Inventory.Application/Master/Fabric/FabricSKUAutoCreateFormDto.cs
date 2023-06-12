using System;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric
{
    public class FabricSKUAutoCreateFormDto
    {
        public string Construction { get; set; }
        public string Grade { get; set; }
        public string ProcessType { get; set; }
        public string UOM { get; set; }
        public string Warp { get; set; }
        public string Weft { get; set; }
        public string Width { get; set; }
        public string WovenType { get; set; }
        public string YarnType { get; set; }
        public string ProductionOrderNo { get; set; }
        public int materialId { get; set; }
        public string materialName { get; set; }
        public int materialConstructionId { get; set; }
        public string materialConstructionName { get; set; }
        public int yarnMaterialId { get; set; }
        public string yarnMaterialName { get; set; }
        public string uomUnit { get; set; }
        public string motif { get; set; }
        public string color { get; set; }
        public DateTime CreatedUtcOrderNo { get; set; }
    }
}