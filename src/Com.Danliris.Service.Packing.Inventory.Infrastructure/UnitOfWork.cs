using Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PackingInventoryDbContext _dbContext;
        
        private IBaseRepository<ProductPackingInventoryDocumentModel> _productPackingInventoryDocuments;
        private IBaseRepository<ProductPackingInventoryMovementModel> _productPackingInventoryMovements;
        private IBaseRepository<ProductPackingInventorySummaryModel> _productPackingInventorySummaries;
        private IBaseRepository<ProductSKUInventoryDocumentModel> _productSKUInventoryDocuments;
        private IBaseRepository<ProductSKUInventoryMovementModel> _productSKUInventoryMovements;
        private IBaseRepository<ProductSKUInventorySummaryModel> _productSKUInventorySummaries;
        private IBaseRepository<ProductSKUModel> _productSKUs;
        private IBaseRepository<UnitOfMeasurementModel> _uoms;
        private IBaseRepository<CategoryModel> _categories;
        private IBaseRepository<FabricProductSKUModel> _fabricSKUProducts;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;

            _productPackingInventoryDocuments = serviceProvider.GetService<IBaseRepository<ProductPackingInventoryDocumentModel>>();
            _productPackingInventoryMovements = serviceProvider.GetService<IBaseRepository<ProductPackingInventoryMovementModel>>();
            _productPackingInventorySummaries = serviceProvider.GetService<IBaseRepository<ProductPackingInventorySummaryModel>>();
            _productSKUInventoryDocuments = serviceProvider.GetService<IBaseRepository<ProductSKUInventoryDocumentModel>>();
            _productSKUInventoryMovements = serviceProvider.GetService<IBaseRepository<ProductSKUInventoryMovementModel>>();
            _productSKUInventorySummaries = serviceProvider.GetService<IBaseRepository<ProductSKUInventorySummaryModel>>();
            _productSKUs = serviceProvider.GetService<IBaseRepository<ProductSKUModel>>();
            _uoms = serviceProvider.GetService<IBaseRepository<UnitOfMeasurementModel>>();
            _categories = serviceProvider.GetService<IBaseRepository<CategoryModel>>();

            _fabricSKUProducts = serviceProvider.GetService<IBaseRepository<FabricProductSKUModel>>();

            _serviceProvider = serviceProvider;
            
        }

        public IBaseRepository<ProductPackingInventoryDocumentModel> ProductPackingInventoryDocuments
        {
            get
            {
                return _productPackingInventoryDocuments ??
                    (_productPackingInventoryDocuments = new BaseRepository<ProductPackingInventoryDocumentModel>(_dbContext, _serviceProvider));
            }
        }

        public IBaseRepository<ProductPackingInventoryMovementModel> ProductPackingInventoryMovements
        {
            get
            {
                return _productPackingInventoryMovements ??
                    (_productPackingInventoryMovements = new BaseRepository<ProductPackingInventoryMovementModel>(_dbContext, _serviceProvider));
            }
        }

        public IBaseRepository<ProductPackingInventorySummaryModel> ProductPackingInventorySummaries
        {
            get
            {
                return _productPackingInventorySummaries ??
                    (_productPackingInventorySummaries = new BaseRepository<ProductPackingInventorySummaryModel>(_dbContext, _serviceProvider));
            }
        }

        public IBaseRepository<ProductSKUInventoryDocumentModel> ProductSKUInventoryDocuments
        {
            get
            {
                return _productSKUInventoryDocuments ??
                    (_productSKUInventoryDocuments = new BaseRepository<ProductSKUInventoryDocumentModel>(_dbContext, _serviceProvider));
            }
        }

        public IBaseRepository<ProductSKUInventoryMovementModel> ProductSKUInventoryMovements
        {
            get
            {
                return _productSKUInventoryMovements ??
                    (_productSKUInventoryMovements = new BaseRepository<ProductSKUInventoryMovementModel>(_dbContext, _serviceProvider));
            }
        }

        public IBaseRepository<ProductSKUInventorySummaryModel> ProductSKUInventorySummaries
        {
            get
            {
                return _productSKUInventorySummaries ??
                    (_productSKUInventorySummaries = new BaseRepository<ProductSKUInventorySummaryModel>(_dbContext, _serviceProvider));
            }
        }

        public IBaseRepository<ProductSKUModel> ProductSKUs
        {
            get
            {
                return _productSKUs ??
                    (_productSKUs = new BaseRepository<ProductSKUModel>(_dbContext, _serviceProvider));
            }
        }

        public IBaseRepository<UnitOfMeasurementModel> UOMs
        {
            get
            {
                return _uoms ??
                    (_uoms = new BaseRepository<UnitOfMeasurementModel>(_dbContext, _serviceProvider));
            }
        }

        public IBaseRepository<CategoryModel> Categories
        {
            get
            {
                return _categories ??
                    (_categories = new BaseRepository<CategoryModel>(_dbContext, _serviceProvider));
            }
        }

        public IBaseRepository<FabricProductSKUModel> FabricSKUProducts
        {
            get
            {
                return _fabricSKUProducts ??
                    (_fabricSKUProducts = new BaseRepository<FabricProductSKUModel>(_dbContext, _serviceProvider));
            }
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
