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
    public class CategoryDataUtil
    {
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<CategoryModel> _dbSet;

        public CategoryDataUtil(PackingInventoryDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<CategoryModel>();
        }

        public CategoryModel GetModel()
        {
            return new CategoryModel("category" + DateTime.Now.Ticks, "code" + DateTime.Now.Ticks);
        }

        public async Task<CategoryModel> GetNewData()
        {
            var model = GetModel();

            EntityExtension.FlagForCreate(model, "test", "test");

            _dbSet.Add(model);

            await _dbContext.SaveChangesAsync();

            return model;
        }
    }
}
