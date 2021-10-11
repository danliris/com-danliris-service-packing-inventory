using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentDraftPackingListItem;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentDraftPackingListItem;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentDraftPackingListItem;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentDraftPackingListItem
{
    public class GarmentDraftPackingListItemRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, GarmentDraftPackingListItemRepository, GarmentDraftPackingListItemModel, GarmentDraftPackingListItemDataUtil>
    {
        private const string ENTITY = "GarmentDraftPackingListItem";

        public GarmentDraftPackingListItemRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async Task Should_Success_Update_Data()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new GarmentDraftPackingListItemRepository(dbContext, serviceProvider);

            var details = new HashSet<GarmentDraftPackingListDetailModel> {
                    new GarmentDraftPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, 1, 1, new HashSet<GarmentDraftPackingListDetailSizeModel> {
                        new GarmentDraftPackingListDetailSizeModel(1, "", 1),
                        new GarmentDraftPackingListDetailSizeModel(1, "", 1)
                    },1),
                    new GarmentDraftPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, 1, 1, new HashSet<GarmentDraftPackingListDetailSizeModel> {
                        new GarmentDraftPackingListDetailSizeModel(1, "", 1)
                    },1)
            };
            
            var oldModel = new GarmentDraftPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", details);

            await repo.InsertAsync(oldModel);

            var item = DataUtil(repo, dbContext).CopyModel(oldModel);

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
            item.SetPriceCmt(1 + item.PriceCMT, item.LastModifiedBy, item.LastModifiedAgent);
            item.SetPriceFob(1 + item.PriceFOB, item.LastModifiedBy, item.LastModifiedAgent);
            item.SetAmount(1 + item.Amount, item.LastModifiedBy, item.LastModifiedAgent);
            item.SetValas("Updated " + item.Valas, item.LastModifiedBy, item.LastModifiedAgent);
            item.SetUnitId(1 + item.UnitId, item.LastModifiedBy, item.LastModifiedAgent);
            item.SetUnitCode("Updated " + item.UnitCode, item.LastModifiedBy, item.LastModifiedAgent);
            item.SetArticle("Updated " + item.Article, item.LastModifiedBy, item.LastModifiedAgent);
            item.SetOrderNo("Updated " + item.OrderNo, item.LastModifiedBy, item.LastModifiedAgent);
            item.SetDescription("Updated " + item.Description, item.LastModifiedBy, item.LastModifiedAgent);
            item.SetDescriptionMd("Updated " + item.DescriptionMd, item.LastModifiedBy, item.LastModifiedAgent);

            foreach (var detail in item.Details)
            {
                detail.SetCarton1(1 + detail.Carton1, detail.LastModifiedBy, detail.LastModifiedAgent);
                detail.SetCarton2(1 + detail.Carton2, detail.LastModifiedBy, detail.LastModifiedAgent);
                detail.SetColour(1 + detail.Colour, detail.LastModifiedBy, detail.LastModifiedAgent);
                detail.SetCartonQuantity(1 + detail.CartonQuantity, detail.LastModifiedBy, detail.LastModifiedAgent);
                detail.SetQuantityPCS(1 + detail.QuantityPCS, detail.LastModifiedBy, detail.LastModifiedAgent);
                detail.SetTotalQuantity(1 + detail.TotalQuantity, detail.LastModifiedBy, detail.LastModifiedAgent);
                detail.SetLength(1 + detail.Length, detail.LastModifiedBy, detail.LastModifiedAgent);
                detail.SetWidth(1 + detail.Width, detail.LastModifiedBy, detail.LastModifiedAgent);
                detail.SetHeight(1 + detail.Height, detail.LastModifiedBy, detail.LastModifiedAgent);
                detail.SetGrossWeight(1 + detail.GrossWeight, detail.LastModifiedBy, detail.LastModifiedAgent);
                detail.SetNetWeight(1 + detail.NetWeight, detail.LastModifiedBy, detail.LastModifiedAgent);
                detail.SetNetNetWeight(1 + detail.NetNetWeight, detail.LastModifiedBy, detail.LastModifiedAgent);

                foreach (var size in detail.Sizes)
                {
                    size.SetSizeId(1 + size.SizeId, size.LastModifiedBy, size.LastModifiedAgent);
                    size.SetSize(1 + size.Size, size.LastModifiedBy, size.LastModifiedAgent);
                    size.SetQuantity(1 + size.Quantity, size.LastModifiedBy, size.LastModifiedAgent);

                    if (item.Id == 2 || detail.Id == 2 || size.Id == 2)
                    {
                        size.Id = 0;
                    }
                }

                if (item.Id == 2 || detail.Id == 2)
                {
                    detail.Id = 0;
                }
            }
                
            
            var result = await repo.UpdateAsync(item.Id, item);

            Assert.NotEqual(0, result);
        }

    }
}
