using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.DetailShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.DetailLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentDetailLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.LocalSalesContract;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentShippingDetailLocalSalesNote
{
    public class GarmentShippingDetailLocalSalesNoteRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingDetailLocalSalesNoteRepository, GarmentShippingDetailLocalSalesNoteModel, GarmentShippingDetailLocalSalesNoteDataUtil>
    {
        private const string ENTITY = "GarmentShippingDetailLocalSalesNote";

        public GarmentShippingDetailLocalSalesNoteRepositoryTest() : base(ENTITY)
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


            GarmentShippingLocalSalesNoteRepository repoLS = new GarmentShippingLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteDataUtil utilLS = new GarmentShippingLocalSalesNoteDataUtil(repoLS, utilSC);
            GarmentShippingLocalSalesNoteModel dataLS = utilLS.GetModel();
            var dataSalesNote = await repoLS.InsertAsync(dataLS);

            GarmentShippingDetailLocalSalesNoteRepository repo = new GarmentShippingDetailLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingDetailLocalSalesNoteDataUtil salesNoteDataUtil = new GarmentShippingDetailLocalSalesNoteDataUtil(repo, utilLS);
            GarmentShippingDetailLocalSalesNoteModel data = salesNoteDataUtil.GetModel();
            data.LocalSalesNoteId = dataLS.Id;
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


            GarmentShippingLocalSalesNoteRepository repoLS = new GarmentShippingLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteDataUtil utilLS = new GarmentShippingLocalSalesNoteDataUtil(repoLS, utilSC);
            GarmentShippingLocalSalesNoteModel dataLS = utilLS.GetModel();
            var dataSalesNote = await repoLS.InsertAsync(dataLS);

            GarmentShippingDetailLocalSalesNoteRepository repo = new GarmentShippingDetailLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingDetailLocalSalesNoteDataUtil salesNoteDataUtil = new GarmentShippingDetailLocalSalesNoteDataUtil(repo, utilLS);
            GarmentShippingDetailLocalSalesNoteModel data = salesNoteDataUtil.GetModel();
            data.LocalSalesNoteId = dataLS.Id;
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


            GarmentShippingLocalSalesNoteRepository repoLS = new GarmentShippingLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteDataUtil utilLS = new GarmentShippingLocalSalesNoteDataUtil(repoLS, utilSC);
            GarmentShippingLocalSalesNoteModel dataLS = utilLS.GetModel();
            var dataSalesNote = await repoLS.InsertAsync(dataLS);

            GarmentShippingDetailLocalSalesNoteRepository repo = new GarmentShippingDetailLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingDetailLocalSalesNoteDataUtil salesNoteDataUtil = new GarmentShippingDetailLocalSalesNoteDataUtil(repo, utilLS);
            GarmentShippingDetailLocalSalesNoteModel data = salesNoteDataUtil.GetModel();
            data.LocalSalesNoteId = dataLS.Id;
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

            GarmentShippingLocalSalesNoteRepository repoLS = new GarmentShippingLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteDataUtil utilLS = new GarmentShippingLocalSalesNoteDataUtil(repoLS, utilSC);
            GarmentShippingLocalSalesNoteModel dataLS = utilLS.GetModel();
            var dataSalesNote = await repoLS.InsertAsync(dataLS);

            GarmentShippingDetailLocalSalesNoteRepository repo = new GarmentShippingDetailLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingDetailLocalSalesNoteDataUtil salesNoteDataUtil = new GarmentShippingDetailLocalSalesNoteDataUtil(repo, utilLS);
            GarmentShippingDetailLocalSalesNoteModel data = salesNoteDataUtil.GetModel();
            data.LocalSalesNoteId = dataLS.Id;
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

            GarmentShippingLocalSalesNoteRepository repoLS = new GarmentShippingLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteDataUtil utilLS = new GarmentShippingLocalSalesNoteDataUtil(repoLS, utilSC);
            GarmentShippingLocalSalesNoteModel dataLS = utilLS.GetModel();
            var dataSalesNote = await repoLS.InsertAsync(dataLS);

            GarmentShippingDetailLocalSalesNoteRepository repo = new GarmentShippingDetailLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingDetailLocalSalesNoteDataUtil salesNoteDataUtil = new GarmentShippingDetailLocalSalesNoteDataUtil(repo, utilLS);
            GarmentShippingDetailLocalSalesNoteModel oldModel = salesNoteDataUtil.GetModel();
            oldModel.LocalSalesNoteId = dataLS.Id;
            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);

            data.SetDate(data.Date.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            data.SetAmount(model.Amount + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetBuyerCode(model.BuyerCode + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetBuyerName(model.BuyerName + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetTransactionTypeCode(model.TransactionTypeCode + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetTransactionTypeName(model.TransactionTypeName + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetSalesContractNo(model.SalesContractNo + 1, data.LastModifiedBy, data.LastModifiedAgent);
         
            foreach (var item in data.Items)
            {
                item.SetUnitId(item.UnitId + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetUnitCode(item.UnitCode + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetUnitName(item.UnitName + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetQuantity(item.Quantity + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetUomId(item.UomId + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetUomUnit(item.UomUnit + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetAmount(item.Amount + 1, data.LastModifiedBy, data.LastModifiedAgent);
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

            GarmentShippingLocalSalesNoteRepository repoLS = new GarmentShippingLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteDataUtil utilLS = new GarmentShippingLocalSalesNoteDataUtil(repoLS, utilSC);
            GarmentShippingLocalSalesNoteModel dataLS = utilLS.GetModel();
            var dataSalesNote = await repoLS.InsertAsync(dataLS);

            GarmentShippingDetailLocalSalesNoteRepository repo = new GarmentShippingDetailLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingDetailLocalSalesNoteDataUtil salesNoteDataUtil = new GarmentShippingDetailLocalSalesNoteDataUtil(repo, utilLS);
            GarmentShippingDetailLocalSalesNoteModel oldModel = salesNoteDataUtil.GetModel();
            oldModel.LocalSalesNoteId = dataLS.Id;
            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);

            data.SetDate(data.Date.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            data.SetAmount(model.Amount + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetBuyerCode(model.BuyerCode + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetBuyerName(model.BuyerName + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetTransactionTypeCode(model.TransactionTypeCode + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetTransactionTypeName(model.TransactionTypeName + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetSalesContractNo(model.SalesContractNo + 1, data.LastModifiedBy, data.LastModifiedAgent);

            foreach (var item in data.Items)
            {
                item.SetUnitId(item.UnitId + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetUnitCode(item.UnitCode + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetUnitName(item.UnitName + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetQuantity(item.Quantity + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetUomId(item.UomId + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetUomUnit(item.UomUnit + 1, data.LastModifiedBy, data.LastModifiedAgent);
                item.SetAmount(item.Amount + 1, data.LastModifiedBy, data.LastModifiedAgent);
            }

            var result = await repo.UpdateAsync(data.Id, data);

            Assert.NotEqual(0, result);
        }        
    }
}
