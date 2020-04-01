using Microsoft.Extensions.DependencyInjection;
using System;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Linq;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.InventoryDocumentPacking;
using System.Collections.Generic;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentPacking
{
    public class InventoryDocumentPackingService : IInventoryDocumentPackingService
    {
        private readonly IInventoryDocumentPackingRepository _inventoryDocumentPackingRepository;

        public InventoryDocumentPackingService(IServiceProvider serviceProvider)
        {
            _inventoryDocumentPackingRepository = serviceProvider.GetService<IInventoryDocumentPackingRepository>();
        }

        public Task Create(CreateInventoryDocumentPackingViewModel viewModel)
        {
            var inventoryPackingModel = new InventoryDocumentPackingModel(
                null,
                DateTimeOffset.UtcNow,
                viewModel.Items.Select(item =>
                {
                    return new InventoryDocumentPackingItemModel(
                        item.PackingId.GetValueOrDefault(),
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

            return _inventoryDocumentPackingRepository.InsertAsync(inventoryPackingModel);
        }

        public Task<InventoryDocumentPackingModel> ReadById(int id)
        {
            return _inventoryDocumentPackingRepository.ReadByIdAsync(id);
        }

        public ListResult<IndexViewModel> ReadByKeyword(string keyword, string order, int page, int size)
        {
            var query = _inventoryDocumentPackingRepository.ReadAll();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(entity => entity.Storage.Contains(keyword) || entity.Code.Contains(keyword));
            }

            if (string.IsNullOrWhiteSpace(order))
            {
                order = "{}";
            }
            var orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<InventoryDocumentPackingModel>.Order(query, orderDictionary);

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
