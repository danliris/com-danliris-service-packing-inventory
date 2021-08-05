using Com.Danliris.Service.Packing.Inventory.Application.DTOs;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric
{
    public interface IFabricProductService
    {
        Task<string> GenerateProductSKUCodeByCompositeString(FabricProductSKUCompositeStringFormDto form);
        Task<string> GenerateProductSKUCodeByCompositeId(FabricProductSKUCompositeIdFormDto form);
        Task<string> GenerateProductPackingCodeByCompositeString(FabricProductPackingCompositeStringFormDto form);
        Task<string> GenerateProductPackingCodeByCompositeId(FabricProductPackingCompositeIdFormDto form);
        Task<IdAndCodeDto> UpsertProductSKU(FabricProductCompositeStringDto form);
    }
}
