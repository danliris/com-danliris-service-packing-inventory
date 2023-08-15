using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentLocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.LocalSalesContract;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentLocalCoverLetter
{
    public class LocalCoverLetterRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentLocalCoverLetterRepository, GarmentShippingLocalCoverLetterModel, GarmentLocalCoverLetterDataUtil>
    {
        private const string ENTITY = "GarmentLocalCoverLetter";

        public LocalCoverLetterRepositoryTest() : base(ENTITY)
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
            var dataLocalSalesNote = await repo.InsertAsync(data);

            GarmentLocalCoverLetterRepository repoCL = new GarmentLocalCoverLetterRepository(dbContext, serviceProvider);
            GarmentLocalCoverLetterDataUtil LocalCLDataUtil = new GarmentLocalCoverLetterDataUtil(repoCL);
            GarmentShippingLocalCoverLetterModel dataLocalCL = LocalCLDataUtil.GetModel();
            var result = await repoCL.InsertAsync(dataLocalCL);
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
            var dataLocalSalesNote = await repo.InsertAsync(data);

            GarmentLocalCoverLetterRepository repoCL = new GarmentLocalCoverLetterRepository(dbContext, serviceProvider);
            GarmentLocalCoverLetterDataUtil LocalCLDataUtil = new GarmentLocalCoverLetterDataUtil(repoCL);
            GarmentShippingLocalCoverLetterModel dataLocalCL = LocalCLDataUtil.GetModel();

            var result = await repoCL.InsertAsync(dataLocalCL);
            var resultdelete = await repoCL.DeleteAsync(dataLocalCL.Id);
            Assert.NotEqual(0, resultdelete);
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
            var dataLocalSalesNote = await repo.InsertAsync(data);

            GarmentLocalCoverLetterRepository repoCL = new GarmentLocalCoverLetterRepository(dbContext, serviceProvider);
            GarmentLocalCoverLetterDataUtil LocalCLDataUtil = new GarmentLocalCoverLetterDataUtil(repoCL);
            GarmentShippingLocalCoverLetterModel dataLocalCL = LocalCLDataUtil.GetModel();

            dataLocalCL.SetLocalSalesNoteId(data.Id, "test", "unitTest");
            var result = await repoCL.InsertAsync(dataLocalCL);
            var results = repoCL.ReadByIdAsync(dataLocalCL.Id);

            Assert.NotNull(results);
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
            var dataLocalSalesNote = await repo.InsertAsync(data);

            GarmentLocalCoverLetterRepository repoCL = new GarmentLocalCoverLetterRepository(dbContext, serviceProvider);
            GarmentLocalCoverLetterDataUtil LocalCLDataUtil = new GarmentLocalCoverLetterDataUtil(repoCL);
            GarmentShippingLocalCoverLetterModel dataLocalCL = LocalCLDataUtil.GetModel();

            dataLocalCL.SetLocalSalesNoteId(data.Id, "test", "unitTest");
            var result = await repoCL.InsertAsync(dataLocalCL);
            var results = repoCL.ReadAll();

            Assert.NotEmpty(results);
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
            GarmentShippingLocalSalesNoteModel data = salesNoteDataUtil.GetModel();
            data.LocalSalesContractId = dataSC.Id;
            var dataLocalSalesNote = await repo.InsertAsync(data);

            GarmentLocalCoverLetterRepository repoCL = new GarmentLocalCoverLetterRepository(dbContext, serviceProvider);

            GarmentLocalCoverLetterRepository repoCL2 = new GarmentLocalCoverLetterRepository(dbContext, serviceProvider);
            GarmentLocalCoverLetterDataUtil LocalCLDataUtil = new GarmentLocalCoverLetterDataUtil(repoCL);
            GarmentShippingLocalCoverLetterModel oldModel = LocalCLDataUtil.GetModel();

            oldModel.SetLocalSalesNoteId(data.Id, "test", "unitTest");

            await repoCL.InsertAsync(oldModel);

            var model = repoCL.ReadAll().FirstOrDefault();
            var modelToUpdate = await repoCL.ReadByIdAsync(model.Id);

            modelToUpdate.SetDate(oldModel.Date.AddDays(1), oldModel.LastModifiedBy, oldModel.LastModifiedAgent);
            modelToUpdate.SetBCNo("Updated " + oldModel.BCNo, oldModel.LastModifiedBy, oldModel.LastModifiedAgent);
            modelToUpdate.SetBCDate(oldModel.BCDate, oldModel.LastModifiedBy, oldModel.LastModifiedAgent);
            modelToUpdate.SetRemark("Updated " + oldModel.Remark, oldModel.LastModifiedBy, oldModel.LastModifiedAgent);
            modelToUpdate.SetTruck("Updated " + oldModel.Truck, oldModel.LastModifiedBy, oldModel.LastModifiedAgent);
            modelToUpdate.SetPlateNumber("Updated " + oldModel.PlateNumber, oldModel.LastModifiedBy, oldModel.LastModifiedAgent);
            modelToUpdate.SetDriver("Updated " + oldModel.Driver, oldModel.LastModifiedBy, oldModel.LastModifiedAgent);
            modelToUpdate.SetShippingStaffId(oldModel.ShippingStaffId, oldModel.LastModifiedBy, oldModel.LastModifiedAgent);
            modelToUpdate.SetShippingStaffName("Updated " + oldModel.ShippingStaffName, oldModel.LastModifiedBy, oldModel.LastModifiedAgent);

            var result = await repoCL2.UpdateAsync(modelToUpdate.Id, modelToUpdate);

            Assert.NotEqual(0, result);
        }
    }
}
