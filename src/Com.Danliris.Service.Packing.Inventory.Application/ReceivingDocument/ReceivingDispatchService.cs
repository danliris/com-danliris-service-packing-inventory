using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.InventoryDocumentPacking;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.InventoryDocumentSKU;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductPacking;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductSKU;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ReceivingDocument
{
    public class ReceivingDispatchService : IReceivingDispatchService
    {
        private readonly IInventoryDocumentSKURepository _inventoryDocumentSKURepository;
        private readonly IInventoryDocumentPackingRepository _inventoryDocumentPackingRepository;
        private readonly IProductSKURepository _productSKURepository;
        private readonly IProductPackingRepository _productPackingRepository;

        public ReceivingDispatchService(IInventoryDocumentSKURepository inventoryDocumentSKURepository, IInventoryDocumentPackingRepository inventoryDocumentPackingRepository, IProductSKURepository productSKURepository, IProductPackingRepository productPackingRepository)
        {
            _inventoryDocumentSKURepository = inventoryDocumentSKURepository;
            _inventoryDocumentPackingRepository = inventoryDocumentPackingRepository;
            _productSKURepository = productSKURepository;
            _productPackingRepository = productPackingRepository;
        }

        public async Task Dispatch(CreateReceivingDispatchDocumentViewModel viewModel)
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
                null,
                $"Penerimaan Barang {viewModel.Storage.Name}",
                null,
                JsonConvert.SerializeObject(viewModel.Storage),
                viewModel.Storage.Id.GetValueOrDefault(),
                "OUT"
                );

            await _inventoryDocumentSKURepository.InsertAsync(inventorySKUModel);

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
                null,
                $"Penerimaan Barang {viewModel.Storage.Name}",
                null,
                JsonConvert.SerializeObject(viewModel.Storage),
                viewModel.Storage.Id.GetValueOrDefault(),
                "OUT"
                );

            await _inventoryDocumentPackingRepository.InsertAsync(inventoryPackingModel);
        }

        public async Task Receive(CreateReceivingDispatchDocumentViewModel viewModel)
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
                null,
                $"Pengeluaran Barang {viewModel.Storage.Name}",
                null,
                JsonConvert.SerializeObject(viewModel.Storage),
                viewModel.Storage.Id.GetValueOrDefault(),
                "IN"
                );

            await _inventoryDocumentSKURepository.InsertAsync(inventorySKUModel);

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
                null,
                $"Pengeluaran Barang {viewModel.Storage.Name}",
                null,
                JsonConvert.SerializeObject(viewModel.Storage),
                viewModel.Storage.Id.GetValueOrDefault(),
                "IN"
                );

            await _inventoryDocumentPackingRepository.InsertAsync(inventoryPackingModel);
        }
    }
}
