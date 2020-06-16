using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric
{
    public interface IFabricProductService
    {
        Task<BarcodeInfo> CreateProductFabric(FabricFormDto form);
        Task<BarcodeInfo> GetBarcodeByPackingCode(string code);
    }
}
