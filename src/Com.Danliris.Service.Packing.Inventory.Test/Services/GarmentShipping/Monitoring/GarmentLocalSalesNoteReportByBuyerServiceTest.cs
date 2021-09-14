using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentLocalSalesReportByBuyer;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.Monitoring
{
    public class GarmentLocalSalesReportByBuyerServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingLocalSalesNoteRepository repository, IGarmentShippingLocalSalesNoteItemRepository itemRepository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingLocalSalesNoteRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IGarmentShippingLocalSalesNoteItemRepository)))
                .Returns(itemRepository);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentLocalSalesReportByBuyerService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentLocalSalesReportByBuyerService(serviceProvider);
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
            
            var model = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "LBL", "LBL TRANSAKSI", 1, "A999", "", "", "", 1, "", "", true, "", false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, items)
            {
                Id = 1
            };


            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingLocalSalesNoteItemRepository>();

            repoMock1.Setup(s => s.ReadAll())
                .Returns(items.AsQueryable());
         
            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);

            var result = service.GetReportData(model.BuyerCode, model.TransactionTypeCode, DateTime.MinValue, DateTime.Now, 0);

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

            var model = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "LBL", "LBL TRANSAKSI", 1, "A999", "", "", "", 1, "", "", true, "", false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, items)
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingLocalSalesNoteItemRepository>();

            repoMock1.Setup(s => s.ReadAll())
                .Returns(items.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);

            var result = service.GenerateExcel(model.BuyerCode, null, DateTime.MinValue, DateTime.Now, 7);

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

            var result = service.GenerateExcel(null, null, null, null, 0);

            Assert.NotNull(result);
        }
    }
}
