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
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.UOM
{
    public class CategoryService : IUOMService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRepository<UnitOfMeasurementModel> _uomRepository;

        public CategoryService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _uomRepository = serviceProvider.GetService<IRepository<UnitOfMeasurementModel>>();
        }

        public Task<int> Create(FormDto form)
        {
            if (_uomRepository.ReadAll().Any(entity => entity.Unit == form.Unit))
            {
                var errorResult = new List<ValidationResult>()
                {
                    new ValidationResult("Unit tidak boleh duplikat", new List<string> { "Unit" })
                };
                var validationContext = new ValidationContext(form, _serviceProvider, null);
                throw new ServiceValidationException(validationContext, errorResult);
            }

            var model = new UnitOfMeasurementModel(form.Unit);
            return _uomRepository.InsertAsync(model);

        }

        public Task<int> Delete(int id)
        {
            return _uomRepository.DeleteAsync(id);
        }

        public async Task<UnitOfMeasurementDto> GetById(int id)
        {
            var uom = await _uomRepository.ReadByIdAsync(id);
            if (uom != null)
            {
                return new UnitOfMeasurementDto(uom);
            }

            return null;
        }

        public async Task<UOMIndex> GetIndex(IndexQueryParam queryParam)
        {
            if (string.IsNullOrWhiteSpace(queryParam.order))
                queryParam.order = "{}";

            var searchAttributes = new List<string>() { "Unit" };
            var order = JsonConvert.DeserializeObject<Dictionary<string, string>>(queryParam.order);

            var categoryQuery = _uomRepository.ReadAll();

            var query = _uomRepository.ReadAll().Select(entity => new UOMIndexInfo(entity));

            query = QueryHelper<UOMIndexInfo>.Search(query, searchAttributes, queryParam.keyword);
            query = QueryHelper<UOMIndexInfo>.Order(query, order);

            var total = await query.CountAsync();
            var data = query.Skip(queryParam.size * (queryParam.page - 1)).Take(queryParam.size).ToList();
            return new UOMIndex(data, total, queryParam.page, queryParam.size);
        }

        public async Task<int> Update(int id, FormDto form)
        {
            var model = await _uomRepository.ReadByIdAsync(id);

            model.SetUnit(form.Unit);

            return await _uomRepository.UpdateAsync(id, model);
        }

        public async Task<int> Upsert(FormDto form)
        {
            if (!_uomRepository.ReadAll().Any(entity => entity.Unit.ToLower() == form.Unit.ToLower()))
            {
                var id = await Create(form);
                return id;
            }
            else
            {
                var model = _uomRepository.ReadAll().FirstOrDefault(entity => entity.Unit.ToLower() == form.Unit.ToLower());
                return model.Id;
            }
        }
    }
}
