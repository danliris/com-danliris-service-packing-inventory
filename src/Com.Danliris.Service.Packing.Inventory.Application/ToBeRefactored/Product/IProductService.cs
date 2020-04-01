using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.Product
{
    public interface IProductService
    {
        Task<ProductPackingBarcodeInfo> CreateProductPackAndSKU(CreateProductPackAndSKUViewModel viewModel);
    }
}
