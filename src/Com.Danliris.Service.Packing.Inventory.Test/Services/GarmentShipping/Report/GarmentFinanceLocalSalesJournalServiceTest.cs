using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Report.GarmentFinanceLocalSalesJournal;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.Report
{
    public class GarmentFinanceLocalSalesJournalServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingLocalSalesNoteRepository repository, IGarmentShippingLocalSalesNoteItemRepository repositoryItem)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingLocalSalesNoteRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IGarmentShippingLocalSalesNoteItemRepository)))
               .Returns(repositoryItem);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentFinanceLocalSalesJournalService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentFinanceLocalSalesJournalService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success_LJS()
        {
            var item = new GarmentShippingLocalSalesNoteItemModel(1, 1, "", "", 1, 1, "", 1, 1, 1, "") { LocalSalesNoteId = 1 };
            var items = new List<GarmentShippingLocalSalesNoteItemModel>() { item };
            var model = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "LJS", "", 1, "", "", "", "", 1, "", "", true, "", false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", items) { Id = 1 };
            

            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingLocalSalesNoteItemRepository>();

            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteItemModel>() { item }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);

            var result = service.GetReportData(model.Date.Month, model.Date.Year, 7);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GetReportData_Success_LBJ()
        {
            var item = new GarmentShippingLocalSalesNoteItemModel(1, 1, "", "", 1, 1, "", 1, 1, 1, "") { LocalSalesNoteId = 1 };
            var items = new List<GarmentShippingLocalSalesNoteItemModel>() { item };
            var model = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "LBJ", "", 1, "", "", "", "", 1, "", "", true, "", false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", items) { Id = 1 };


            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingLocalSalesNoteItemRepository>();

            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteItemModel>() { item }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);

            var result = service.GetReportData(model.Date.Month, model.Date.Year, 7);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GetReportData_Success_LBM()
        {
            var item = new GarmentShippingLocalSalesNoteItemModel(1, 1, "", "", 1, 1, "", 1, 1, 1, "") { LocalSalesNoteId = 1 };
            var items = new List<GarmentShippingLocalSalesNoteItemModel>() { item };
            var model = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "LBM", "", 1, "", "", "", "", 1, "", "", true, "", false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", items) { Id = 1 };


            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingLocalSalesNoteItemRepository>();

            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteItemModel>() { item }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);

            var result = service.GetReportData(model.Date.Month, model.Date.Year, 7);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GenerateExcel_Success()
        {
            var item = new GarmentShippingLocalSalesNoteItemModel(1, 1, "", "", 1, 1, "", 1, 1, 1, "") { LocalSalesNoteId = 1 };
            var items = new List<GarmentShippingLocalSalesNoteItemModel>() { item };
            var model = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "LJS", "", 1, "", "", "", "", 1, "", "", true, "", false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", items) { Id = 1 };


            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingLocalSalesNoteItemRepository>();

            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteItemModel>() { item }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);


            var result = service.GenerateExcel(model.Date.Month, model.Date.Year, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>().AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingLocalSalesNoteItemRepository>();

            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteItemModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);

            var result = service.GenerateExcel(1, 1, 7);

            Assert.NotNull(result);
        }
    }
}

