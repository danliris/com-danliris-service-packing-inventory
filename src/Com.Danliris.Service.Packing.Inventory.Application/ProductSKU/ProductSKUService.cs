using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductSKU;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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

        public async Task Create(CreateProductSKUViewModel viewModel)
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

            await _productSKURepository.InsertAsync(productSKU);
        }
    }
}
