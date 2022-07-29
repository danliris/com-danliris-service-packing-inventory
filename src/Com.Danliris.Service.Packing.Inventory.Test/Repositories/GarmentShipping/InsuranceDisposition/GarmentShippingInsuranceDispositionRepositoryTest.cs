using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.InsuranceDisposition;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.InsuranceDisposition;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.InsuranceDisposition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.InsuranceDisposition
{
    public class GarmentShippingInsuranceDispositionRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingInsuranceDispositionRepository, GarmentShippingInsuranceDispositionModel, GarmentShippingInsuranceDispositionDataUtil>
    {
        private const string ENTITY = "GarmentShippingInsuranceDisposition";

        public GarmentShippingInsuranceDispositionRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async Task Should_Success_Update_Data()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new GarmentShippingInsuranceDispositionRepository(dbContext, serviceProvider);

            var oldModel = DataUtil(repo, dbContext).GetModel();
            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);
            data.SetBankName("updated", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetInsuranceCode("updated", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetInsuranceId(model.InsuranceId + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetInsuranceName("updated", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetPaymentDate(model.PaymentDate.AddDays(2), data.LastModifiedBy, data.LastModifiedAgent);
            data.SetRate(model.Rate+1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetRemark("updated", data.LastModifiedBy, data.LastModifiedAgent);

            foreach (var item in data.Items)
            {
                item.SetAmount(item.Amount + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetCurrencyRate(item.CurrencyRate + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetPolicyDate(item.PolicyDate.AddDays(2), data.LastModifiedBy, data.LastModifiedAgent);
                item.SetPolicyNo(item.PolicyNo + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetAmount2A(item.Amount2A + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetAmount2B(item.Amount2B + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetAmount2C(item.Amount2C + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetAmount1A(item.Amount1A + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetAmount1B(item.Amount1B + 1, data.LastModifiedBy, data.LastModifiedAgent);
            }


            var result = await repo.UpdateAsync(data.Id, data);

            Assert.NotEqual(0, result);
        }
    }
}
