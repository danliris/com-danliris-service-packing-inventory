using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentShippingInvoice;
using System;
using System.Linq;
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
			GarmentShippingInvoiceModel oldModel = invoiceDataUtil.GetModels();
			oldModel.PackingListId = dataPL.Id;
			await repo.InsertAsync(oldModel);

			var model = repo.ReadAll().FirstOrDefault();
			var data = await repo.ReadByIdAsync(model.Id);

			data.SetFrom("aaaa", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetTo("bbb", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetConsignee ( "dsdsds", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetShippingPer( "model.ShippingPer", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetSailingDate( DateTimeOffset.Now.AddDays(3), data.LastModifiedBy, data.LastModifiedAgent);
			data.SetConfirmationOfOrderNo( "dada", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetShippingStaffId( 4, data.LastModifiedBy, data.LastModifiedAgent);
			data.SetShippingStaff( " model.ShippingStaff", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetFabricTypeId( 2, data.LastModifiedBy, data.LastModifiedAgent);
			data.SetFabricType( "model.FabricType", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetBankAccountId( 3, data.LastModifiedBy, data.LastModifiedAgent);
			data.SetBankAccount( "model.BankAccount", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetPaymentDue( 33, data.LastModifiedBy, data.LastModifiedAgent);
			data.SetPEBNo( "model.PEBNo", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetPEBDate( DateTimeOffset.Now.AddDays(3), data.LastModifiedBy, data.LastModifiedAgent);
			data.SetNPENo( "model.NPENo", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetNPEDate( DateTimeOffset.Now.AddDays(3), data.LastModifiedBy, data.LastModifiedAgent);
			data.SetBL( "model.BL", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetBLDate( DateTimeOffset.Now.AddDays(3), data.LastModifiedBy, data.LastModifiedAgent);
			data.SetCO( "model.CO", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetCODate( DateTimeOffset.Now.AddDays(3), data.LastModifiedBy, data.LastModifiedAgent);
			data.SetCOTP("model.COTP", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetCOTPDate( DateTimeOffset.Now.AddDays(3), data.LastModifiedBy, data.LastModifiedAgent);
			data.SetDescription("model.Description", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetRemark("model.Remark", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetCPrice( "cprice", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetMemo("model.Memo", data.LastModifiedBy, data.LastModifiedAgent);
			data.SetAmountToBePaid( 500, data.LastModifiedBy, data.LastModifiedAgent);
			data.SetTotalAmount(2, data.LastModifiedBy, data.LastModifiedAgent);
            data.SetConsigneeAddress("updated", data.LastModifiedBy, data.LastModifiedAgent);
            data.SetDeliverTo("updated", data.LastModifiedBy, data.LastModifiedAgent);
            data.Items.Add(new GarmentShippingInvoiceItemModel("ro", "scno", 1, "buyerbrandname", 1, 1, "comocode", "comoname", "comodesc", "comodesc", "comodesc", "comodesc", 1, "pcs", 10, 10, 100, "usd", 1, "unitcode", 3));
			foreach (var item in data.Items)
			{
				
				item.SetPrice(1039, item.LastModifiedBy, item.LastModifiedAgent);
				item.SetComodityDesc("hahhahah", item.LastModifiedBy, item.LastModifiedAgent);
                item.SetDesc2("hahhahah", item.LastModifiedBy, item.LastModifiedAgent);
                item.SetDesc3("hahhahah", item.LastModifiedBy, item.LastModifiedAgent);
                item.SetDesc4("hahhahah", item.LastModifiedBy, item.LastModifiedAgent);
                item.SetCMTPrice(56000, item.LastModifiedBy, item.LastModifiedAgent);
				item.SetUomId(2, item.LastModifiedBy, item.LastModifiedAgent);
				item.SetUomUnit("sss", item.LastModifiedBy, item.LastModifiedAgent);
			}
			var ajdData = data.GarmentShippingInvoiceAdjustment.FirstOrDefault();
			data.GarmentShippingInvoiceAdjustment.Add(new GarmentShippingInvoiceAdjustmentModel(data.Id,"ddd",1000, 1));
			ajdData.SetAdjustmentDescription("dsds", ajdData.LastModifiedBy, ajdData.LastModifiedAgent);
			ajdData.SetAdjustmentValue( 10000 + ajdData.AdjustmentValue, ajdData.LastModifiedBy, ajdData.LastModifiedAgent);
            ajdData.SetAdditionalChargesId(1 + ajdData.AdditionalChargesId, ajdData.LastModifiedBy, ajdData.LastModifiedAgent);

            var unitData = data.GarmentShippingInvoiceUnit.FirstOrDefault();
            data.GarmentShippingInvoiceUnit.Add(new GarmentShippingInvoiceUnitModel(1, "ddd",100, 1000));
            unitData.SetUnitCode("dsdsasda", unitData.LastModifiedBy, ajdData.LastModifiedAgent);
            unitData.SetUnitId(unitData.UnitId+1, unitData.LastModifiedBy, ajdData.LastModifiedAgent);
            unitData.SetQuantityPercentage(unitData.QuantityPercentage+1, unitData.LastModifiedBy, ajdData.LastModifiedAgent);
            unitData.SetAmountPercentage(unitData.AmountPercentage + 1, unitData.LastModifiedBy, ajdData.LastModifiedAgent);

            

            var result = await repo2.UpdateAsync(data.Id, data);

			Assert.NotEqual(0, result);

		}

        [Fact]
        public async Task Should_Success_Update_2()
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
            GarmentShippingInvoiceModel oldModel = invoiceDataUtil.GetModels();
            oldModel.PackingListId = dataPL.Id;
            await repo.InsertAsync(oldModel);

            var model = repo.ReadAll().FirstOrDefault();
            var data = await repo.ReadByIdAsync(model.Id);

            var Newdata = invoiceDataUtil.CopyModel(oldModel);

            var unitData = Newdata.GarmentShippingInvoiceUnit.FirstOrDefault();
            Newdata.GarmentShippingInvoiceUnit.Remove(unitData);

            var result = await repo2.UpdateAsync(data.Id, Newdata);

            Assert.NotEqual(0, result);

        }
    }
}
