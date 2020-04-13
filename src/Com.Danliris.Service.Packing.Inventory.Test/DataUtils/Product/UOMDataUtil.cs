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
    public class UOMDataUtil
    {
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<UnitOfMeasurementModel> _dbSet;

        public UOMDataUtil(PackingInventoryDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<UnitOfMeasurementModel>();
        }

        public UnitOfMeasurementModel GetModel()
        {
            return new UnitOfMeasurementModel("unit" + DateTime.Now.Ticks);
        }

        public async Task<UnitOfMeasurementModel> GetNewData()
        {
            var model = GetModel();

            EntityExtension.FlagForCreate(model, "test", "test");

            _dbSet.Add(model);

            await _dbContext.SaveChangesAsync();

            return model;
        }
    }
}
