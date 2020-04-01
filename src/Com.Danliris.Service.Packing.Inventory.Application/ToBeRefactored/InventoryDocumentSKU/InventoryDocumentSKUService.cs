using Microsoft.Extensions.DependencyInjection;
using System;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.InventoryDocumentSKU;
using System.Linq;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentSKU
{
    public class InventoryDocumentSKUService : IInventoryDocumentSKUService
    {
        private readonly IInventoryDocumentSKURepository _inventoryDocumentSKURepository;

        public InventoryDocumentSKUService(IServiceProvider serviceProvider)
        {
            _inventoryDocumentSKURepository = serviceProvider.GetService<IInventoryDocumentSKURepository>();
        }

        public Task Create(CreateInventoryDocumentSKUViewModel viewModel)
        {
            var inventorySKUModel = new InventoryDocumentSKUModel(
                null,
                DateTimeOffset.UtcNow,
                viewModel.Items.Select(item =>
                {

                    return new InventoryDocumentSKUItemModel(
                        item.Quantity.GetValueOrDefault(),
                        item.SKUId.GetValueOrDefault(),
                        item.UOMUnit
                        );
                }).ToList(),
                viewModel.ReferenceNo,
                viewModel.ReferenceType,
                viewModel.Remark,
                JsonConvert.SerializeObject(viewModel.Storage),
                viewModel.Storage.Id.GetValueOrDefault(),
                viewModel.Type
                );

            return _inventoryDocumentSKURepository.InsertAsync(inventorySKUModel);
        }

        public Task<InventoryDocumentSKUModel> ReadById(int id)
        {
            return _inventoryDocumentSKURepository.ReadByIdAsync(id);
        }

        public ListResult<IndexViewModel> ReadByKeyword(string keyword, string order, int page, int size)
        {
            var query = _inventoryDocumentSKURepository.ReadAll();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(entity => entity.Storage.Contains(keyword) || entity.Code.Contains(keyword));
            }

            if (string.IsNullOrWhiteSpace(order))
            {
                order = "{}";
            }
            var orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<InventoryDocumentSKUModel>.Order(query, orderDictionary);

            var queryData = query.Skip((page - 1) * size).Take(size).Select(entity => new 
            {
                entity.Code,
                entity.Date,
                entity.Id,
                entity.ReferenceNo,
                entity.ReferenceType,
                entity.Remark,
                entity.Storage,
                entity.Type
            }).ToList();
            var totalRow = queryData.Count();

            var data = queryData.Select(entity => new IndexViewModel()
            {
                Code = entity.Code,
                Date = entity.Date,
                Id = entity.Id,
                ReferenceNo = entity.ReferenceNo,
                ReferenceType = entity.ReferenceType,
                Storage = JsonConvert.DeserializeObject<Storage>(entity.Storage),
                Type = entity.Type
            }).ToList();

            return new ListResult<IndexViewModel>(data, page, size, totalRow);
        }
    }
}
