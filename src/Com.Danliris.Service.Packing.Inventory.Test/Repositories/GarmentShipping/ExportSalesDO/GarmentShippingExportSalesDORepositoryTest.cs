using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ExportSalesDO;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ExportSalesDO;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.ExportSalesDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.ExportSalesDO
{
    public class GarmentShippingExportSalesDORepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingExportSalesDORepository, GarmentShippingExportSalesDOModel, GarmentShippingExportSalesDODataUtil>
    {
        private const string ENTITY = "GarmentShippingExportSalesDO";

        public GarmentShippingExportSalesDORepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async Task Should_Success_Update_Data()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new GarmentShippingExportSalesDORepository(dbContext, serviceProvider);

            var oldModel = DataUtil(repo, dbContext).GetModel();
            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);
            data.SetDate(data.Date.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            data.SetTo("Updated " + data.To, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetUnitCode("Updated " + data.UnitCode, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetUnitId(2, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetUnitName("Updated " + data.UnitName, data.LastModifiedBy, data.LastModifiedAgent);

            foreach (var item in data.Items)
            {
                item.SetComodityId(1 + item.ComodityId, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetComodityCode("Updated " + item.ComodityCode, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetComodityName("Updated " + item.ComodityName, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetQuantity(1 + item.Quantity, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetUomId(1 + item.UomId, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetUomUnit("Updated " + item.UomUnit, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetDescription("Updated " + item.Description, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetCartonQuantity(1 + item.CartonQuantity, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetGrossWeight(1 + item.GrossWeight, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetNettWeight(1 + item.NettWeight, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetVolume(1 + item.Volume, item.LastModifiedBy, item.LastModifiedAgent);

            }

            var result = await repo.UpdateAsync(data.Id, data);

            Assert.NotEqual(0, result);
        }

    }
}
