using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.LocalSalesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.LocalSalesContract
{
    public class GarmentShippingLocalSalesContractRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingLocalSalesContractRepository, GarmentShippingLocalSalesContractModel, GarmentShippingLocalSalesContractDataUtil>
    {
        private const string ENTITY = "GarmentShippingLocalSalesContract";

        public GarmentShippingLocalSalesContractRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async Task Should_Success_Update_Data()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new GarmentShippingLocalSalesContractRepository(dbContext, serviceProvider);

            var oldModel = DataUtil(repo, dbContext).GetModel();
            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);
            data.SetBuyerAddress("updated", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetBuyerCode("updated", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetBuyerId(model.BuyerId + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetBuyerName("updated", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetBuyerNPWP("updated" + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetIsUsed(!data.IsUsed, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetIsUseVat(!data.IsUseVat, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetSellerAddress(model.SellerAddress + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetSellerName(model.SellerName + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetSellerNPWP(model.SellerNPWP + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetSellerPosition(model.SellerPosition + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetSubTotal(model.SubTotal + 1, data.LastModifiedBy, data.LastModifiedAgent);

            foreach (var item in data.Items)
            {
                item.SetProductId(item.ProductId + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetProductCode(item.ProductCode + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetProductName(item.ProductName + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetQuantity(item.Quantity + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetUomId(item.UomId + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetUomUnit(item.UomUnit + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetPrice(item.Price + 1, data.LastModifiedBy, data.LastModifiedAgent);
            }

            var result = await repo.UpdateAsync(data.Id, data);

            Assert.NotEqual(0, result);
        }
    }
}
