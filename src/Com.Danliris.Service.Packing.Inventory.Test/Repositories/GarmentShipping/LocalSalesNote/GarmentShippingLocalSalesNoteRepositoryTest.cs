using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.LocalSalesContract;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentShippingLocalSalesNote
{
    public class GarmentShippingLocalSalesNoteRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingLocalSalesNoteRepository, GarmentShippingLocalSalesNoteModel, GarmentShippingLocalSalesNoteDataUtil>
    {
        private const string ENTITY = "GarmentShippingLocalSalesNote";

        public GarmentShippingLocalSalesNoteRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async override Task Should_Success_Insert()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentShippingLocalSalesContractRepository repoSC = new GarmentShippingLocalSalesContractRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesContractDataUtil utilSC = new GarmentShippingLocalSalesContractDataUtil(repoSC);
            GarmentShippingLocalSalesContractModel dataSC = utilSC.GetModel();
            var dataSalesContract = await repoSC.InsertAsync(dataSC);

            GarmentShippingLocalSalesNoteRepository repo = new GarmentShippingLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteDataUtil(repo, utilSC);
            GarmentShippingLocalSalesNoteModel data = salesNoteDataUtil.GetModel();
            data.LocalSalesContractId = dataSC.Id;
            var result = await repo.InsertAsync(data);
            Assert.NotEqual(0, result);

        }
        [Fact]
        public async override Task Should_Success_Delete()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentShippingLocalSalesContractRepository repoSC = new GarmentShippingLocalSalesContractRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesContractDataUtil utilSC = new GarmentShippingLocalSalesContractDataUtil(repoSC);
            GarmentShippingLocalSalesContractModel dataSC = utilSC.GetModel();
            var dataSalesContract = await repoSC.InsertAsync(dataSC);

            GarmentShippingLocalSalesNoteRepository repo = new GarmentShippingLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteDataUtil(repo, utilSC);
            GarmentShippingLocalSalesNoteModel data = salesNoteDataUtil.GetModel();
            data.LocalSalesContractId = dataSC.Id;
            var result = await repo.InsertAsync(data);
            var resultdelete = await repo.DeleteAsync(data.Id);
            Assert.NotEqual(0, result);
        }

        [Fact]
        public async override Task Should_Success_ReadById()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentShippingLocalSalesContractRepository repoSC = new GarmentShippingLocalSalesContractRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesContractDataUtil utilSC = new GarmentShippingLocalSalesContractDataUtil(repoSC);
            GarmentShippingLocalSalesContractModel dataSC = utilSC.GetModel();
            var dataSalesContract = await repoSC.InsertAsync(dataSC);

            GarmentShippingLocalSalesNoteRepository repo = new GarmentShippingLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteDataUtil(repo, utilSC);
            GarmentShippingLocalSalesNoteModel data = salesNoteDataUtil.GetModel();
            data.LocalSalesContractId = dataSC.Id;
            var results = await repo.InsertAsync(data);
            var result = repo.ReadByIdAsync(data.Id);

            Assert.NotNull(result);
        }
        [Fact]
        public async override Task Should_Success_ReadAll()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            GarmentShippingLocalSalesContractRepository repoSC = new GarmentShippingLocalSalesContractRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesContractDataUtil utilSC = new GarmentShippingLocalSalesContractDataUtil(repoSC);
            GarmentShippingLocalSalesContractModel dataSC = utilSC.GetModel();
            var dataSalesContract = await repoSC.InsertAsync(dataSC);

            GarmentShippingLocalSalesNoteRepository repo = new GarmentShippingLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteDataUtil(repo, utilSC);
            GarmentShippingLocalSalesNoteModel data = salesNoteDataUtil.GetModel();
            data.LocalSalesContractId = dataSC.Id;
            var results = await repo.InsertAsync(data);
            var result = repo.ReadAll();

            Assert.NotEmpty(result);
        }
        [Fact]
        public async override Task Should_Success_Update()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            GarmentShippingLocalSalesContractRepository repoSC = new GarmentShippingLocalSalesContractRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesContractDataUtil utilSC = new GarmentShippingLocalSalesContractDataUtil(repoSC);
            GarmentShippingLocalSalesContractModel dataSC = utilSC.GetModel();
            var dataSalesContract = await repoSC.InsertAsync(dataSC);

            GarmentShippingLocalSalesNoteRepository repo = new GarmentShippingLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteDataUtil(repo, utilSC);
            GarmentShippingLocalSalesNoteModel oldModel = salesNoteDataUtil.GetModel();
            oldModel.LocalSalesContractId = dataSC.Id;
            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);

            data.SetDate(data.Date.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            data.SetTempo(model.Tempo + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetExpenditureNo(model.ExpenditureNo + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetDispositionNo(model.DispositionNo + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetUseVat(!model.UseVat, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetRemark(model.Remark + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetPaymentType(model.PaymentType + 1, data.LastModifiedBy, data.LastModifiedAgent);

            foreach (var item in data.Items)
            {
                item.SetProductId(item.ProductId + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetProductCode(item.ProductCode + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetProductName(item.ProductName + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetQuantity(item.Quantity + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetUomId(item.UomId + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetUomUnit(item.UomUnit + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetPrice(item.Price + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetPackageUomUnit(item.PackageUomUnit + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetPackageUomId(item.PackageUomId + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetPackageQuantity(item.PackageQuantity + 1, data.LastModifiedBy, data.LastModifiedAgent);
            }

            var result = await repo.UpdateAsync(data.Id, data);

            Assert.NotEqual(0, result);

        }

        [Fact]
        public async Task Should_Success_Update_Data()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            GarmentShippingLocalSalesContractRepository repoSC = new GarmentShippingLocalSalesContractRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesContractDataUtil utilSC = new GarmentShippingLocalSalesContractDataUtil(repoSC);
            GarmentShippingLocalSalesContractModel dataSC = utilSC.GetModel();
            var dataSalesContract = await repoSC.InsertAsync(dataSC);

            GarmentShippingLocalSalesNoteRepository repo = new GarmentShippingLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteDataUtil(repo, utilSC);
            GarmentShippingLocalSalesNoteModel oldModel = salesNoteDataUtil.GetModel();
            oldModel.LocalSalesContractId = dataSC.Id;
            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);
            data.SetDate(data.Date.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            data.SetTempo(model.Tempo + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetDispositionNo(model.DispositionNo + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetUseVat(!model.UseVat, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetRemark(model.Remark + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetPaymentType(model.PaymentType + 1, data.LastModifiedBy, data.LastModifiedAgent);

            foreach (var item in data.Items)
            {
                item.SetProductId(item.ProductId + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetProductCode(item.ProductCode + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetProductName(item.ProductName + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetQuantity(item.Quantity + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetUomId(item.UomId + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetUomUnit(item.UomUnit + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetPrice(item.Price + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetPackageUomUnit(item.PackageUomUnit + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetPackageUomId(item.PackageUomId + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetPackageQuantity(item.PackageQuantity + 1, data.LastModifiedBy, data.LastModifiedAgent);
            }

            var result = await repo.UpdateAsync(data.Id, data);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_ApproveShipping_Data()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            GarmentShippingLocalSalesContractRepository repoSC = new GarmentShippingLocalSalesContractRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesContractDataUtil utilSC = new GarmentShippingLocalSalesContractDataUtil(repoSC);
            GarmentShippingLocalSalesContractModel dataSC = utilSC.GetModel();
            var dataSalesContract = await repoSC.InsertAsync(dataSC);

            GarmentShippingLocalSalesNoteRepository repo = new GarmentShippingLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteDataUtil(repo, utilSC);
            GarmentShippingLocalSalesNoteModel oldModel = salesNoteDataUtil.GetModel();
            oldModel.LocalSalesContractId = dataSC.Id;
            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);
            data.SetApproveShippingDate(data.Date.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            data.SetApproveShippingBy("approve", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetIsApproveShipping(true, data.LastModifiedBy, data.LastModifiedAgent);

            var result = await repo.ApproveShippingAsync(data.Id);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_ApproveFinance_Data()
        {
            string testName = GetCurrentMethod() + "Update";
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            GarmentShippingLocalSalesContractRepository repoSC = new GarmentShippingLocalSalesContractRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesContractDataUtil utilSC = new GarmentShippingLocalSalesContractDataUtil(repoSC);
            GarmentShippingLocalSalesContractModel dataSC = utilSC.GetModel();
            var dataSalesContract = await repoSC.InsertAsync(dataSC);

            GarmentShippingLocalSalesNoteRepository repo = new GarmentShippingLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteDataUtil(repo, utilSC);
            GarmentShippingLocalSalesNoteModel oldModel = salesNoteDataUtil.GetModel();
            oldModel.LocalSalesContractId = dataSC.Id;
            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);
            data.SetApproveFinanceDate(data.Date.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            data.SetApproveFinanceBy("approve", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetIsApproveFinance(true, data.LastModifiedBy, data.LastModifiedAgent);

            var result = await repo.ApproveFinanceAsync(data.Id);

            Assert.NotEqual(0, result);
        }
    }
}
