using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Product
{
    public class ProductDataUtil
    {
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<ProductModel> _dbSet;
        private readonly CategoryDataUtil _categoryDataUtil;
        private readonly UOMDataUtil _uomDataUtil;
        private readonly PackingDataUtil _packingDataUtil;

        public ProductDataUtil(PackingInventoryDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<ProductModel>();

            _categoryDataUtil = new CategoryDataUtil(dbContext);
            _uomDataUtil = new UOMDataUtil(dbContext);
            _packingDataUtil = new PackingDataUtil(dbContext);
        }

        public async Task<ProductModel> GetModel()
        {
            var category = await _categoryDataUtil.GetNewData();
            var uom = await _uomDataUtil.GetNewData();
            var packing = await _packingDataUtil.GetNewData();

            return new ProductModel("code", "name", uom.Id, category.Id, packing.Id, "");
        }

        public async Task<ProductModel> GetNewData()
        {
            var model = await GetModel();

            EntityExtension.FlagForCreate(model, "test", "test");

            _dbSet.Add(model);

            await _dbContext.SaveChangesAsync();

            return model;
        }
    }
}
