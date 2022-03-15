using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentShippingInvoice;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentCreditAdvice
{
    public class CreditAdviceRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingCreditAdviceRepository, GarmentShippingCreditAdviceModel, GarmentCreditAdviceDataUtil>
    {
        private const string ENTITY = "GarmentCreditAdvice";

        public CreditAdviceRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async override Task Should_Success_Insert()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentPackingListRepository repoPL = new GarmentPackingListRepository(dbContext, serviceProvider);
            GarmentPackingListDataUtil utilPL = new GarmentPackingListDataUtil(repoPL);
            GarmentPackingListModel dataPL = utilPL.GetModel();
            var dataPackingList = await repoPL.InsertAsync(dataPL);

            GarmentShippingInvoiceRepository repo = new GarmentShippingInvoiceRepository(dbContext, serviceProvider);
            GarmentShippingInvoiceDataUtil invoiceDataUtil = new GarmentShippingInvoiceDataUtil(repo, utilPL);
            GarmentShippingInvoiceModel data = invoiceDataUtil.GetModel();
            data.PackingListId = dataPL.Id;
            var dataInvoice = await repo.InsertAsync(data);

            GarmentShippingCreditAdviceRepository repoCreditAdvice = new GarmentShippingCreditAdviceRepository(dbContext, serviceProvider);
            GarmentCreditAdviceDataUtil CreditAdviceDataUtil = new GarmentCreditAdviceDataUtil(repoCreditAdvice);
            GarmentShippingCreditAdviceModel dataCreditAdvice = CreditAdviceDataUtil.GetModel();
            dataCreditAdvice.SetInvoiceId(data.Id, "test", "unitTest");
            var result = await repoCreditAdvice.InsertAsync(dataCreditAdvice);
            Assert.NotEqual(0, result);
        }

        [Fact]
        public async override Task Should_Success_Delete()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentPackingListRepository repoPL = new GarmentPackingListRepository(dbContext, serviceProvider);
            GarmentPackingListDataUtil utilPL = new GarmentPackingListDataUtil(repoPL);
            GarmentPackingListModel dataPL = utilPL.GetModel();
            var dataPackingList = await repoPL.InsertAsync(dataPL);

            GarmentShippingInvoiceRepository repo = new GarmentShippingInvoiceRepository(dbContext, serviceProvider);
            GarmentShippingInvoiceDataUtil invoiceDataUtil = new GarmentShippingInvoiceDataUtil(repo, utilPL);
            GarmentShippingInvoiceModel data = invoiceDataUtil.GetModel();
            data.PackingListId = dataPL.Id;
            var dataInvoice = await repo.InsertAsync(data);

            GarmentShippingCreditAdviceRepository repoCreditAdvice = new GarmentShippingCreditAdviceRepository(dbContext, serviceProvider);
            GarmentCreditAdviceDataUtil CreditAdviceDataUtil = new GarmentCreditAdviceDataUtil(repoCreditAdvice);
            GarmentShippingCreditAdviceModel dataCreditAdvice = CreditAdviceDataUtil.GetModel();
            dataCreditAdvice.SetInvoiceId(data.Id, "test", "unitTest");
            var result = await repoCreditAdvice.InsertAsync(dataCreditAdvice);
            var resultdelete = await repoCreditAdvice.DeleteAsync(dataCreditAdvice.Id);
            Assert.NotEqual(0, resultdelete);
        }

        [Fact]
        public async override Task Should_Success_ReadById()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            GarmentPackingListRepository repoPL = new GarmentPackingListRepository(dbContext, serviceProvider);
            GarmentPackingListDataUtil utilPL = new GarmentPackingListDataUtil(repoPL);
            GarmentPackingListModel dataPL = utilPL.GetModel();
            var dataPackingList = await repoPL.InsertAsync(dataPL);

            GarmentShippingInvoiceRepository repo = new GarmentShippingInvoiceRepository(dbContext, serviceProvider);
            GarmentShippingInvoiceDataUtil invoiceDataUtil = new GarmentShippingInvoiceDataUtil(repo, utilPL);
            GarmentShippingInvoiceModel data = invoiceDataUtil.GetModel();
            data.PackingListId = dataPL.Id;
            var dataInvoice = await repo.InsertAsync(data);

            GarmentShippingCreditAdviceRepository repoCreditAdvice = new GarmentShippingCreditAdviceRepository(dbContext, serviceProvider);
            GarmentCreditAdviceDataUtil CreditAdviceDataUtil = new GarmentCreditAdviceDataUtil(repoCreditAdvice);
            GarmentShippingCreditAdviceModel dataCreditAdvice = CreditAdviceDataUtil.GetModel();
            dataCreditAdvice.SetInvoiceId(data.Id, "test", "unitTest");
            var results = await repoCreditAdvice.InsertAsync(dataCreditAdvice);
            var result = repoCreditAdvice.ReadByIdAsync(dataCreditAdvice.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async override Task Should_Success_ReadAll()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            GarmentPackingListRepository repoPL = new GarmentPackingListRepository(dbContext, serviceProvider);
            GarmentPackingListDataUtil utilPL = new GarmentPackingListDataUtil(repoPL);
            GarmentPackingListModel dataPL = utilPL.GetModel();
            var dataPackingList = await repoPL.InsertAsync(dataPL);

            GarmentShippingInvoiceRepository repo = new GarmentShippingInvoiceRepository(dbContext, serviceProvider);
            GarmentShippingInvoiceDataUtil invoiceDataUtil = new GarmentShippingInvoiceDataUtil(repo, utilPL);
            GarmentShippingInvoiceModel data = invoiceDataUtil.GetModel();
            data.PackingListId = dataPL.Id;
            var dataInvoice = await repo.InsertAsync(data);

            GarmentShippingCreditAdviceRepository repoCreditAdvice = new GarmentShippingCreditAdviceRepository(dbContext, serviceProvider);
            GarmentCreditAdviceDataUtil CreditAdviceDataUtil = new GarmentCreditAdviceDataUtil(repoCreditAdvice);
            GarmentShippingCreditAdviceModel dataCreditAdvice = CreditAdviceDataUtil.GetModel();
            dataCreditAdvice.SetInvoiceId(data.Id, "test", "unitTest");
            var results = await repoCreditAdvice.InsertAsync(dataCreditAdvice);
            var result = repoCreditAdvice.ReadAll();

            Assert.NotEmpty(result);
        }

        [Fact]
        public async override Task Should_Success_Update()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentPackingListRepository repoPL = new GarmentPackingListRepository(dbContext, serviceProvider);
            GarmentPackingListDataUtil utilPL = new GarmentPackingListDataUtil(repoPL);
            GarmentPackingListModel dataPL = utilPL.GetModel();
            var dataPackingList = await repoPL.InsertAsync(dataPL);

            GarmentShippingInvoiceRepository repo = new GarmentShippingInvoiceRepository(dbContext, serviceProvider);
            GarmentShippingInvoiceDataUtil invoiceDataUtil = new GarmentShippingInvoiceDataUtil(repo, utilPL);
            GarmentShippingInvoiceModel data = invoiceDataUtil.GetModel();
            data.PackingListId = dataPL.Id;
            var dataInvoice = await repo.InsertAsync(data);

            GarmentShippingCreditAdviceRepository repoCreditAdvice = new GarmentShippingCreditAdviceRepository(dbContext, serviceProvider);

            GarmentShippingCreditAdviceRepository repoCreditAdvice2 = new GarmentShippingCreditAdviceRepository(dbContext, serviceProvider);
            GarmentCreditAdviceDataUtil CreditAdviceDataUtil = new GarmentCreditAdviceDataUtil(repoCreditAdvice);
            GarmentShippingCreditAdviceModel oldModel = CreditAdviceDataUtil.GetModel();
            oldModel.SetInvoiceId(data.Id, "test", "unitTest");
            await repoCreditAdvice.InsertAsync(oldModel);

            var model = repoCreditAdvice.ReadAll().FirstOrDefault();
            var modelToUpdate = await repoCreditAdvice.ReadByIdAsync(model.Id);

            modelToUpdate.SetValas(false, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetLCType("model.LCType", data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetInkaso(model.Inkaso, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetDisconto(model.Disconto, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetSRNo("model.SRNo", data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetNegoDate(model.NegoDate.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetPaymentDate(model.PaymentDate.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetReceiptNo("model.ReceiptNo", data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetCondition("model.Condition", data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetBankComission(model.BankComission, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetDiscrepancyFee(model.DiscrepancyFee, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetNettNego(model.NettNego, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetBTBCADate(model.BTBCADate.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetBTBAmount(model.BTBAmount, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetBTBRatio(model.BTBRatio, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetBTBRate(model.BTBRate, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetBTBTransfer(model.BTBTransfer, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetBTBMaterial(model.BTBMaterial, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetBillDays(model.BillDays, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetBillAmount(model.BillAmount, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetBillCA("model.BillCA", data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetCreditInterest(model.CreditInterest, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetBankCharges(model.BankCharges, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetOtherCharge(model.OtherCharge, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetDocumentPresente(model.DocumentPresente.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetCargoPolicyNo("model.CargoPolicyNo", data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetCargoPolicyDate(model.CargoPolicyDate.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetCargoPolicyValue(model.CargoPolicyValue, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetAccountsReceivablePolicyNo("model.AccountsReceivablePolicyNo", data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetAccountsReceivablePolicyDate(model.AccountsReceivablePolicyDate.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetAccountsReceivablePolicyValue(model.AccountsReceivablePolicyValue, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetDocumentSendDate(model.DocumentSendDate.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetRemark("model.Remark", data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetAmountPaid(model.AmountPaid, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetBalanceAmount(model.BalanceAmount, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetInvoiceId(model.InvoiceId, data.LastModifiedBy, data.LastModifiedAgent);

            var result = await repoCreditAdvice2.UpdateAsync(modelToUpdate.Id, modelToUpdate);

            Assert.NotEqual(0, result);
        }
    }
}
