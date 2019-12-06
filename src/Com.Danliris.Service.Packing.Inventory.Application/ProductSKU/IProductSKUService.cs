using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ProductSKU
{
    public interface IProductSKUService
    {
        Task Create(CreateProductSKUViewModel viewModel);
        Task Update(int id, UpdateProductSKUViewModel viewModel);
        Task Delete(int id);
        Task<ProductSKUModel> ReadById(int id);
        ListResult<IndexViewModel> ReadByKeyword(string keyword, int page, int size);
    }
}
