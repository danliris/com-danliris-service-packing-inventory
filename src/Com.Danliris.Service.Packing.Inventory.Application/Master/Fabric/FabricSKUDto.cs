using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Master;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric
{
    public class FabricSKUDto
    {
        public string Code { get; internal set; }
        public IPWovenTypeModel WovenType { get; internal set; }
        public MaterialConstructionModel Construction { get; internal set; }
        public WarpTypeModel Warp { get; internal set; }
        public int Id { get; internal set; }
        public int ProductSKUId { get; internal set; }
        public WeftTypeModel Weft { get; internal set; }
        public IPProcessTypeModel ProcessType { get; internal set; }
        public IPYarnTypeModel YarnType { get; internal set; }
        public GradeModel Grade { get; internal set; }
        public UnitOfMeasurementModel UOM { get; internal set; }
        public IPWidthTypeModel Width { get; internal set; }
    }
}