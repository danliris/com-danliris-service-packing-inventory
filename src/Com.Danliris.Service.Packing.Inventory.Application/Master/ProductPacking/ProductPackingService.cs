using Com.Danliris.Service.Packing.Inventory.Application.DTOs;
using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.ProductPacking
{
    public class ProductPackingService : IProductPackingService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRepository<ProductPackingModel> _productPackingRepository;
        private readonly IRepository<ProductSKUModel> _productSKURepository;
        private readonly IRepository<CategoryModel> _categoryRepository;
        private readonly IRepository<UnitOfMeasurementModel> _uomRepository;

        public ProductPackingService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _productPackingRepository = serviceProvider.GetService<IRepository<ProductPackingModel>>();
            _productSKURepository = serviceProvider.GetService<IRepository<ProductSKUModel>>();

            _uomRepository = serviceProvider.GetService<IRepository<UnitOfMeasurementModel>>();
        }

        public async Task<int> Create(FormDto form)
        {
            if (_productPackingRepository.ReadAll().Any(entity => entity.Name == form.Name))
            {
                var errorResult = new List<ValidationResult>()
                {
                    new ValidationResult("Nama tidak boleh duplikat", new List<string> { "Name" })
                };
                var validationContext = new ValidationContext(form, _serviceProvider, null);
                throw new ServiceValidationException(validationContext, errorResult);
            }

            if (_productPackingRepository.ReadAll().Any(entity => entity.Code == form.Code))
            {
                var errorResult = new List<ValidationResult>()
                {
                    new ValidationResult("Kode tidak boleh duplikat", new List<string> { "Code" })
                };
                var validationContext = new ValidationContext(form, _serviceProvider, null);
                throw new ServiceValidationException(validationContext, errorResult);
            }

            if (_productPackingRepository.ReadAll().Any(entity => entity.ProductSKUId == form.ProductSKUId.GetValueOrDefault() && entity.PackingSize == form.PackingSize.GetValueOrDefault()))
            {
                var errorResult = new List<ValidationResult>()
                {
                    new ValidationResult("Sudah ada SKU dengan Kuantiti Per packing yang sama", new List<string> { "ProductSKU" })
                };
                var validationContext = new ValidationContext(form, _serviceProvider, null);
                throw new ServiceValidationException(validationContext, errorResult);
            }

            var model = new ProductPackingModel(form.ProductSKUId.GetValueOrDefault(), form.UOMId.GetValueOrDefault(), form.PackingSize.GetValueOrDefault(), form.Code, form.Name, form.Description);

            return await _productPackingRepository.InsertAsync(model);
        }

        public Task<int> Delete(int id)
        {
            return _productPackingRepository.DeleteAsync(id);
        }

        public async Task<ProductPackingDto> GetById(int id)
        {
            var productPacking = await _productPackingRepository.ReadByIdAsync(id);
            if (productPacking != null)
            {
                var uom = await _uomRepository.ReadByIdAsync(productPacking.UOMId);
                var productSKU = await _productSKURepository.ReadByIdAsync(productPacking.ProductSKUId);
                var skuUOM = await _uomRepository.ReadByIdAsync(productSKU.UOMId);
                var skuCategory = await _categoryRepository.ReadByIdAsync(productSKU.CategoryId);

                return new ProductPackingDto(productPacking, productSKU, uom, skuUOM, skuCategory);
            }

            return null;
        }

        public async Task<ProductPackingIndex> GetIndex(IndexQueryParam queryParam)
        {
            if (string.IsNullOrWhiteSpace(queryParam.order))
                queryParam.order = "{}";

            var searchAttributes = new List<string>() { "Name", "Code", "UOMUnit", "ProductSKUName" };
            var order = JsonConvert.DeserializeObject<Dictionary<string, string>>(queryParam.order);

            var productPackingQuery = _productPackingRepository.ReadAll();
            var uomQuery = _uomRepository.ReadAll();
            var productSKUQuery = _productSKURepository.ReadAll();

            var query = from productPacking in productPackingQuery
                        join uom in uomQuery on productPacking.UOMId equals uom.Id into uomProducts
                        from uomProduct in uomProducts.DefaultIfEmpty()

                        join productSKU in productSKUQuery on productPacking.ProductSKUId equals productSKU.Id into products
                        from product in products.DefaultIfEmpty()

                        select new ProductPackingIndexInfo()
                        {
                            LastModifiedUtc = productPacking.LastModifiedUtc,
                            Name = productPacking.Name,
                            Code = productPacking.Code,
                            UOMUnit = uomProduct.Unit,
                            ProductSKUName = product.Name,
                            PackingSize = productPacking.PackingSize,
                            Id = productPacking.Id
                        };

            query = QueryHelper<ProductPackingIndexInfo>.Search(query, searchAttributes, queryParam.keyword, true);
            query = QueryHelper<ProductPackingIndexInfo>.Order(query, order);

            var total = await query.CountAsync();
            var data = query.Skip(queryParam.size * (queryParam.page - 1)).Take(queryParam.size).ToList();
            return new ProductPackingIndex(data, total, queryParam.page, queryParam.size);
        }

        public async Task<int> Update(int id, FormDto form)
        {
            var model = await _productPackingRepository.ReadByIdAsync(id);

            if (_productPackingRepository.ReadAll().Any(entity => entity.Name == form.Name) && form.Name == model.Name)
            {
                var errorResult = new List<ValidationResult>()
                {
                    new ValidationResult("Nama tidak boleh duplikat", new List<string> { "Name" })
                };
                var validationContext = new ValidationContext(form, _serviceProvider, null);
                throw new ServiceValidationException(validationContext, errorResult);
            }

            if (_productPackingRepository.ReadAll().Any(entity => entity.Code == form.Code) && form.Code == model.Code)
            {
                var errorResult = new List<ValidationResult>()
                {
                    new ValidationResult("Kode tidak boleh duplikat", new List<string> { "Code" })
                };
                var validationContext = new ValidationContext(form, _serviceProvider, null);
                throw new ServiceValidationException(validationContext, errorResult);
            }

            if (_productPackingRepository.ReadAll().Any(entity => entity.ProductSKUId == form.ProductSKUId.GetValueOrDefault() && entity.PackingSize == form.PackingSize.GetValueOrDefault()) && (form.ProductSKUId.GetValueOrDefault() != model.ProductSKUId && form.PackingSize.GetValueOrDefault() != model.PackingSize ))
            {
                var errorResult = new List<ValidationResult>()
                {
                    new ValidationResult("Sudah ada SKU dengan Kuantiti Per packing yang sama", new List<string> { "ProductSKU" })
                };
                var validationContext = new ValidationContext(form, _serviceProvider, null);
                throw new ServiceValidationException(validationContext, errorResult);
            }

            model.SetCode(form.Code);
            model.SetName(form.Name);
            model.SetProductSKU(form.ProductSKUId.GetValueOrDefault());
            model.SetPackingSize(form.PackingSize.GetValueOrDefault());
            model.SetUOM(form.UOMId.GetValueOrDefault());
            model.SetDescription(form.Description);

            return await _productPackingRepository.UpdateAsync(id, model);
        }
    }
}
