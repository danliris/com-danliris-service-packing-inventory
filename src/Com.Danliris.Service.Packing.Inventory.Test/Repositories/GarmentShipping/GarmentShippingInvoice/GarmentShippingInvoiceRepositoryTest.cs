using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentShippingInvoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentShippingInvoice
{
	public class GarmentShippingInvoiceRepositoryTest : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingInvoiceRepository, GarmentShippingInvoiceModel, GarmentShippingInvoiceDataUtil>
	{
		private const string ENTITY = "GarmentShippingInvoice";

		public GarmentShippingInvoiceRepositoryTest() : base(ENTITY)
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

			GarmentShippingInvoiceRepository repo = new GarmentShippingInvoiceRepository(dbContext, serviceProvider);
			GarmentShippingInvoiceDataUtil invoiceDataUtil = new GarmentShippingInvoiceDataUtil(repo, utilPL);
			GarmentShippingInvoiceModel data = invoiceDataUtil.GetModel();
			data.PackingListId = dataPL.Id;
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

			GarmentShippingInvoiceRepository repo = new GarmentShippingInvoiceRepository(dbContext, serviceProvider);
			GarmentShippingInvoiceDataUtil invoiceDataUtil = new GarmentShippingInvoiceDataUtil(repo, utilPL);
			GarmentShippingInvoiceModel data = invoiceDataUtil.GetModel();
			data.PackingListId = dataPL.Id;
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

			GarmentShippingInvoiceRepository repo = new GarmentShippingInvoiceRepository(dbContext, serviceProvider);
			GarmentShippingInvoiceDataUtil invoiceDataUtil = new GarmentShippingInvoiceDataUtil(repo, utilPL);
			GarmentShippingInvoiceModel data = invoiceDataUtil.GetModel();
			data.PackingListId = dataPL.Id;
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

			GarmentShippingInvoiceRepository repo = new GarmentShippingInvoiceRepository(dbContext, serviceProvider);

			GarmentShippingInvoiceRepository repo2 = new GarmentShippingInvoiceRepository(dbContext, serviceProvider);
			GarmentShippingInvoiceDataUtil invoiceDataUtil = new GarmentShippingInvoiceDataUtil(repo, utilPL);
			GarmentShippingInvoiceModel oldModel = invoiceDataUtil.GetModel();
			oldModel.PackingListId = dataPL.Id;
			await repo.InsertAsync(oldModel);

			var model = repo.ReadAll().FirstOrDefault();
			var data = await repo.ReadByIdAsync(model.Id);

			data.From = "aaaa";
			data.To = "bbb";
			data.Consignee = "dsdsds";
			data.ShippingPer = "model.ShippingPer";
			data.SailingDate = DateTimeOffset.Now.AddDays(3);
			data.ConfirmationOfOrderNo = "dada";
			data.ShippingStaffId = 4;
			data.ShippingStaff = " model.ShippingStaff";
			data.FabricTypeId = 2;
			data.FabricType = "model.FabricType";
			data.BankAccountId = 3;
			data.BankAccount = "model.BankAccount";
			data.PaymentDue = 33;
			data.PEBNo = "model.PEBNo";
			data.PEBDate = DateTimeOffset.Now.AddDays(3);
			data.NPENo = "model.NPENo";
			data.NPEDate = DateTimeOffset.Now.AddDays(3);
			data.BL = "model.BL";
			data.BLDate = DateTimeOffset.Now.AddDays(3);
			data.CO = "model.CO";
			data.CODate = DateTimeOffset.Now.AddDays(3);
			data.COTP = "model.COTP";
			data.COTPDate = DateTimeOffset.Now.AddDays(3);
			data.Description = "model.Description";
			data.Say = "model.Say";
			data.Memo = "model.Memo";
			data.AmountToBePaid = 500;
			data.TotalAmount = 2;
			foreach (var item in data.Items)
			{
				item.Price = 1039;
				item.ComodityDesc = "hahhahah";
				item.CMTPrice = 56000;
			}
			foreach (var item in data.GarmentShippingInvoiceAdjustment)
			{
				item.SetAdjustmentDescription("dsds",item.LastModifiedBy,item.LastModifiedAgent);
				item.SetAdjustmentValue( 10000 + item.AdjustmentValue,item.LastModifiedBy, item.LastModifiedAgent);

			}
			var result = await repo2.UpdateAsync(data.Id, data);

			Assert.NotEqual(0, result);

		}
	}
}
