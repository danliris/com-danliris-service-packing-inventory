using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingProduct;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingStockOpname;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services
{
    public class DyeingPrintingProductServiceTest
    {
        private const string ENTITY = "DyeingPrintingProduct";
        public DyeingPrintingProductService GetService(IServiceProvider serviceProvider)
        {
            return new DyeingPrintingProductService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaOutputProductionOrderRepository repo)
        {
            var spMock = new Mock<IServiceProvider>();

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(repo);

            var dbContext = DbContext(GetCurrentMethod());

            spMock.Setup(s => s.GetService(typeof(PackingInventoryDbContext)))
                .Returns(dbContext);

            return spMock;
        }

        protected string GetCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }

        protected PackingInventoryDbContext DbContext(string testName)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PackingInventoryDbContext>();

            var serviceProvider = new ServiceCollection()
               .AddEntityFrameworkInMemoryDatabase()
               .BuildServiceProvider();

            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .UseInternalServiceProvider(serviceProvider);

            var dbContext = Activator.CreateInstance(typeof(PackingInventoryDbContext), optionsBuilder.Options) as PackingInventoryDbContext;

            return dbContext;
        }

        private DyeingPrintingProductPackingViewModel ViewModel
        {
            get
            {
                return new DyeingPrintingProductPackingViewModel()
                {
                    Color = "c",
                    FabricPackingId = 1,
                    FabricSKUId = 1,
                    HasPrintingProductPacking = true,
                    HasPrintingProductSKU = true,
                    Id = 1,
                    Material = new Application.CommonViewModelObjectProperties.Material()
                    {
                        Id = 1,
                        Name = "s"
                    },
                    MaterialConstruction = new Application.CommonViewModelObjectProperties.MaterialConstruction()
                    {
                        Id = 1,
                        Name = "s"
                    },
                    MaterialWidth = "1",
                    MaterialOrigin = "a",
                    FinishWidth = "a",
                    Motif = "s",
                    ProductionOrder = new Application.CommonViewModelObjectProperties.ProductionOrder()
                    {
                        Id = 1,
                        No = "s"
                    },
                    ProductPackingCodes = new List<string>() { "s" },
                    ProductPackingId = 1,
                    ProductSKUCode = "s",
                    ProductSKUId = 1,
                    Quantity = 1,
                    ProductPackingLength = 1,
                    ProductPackingType = "s",
                    UomUnit = "s",
                    YarnMaterial = new Application.ToBeRefactored.CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = 1,
                        Name = "s"
                    },
                    Grade = "A",
                    DocumentNo = "T1" 
                };
            }
        }

        private DyeingPrintingAreaOutputProductionOrderModel Model
        {
            get
            {
                var model = new DyeingPrintingAreaOutputProductionOrderModel("PACKING", "GUDANG JADI", true, ViewModel.ProductionOrder.Id, ViewModel.ProductionOrder.No, ViewModel.ProductionOrder.Type, ViewModel.ProductionOrder.OrderQuantity, "", "", "", "",
                    "", ViewModel.Color, ViewModel.Motif, ViewModel.UomUnit, "", "zimmer", ViewModel.Grade, "", 0, ViewModel.Id, 0, ViewModel.Material.Id, ViewModel.Material.Name, ViewModel.MaterialConstruction.Id, ViewModel.MaterialConstruction.Name,
                    ViewModel.MaterialWidth, ViewModel.DocumentNo, ViewModel.Quantity, "", ViewModel.ProductPackingType, 0, "", "", 0, "", ViewModel.YarnMaterial.Id, ViewModel.YarnMaterial.Name, ViewModel.ProductSKUId, ViewModel.FabricSKUId, ViewModel.ProductSKUCode,
                    ViewModel.HasPrintingProductSKU, ViewModel.ProductPackingId, ViewModel.FabricPackingId, string.Join(',', ViewModel.ProductPackingCodes), ViewModel.HasPrintingProductPacking, ViewModel.ProductPackingLength, ViewModel.FinishWidth, DateTimeOffset.Now, DateTimeOffset.Now, ViewModel.MaterialOrigin);
                    


                model.DyeingPrintingAreaOutput = new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "PACKING", "s", "s", false, "GUDANG JADI", "S", "OUT", new List<DyeingPrintingAreaOutputProductionOrderModel>() { model });

                return model;
            }
        }

        [Fact]
        public void Should_Success_Read()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputProductionOrderModel>() { Model }.AsQueryable());

            repoMock.Setup(s => s.UpdateHasPrintingProductPacking(It.IsAny<int>(), It.IsAny<bool>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GetDataProductPacking(1, 25, "{}", "{}", null, It.IsAny<bool>());

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void Should_Success_Read2_StockOpname()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var stockOpnameMock = new Mock<IDyeingPrintingStockOpnameProductionOrderRepository>();


            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputProductionOrderModel>() { Model }.AsQueryable());

            repoMock.Setup(s => s.UpdateHasPrintingProductPacking(It.IsAny<int>(), It.IsAny<bool>()))
                 .ReturnsAsync(1);

            var stockModel = new DyeingPrintingStockOpnameProductionOrderModel(1, 1, "test", "color", "construction", "documentNO", "grade", 1, "constructionName", 1, "materialName","materialWidth", "motif", "packingInstruction", 1, 1, "packagingType", "packagingUnit", 1, "orderNo", "orderType", 1, 1, "processTypeName", 1, "yarnName", "remark", "status", "unit", "uomUnit", false, null);
            stockModel.SetPackingCode("1,2");
            stockOpnameMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingStockOpnameProductionOrderModel>() { stockModel }.AsQueryable());

            //stockOpnameMock.Setup(s => s.UpdateHasPrintingProductPacking(It.IsAny<int>(), It.IsAny<bool>()))
            //     .ReturnsAsync(1);

            var serviceProviderMock = GetServiceProvider(repoMock.Object);
            serviceProviderMock
                .Setup(sp => sp.GetService(typeof(IDyeingPrintingStockOpnameProductionOrderRepository)))
                .Returns(stockOpnameMock.Object);

            var service = GetService(serviceProviderMock.Object);

            var result = service.GetDataProductPacking(1, 25, "{}", "{}", null, true);

            Assert.NotNull(result.Data);
        }

        [Fact]
        public void Should_Success_Read2()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var stockOpnameMock = new Mock<IDyeingPrintingStockOpnameProductionOrderRepository>();


            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputProductionOrderModel>() { Model }.AsQueryable());

            repoMock.Setup(s => s.UpdateHasPrintingProductPacking(It.IsAny<int>(), It.IsAny<bool>()))
                 .ReturnsAsync(1);

            var stockModel = new DyeingPrintingStockOpnameProductionOrderModel();
            stockModel.SetPackingCode("1,2");
            stockOpnameMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingStockOpnameProductionOrderModel>() { stockModel }.AsQueryable());

            //stockOpnameMock.Setup(s => s.UpdateHasPrintingProductPacking(It.IsAny<int>(), It.IsAny<bool>()))
            //     .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GetDataProductPacking(1, 25, "{}", "{}", null, false);

            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task Should_Success_UpdatePrintingPacking()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            repoMock.Setup(s => s.UpdateHasPrintingProductPacking(It.IsAny<int>(), It.IsAny<bool>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.UpdatePrintingStatusProductPacking(ViewModel.Id, true);

            Assert.NotEqual(0, result);
        }
    }
}
