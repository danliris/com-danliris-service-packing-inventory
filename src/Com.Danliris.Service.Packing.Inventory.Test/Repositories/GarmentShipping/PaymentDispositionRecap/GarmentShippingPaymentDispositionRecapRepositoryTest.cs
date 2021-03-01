using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDispositionRecap;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDispositionRecap;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.PaymentDispositionRecap;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.ShippingPaymentDispositionRecap
{
    public class GarmentShippingPaymentDispositionRecapRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingPaymentDispositionRecapRepository, GarmentShippingPaymentDispositionRecapModel, PaymentDispositionRecapDataUtil>
    {
        private const string ENTITY = "GarmentShippingPaymentDispositionRecap";

        public GarmentShippingPaymentDispositionRecapRepositoryTest() : base(ENTITY)
        {
        }


        [Fact]
        public async Task Should_Success_Update_Data()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new GarmentShippingPaymentDispositionRecapRepository(dbContext, serviceProvider);

            var bills = new HashSet<GarmentShippingPaymentDispositionBillDetailModel> { new GarmentShippingPaymentDispositionBillDetailModel("", 1) };
            var units = new HashSet<GarmentShippingPaymentDispositionUnitChargeModel> { new GarmentShippingPaymentDispositionUnitChargeModel(1, "", 1, 1) };
            var invoices = new HashSet<GarmentShippingPaymentDispositionInvoiceDetailModel> { new GarmentShippingPaymentDispositionInvoiceDetailModel("", 1, 1, 1, 1, 1, 1, 1) };
            var dispoModel = new GarmentShippingPaymentDispositionModel("", "", "", "", "", 1, "", "", "", 1, "", "", 1, "", "", 1, "", "", "", "", "", DateTimeOffset.Now, "", 1, 1, 1, "", 1, 1, 1, DateTimeOffset.Now, "", "", true, "", "", DateTimeOffset.Now, "", "", "", invoices, bills, units);

            var item = new GarmentShippingPaymentDispositionRecapItemModel(1, 10, 10, 10, 10, 10) { Id = 1 };
            item.SetPaymentDisposition(dispoModel);
            var items = new HashSet<GarmentShippingPaymentDispositionRecapItemModel> { item };

            var oldModel = new GarmentShippingPaymentDispositionRecapModel("", DateTimeOffset.Now, 1, "", "", "", "", items);

            await repo.InsertAsync(oldModel);

            var data = DataUtil(repo, dbContext).CopyModel(oldModel);


            foreach (var itemToUpdate in data.Items)
            {
                item.SetOthersPayment(1 + item.OthersPayment, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetTruckingPayment(1 + item.TruckingPayment, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetVatService(1 + item.VatService, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetAmountService(1 + item.AmountService, item.LastModifiedBy, item.LastModifiedAgent);
            }

            var result = await repo.UpdateAsync(data.Id, data);

            Assert.NotEqual(0, result);
        }
    }
}