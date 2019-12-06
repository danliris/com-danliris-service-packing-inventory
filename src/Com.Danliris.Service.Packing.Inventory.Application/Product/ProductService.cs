using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductPacking;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductSKU;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductPackingRepository _productPackingRepository;
        private readonly IProductSKURepository _productSKURepository;

        public ProductService(IServiceProvider serviceProvider)
        {
            _productPackingRepository = serviceProvider.GetService<IProductPackingRepository>();
            _productSKURepository = serviceProvider.GetService<IProductSKURepository>();
        }

        public async Task<ProductPackingBarcodeInfo> CreateProductPackAndSKU(CreateProductPackAndSKUViewModel viewModel)
        {
            var productSKUToCheck = new ProductSKUModel(
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

            if (IsSKUAlreadyExist(productSKUToCheck))
            {
                var skuId = _productSKURepository.ReadAll().Where(entity => entity.Name == productSKUToCheck.Name).Select(entity => entity.Id).FirstOrDefault();
                var productPackingModel = new ProductPackingModel(null, viewModel.PackType, viewModel.Quantity.GetValueOrDefault(), skuId);
                await _productPackingRepository.InsertAsync(productPackingModel);

                var result = new ProductPackingBarcodeInfo(productPackingModel.Code, productPackingModel.SKUId, productPackingModel.Quantity, productSKUToCheck.UOMUnit, productPackingModel.PackType, productPackingModel.Id);
                return result;
            }
            else
            {
                await _productSKURepository.InsertAsync(productSKUToCheck);
                var productPackingModel = new ProductPackingModel(null, viewModel.PackType, viewModel.Quantity.GetValueOrDefault(), productSKUToCheck.Id);

                await _productPackingRepository.InsertAsync(productPackingModel);

                var result = new ProductPackingBarcodeInfo(productPackingModel.Code, productPackingModel.SKUId, productPackingModel.Quantity, productSKUToCheck.UOMUnit, productPackingModel.PackType, productPackingModel.Id);

                return result;
            }
        }

        private bool IsSKUAlreadyExist(ProductSKUModel productSKU)
        {
            return _productSKURepository.ReadAll().Any(entity => entity.Name.ToUpper() == productSKU.Name.ToUpper());
        }
    }
}
