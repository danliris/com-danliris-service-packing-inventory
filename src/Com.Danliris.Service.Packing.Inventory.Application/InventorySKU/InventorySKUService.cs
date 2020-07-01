using Com.Danliris.Service.Packing.Inventory.Application.QueueService;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.InventorySKU
{
    public class InventorySKUService : IInventorySKUService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAzureServiceBusSender<ProductSKUInventoryMovementModel> _queueService;

        public InventorySKUService(IServiceProvider serviceProvider)
        {
            _unitOfWork = serviceProvider.GetService<IUnitOfWork>();
            _queueService = serviceProvider.GetService<IAzureServiceBusSender<ProductSKUInventoryMovementModel>>();
        }

        public async Task<int> AddDocument(FormDto form)
        {
            var documentNo = CodeGenerator.GenerateCode();
            do
            {
                documentNo = CodeGenerator.GenerateCode();
            }
            while (_unitOfWork.ProductSKUInventoryDocuments.Get(entity => entity.DocumentNo == documentNo).Count() > 0);

            var model = new ProductSKUInventoryDocumentModel(
                documentNo,
                form.Date.GetValueOrDefault(),
                form.ReferenceNo,
                form.ReferenceType,
                form.Storage.Id.GetValueOrDefault(),
                form.Storage.Name,
                form.Storage.Code,
                form.Type,
                form.Remark);

            _unitOfWork.ProductSKUInventoryDocuments.Insert(model);
            _unitOfWork.Commit();

            if (model.Id > 0)
            {
                foreach (var item in form.Items)
                {
                    var movementItem = new ProductSKUInventoryMovementModel(model.Id, item.ProductSKUId, item.UOMId, model.StorageId, model.StorageCode, model.StorageName, item.Quantity, form.Type, item.Remark);
                    await _queueService.SendMessage(movementItem);
                }
            }

            return model.Id;
        }

        public void AddMovement(ProductSKUInventoryMovementModel inventoryMovement)
        {
            var summary = _unitOfWork.ProductSKUInventorySummaries.Get().FirstOrDefault(entity => entity.ProductSKUId == inventoryMovement.ProductSKUId && entity.UOMId == inventoryMovement.UOMId && entity.StorageId == inventoryMovement.StorageId);
            if (summary == null)
            {
                switch (inventoryMovement.Type.ToUpper())
                {
                    case "IN":
                        inventoryMovement.SetCurrentBalance(inventoryMovement.Quantity);
                        break;
                    case "OUT":
                        inventoryMovement.SetCurrentBalance(inventoryMovement.Quantity * -1);
                        break;
                    case "ADJ":
                        inventoryMovement.SetCurrentBalance(inventoryMovement.Quantity);
                        break;
                    default:
                        throw new Exception("Invalid Type");
                }
                _unitOfWork.ProductSKUInventoryMovements.Insert(inventoryMovement);

                summary = new ProductSKUInventorySummaryModel(inventoryMovement.ProductSKUId, inventoryMovement.StorageId, inventoryMovement.StorageCode, inventoryMovement.StorageName, inventoryMovement.UOMId);
                summary.SetBalance(inventoryMovement.CurrentBalance);
                _unitOfWork.ProductSKUInventorySummaries.Insert(summary);
            }
            else
            {
                inventoryMovement.SetPreviousBalance(summary.Balance);
                switch (inventoryMovement.Type.ToUpper())
                {
                    case "IN":
                        inventoryMovement.SetCurrentBalance(inventoryMovement.Quantity);
                        break;
                    case "OUT":
                        inventoryMovement.SetCurrentBalance(inventoryMovement.Quantity * -1);
                        break;
                    case "ADJ":
                        inventoryMovement.SetCurrentBalance(inventoryMovement.Quantity);
                        break;
                    default:
                        throw new Exception("Invalid Type");
                }
                _unitOfWork.ProductSKUInventoryMovements.Insert(inventoryMovement);

                summary.SetBalance(inventoryMovement.CurrentBalance);
                _unitOfWork.ProductSKUInventorySummaries.Update(summary);
            }

            _unitOfWork.Commit();
        }

        public DocumentDto GetDocumentById(int id)
        {
            var document = _unitOfWork.ProductSKUInventoryDocuments.GetByID(id);
            var items = _unitOfWork.ProductSKUInventoryMovements.Get(entity => entity.InventoryDocumentId == id).ToList();

            var productSKUIds = items.Select(element => element.ProductSKUId).ToList();
            var products = _unitOfWork.ProductSKUs.Get(entity => productSKUIds.Contains(entity.Id)).ToList();

            var uomIds = items.Select(element => element.UOMId).ToList();
            var uoms = _unitOfWork.UOMs.Get(entity => uomIds.Contains(entity.Id)).ToList();

            var categoryIds = products.Select(element => element.CategoryId).ToList();
            var categories = _unitOfWork.Categories.Get(entity => categoryIds.Contains(entity.Id)).ToList();

            return new DocumentDto(document, items, products, uoms, categories);
        }

        public DocumentIndexDto GetDocumentIndex(IndexQueryParam query)
        {
            if (!string.IsNullOrWhiteSpace(query.keyword))
            {
                var documents = _unitOfWork.ProductSKUInventoryDocuments.Get(
                    filter: entity => entity.DocumentNo.Contains(query.keyword) || entity.ReferenceNo.Contains(query.keyword) || entity.ReferenceType.Contains(query.keyword),
                    orderBy: entity => entity.OrderBy(ordered => ordered.LastModifiedUtc),
                    page: query.page,
                    size: query.size);

                return new DocumentIndexDto(documents, query.page, query.size);
            }
            else
            {
                var documents = _unitOfWork.ProductSKUInventoryDocuments.Get(
                    orderBy: entity => entity.OrderBy(ordered => ordered.LastModifiedUtc),
                    page: query.page,
                    size: query.size);

                return new DocumentIndexDto(documents, query.page, query.size);
            }
        }

        public MovementIndexDto GetMovementIndex(int storageId, int productSKUId, string type, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public SummaryIndexDto GetSummaryIndex(int storageId, int productSKUId)
        {
            throw new NotImplementedException();
        }
    }
}
