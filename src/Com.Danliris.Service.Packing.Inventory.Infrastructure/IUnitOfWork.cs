using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
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
        IBaseRepository<ProductSKUModel> ProductSKUs { get; }
        IBaseRepository<UnitOfMeasurementModel> UOMs { get; }
        IBaseRepository<CategoryModel> Categories { get; }

        IBaseRepository<FabricProductSKUModel> FabricSKUProducts { get; }
        void Commit();
    }
}
