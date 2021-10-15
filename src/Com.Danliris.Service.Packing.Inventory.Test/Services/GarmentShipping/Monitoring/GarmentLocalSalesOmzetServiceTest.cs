using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentLocalSalesOmzet;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.Monitoring
{
    public class GarmentLocalSalesOmzetServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingLocalSalesNoteRepository repository, IGarmentShippingLocalSalesNoteItemRepository itemrepository, IGarmentLocalCoverLetterRepository lclrepository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingLocalSalesNoteRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IGarmentShippingLocalSalesNoteItemRepository)))
                .Returns(itemrepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentLocalCoverLetterRepository)))
                .Returns(lclrepository);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentLocalSalesOmzetService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentLocalSalesOmzetService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success()
        {
            var items = new List<GarmentShippingLocalSalesNoteItemModel>
                {
                     new GarmentShippingLocalSalesNoteItemModel(1,1, "", "", 1, 1, "UOM1", 1, 1, 1, "")
                         {
                           LocalSalesNoteId = 1
                         },
                     new GarmentShippingLocalSalesNoteItemModel(1,1, "", "", 1, 2, "UOM2", 1, 1, 1, "")
                         {
                           LocalSalesNoteId = 1
                         },
                     new GarmentShippingLocalSalesNoteItemModel(1,1, "", "", 1, 1, "UOM1", 1, 1, 1, "")
                         {
                           LocalSalesNoteId = 1
                         },
                };

            var model = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "LBL", "LBL TRANSAKSI", 1, "A999", "", "", "", 1, "", "", true, "", false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", items)
            {
                Id = 1
            };

            var model2 = new GarmentShippingLocalCoverLetterModel(1, "", "", DateTimeOffset.Now, 1, "", "", "", "", "BC123", DateTimeOffset.Now, "", "", "", 1, "");

            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingLocalSalesNoteItemRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(items.AsQueryable());

            var repoMock2 = new Mock<IGarmentLocalCoverLetterRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalCoverLetterModel>() { model2 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object).Object);

            var result = service.GetReportData(DateTime.MinValue, DateTime.Now, 0);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GenerateExcel_Success()
        {
            var items = new List<GarmentShippingLocalSalesNoteItemModel>
                {
                     new GarmentShippingLocalSalesNoteItemModel(1,1, "", "", 1, 1, "UOM1", 1, 1, 1, "")
                         {
                           LocalSalesNoteId = 1
                         },
                     new GarmentShippingLocalSalesNoteItemModel(1,1, "", "", 1, 2, "UOM2", 1, 1, 1, "")
                         {
                           LocalSalesNoteId = 1
                         },
                     new GarmentShippingLocalSalesNoteItemModel(1,1, "", "", 1, 1, "UOM1", 1, 1, 1, "")
                         {
                           LocalSalesNoteId = 1
                         },
                };

            var model = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "LBL", "LBL TRANSAKSI", 1, "A999", "", "", "", 1, "", "", true, "", false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", items)
            {
                Id = 1
            };

            var model2 = new GarmentShippingLocalCoverLetterModel(1, "", "", DateTimeOffset.Now, 1, "", "", "", "", "BC123", DateTimeOffset.Now, "", "", "", 1, "");

            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingLocalSalesNoteItemRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(items.AsQueryable());

            var repoMock2 = new Mock<IGarmentLocalCoverLetterRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalCoverLetterModel>() { model2 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object).Object);

            var result = service.GenerateExcel(DateTime.MinValue, DateTime.Now, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>().AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingLocalSalesNoteItemRepository>();

            var repoMock2 = new Mock<IGarmentLocalCoverLetterRepository>();

            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalCoverLetterModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object).Object);

            var result = service.GenerateExcel(null, null, 0);

            Assert.NotNull(result);
        }
    }
}
