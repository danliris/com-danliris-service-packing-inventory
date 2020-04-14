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
    public class PackingDataUtil
    {
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<PackingModel> _dbSet;

        public PackingDataUtil(PackingInventoryDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<PackingModel>();
        }

        public PackingModel GetModel()
        {
            return new PackingModel("packingh" + DateTime.Now.Ticks, 100, "");
        }

        public async Task<PackingModel> GetNewData()
        {
            var model = GetModel();

            EntityExtension.FlagForCreate(model, "test", "test");

            _dbSet.Add(model);

            await _dbContext.SaveChangesAsync();

            return model;
        }
    }
}
