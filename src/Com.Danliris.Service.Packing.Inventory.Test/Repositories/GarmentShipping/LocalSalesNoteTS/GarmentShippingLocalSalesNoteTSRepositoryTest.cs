using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentReceiptSubconPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentSubcon.ShippingLocalSalesNoteTS;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentReceiptSubconPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNoteTS;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentLocalSalesNoteTS;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentReceiptSubconPackingList;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentShippingLocalSalesNoteTS
{
    public class GarmentShippingLocalSalesNoteTSRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingLocalSalesNoteTSRepository, GarmentShippingLocalSalesNoteTSModel, GarmentShippingLocalSalesNoteTSDataUtil>
    {
        private const string ENTITY = "GarmentShippingLocalSalesNote";

        public GarmentShippingLocalSalesNoteTSRepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async override Task Should_Success_Insert()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentReceiptSubconPackingListRepository repoSC = new GarmentReceiptSubconPackingListRepository(dbContext, serviceProvider);
            GarmentReceiptSubconPackingListDataUtil utilSC = new GarmentReceiptSubconPackingListDataUtil(repoSC);
            GarmentReceiptSubconPackingListModel dataSC = utilSC.GetModel();
            var dataSalesContract = await repoSC.InsertAsync(dataSC);

            GarmentShippingLocalSalesNoteTSRepository repo = new GarmentShippingLocalSalesNoteTSRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteTSDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteTSDataUtil(repo);
            GarmentShippingLocalSalesNoteTSModel data = salesNoteDataUtil.GetModel();

            foreach(var item in data.Items)
            {
                item.PackingListId = dataSC.Items.Select(x => x.Id).FirstOrDefault();
            }

            var result = await repo.InsertAsync(data);
            Assert.NotEqual(0, result);

        }
        [Fact]
        public async override Task Should_Success_Delete()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentReceiptSubconPackingListRepository repoSC = new GarmentReceiptSubconPackingListRepository(dbContext, serviceProvider);
            GarmentReceiptSubconPackingListDataUtil utilSC = new GarmentReceiptSubconPackingListDataUtil(repoSC);
            GarmentReceiptSubconPackingListModel dataSC = utilSC.GetModel();
            var dataSalesContract = await repoSC.InsertAsync(dataSC);

            GarmentShippingLocalSalesNoteTSRepository repo = new GarmentShippingLocalSalesNoteTSRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteTSDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteTSDataUtil(repo);
            GarmentShippingLocalSalesNoteTSModel data = salesNoteDataUtil.GetModel();

            foreach (var item in data.Items)
            {
                item.PackingListId = dataSC.Items.Select(x => x.Id).FirstOrDefault();
            }

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

            GarmentReceiptSubconPackingListRepository repoSC = new GarmentReceiptSubconPackingListRepository(dbContext, serviceProvider);
            GarmentReceiptSubconPackingListDataUtil utilSC = new GarmentReceiptSubconPackingListDataUtil(repoSC);
            GarmentReceiptSubconPackingListModel dataSC = utilSC.GetModel();
            var dataSalesContract = await repoSC.InsertAsync(dataSC);

            GarmentShippingLocalSalesNoteTSRepository repo = new GarmentShippingLocalSalesNoteTSRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteTSDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteTSDataUtil(repo );
            GarmentShippingLocalSalesNoteTSModel data = salesNoteDataUtil.GetModel();

            foreach (var item in data.Items)
            {
                item.PackingListId = dataSC.Items.Select(x => x.Id).FirstOrDefault();
            }

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
            GarmentReceiptSubconPackingListRepository repoSC = new GarmentReceiptSubconPackingListRepository(dbContext, serviceProvider);
            GarmentReceiptSubconPackingListDataUtil utilSC = new GarmentReceiptSubconPackingListDataUtil(repoSC);
            GarmentReceiptSubconPackingListModel dataSC = utilSC.GetModel();
            var dataSalesContract = await repoSC.InsertAsync(dataSC);

            GarmentShippingLocalSalesNoteTSRepository repo = new GarmentShippingLocalSalesNoteTSRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteTSDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteTSDataUtil(repo );
            GarmentShippingLocalSalesNoteTSModel data = salesNoteDataUtil.GetModel();

            foreach (var item in data.Items)
            {
                item.PackingListId = dataSC.Items.Select(x => x.Id).FirstOrDefault();
            }

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
            GarmentReceiptSubconPackingListRepository repoSC = new GarmentReceiptSubconPackingListRepository(dbContext, serviceProvider);
            GarmentReceiptSubconPackingListDataUtil utilSC = new GarmentReceiptSubconPackingListDataUtil(repoSC);
            GarmentReceiptSubconPackingListModel dataSC = utilSC.GetModel();
            var dataSalesContract = await repoSC.InsertAsync(dataSC);

            GarmentShippingLocalSalesNoteTSRepository repo = new GarmentShippingLocalSalesNoteTSRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteTSDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteTSDataUtil(repo );
            GarmentShippingLocalSalesNoteTSModel oldModel = salesNoteDataUtil.GetModel();

            foreach (var item in oldModel.Items)
            {
                item.PackingListId = dataSC.Items.Select(x => x.Id).FirstOrDefault();
            }

            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);

            data.SetDate(data.Date.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            data.SetTempo(model.Tempo + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetUseVat(!model.UseVat, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetRemark(model.Remark + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetPaymentType(model.PaymentType + 1, data.LastModifiedBy, data.LastModifiedAgent);

            foreach (var item in data.Items)
            {
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
            GarmentReceiptSubconPackingListRepository repoSC = new GarmentReceiptSubconPackingListRepository(dbContext, serviceProvider);
            GarmentReceiptSubconPackingListDataUtil utilSC = new GarmentReceiptSubconPackingListDataUtil(repoSC);
            GarmentReceiptSubconPackingListModel dataSC = utilSC.GetModel();
            var dataSalesContract = await repoSC.InsertAsync(dataSC);

            GarmentShippingLocalSalesNoteTSRepository repo = new GarmentShippingLocalSalesNoteTSRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteTSDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteTSDataUtil(repo );
            GarmentShippingLocalSalesNoteTSModel oldModel = salesNoteDataUtil.GetModel();
            foreach (var item in oldModel.Items)
            {
                item.PackingListId = dataSC.Items.Select(x => x.Id).FirstOrDefault();
            }
            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);
            data.SetDate(data.Date.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            data.SetTempo(model.Tempo + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetUseVat(!model.UseVat, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetRemark(model.Remark + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetPaymentType(model.PaymentType + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetPaymentType(model.PaymentType + 1, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetRejectedReason(null, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetIsRejectedShipping(false, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetIsRejectedFinance(false, data.LastModifiedBy, data.LastModifiedAgent);

            foreach (var item in data.Items)
            {
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

        //[Fact]
        //public async Task Should_Success_ApproveShipping_Data()
        //{
        //    string testName = GetCurrentMethod() + "Update";
        //    var dbContext = DbContext(testName);

        //    var serviceProvider = GetServiceProviderMock(dbContext).Object;
        //    GarmentReceiptSubconPackingListRepository repoSC = new GarmentReceiptSubconPackingListRepository(dbContext, serviceProvider);
        //    GarmentReceiptSubconPackingListDataUtil utilSC = new GarmentReceiptSubconPackingListDataUtil(repoSC);
        //    GarmentReceiptSubconPackingListModel dataSC = utilSC.GetModel();
        //    var dataSalesContract = await repoSC.InsertAsync(dataSC);

        //    GarmentShippingLocalSalesNoteTSRepository repo = new GarmentShippingLocalSalesNoteTSRepository(dbContext, serviceProvider);
        //    GarmentShippingLocalSalesNoteTSDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteTSDataUtil(repo );
        //    GarmentShippingLocalSalesNoteTSModel oldModel = salesNoteDataUtil.GetModel();
        //    oldModel.LocalSalesContractId = dataSC.Id;
        //    await repo.InsertAsync(oldModel);

        //    var model = repo.ReadAll().FirstOrDefault();
        //    var data = await repo.ReadByIdAsync(model.Id);
        //    data.SetApproveShippingDate(data.Date.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
        //    data.SetApproveShippingBy("approve", data.LastModifiedBy, data.LastModifiedAgent);
        //    data.SetIsApproveShipping(true, data.LastModifiedBy, data.LastModifiedAgent);

        //    var result = await repo.ApproveShippingAsync(data.Id);

        //    Assert.NotEqual(0, result);
        //}

        //[Fact]
        //public async Task Should_Success_ApproveFinance_Data()
        //{
        //    string testName = GetCurrentMethod() + "Update";
        //    var dbContext = DbContext(testName);

        //    var serviceProvider = GetServiceProviderMock(dbContext).Object;
        //    GarmentReceiptSubconPackingListRepository repoSC = new GarmentReceiptSubconPackingListRepository(dbContext, serviceProvider);
        //    GarmentReceiptSubconPackingListDataUtil utilSC = new GarmentReceiptSubconPackingListDataUtil(repoSC);
        //    GarmentReceiptSubconPackingListModel dataSC = utilSC.GetModel();
        //    var dataSalesContract = await repoSC.InsertAsync(dataSC);

        //    GarmentShippingLocalSalesNoteTSRepository repo = new GarmentShippingLocalSalesNoteTSRepository(dbContext, serviceProvider);
        //    GarmentShippingLocalSalesNoteTSDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteTSDataUtil(repo );
        //    GarmentShippingLocalSalesNoteTSModel oldModel = salesNoteDataUtil.GetModel();
        //    oldModel.LocalSalesContractId = dataSC.Id;
        //    await repo.InsertAsync(oldModel);

        //    var model = repo.ReadAll().FirstOrDefault();
        //    var data = await repo.ReadByIdAsync(model.Id);
        //    data.SetApproveFinanceDate(data.Date.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
        //    data.SetApproveFinanceBy("approve", data.LastModifiedBy, data.LastModifiedAgent);
        //    data.SetIsApproveFinance(true, data.LastModifiedBy, data.LastModifiedAgent);

        //    var result = await repo.ApproveFinanceAsync(data.Id);

        //    Assert.NotEqual(0, result);
        //}

        //[Fact]
        //public async Task Should_Success_RejectFinance_Data()
        //{
        //    string testName = GetCurrentMethod() + "Update";
        //    var dbContext = DbContext(testName);

        //    var serviceProvider = GetServiceProviderMock(dbContext).Object;
        //    GarmentReceiptSubconPackingListRepository repoSC = new GarmentReceiptSubconPackingListRepository(dbContext, serviceProvider);
        //    GarmentReceiptSubconPackingListDataUtil utilSC = new GarmentReceiptSubconPackingListDataUtil(repoSC);
        //    GarmentReceiptSubconPackingListModel dataSC = utilSC.GetModel();
        //    var dataSalesContract = await repoSC.InsertAsync(dataSC);

        //    GarmentShippingLocalSalesNoteTSRepository repo = new GarmentShippingLocalSalesNoteTSRepository(dbContext, serviceProvider);
        //    GarmentShippingLocalSalesNoteTSDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteTSDataUtil(repo );
        //    GarmentShippingLocalSalesNoteTSModel oldModel = salesNoteDataUtil.GetModel();
        //    oldModel.LocalSalesContractId = dataSC.Id;
        //    await repo.InsertAsync(oldModel);

        //    var model = repo.ReadAll().FirstOrDefault();
        //    var data = await repo.ReadByIdAsync(model.Id);
        //    data.SetIsRejectedFinance(true, data.LastModifiedBy, data.LastModifiedAgent);
        //    data.SetRejectedReason("reject!", data.LastModifiedBy, data.LastModifiedAgent);
        //    data.SetIsApproveShipping(false, data.LastModifiedBy, data.LastModifiedAgent);

        //    var result = await repo.RejectFinanceAsync(data.Id, data);

        //    Assert.NotEqual(0, result);
        //}

        //[Fact]
        //public async Task Should_Success_RejectShipping_Data()
        //{
        //    string testName = GetCurrentMethod() + "Update";
        //    var dbContext = DbContext(testName);

        //    var serviceProvider = GetServiceProviderMock(dbContext).Object;
        //    GarmentReceiptSubconPackingListRepository repoSC = new GarmentReceiptSubconPackingListRepository(dbContext, serviceProvider);
        //    GarmentReceiptSubconPackingListDataUtil utilSC = new GarmentReceiptSubconPackingListDataUtil(repoSC);
        //    GarmentReceiptSubconPackingListModel dataSC = utilSC.GetModel();
        //    var dataSalesContract = await repoSC.InsertAsync(dataSC);

        //    GarmentShippingLocalSalesNoteTSRepository repo = new GarmentShippingLocalSalesNoteTSRepository(dbContext, serviceProvider);
        //    GarmentShippingLocalSalesNoteTSDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteTSDataUtil(repo );
        //    GarmentShippingLocalSalesNoteTSModel oldModel = salesNoteDataUtil.GetModel();
        //    oldModel.LocalSalesContractId = dataSC.Id;
        //    await repo.InsertAsync(oldModel);

        //    var model = repo.ReadAll().FirstOrDefault();
        //    var data = await repo.ReadByIdAsync(model.Id);

        //    data.SetIsRejectedShipping(true, data.LastModifiedBy, data.LastModifiedAgent);
        //    data.SetRejectedReason("reject!", data.LastModifiedBy, data.LastModifiedAgent);

        //    var result = await repo.RejectShippingAsync(data.Id, data);

        //    Assert.NotEqual(0, result);
        //}
    }
}
