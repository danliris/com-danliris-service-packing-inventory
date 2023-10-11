using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNoteCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingNoteCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.ShippingNoteCreditAdvice;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmrntShippingNoteCreditAdvice
{
    public class ShippingNoteCreditAdviceRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingNoteCreditAdviceRepository, GarmentShippingNoteCreditAdviceModel, GarmentShippingNoteCreditAdviceDataUtil>
    {
        private const string ENTITY = "GarmentCreditAdvice";

        public ShippingNoteCreditAdviceRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async override Task Should_Success_Insert()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentShippingNoteRepository repo = new GarmentShippingNoteRepository(dbContext, serviceProvider);
            GarmentShippingNoteDataUtil shippingnoteDataUtil = new GarmentShippingNoteDataUtil(repo);
            GarmentShippingNoteModel data = shippingnoteDataUtil.GetModel();
            var dataShippingNote = await repo.InsertAsync(data);

            GarmentShippingNoteCreditAdviceRepository repoCreditAdvice = new GarmentShippingNoteCreditAdviceRepository(dbContext, serviceProvider);
            GarmentShippingNoteCreditAdviceDataUtil CreditAdviceDataUtil = new GarmentShippingNoteCreditAdviceDataUtil(repoCreditAdvice);
            GarmentShippingNoteCreditAdviceModel dataCreditAdvice = CreditAdviceDataUtil.GetModel();
            dataCreditAdvice.SetShippingNoteId(data.Id, "test", "unitTest");

            var result = await repoCreditAdvice.InsertAsync(dataCreditAdvice);
            Assert.NotEqual(0, result);
        }

        [Fact]
        public async override Task Should_Success_Delete()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentShippingNoteRepository repo = new GarmentShippingNoteRepository(dbContext, serviceProvider);
            GarmentShippingNoteDataUtil shippingnoteDataUtil = new GarmentShippingNoteDataUtil(repo);
            GarmentShippingNoteModel data = shippingnoteDataUtil.GetModel();
            var dataShippingNote = await repo.InsertAsync(data);

            GarmentShippingNoteCreditAdviceRepository repoCreditAdvice = new GarmentShippingNoteCreditAdviceRepository(dbContext, serviceProvider);
            GarmentShippingNoteCreditAdviceDataUtil CreditAdviceDataUtil = new GarmentShippingNoteCreditAdviceDataUtil(repoCreditAdvice);
            GarmentShippingNoteCreditAdviceModel dataCreditAdvice = CreditAdviceDataUtil.GetModel();
            dataCreditAdvice.SetShippingNoteId(data.Id, "test", "unitTest");

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

            GarmentShippingNoteRepository repo = new GarmentShippingNoteRepository(dbContext, serviceProvider);
            GarmentShippingNoteDataUtil shippingnoteDataUtil = new GarmentShippingNoteDataUtil(repo);
            GarmentShippingNoteModel data = shippingnoteDataUtil.GetModel();
            var dataShippingNote = await repo.InsertAsync(data);

            GarmentShippingNoteCreditAdviceRepository repoCreditAdvice = new GarmentShippingNoteCreditAdviceRepository(dbContext, serviceProvider);
            GarmentShippingNoteCreditAdviceDataUtil CreditAdviceDataUtil = new GarmentShippingNoteCreditAdviceDataUtil(repoCreditAdvice);
            GarmentShippingNoteCreditAdviceModel dataCreditAdvice = CreditAdviceDataUtil.GetModel();
            dataCreditAdvice.SetShippingNoteId(data.Id, "test", "unitTest");

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

            GarmentShippingNoteRepository repo = new GarmentShippingNoteRepository(dbContext, serviceProvider);
            GarmentShippingNoteDataUtil shippingnoteDataUtil = new GarmentShippingNoteDataUtil(repo);
            GarmentShippingNoteModel data = shippingnoteDataUtil.GetModel();
            var dataShippingNote = await repo.InsertAsync(data);

            GarmentShippingNoteCreditAdviceRepository repoCreditAdvice = new GarmentShippingNoteCreditAdviceRepository(dbContext, serviceProvider);
            GarmentShippingNoteCreditAdviceDataUtil CreditAdviceDataUtil = new GarmentShippingNoteCreditAdviceDataUtil(repoCreditAdvice);
            GarmentShippingNoteCreditAdviceModel dataCreditAdvice = CreditAdviceDataUtil.GetModel();
            dataCreditAdvice.SetShippingNoteId(data.Id, "test", "unitTest");

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

            GarmentShippingNoteRepository repo = new GarmentShippingNoteRepository(dbContext, serviceProvider);
            GarmentShippingNoteDataUtil shippingnoteDataUtil = new GarmentShippingNoteDataUtil(repo);
            GarmentShippingNoteModel data = shippingnoteDataUtil.GetModel();
            var dataShippingNote = await repo.InsertAsync(data);

            GarmentShippingNoteCreditAdviceRepository repoCreditAdvice = new GarmentShippingNoteCreditAdviceRepository(dbContext, serviceProvider);
            GarmentShippingNoteCreditAdviceRepository repoCreditAdvice2 = new GarmentShippingNoteCreditAdviceRepository(dbContext, serviceProvider);

            GarmentShippingNoteCreditAdviceDataUtil CreditAdviceDataUtil = new GarmentShippingNoteCreditAdviceDataUtil(repoCreditAdvice);
            GarmentShippingNoteCreditAdviceModel oldModel = CreditAdviceDataUtil.GetModel();
            oldModel.SetShippingNoteId(data.Id, "test", "unitTest");
            await repoCreditAdvice.InsertAsync(oldModel);

            var model = repoCreditAdvice.ReadAll().FirstOrDefault();
            var modelToUpdate = await repoCreditAdvice.ReadByIdAsync(model.Id);


            modelToUpdate.SetPaymentDate(model.PaymentDate.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetReceiptNo(model.ReceiptNo, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetPaymentTerm("model.PaymentTerm", data.LastModifiedBy, data.LastModifiedAgent);

            modelToUpdate.SetAmount(model.Amount, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetPaidAmount(model.PaidAmount, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetBalanceAmount(model.BalanceAmount, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetBankComission(model.BankComission, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetCreditInterest(model.CreditInterest, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetBankCharges(model.BankCharges, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetInsuranceCharge(model.InsuranceCharge, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetNettNego(model.NettNego, data.LastModifiedBy, data.LastModifiedAgent);

            modelToUpdate.SetDocumentSendDate(model.DocumentSendDate.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetRemark("model.Remark", data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetShippingNoteId(model.ShippingNoteId, data.LastModifiedBy, data.LastModifiedAgent);

            var result = await repoCreditAdvice2.UpdateAsync(modelToUpdate.Id, modelToUpdate);

            Assert.NotEqual(0, result);
        }
    }
}
