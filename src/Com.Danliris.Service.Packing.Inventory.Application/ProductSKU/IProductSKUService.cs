using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ProductSKU
{
    public interface IProductSKUService
    {
        Task Create(CreateProductSKUViewModel viewModel);
    }
}
