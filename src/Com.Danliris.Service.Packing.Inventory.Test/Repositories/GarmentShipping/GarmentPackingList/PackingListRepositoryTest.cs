﻿using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentPackingList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentPackingList
{
    public class PackingListRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentPackingListRepository, GarmentPackingListModel, GarmentPackingListDataUtil>
    {
        private const string ENTITY = "GarmentPackingList";

        public PackingListRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async Task Should_Success_Update_Data()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new GarmentPackingListRepository(dbContext, serviceProvider);

            var oldModel = DataUtil(repo, dbContext).GetModel();
            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);
            data.SetAccounting(!data.Accounting, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetOmzet(!data.Omzet, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetIsUsed(!data.IsUsed, data.LastModifiedBy, data.LastModifiedAgent);

            foreach (var item in data.Items)
            {
                item.SetRONo("Updated " + item.RONo, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetSCNo("Updated " + item.SCNo, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetBuyerBrandId(1 + item.BuyerBrandId, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetBuyerBrandName("Updated " + item.BuyerBrandName, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetComodityId(1 + item.ComodityId, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetComodityCode("Updated " + item.ComodityCode, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetComodityName("Updated " + item.ComodityName, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetComodityDescription("Updated " + item.ComodityDescription, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetQuantity(1 + item.Quantity, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetUomId(1 + item.UomId, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetUomUnit("Updated " + item.UomUnit, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetPriceRO(1 + item.PriceRO, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetPrice(1 + item.Price, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetAmount(1 + item.Amount, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetValas("Updated " + item.Valas, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetUnitId(1 + item.UnitId, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetUnitCode("Updated " + item.UnitCode, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetArticle("Updated " + item.Article, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetOrderNo("Updated " + item.OrderNo, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetDescription("Updated " + item.Description, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetAVG_GW(1 + item.AVG_GW, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetAVG_NW(1 + item.AVG_NW, item.LastModifiedBy, item.LastModifiedAgent);

                foreach (var detail in item.Details)
                {
                    detail.SetCarton1(1 + detail.Carton1, detail.LastModifiedBy, detail.LastModifiedAgent);
                    detail.SetCarton2(1 + detail.Carton2, detail.LastModifiedBy, detail.LastModifiedAgent);
                    detail.SetColour(1 + detail.Colour, detail.LastModifiedBy, detail.LastModifiedAgent);
                    detail.SetCartonQuantity(1 + detail.CartonQuantity, detail.LastModifiedBy, detail.LastModifiedAgent);
                    detail.SetQuantityPCS(1 + detail.QuantityPCS, detail.LastModifiedBy, detail.LastModifiedAgent);
                    detail.SetTotalQuantity(1 + detail.TotalQuantity, detail.LastModifiedBy, detail.LastModifiedAgent);

                    foreach (var size in detail.Sizes)
                    {
                        size.SetSizeId(1 + size.SizeId, size.LastModifiedBy, size.LastModifiedAgent);
                        size.SetSize(1 + size.Size, size.LastModifiedBy, size.LastModifiedAgent);
                        size.SetQuantity(1 + size.Quantity, size.LastModifiedBy, size.LastModifiedAgent);
                    }
                }
            }

            foreach (var measurement in data.Measurements)
            {
                measurement.SetLength(1 + measurement.Length, measurement.LastModifiedBy, measurement.LastModifiedAgent);
                measurement.SetWidth(1 + measurement.Width, measurement.LastModifiedBy, measurement.LastModifiedAgent);
                measurement.SetHeight(1 + measurement.Height, measurement.LastModifiedBy, measurement.LastModifiedAgent);
                measurement.SetCartonsQuantity(1 + measurement.CartonsQuantity, measurement.LastModifiedBy, measurement.LastModifiedAgent);
            }

            var result = await repo.UpdateAsync(data.Id, data);

            Assert.NotEqual(0, result);
        }
    }
}
