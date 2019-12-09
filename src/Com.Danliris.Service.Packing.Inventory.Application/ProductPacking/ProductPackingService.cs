using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductPacking;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductSKU;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ProductPacking
{
    public class ProductPackingService : IProductPackingService
    {
        private readonly IProductPackingRepository _productPackingRepository;
        private readonly IProductSKURepository _productSKURepository;

        public ProductPackingService(IServiceProvider serviceProvider)
        {
            _productPackingRepository = serviceProvider.GetService<IProductPackingRepository>();
            _productSKURepository = serviceProvider.GetService<IProductSKURepository>();
        }
        public async Task Create(ProductPackingFormViewModel viewModel)
        {
            var model = new ProductPackingModel(null, viewModel.PackingType, viewModel.Quantity.GetValueOrDefault(), viewModel.SKUId.GetValueOrDefault());
            await _productPackingRepository.InsertAsync(model);
        }

        public Task Delete(int id)
        {
            return _productPackingRepository.DeleteAsync(id);
        }

        public Task<ProductPackingModel> ReadById(int id)
        {
            return _productPackingRepository.ReadByIdAsync(id);
        }

        public ListResult<IndexViewModel> ReadByKeyword(string keyword, int page, int size)
        {
            var productPackingQuery = _productPackingRepository.ReadAll();
            var productSKUQuery = _productSKURepository.ReadAll();

            var joinQuery = from productPacking in productPackingQuery
                            join productSKU in productSKUQuery on productPacking.SKUId equals productSKU.Id into productPackingSKU
                            from product in productPackingSKU.DefaultIfEmpty()
                            select new IndexViewModel()
                            {
                                Code = productPacking.Code,
                                Id = productPacking.Id,
                                LastModifiedUtc = productPacking.LastModifiedUtc,
                                Name = product.Name,
                                PackingType = productPacking.PackType,
                                Quantity = productPacking.Quantity
                            };

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                joinQuery = joinQuery.Where(entity => entity.Name.Contains(keyword));
            }

            var data = joinQuery.OrderByDescending(entity => entity.LastModifiedUtc).Skip((page - 1) * size).Take(size).ToList();
            var totalRow = joinQuery.Select(entity => entity.Id).Count();

            return new ListResult<IndexViewModel>(data, page, size, totalRow);
        }

        public Task Update(int id, ProductPackingFormViewModel viewModel)
        {
            var model = new ProductPackingModel(null, viewModel.PackingType, viewModel.Quantity.GetValueOrDefault(), viewModel.SKUId.GetValueOrDefault());
            return _productPackingRepository.UpdateAsync(id, model);
        }
    }
}
