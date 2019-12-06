using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductSKU;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ProductSKU
{
    public class ProductSKUService : IProductSKUService
    {
        private readonly IProductSKURepository _productSKURepository;

        public ProductSKUService(IServiceProvider serviceProvider)
        {
            _productSKURepository = serviceProvider.GetService<IProductSKURepository>();
        }

        public Task Create(CreateProductSKUViewModel viewModel)
        {
            var productSKU = new ProductSKUModel(
                null,
                viewModel.Composition,
                viewModel.Construction,
                viewModel.Design,
                viewModel.Grade,
                viewModel.LotNo,
                viewModel.ProductType,
                viewModel.UOMUnit,
                viewModel.Width,
                viewModel.WovenType,
                viewModel.YarnType1,
                viewModel.YarnType2
                );

            return _productSKURepository.InsertAsync(productSKU);
        }

        public Task Delete(int id)
        {
            return _productSKURepository.DeleteAsync(id);
        }

        public Task<ProductSKUModel> ReadById(int id)
        {
            return _productSKURepository.ReadByIdAsync(id);
        }

        public ListResult<IndexViewModel> ReadByKeyword(string keyword, int page, int size)
        {
            var query = _productSKURepository.ReadAll();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(entity => entity.Name.Contains(keyword) || entity.ProductType.Contains(keyword));
            }

            var data = query.Skip((page - 1) * size).Take(size).Select(entity => new IndexViewModel()
            {
                Code = entity.Code,
                Id = entity.Id,
                Name = entity.Name,
                ProductType = entity.ProductType
            }).ToList();
            var totalRow = query.Select(entity => entity.Id).Count();

            return new ListResult<IndexViewModel>(data, page, size, totalRow);
        }

        public Task Update(int id, UpdateProductSKUViewModel viewModel)
        {
            var productSKU = new ProductSKUModel(
                null,
                viewModel.Composition,
                viewModel.Construction,
                viewModel.Design,
                viewModel.Grade,
                viewModel.LotNo,
                viewModel.ProductType,
                viewModel.UOMUnit,
                viewModel.Width,
                viewModel.WovenType,
                viewModel.YarnType1,
                viewModel.YarnType2
                );

            return _productSKURepository.UpdateAsync(id, productSKU);
        }
    }
}
