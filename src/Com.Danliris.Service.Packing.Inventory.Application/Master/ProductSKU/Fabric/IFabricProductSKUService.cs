using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.ProductSKU.Fabric
{
    public interface IFabricProductSKUService
    {
        Task<FabricBarcodeInfo> CreateProductFabric(FabricFormDto form);
        Task<FabricBarcodeInfo> GetBarcodeByCode(string code);
    }
}
