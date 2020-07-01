using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure
{
    public interface IUnitOfWork
    {
        IBaseRepository<ProductPackingInventoryDocumentModel> ProductPackingInventoryDocuments { get; }
        IBaseRepository<ProductPackingInventoryMovementModel> ProductPackingInventoryMovements { get; }
        IBaseRepository<ProductPackingInventorySummaryModel> ProductPackingInventorySummaries { get; }
        IBaseRepository<ProductSKUInventoryDocumentModel> ProductSKUInventoryDocuments { get; }
        IBaseRepository<ProductSKUInventoryMovementModel> ProductSKUInventoryMovements { get; }
        IBaseRepository<ProductSKUInventorySummaryModel> ProductSKUInventorySummaries { get; }
        void Commit();
    }
}
