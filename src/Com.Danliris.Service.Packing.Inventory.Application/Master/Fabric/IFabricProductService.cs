using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric
{
    public interface IFabricProductService
    {
        // should return product code
        Task<string> CreateProductFabric(FabricFormDto form);
        Task<string> GetBarcodeByPackingCode(string code);
    }
}
