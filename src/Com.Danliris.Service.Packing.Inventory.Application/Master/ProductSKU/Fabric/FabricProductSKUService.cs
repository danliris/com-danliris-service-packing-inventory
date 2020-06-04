using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using System;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.ProductSKU.Fabric
{
    public class FabricProductSKUService : IFabricProductSKUService
    {
        public Task<FabricBarcodeInfo> CreateProductFabric(FabricFormDto form)
        {
            var code = CodeGenerator.Generate(8);
            throw new NotImplementedException();
        }

        public Task<FabricBarcodeInfo> GetBarcodeByCode(string code)
        {
            throw new NotImplementedException();
        }
    }
}
