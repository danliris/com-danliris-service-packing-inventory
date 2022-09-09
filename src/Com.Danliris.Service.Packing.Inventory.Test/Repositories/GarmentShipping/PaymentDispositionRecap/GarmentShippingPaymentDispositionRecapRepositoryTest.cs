using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDispositionRecap;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDisposition;
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
            var repoDispo = new GarmentShippingPaymentDispositionRepository(dbContext, serviceProvider);

            var bills = new HashSet<GarmentShippingPaymentDispositionBillDetailModel> { new GarmentShippingPaymentDispositionBillDetailModel("", 1) };
            var units = new HashSet<GarmentShippingPaymentDispositionUnitChargeModel> { new GarmentShippingPaymentDispositionUnitChargeModel(1, "", 1, 1) };
            var invoices = new HashSet<GarmentShippingPaymentDispositionInvoiceDetailModel> { new GarmentShippingPaymentDispositionInvoiceDetailModel("", 1, 1, 1, 1, 1, 1, 1) };
            var payments = new HashSet<GarmentShippingPaymentDispositionPaymentDetailModel> { new GarmentShippingPaymentDispositionPaymentDetailModel(DateTimeOffset.Now, "", 1) };
            var dispoModel = new GarmentShippingPaymentDispositionModel("", "", "", "", "", 1, "", "", "", 1, "", "", 1, "", "", 1, "", "", "", "", "", DateTimeOffset.Now, "", 1, 1, 1, "", 1, 1, 1, DateTimeOffset.Now, "", "", true, "", "", DateTimeOffset.Now, "", "", "", invoices, bills, units, payments) { Id = 1 };

            var item = new GarmentShippingPaymentDispositionRecapItemModel(1, 10, 10, 10, 10, 10) { Id = 1 };
           
            item.SetPaymentDisposition(dispoModel);
            var items = new HashSet<GarmentShippingPaymentDispositionRecapItemModel> { item };

            var oldModel = new GarmentShippingPaymentDispositionRecapModel("", DateTimeOffset.Now, 1, "", "", "", "", items);

            await repo.InsertAsync(oldModel);
            await repoDispo.InsertAsync(dispoModel);

            var data = DataUtil(repo, dbContext).CopyModel(oldModel);

            foreach (var itemToUpdate in data.Items)
            {
                itemToUpdate.SetOthersPayment(1 + item.OthersPayment, item.LastModifiedBy, item.LastModifiedAgent);
                itemToUpdate.SetTruckingPayment(1 + item.TruckingPayment, item.LastModifiedBy, item.LastModifiedAgent);
                itemToUpdate.SetVatService(1 + item.VatService, item.LastModifiedBy, item.LastModifiedAgent);
                itemToUpdate.SetAmountService(1 + item.AmountService, item.LastModifiedBy, item.LastModifiedAgent);

                var paymentDisposition = await repoDispo.ReadByIdAsync(itemToUpdate.PaymentDispositionId);

                if (paymentDisposition != null)
                {
                    paymentDisposition.SetIncomeTaxValue(1 + item.PaymentDisposition.IncomeTaxValue, "", "");;
                    itemToUpdate.SetPaymentDisposition(paymentDisposition);
                }
            }

            var result = await repo.UpdateAsync(data.Id, data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_Data()
        {
            string testName = GetCurrentMethod() + "Create";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new GarmentShippingPaymentDispositionRecapRepository(dbContext, serviceProvider);
            var repoDispo = new GarmentShippingPaymentDispositionRepository(dbContext, serviceProvider);

            var bills = new HashSet<GarmentShippingPaymentDispositionBillDetailModel> { new GarmentShippingPaymentDispositionBillDetailModel("", 1) };
            var units = new HashSet<GarmentShippingPaymentDispositionUnitChargeModel> { new GarmentShippingPaymentDispositionUnitChargeModel(1, "", 1, 1) };
            var invoices = new HashSet<GarmentShippingPaymentDispositionInvoiceDetailModel> { new GarmentShippingPaymentDispositionInvoiceDetailModel("", 1, 1, 1, 1, 1, 1, 1) };
            var payments = new HashSet<GarmentShippingPaymentDispositionPaymentDetailModel> { new GarmentShippingPaymentDispositionPaymentDetailModel(DateTimeOffset.Now, "", 1) };
            var dispoModel = new GarmentShippingPaymentDispositionModel("", "", "", "", "", 1, "", "", "", 1, "", "", 1, "", "", 1, "", "", "", "", "", DateTimeOffset.Now, "", 1, 1, 1, "", 1, 1, 1, DateTimeOffset.Now, "", "", true, "", "", DateTimeOffset.Now, "", "", "", invoices, bills, units, payments) { Id = 1 };

            var item = new GarmentShippingPaymentDispositionRecapItemModel(1, 10, 10, 10, 10, 10) { Id = 1 };

            item.SetPaymentDisposition(dispoModel);
            var items = new HashSet<GarmentShippingPaymentDispositionRecapItemModel> { item };

            var data = new GarmentShippingPaymentDispositionRecapModel("", DateTimeOffset.Now, 1, "", "", "", "", items);

            await repoDispo.InsertAsync(dispoModel);

            foreach (var itemToUpdate in data.Items)
            {
                var paymentDisposition = await repoDispo.ReadByIdAsync(itemToUpdate.PaymentDispositionId);

                if (paymentDisposition != null)
                {
                    paymentDisposition.SetIncomeTaxValue(1 + item.PaymentDisposition.IncomeTaxValue, "", ""); ;
                    itemToUpdate.SetPaymentDisposition(paymentDisposition);
                }
            }

            var result = await repo.InsertAsync(data);

            Assert.NotEqual(0, result);
        }
    }
}