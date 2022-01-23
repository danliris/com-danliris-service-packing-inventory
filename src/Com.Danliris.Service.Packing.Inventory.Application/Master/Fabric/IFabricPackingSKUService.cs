using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric
{
    public interface IFabricPackingSKUService
    {
        int CreateSKU(FabricSKUFormDto form);
        FabricSKUIdCodeDto AutoCreateSKU(FabricSKUAutoCreateFormDto form);
        //FabricSKUIdCodeDto AutoCreateSKU(NewFabricSKUAutoCreateFormDto form);
        FabricPackingIdCodeDto AutoCreatePacking(FabricPackingAutoCreateFormDto form);
        Task<int> UpdateSKU(int id, FabricSKUFormDto form);
        int DeleteSKU(int id);
        FabricSKUDto GetById(int id);
        Task<FabricSKUIndex> GetIndex(IndexQueryParam queryParam);
        FabricProductBarcodeDetail GetBarcodeDetail(string barcode);
    }
}
