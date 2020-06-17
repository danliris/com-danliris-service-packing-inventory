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
            var code = CodeGenerator.Generate(8);

            while (_productPackingRepository.ReadAll().Any(entity => entity.Code == code))
            {
                code = CodeGenerator.Generate(8);
            }

            var product = await _productSKURepository.ReadByIdAsync(form.ProductSKUId.GetValueOrDefault());
            var uom = await _uomRepository.ReadByIdAsync(form.UOMId.GetValueOrDefault());

            var name = product.Name + " " + uom.Unit;

            if (_productPackingRepository.ReadAll().Any(entity => entity.Name == name))
            {
                var errorResult = new List<ValidationResult>()
                {
                    new ValidationResult("Nama tidak boleh duplikat", new List<string> { "Name" })
                };
                var validationContext = new ValidationContext(form, _serviceProvider, null);
                throw new ServiceValidationException(validationContext, errorResult);
            }

            //var model = new ProductPackingModel(
            //    product.Id,
            //    uom.Id,
            //    form.PackingSize.GetValueOrDefault(),
            //    code,
            //    name
            //    );

            //return await _productPackingRepository.InsertAsync(model);

            throw new NotImplementedException();
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
                var product = await _productSKURepository.ReadByIdAsync(productPacking.ProductSKUId);

                return new ProductPackingDto(productPacking, product, uom);
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

            query = QueryHelper<ProductPackingIndexInfo>.Search(query, searchAttributes, queryParam.keyword);
            query = QueryHelper<ProductPackingIndexInfo>.Order(query, order);

            var total = await query.CountAsync();
            var data = query.Skip(queryParam.size * (queryParam.page - 1)).Take(queryParam.size).ToList();
            return new ProductPackingIndex(data, total, queryParam.page, queryParam.size);
        }

        public Task<int> Update(int id, FormDto form)
        {
            //var model = await _productPackingRepository.ReadByIdAsync(id);

            //model.Set(form.Name);
            //model.SetUOM(form.UOMId.GetValueOrDefault());
            //model.SetCategory(form.CategoryId.GetValueOrDefault());
            //model.SetDescription(form.Description);

            //return await _productPackingRepository.UpdateAsync(id, model);

            throw new NotImplementedException();
        }
    }
}
