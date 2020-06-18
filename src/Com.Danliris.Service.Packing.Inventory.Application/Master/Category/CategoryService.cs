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

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRepository<CategoryModel> _categoryRepository;

        public CategoryService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _categoryRepository = serviceProvider.GetService<IRepository<CategoryModel>>();
        }

        public Task<int> Create(FormDto form)
        {
            var code = CodeGenerator.Generate(8);

            while (_categoryRepository.ReadAll().Any(entity => entity.Code == code))
            {
                code = CodeGenerator.Generate(8);
            }

            if (_categoryRepository.ReadAll().Any(entity => entity.Name == form.Name))
            {
                var errorResult = new List<ValidationResult>()
                {
                    new ValidationResult("Nama tidak boleh duplikat", new List<string> { "Name" })
                };
                var validationContext = new ValidationContext(form, _serviceProvider, null);
                throw new ServiceValidationException(validationContext, errorResult);
            }

            var model = new CategoryModel(form.Name, code);

            return _categoryRepository.InsertAsync(model);
        }

        public Task<int> Delete(int id)
        {
            return _categoryRepository.DeleteAsync(id);
        }

        public async Task<CategoryDto> GetById(int id)
        {
            var category = await _categoryRepository.ReadByIdAsync(id);
            if (category != null)
            {
                return new CategoryDto(category);
            }

            return null;
        }

        public async Task<CategoryIndex> GetIndex(IndexQueryParam queryParam)
        {
            if (string.IsNullOrWhiteSpace(queryParam.order))
                queryParam.order = "{}";

            var searchAttributes = new List<string>() { "Name", "Code" };
            var order = JsonConvert.DeserializeObject<Dictionary<string, string>>(queryParam.order);

            var query = _categoryRepository.ReadAll().Select(entity => new CategoryIndexInfo(entity));

            //if (!string.IsNullOrWhiteSpace(queryParam.keyword))
            //    query = query.Where(entity => entity.Code.Contains(queryParam.keyword) || entity.Name.Contains(queryParam.keyword));

            query = QueryHelper<CategoryIndexInfo>.Search(query, searchAttributes, queryParam.keyword, true);
            query = QueryHelper<CategoryIndexInfo>.Order(query, order);

            var total = await query.CountAsync();
            var data = query.Skip(queryParam.size * (queryParam.page - 1)).Take(queryParam.size).ToList();
            return new CategoryIndex(data, total, queryParam.page, queryParam.size);
        }

        public async Task<int> Update(int id, FormDto form)
        {
            var model = await _categoryRepository.ReadByIdAsync(id);

            model.SetName(form.Name);

            return await _categoryRepository.UpdateAsync(id, model);
        }

        public async Task<int> Upsert(FormDto form)
        {
            if (!_categoryRepository.ReadAll().Any(entity => entity.Name.ToLower() == form.Name.ToLower()))
            {
                var categoryId = await Create(form);
                return categoryId;
            }
            else
            {
                var category = _categoryRepository.ReadAll().FirstOrDefault(entity => entity.Name.ToLower() == form.Name.ToLower());
                return category.Id;
            }
        }
    }
}
