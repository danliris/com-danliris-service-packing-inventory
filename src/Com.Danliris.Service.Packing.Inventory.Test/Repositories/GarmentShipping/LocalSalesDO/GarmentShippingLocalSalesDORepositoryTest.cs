using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesDO;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalSalesDO;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.LocalSalesDO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.LocalSalesDO
{
    public class GarmentShippingLocalSalesDORepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingLocalSalesDORepository, GarmentShippingLocalSalesDOModel, GarmentShippingLocalSalesDODataUtil>
    {
        private const string ENTITY = "GarmentShippingLocalSalesDO";

        public GarmentShippingLocalSalesDORepositoryTest() : base(ENTITY)
        {
        }

        [Fact]
        public async override Task Should_Success_Insert()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentShippingLocalSalesNoteRepository repo = new GarmentShippingLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteDataUtil(repo);
            GarmentShippingLocalSalesNoteModel data = salesNoteDataUtil.GetModel();
            var dataLocalSalesNote = await repo.InsertAsync(data);

            GarmentShippingLocalSalesDORepository repoLocalSalesDO = new GarmentShippingLocalSalesDORepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesDODataUtil LocalSalesDODataUtil = new GarmentShippingLocalSalesDODataUtil(repoLocalSalesDO, salesNoteDataUtil);
            GarmentShippingLocalSalesDOModel dataLocalSalesDO = LocalSalesDODataUtil.GetModel();
            dataLocalSalesDO.SetLocalSalesNoteId(data.Id, "test", "unitTest");
            var result = await repoLocalSalesDO.InsertAsync(dataLocalSalesDO);
            Assert.NotEqual(0, result);

        }

        [Fact]
        public async override Task Should_Success_Delete()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentShippingLocalSalesNoteRepository repo = new GarmentShippingLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteDataUtil(repo);
            GarmentShippingLocalSalesNoteModel data = salesNoteDataUtil.GetModel();
            var dataLocalSalesNote = await repo.InsertAsync(data);

            GarmentShippingLocalSalesDORepository repoLocalSalesDO = new GarmentShippingLocalSalesDORepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesDODataUtil LocalSalesDODataUtil = new GarmentShippingLocalSalesDODataUtil(repoLocalSalesDO, salesNoteDataUtil);
            GarmentShippingLocalSalesDOModel dataLocalSalesDO = LocalSalesDODataUtil.GetModel();
            dataLocalSalesDO.SetLocalSalesNoteId(data.Id, "test", "unitTest");
            var result = await repoLocalSalesDO.InsertAsync(dataLocalSalesDO);
            var resultdelete = await repoLocalSalesDO.DeleteAsync(dataLocalSalesDO.Id);
            Assert.NotEqual(0, resultdelete);
        }

        [Fact]
        public async override Task Should_Success_ReadById()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            GarmentShippingLocalSalesNoteRepository repo = new GarmentShippingLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteDataUtil(repo);
            GarmentShippingLocalSalesNoteModel data = salesNoteDataUtil.GetModel();
            var dataLocalSalesNote = await repo.InsertAsync(data);

            GarmentShippingLocalSalesDORepository repoLocalSalesDO = new GarmentShippingLocalSalesDORepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesDODataUtil LocalSalesDODataUtil = new GarmentShippingLocalSalesDODataUtil(repoLocalSalesDO, salesNoteDataUtil);
            GarmentShippingLocalSalesDOModel dataLocalSalesDO = LocalSalesDODataUtil.GetModel();
            dataLocalSalesDO.SetLocalSalesNoteId(data.Id, "test", "unitTest");
            var result = await repoLocalSalesDO.InsertAsync(dataLocalSalesDO);
            var results = repoLocalSalesDO.ReadByIdAsync(dataLocalSalesDO.Id);

            Assert.NotNull(results);
        }
        [Fact]
        public async override Task Should_Success_ReadAll()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            GarmentShippingLocalSalesNoteRepository repo = new GarmentShippingLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteDataUtil(repo);
            GarmentShippingLocalSalesNoteModel data = salesNoteDataUtil.GetModel();
            var dataLocalSalesNote = await repo.InsertAsync(data);

            GarmentShippingLocalSalesDORepository repoLocalSalesDO = new GarmentShippingLocalSalesDORepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesDODataUtil LocalSalesDODataUtil = new GarmentShippingLocalSalesDODataUtil(repoLocalSalesDO, salesNoteDataUtil);
            GarmentShippingLocalSalesDOModel dataLocalSalesDO = LocalSalesDODataUtil.GetModel();
            dataLocalSalesDO.SetLocalSalesNoteId(data.Id, "test", "unitTest");
            var result = await repoLocalSalesDO.InsertAsync(dataLocalSalesDO);
            var results = repoLocalSalesDO.ReadAll();

            Assert.NotEmpty(results);
        }
        [Fact]
        public async override Task Should_Success_Update()
        {
            string testName = GetCurrentMethod();
            var dbContext = DbContext(testName);

            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            GarmentShippingLocalSalesNoteRepository repo = new GarmentShippingLocalSalesNoteRepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesNoteDataUtil salesNoteDataUtil = new GarmentShippingLocalSalesNoteDataUtil(repo);
            GarmentShippingLocalSalesNoteModel data = salesNoteDataUtil.GetModel();
            var dataLocalSalesNote = await repo.InsertAsync(data);

            GarmentShippingLocalSalesDORepository repoLocalSalesDO = new GarmentShippingLocalSalesDORepository(dbContext, serviceProvider);

            GarmentShippingLocalSalesDORepository repoLocalSalesDO2 = new GarmentShippingLocalSalesDORepository(dbContext, serviceProvider);
            GarmentShippingLocalSalesDODataUtil LocalSalesDODataUtil = new GarmentShippingLocalSalesDODataUtil(repoLocalSalesDO, salesNoteDataUtil);
            GarmentShippingLocalSalesDOModel oldModel = LocalSalesDODataUtil.GetModel();
            oldModel.SetLocalSalesNoteId(data.Id, "test", "unitTest");
            await repoLocalSalesDO.InsertAsync(oldModel);

            var model = repoLocalSalesDO.ReadAll().FirstOrDefault();
            var modelToUpdate = await repoLocalSalesDO.ReadByIdAsync(model.Id);

            modelToUpdate.SetDate(oldModel.Date.AddDays(1), oldModel.LastModifiedBy, oldModel.LastModifiedAgent);
            modelToUpdate.SetTo("Updated " + oldModel.To, oldModel.LastModifiedBy, oldModel.LastModifiedAgent);
            modelToUpdate.SetStorageDivision("Updated " + oldModel.StorageDivision, oldModel.LastModifiedBy, oldModel.LastModifiedAgent);
            modelToUpdate.SetRemark("Updated " + oldModel.Remark, oldModel.LastModifiedBy, oldModel.LastModifiedAgent);
            
            foreach (var item in oldModel.Items)
            { 
                item.SetDescription("Updated " + item.Description, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetGrossWeight(1 + item.GrossWeight, item.LastModifiedBy, item.LastModifiedAgent);
                item.SetNettWeight(1 + item.NettWeight, item.LastModifiedBy, item.LastModifiedAgent);

            }
            GarmentShippingLocalSalesDOItemModel newItem = new GarmentShippingLocalSalesDOItemModel(2, 2, 2, "a", "", "", 2, 2, "", 2, 2, "", 2, 2);
            
            oldModel.Items.Add(newItem);

            var result = await repoLocalSalesDO2.UpdateAsync(modelToUpdate.Id, modelToUpdate);

            Assert.NotEqual(0, result);

            oldModel.Items.Remove(oldModel.Items.First());

            var result2 = await repoLocalSalesDO2.UpdateAsync(oldModel.Id, oldModel);

            Assert.NotEqual(0, result2);


        }
    }
}
