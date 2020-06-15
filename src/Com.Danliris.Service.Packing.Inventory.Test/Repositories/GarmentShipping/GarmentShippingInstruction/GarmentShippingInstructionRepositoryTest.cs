using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentShippingInvoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Repositories.GarmentShipping.GarmentShippingInstruction
{
    public class GarmentShippingInstructionRepositoryTest
        : BaseRepositoryTest<PackingInventoryDbContext, GarmentShippingInstructionRepository, GarmentShippingInstructionModel, GarmentShippingInstructionDataUtil>
    {
        private const string ENTITY = "GarmentShippingInstruction";

        public GarmentShippingInstructionRepositoryTest() : base(ENTITY)
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
            var dataInvoice= await repo.InsertAsync(data);

            GarmentShippingInstructionRepository repoInstruction = new GarmentShippingInstructionRepository(dbContext, serviceProvider);
            GarmentShippingInstructionDataUtil InstructionDataUtil = new GarmentShippingInstructionDataUtil(repoInstruction, invoiceDataUtil);
            GarmentShippingInstructionModel dataInstruction = InstructionDataUtil.GetModel();
            dataInstruction.SetInvoiceId(data.Id, "test", "unitTest");
            var result = await repoInstruction.InsertAsync(dataInstruction);
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

            GarmentShippingInstructionRepository repoInstruction = new GarmentShippingInstructionRepository(dbContext, serviceProvider);
            GarmentShippingInstructionDataUtil InstructionDataUtil = new GarmentShippingInstructionDataUtil(repoInstruction, invoiceDataUtil);
            GarmentShippingInstructionModel dataInstruction = InstructionDataUtil.GetModel();
            dataInstruction.SetInvoiceId(data.Id, "test", "unitTest");
            var result = await repoInstruction.InsertAsync(dataInstruction);
            var resultdelete = await repoInstruction.DeleteAsync(dataInstruction.Id);
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

            GarmentShippingInstructionRepository repoInstruction = new GarmentShippingInstructionRepository(dbContext, serviceProvider);
            GarmentShippingInstructionDataUtil InstructionDataUtil = new GarmentShippingInstructionDataUtil(repoInstruction, invoiceDataUtil);
            GarmentShippingInstructionModel dataInstruction = InstructionDataUtil.GetModel();
            dataInstruction.SetInvoiceId(data.Id, "test", "unitTest");
            var results = await repoInstruction.InsertAsync(dataInstruction);
            var result = repoInstruction.ReadByIdAsync(dataInstruction.Id);

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

            GarmentShippingInstructionRepository repoInstruction = new GarmentShippingInstructionRepository(dbContext, serviceProvider);
            GarmentShippingInstructionDataUtil InstructionDataUtil = new GarmentShippingInstructionDataUtil(repoInstruction, invoiceDataUtil);
            GarmentShippingInstructionModel dataInstruction = InstructionDataUtil.GetModel();
            dataInstruction.SetInvoiceId(data.Id, "test", "unitTest");
            var results = await repoInstruction.InsertAsync(dataInstruction);
            var result = repoInstruction.ReadAll();

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

            GarmentShippingInstructionRepository repoInstruction = new GarmentShippingInstructionRepository(dbContext, serviceProvider);

            GarmentShippingInstructionRepository repoInstruction2 = new GarmentShippingInstructionRepository(dbContext, serviceProvider);
            GarmentShippingInstructionDataUtil InstructionDataUtil = new GarmentShippingInstructionDataUtil(repoInstruction, invoiceDataUtil);
            GarmentShippingInstructionModel oldModel = InstructionDataUtil.GetModel();
            oldModel.SetInvoiceId(data.Id, "test", "unitTest");
            await repoInstruction.InsertAsync(oldModel);

            var model = repoInstruction.ReadAll().FirstOrDefault();
            var modelToUpdate = await repoInstruction.ReadByIdAsync(model.Id);

            data.SetFrom("aaaa", data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetCarrier(model.Carrier, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetCartonNo(model.CartonNo, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetDate(model.Date.AddDays(1), data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetEMKLCode(model.EMKLCode, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetEMKLId(model.EMKLId, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetEMKLName(model.EMKLName, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetFeederVessel(model.FeederVessel, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetFlight(model.Flight, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetNotify(model.Notify, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetOceanVessel(model.OceanVessel, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetPlaceOfDelivery(model.PlaceOfDelivery, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetPortOfDischarge(model.PortOfDischarge, data.LastModifiedBy, data.LastModifiedAgent);
            modelToUpdate.SetShippedBy(model.ShippedBy, data.LastModifiedBy, data.LastModifiedAgent);
            
            var result = await repoInstruction2.UpdateAsync(modelToUpdate.Id, modelToUpdate);

            Assert.NotEqual(0, result);

        }
    }
}
