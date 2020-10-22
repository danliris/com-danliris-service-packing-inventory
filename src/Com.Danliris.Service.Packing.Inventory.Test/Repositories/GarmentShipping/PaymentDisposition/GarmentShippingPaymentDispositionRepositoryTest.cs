using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.PaymentDisposition;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.ShippingPaymentDisposition
{
    public class GarmentShippingShippingPaymentDispositionRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingPaymentDispositionRepository, GarmentShippingPaymentDispositionModel, GarmentShippingPaymentDispositionDataUtil>
    {
        private const string ENTITY = "GarmentShippingShippingPaymentDisposition";

        public GarmentShippingShippingPaymentDispositionRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async Task Should_Success_Update_Data()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            var repo = new GarmentShippingPaymentDispositionRepository(dbContext, serviceProvider);

            var bills = new HashSet<GarmentShippingPaymentDispositionBillDetailModel> {
                new GarmentShippingPaymentDispositionBillDetailModel("",1),
                new GarmentShippingPaymentDispositionBillDetailModel("",1)
            };
            var invoices = new HashSet<GarmentShippingPaymentDispositionInvoiceDetailModel>
            {
                new GarmentShippingPaymentDispositionInvoiceDetailModel("",1,1,1,1,1,1,1),
                new GarmentShippingPaymentDispositionInvoiceDetailModel("",1,1,1,1,1,1,1)
            };
            var units = new HashSet<GarmentShippingPaymentDispositionUnitChargeModel>
            {
                new GarmentShippingPaymentDispositionUnitChargeModel(1, "",1,1),
                new GarmentShippingPaymentDispositionUnitChargeModel(1, "",1,1)
            };
            var oldModel = new GarmentShippingPaymentDispositionModel("", "", "", "", "", 1, "", "", "", 1, "", "", 1, "", "", 1, "", "", "", "", "", DateTimeOffset.Now, "", 1, 1, 1, "", 1, 1, 1, DateTimeOffset.Now, "", "", true, "", "", DateTimeOffset.Now, "", invoices, bills, units);

            await repo.InsertAsync(oldModel);

            var data = DataUtil(repo, dbContext).CopyModel(oldModel);

            data.SetAccNo(data.AccNo+1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetIsFreightCharged(!data.IsFreightCharged, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetAddress("Updated ", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetBank("Updated ", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetBillValue(data.BillValue + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetBuyerAgentCode("Updated ", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetBuyerAgentId(data.BuyerAgentId + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetBuyerAgentName("Updated ", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetCourierCode("Updated ", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetCourierId(data.CourierId + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetCourierName("Updated ", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetEMKLCode("Updated ", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetEMKLId(data.EMKLId + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetEMKLName("Updated ", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetForwarderCode("Updated ", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetForwarderId(data.ForwarderId + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetForwarderName("Updated ", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetFreightBy("Updated ", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetFreightDate(data.FreightDate.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            data.SetFreightNo("Updated ", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetIncomeIncomeTaxRate(data.IncomeTaxRate + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetIncomeTaxId(data.IncomeTaxId + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetIncomeTaxName("Updated ", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetIncomeTaxValue(data.IncomeTaxValue + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetInvoiceDate(data.InvoiceDate.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            data.SetInvoiceNumber("Updated ", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetInvoiceTaxNumber("Updated ", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetPaymentDate(data.PaymentDate.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            data.SetNPWP("Updated ", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetPaymentMethod("Updated ", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetPaymentTerm("Updated ", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetRemark("Updated ", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetSendBy("Updated ", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetTotalBill(data.TotalBill+1, data.LastModifiedBy, data.LastModifiedAgent);

            foreach (var item in data.InvoiceDetails)
            {
                item.SetChargeableWeight(1 + item.ChargeableWeight, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetGrossWeight(1 + item.GrossWeight, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetInvoiceId(1 + item.InvoiceId, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetInvoiceNo(1 + item.InvoiceNo, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetTotalCarton(1 + item.TotalCarton, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetVolume(1 + item.Volume, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetQuantity(1 + item.Quantity, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetAmount(1 + item.Amount, item.LastModifiedBy, item.LastModifiedAgent);

                if (item.Id == 2)
                {
                    item.Id = 0;
                }
            }

            foreach (var unit in data.UnitCharges)
            {
                unit.SetAmountPercentage(1 + unit.AmountPercentage, unit.LastModifiedBy, unit.LastModifiedAgent);
                unit.SetBillAmount(1 + unit.BillAmount, unit.LastModifiedBy, unit.LastModifiedAgent);
                unit.SetUnitCode(1 + unit.UnitCode, unit.LastModifiedBy, unit.LastModifiedAgent);
                unit.SetUnitId(1 + unit.UnitId, unit.LastModifiedBy, unit.LastModifiedAgent);

                if (unit.Id == 2)
                {
                    unit.Id = 0;
                }
            }

            foreach (var bill in data.BillDetails)
            {
                bill.SetAmount(1 + bill.Amount, bill.LastModifiedBy, bill.LastModifiedAgent);
                bill.SetBillDescription(1 + bill.BillDescription, bill.LastModifiedBy, bill.LastModifiedAgent);
                
                if (bill.Id == 2)
                {
                    bill.Id = 0;
                }
            }

            var result = await repo.UpdateAsync(data.Id, data);

            Assert.NotEqual(0, result);
        }
    }
}