using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingCostStructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingCostStructure;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentShippingCostStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentShippingCostStructure
{
    public class GarmentShippingCostStructureRepositoryTest :  BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingCostStructureRepository, GarmentShippingCostStructureModel, GarmentShippingCostStructureDataUtil>
    {
        private const string ENTITY = "GarmentShippingCostStructure";

        public GarmentShippingCostStructureRepositoryTest() : base(ENTITY)
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

			GarmentShippingCostStructureRepository repo = new GarmentShippingCostStructureRepository(dbContext, serviceProvider);
			GarmentShippingCostStructureDataUtil costStructureDataUtil = new GarmentShippingCostStructureDataUtil(repo);
			GarmentShippingCostStructureModel data = costStructureDataUtil.GetModel();
			data.SetPackingListId(dataPL.Id, data.LastModifiedBy, data.LastModifiedAgent);
			var result = await repo.InsertAsync(data);
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

			GarmentShippingCostStructureRepository repo = new GarmentShippingCostStructureRepository(dbContext, serviceProvider);
			GarmentShippingCostStructureDataUtil costStructureDataUtil = new GarmentShippingCostStructureDataUtil(repo);
			GarmentShippingCostStructureModel data = costStructureDataUtil.GetModel();
			data.SetPackingListId(dataPL.Id, data.LastModifiedBy, data.LastModifiedAgent);
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

			GarmentPackingListRepository repoPL = new GarmentPackingListRepository(dbContext, serviceProvider);
			GarmentPackingListDataUtil utilPL = new GarmentPackingListDataUtil(repoPL);
			GarmentPackingListModel dataPL = utilPL.GetModel();
			var dataPackingList = await repoPL.InsertAsync(dataPL);

			GarmentShippingCostStructureRepository repo = new GarmentShippingCostStructureRepository(dbContext, serviceProvider);
			GarmentShippingCostStructureDataUtil costStructureDataUtil = new GarmentShippingCostStructureDataUtil(repo);
			GarmentShippingCostStructureModel data = costStructureDataUtil.GetModel();
			data.SetPackingListId(dataPL.Id, data.LastModifiedBy, data.LastModifiedAgent);

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

			GarmentPackingListRepository repoPL = new GarmentPackingListRepository(dbContext, serviceProvider);
			GarmentPackingListDataUtil utilPL = new GarmentPackingListDataUtil(repoPL);
			GarmentPackingListModel dataPL = utilPL.GetModel();
			var dataPackingList = await repoPL.InsertAsync(dataPL);

			GarmentShippingCostStructureRepository repo = new GarmentShippingCostStructureRepository(dbContext, serviceProvider);
			GarmentShippingCostStructureDataUtil costStructureDataUtil = new GarmentShippingCostStructureDataUtil(repo);
			GarmentShippingCostStructureModel data = costStructureDataUtil.GetModel();
			data.SetPackingListId(dataPL.Id, data.LastModifiedBy, data.LastModifiedAgent);
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

			GarmentPackingListRepository repoPL = new GarmentPackingListRepository(dbContext, serviceProvider);
			GarmentPackingListDataUtil utilPL = new GarmentPackingListDataUtil(repoPL);
			GarmentPackingListModel dataPL = utilPL.GetModel();
			var dataPackingList = await repoPL.InsertAsync(dataPL);

			GarmentShippingCostStructureRepository repo = new GarmentShippingCostStructureRepository(dbContext, serviceProvider);

			GarmentShippingCostStructureRepository repo2 = new GarmentShippingCostStructureRepository(dbContext, serviceProvider);
			GarmentShippingCostStructureDataUtil invoiceDataUtil = new GarmentShippingCostStructureDataUtil(repo);
			GarmentShippingCostStructureModel oldModel = invoiceDataUtil.GetModels();
			await repo.InsertAsync(oldModel);

			var model = repo.ReadAll().FirstOrDefault();
			var data = await repo.ReadByIdAsync(model.Id);

			data.SetAmount(1, data.LastModifiedBy, data.LastModifiedAgent);
			data.SetComodityCode("data.comodityCode", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetComodityName("data.comodityName", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetComodityId(111, data.LastModifiedBy, data.LastModifiedAgent);
			data.SetDestination("data.destination", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetFabricType("data.hsCode", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetHsCode("asdlkjasd", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetFabricType("model.FabricType", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetPackingListId(dataPL.Id, data.LastModifiedBy, data.LastModifiedAgent);

			var result = await repo2.UpdateAsync(data.Id, data);

			Assert.NotEqual(0, result);

		}
	}
}
