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

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.ProductSKU
{
    public class ProductSKUService : IProductSKUService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRepository<ProductSKUModel> _productSKURepository;
        private readonly IRepository<CategoryModel> _categoryRepository;
        private readonly IRepository<UnitOfMeasurementModel> _uomRepository;

        public ProductSKUService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _productSKURepository = serviceProvider.GetService<IRepository<ProductSKUModel>>();

            _categoryRepository = serviceProvider.GetService<IRepository<CategoryModel>>();
            _uomRepository = serviceProvider.GetService<IRepository<UnitOfMeasurementModel>>();
        }

        public Task<int> Create(FormDto form)
        {
            if (_productSKURepository.ReadAll().Any(entity => entity.Name == form.Name))
            {
                var errorResult = new List<ValidationResult>()
                {
                    new ValidationResult("Nama tidak boleh duplikat", new List<string> { "Name" })
                };
                var validationContext = new ValidationContext(form, _serviceProvider, null);
                throw new ServiceValidationException(validationContext, errorResult);
            }

            if (_productSKURepository.ReadAll().Any(entity => entity.Code == form.Code))
            {
                var errorResult = new List<ValidationResult>()
                {
                    new ValidationResult("Kode tidak boleh duplikat", new List<string> { "Kode" })
                };
                var validationContext = new ValidationContext(form, _serviceProvider, null);
                throw new ServiceValidationException(validationContext, errorResult);
            }

            var model = new ProductSKUModel(
                form.Code,
                form.Name,
                form.UOMId.GetValueOrDefault(),
                form.CategoryId.GetValueOrDefault(),
                form.Description
                );

            return _productSKURepository.InsertAsync(model);
        }

        public Task<int> Delete(int id)
        {
            return _productSKURepository.DeleteAsync(id);
        }

        public async Task<ProductSKUDto> GetById(int id)
        {
            var product = await _productSKURepository.ReadByIdAsync(id);
            if (product != null)
            {
                var uom = await _uomRepository.ReadByIdAsync(product.UOMId);
                var category = await _categoryRepository.ReadByIdAsync(product.CategoryId);

                return new ProductSKUDto(product, uom, category);
            }

            return null;
        }

        public async Task<ProductSKUIndex> GetIndex(IndexQueryParam queryParam)
        {
            if (string.IsNullOrWhiteSpace(queryParam.order))
                queryParam.order = "{}";

            var searchAttributes = new List<string>() { "Name", "Code", "UOMUnit", "CategoryName" };
            var order = JsonConvert.DeserializeObject<Dictionary<string, string>>(queryParam.order);

            var productQuery = _productSKURepository.ReadAll();
            var uomQuery = _uomRepository.ReadAll();
            var categoryQuery = _categoryRepository.ReadAll();

            var query = from product in productQuery
                        join uom in uomQuery on product.UOMId equals uom.Id into uomProducts
                        from uomProduct in uomProducts.DefaultIfEmpty()

                        join category in categoryQuery on product.CategoryId equals category.Id into categoryProducts
                        from categoryProduct in categoryProducts.DefaultIfEmpty()

                        select new ProductSKUIndexInfo()
                        {
                            LastModifiedUtc = product.LastModifiedUtc,
                            Name = product.Name,
                            Code = product.Code,
                            UOMUnit = uomProduct.Unit,
                            CategoryName = categoryProduct.Name,
                            Id = product.Id
                        };

            query = QueryHelper<ProductSKUIndexInfo>.Search(query, searchAttributes, queryParam.keyword, true);
            query = QueryHelper<ProductSKUIndexInfo>.Order(query, order);

            var total = await query.CountAsync();
            var data = query.Skip(queryParam.size * (queryParam.page - 1)).Take(queryParam.size).ToList();
            return new ProductSKUIndex(data, total, queryParam.page, queryParam.size);
        }

        public async Task<int> Update(int id, FormDto form)
        {
            var model = await _productSKURepository.ReadByIdAsync(id);

            if (_productSKURepository.ReadAll().Any(entity => entity.Name == form.Name) && form.Name != model.Name)
            {
                var errorResult = new List<ValidationResult>()
                {
                    new ValidationResult("Nama tidak boleh duplikat", new List<string> { "Name" })
                };
                var validationContext = new ValidationContext(form, _serviceProvider, null);
                throw new ServiceValidationException(validationContext, errorResult);
            }

            if (_productSKURepository.ReadAll().Any(entity => entity.Code == form.Code) && form.Code != model.Code)
            {
                var errorResult = new List<ValidationResult>()
                {
                    new ValidationResult("Kode tidak boleh duplikat", new List<string> { "Kode" })
                };
                var validationContext = new ValidationContext(form, _serviceProvider, null);
                throw new ServiceValidationException(validationContext, errorResult);
            }

            model.SetCode(form.Code);
            model.SetName(form.Name);
            model.SetUOM(form.UOMId.GetValueOrDefault());
            model.SetCategory(form.CategoryId.GetValueOrDefault());
            model.SetDescription(form.Description);

            return await _productSKURepository.UpdateAsync(id, model);
        }
    }
}
