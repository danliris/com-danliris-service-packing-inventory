using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentLocalSalesBook;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalReturnNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalPriceCuttingNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalReturnNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalPriceCuttingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote; 
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.Monitoring
{
    public class GarmentLocalSalesBookServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingLocalSalesNoteRepository repository, IGarmentShippingLocalSalesNoteItemRepository itemrepository, IGarmentShippingLocalReturnNoteRepository rtrrepository, IGarmentShippingLocalPriceCuttingNoteRepository cutrepository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingLocalSalesNoteRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IGarmentShippingLocalSalesNoteItemRepository)))
           .Returns(itemrepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentShippingLocalReturnNoteRepository)))
                .Returns(rtrrepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentShippingLocalPriceCuttingNoteRepository)))
                .Returns(cutrepository);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentLocalSalesBookService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentLocalSalesBookService(serviceProvider);
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

            var model = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "", "LBL TRANSAKSI", 1, "A999", "", "", "", 1, "", "", true, "", false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", items)
            {
                Id = 1
            };

            var model2 = new GarmentShippingLocalReturnNoteItemModel(1, new GarmentShippingLocalSalesNoteItemModel(1, 1, "", "", 1, 1, "", 1, 1, 1, ""), 1)
            {
                Id = 1
            };

            var model3 = new GarmentShippingLocalPriceCuttingNoteItemModel(1, "", 1, 1, false)
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingLocalSalesNoteItemRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(items.AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingLocalReturnNoteRepository>();
            repoMock2.Setup(s => s.ReadItemAll())
                .Returns(new List<GarmentShippingLocalReturnNoteItemModel>() { model2 }.AsQueryable());

            var repoMock3 = new Mock<IGarmentShippingLocalPriceCuttingNoteRepository>();
            repoMock3.Setup(s => s.ReadItemAll())
                .Returns(new List<GarmentShippingLocalPriceCuttingNoteItemModel>() { model3 }.AsQueryable());
            
            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object, repoMock3.Object).Object);

            var result = service.GetReportData(DateTime.MinValue, DateTime.Now, 0);

            Assert.NotEmpty(result.Item1);
        }

        [Fact]
        public void GenerateExcel_Success_LBL()
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

            var model2 = new GarmentShippingLocalReturnNoteItemModel(1, new GarmentShippingLocalSalesNoteItemModel(1, 1, "", "", 1, 1, "", 1, 1, 1, ""), 1)
            {
                Id = 1
            };

            var model3 = new GarmentShippingLocalPriceCuttingNoteItemModel(1, "", 1, 1, false)
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingLocalSalesNoteItemRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(items.AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingLocalReturnNoteRepository>();
            repoMock2.Setup(s => s.ReadItemAll())
                .Returns(new List<GarmentShippingLocalReturnNoteItemModel>() { model2 }.AsQueryable());

            var repoMock3 = new Mock<IGarmentShippingLocalPriceCuttingNoteRepository>();
            repoMock3.Setup(s => s.ReadItemAll())
                .Returns(new List<GarmentShippingLocalPriceCuttingNoteItemModel>() { model3 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object, repoMock3.Object).Object);

            var result = service.GenerateExcel(DateTime.MinValue, DateTime.Now, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Success_LBJ()
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

            var model = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "LBJ", "LBJ TRANSAKSI", 1, "A999", "", "", "", 1, "", "", true, "", false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", items)
            {
                Id = 1
            };

            var model2 = new GarmentShippingLocalReturnNoteItemModel(1, new GarmentShippingLocalSalesNoteItemModel(1, 1, "", "", 1, 1, "", 1, 1, 1, ""), 1)
            {
                Id = 1
            };

            var model3 = new GarmentShippingLocalPriceCuttingNoteItemModel(1, "", 1, 1, false)
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingLocalSalesNoteItemRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(items.AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingLocalReturnNoteRepository>();
            repoMock2.Setup(s => s.ReadItemAll())
                .Returns(new List<GarmentShippingLocalReturnNoteItemModel>() { model2 }.AsQueryable());

            var repoMock3 = new Mock<IGarmentShippingLocalPriceCuttingNoteRepository>();
            repoMock3.Setup(s => s.ReadItemAll())
                .Returns(new List<GarmentShippingLocalPriceCuttingNoteItemModel>() { model3 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object, repoMock3.Object).Object);

            var result = service.GenerateExcel(DateTime.MinValue, DateTime.Now, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Success_LBM()
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

            var model = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "LBM", "LBM TRANSAKSI", 1, "A999", "", "", "", 1, "", "", true, "", false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", items)
            {
                Id = 1
            };

            var model2 = new GarmentShippingLocalReturnNoteItemModel(1, new GarmentShippingLocalSalesNoteItemModel(1, 1, "", "", 1, 1, "", 1, 1, 1, ""), 1)
            {
                Id = 1
            };

            var model3 = new GarmentShippingLocalPriceCuttingNoteItemModel(1, "", 1, 1, false)
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingLocalSalesNoteItemRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(items.AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingLocalReturnNoteRepository>();
            repoMock2.Setup(s => s.ReadItemAll())
                .Returns(new List<GarmentShippingLocalReturnNoteItemModel>() { model2 }.AsQueryable());

            var repoMock3 = new Mock<IGarmentShippingLocalPriceCuttingNoteRepository>();
            repoMock3.Setup(s => s.ReadItemAll())
                .Returns(new List<GarmentShippingLocalPriceCuttingNoteItemModel>() { model3 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object, repoMock3.Object).Object);

            var result = service.GenerateExcel(DateTime.MinValue, DateTime.Now, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Success_LJS()
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

            var model = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "LJS", "LJS TRANSAKSI", 1, "A999", "", "", "", 1, "", "", true, "", false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", items)
            {
                Id = 1
            };

            var model2 = new GarmentShippingLocalReturnNoteItemModel(1, new GarmentShippingLocalSalesNoteItemModel(1, 1, "", "", 1, 1, "", 1, 1, 1, ""), 1)
            {
                Id = 1
            };

            var model3 = new GarmentShippingLocalPriceCuttingNoteItemModel(1, "", 1, 1, false)
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingLocalSalesNoteItemRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(items.AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingLocalReturnNoteRepository>();
            repoMock2.Setup(s => s.ReadItemAll())
                .Returns(new List<GarmentShippingLocalReturnNoteItemModel>() { model2 }.AsQueryable());

            var repoMock3 = new Mock<IGarmentShippingLocalPriceCuttingNoteRepository>();
            repoMock3.Setup(s => s.ReadItemAll())
                .Returns(new List<GarmentShippingLocalPriceCuttingNoteItemModel>() { model3 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object, repoMock3.Object).Object);

            var result = service.GenerateExcel(DateTime.MinValue, DateTime.Now, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Success_SBJ()
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

            var model = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "SBJ", "SBJ TRANSAKSI", 1, "A999", "", "", "", 1, "", "", true, "", false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", items)
            {
                Id = 1
            };

            var model2 = new GarmentShippingLocalReturnNoteItemModel(1, new GarmentShippingLocalSalesNoteItemModel(1, 1, "", "", 1, 1, "", 1, 1, 1, ""), 1)
            {
                Id = 1
            };

            var model3 = new GarmentShippingLocalPriceCuttingNoteItemModel(1, "", 1, 1, false)
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingLocalSalesNoteItemRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(items.AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingLocalReturnNoteRepository>();
            repoMock2.Setup(s => s.ReadItemAll())
                .Returns(new List<GarmentShippingLocalReturnNoteItemModel>() { model2 }.AsQueryable());

            var repoMock3 = new Mock<IGarmentShippingLocalPriceCuttingNoteRepository>();
            repoMock3.Setup(s => s.ReadItemAll())
                .Returns(new List<GarmentShippingLocalPriceCuttingNoteItemModel>() { model3 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object, repoMock3.Object).Object);

            var result = service.GenerateExcel(DateTime.MinValue, DateTime.Now, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Success_SMR()
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

            var model = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "SMR", "SMR TRANSAKSI", 1, "A999", "", "", "", 1, "", "", true, "", false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", items)
            {
                Id = 1
            };

            var model2 = new GarmentShippingLocalReturnNoteItemModel(1, new GarmentShippingLocalSalesNoteItemModel(1, 1, "", "", 1, 1, "", 1, 1, 1, ""), 1)
            {
                Id = 1
            };

            var model3 = new GarmentShippingLocalPriceCuttingNoteItemModel(1, "", 1, 1, false)
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingLocalSalesNoteItemRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(items.AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingLocalReturnNoteRepository>();
            repoMock2.Setup(s => s.ReadItemAll())
                .Returns(new List<GarmentShippingLocalReturnNoteItemModel>() { model2 }.AsQueryable());

            var repoMock3 = new Mock<IGarmentShippingLocalPriceCuttingNoteRepository>();
            repoMock3.Setup(s => s.ReadItemAll())
                .Returns(new List<GarmentShippingLocalPriceCuttingNoteItemModel>() { model3 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object, repoMock3.Object).Object);

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
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteItemModel>().AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingLocalReturnNoteRepository>();
            repoMock2.Setup(s => s.ReadItemAll())
                .Returns(new List<GarmentShippingLocalReturnNoteItemModel>().AsQueryable());

            var repoMock3 = new Mock<IGarmentShippingLocalPriceCuttingNoteRepository>();
            repoMock3.Setup(s => s.ReadItemAll())
                .Returns(new List<GarmentShippingLocalPriceCuttingNoteItemModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object, repoMock3.Object).Object);

            var result = service.GenerateExcel(null, null, 0);

            Assert.NotNull(result);
        }
    }
}
