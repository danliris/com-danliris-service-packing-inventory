using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.DyeingPrintingStockOpname.Warehouse
{
    public class StockOpnameMutationWarehouseServiceTest
    {
        public StockOpnameMutationService GetService(IServiceProvider serviceProvider)
        {
            return new StockOpnameMutationService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingStockOpnameRepository stockOpnameRepo, IDyeingPrintingStockOpnameProductionOrderRepository stockOpnameProductionOrderRepo, IDyeingPrintingStockOpnameMutationRepository stockOpnameMutationRepo, IDyeingPrintingStockOpnameMutationItemRepository stockOpnameMutationItemRepo)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock
                .Setup(s => s.GetService(typeof(IDyeingPrintingStockOpnameRepository)))
                .Returns(stockOpnameRepo);

            spMock
                .Setup(s => s.GetService(typeof(IDyeingPrintingStockOpnameProductionOrderRepository)))
                .Returns(stockOpnameProductionOrderRepo);

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingStockOpnameMutationRepository)))
                .Returns(stockOpnameMutationRepo);

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingStockOpnameMutationItemRepository)))
                .Returns(stockOpnameMutationItemRepo);

            return spMock;


        }

        private StockOpnameMutationViewModel viewModel
        {
            get
            {
                return new StockOpnameMutationViewModel()
                {
                    Id = 1,
                    Area = "GUDANG JADI",
                    BonNo = "TR.GJ.20.001",
                    Date = DateTimeOffset.Now,
                    Type = DyeingPrintingArea.STOCK_OPNAME,
                    DyeingPrintingStockOpnameMutationItems = new List<StockOpnameMutationItemViewModel>()
                    {
                        new StockOpnameMutationItemViewModel()
                        {
                            Id = 1,
                            Balance = 1,
                            Color = "color",
                            Construction = "construction",
                            Grade = "grade",
                            Motif = "motif",
                            PackagingQty = 1,
                            PackagingLength = 1,
                            PackagingType = "Packagingtype",
                            PackagingUnit = "PackagingUnit",
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "SLD",
                                Id = 62,
                                Type = "SOLID",
                                No = "F/2020/000"
                            },
                            Remark = "Remark",
                            ProcessTypeId = 1,
                            ProcessTypeName = "ProcessTypeName",
                            Unit = "Unit",
                            UomUnit = "UomUnit",
                            Track = new Track(){ 
                                Id = 1,
                                Name = "Name",
                                Type = "Type",
                                Box = "Box"
                                
                            },
                            ProductPackingId = 1,
                            FabricPackingId = 1,
                            ProductSKUCode = "ProductSKUCode",
                            ProductSKUId = 1,
                            FabricSKUId =1,
                            ProductPackingCode = "ProductPackingCode",
                            TypeOut = "STOCK OPNAME",
                            SendQuantity = 1
                                                    }
                    }
                };
            }
        }

        private DyeingPrintingStockOpnameMutationModel modelMutation
        {
            get
            {
                var stockOpnameMutationItem = new DyeingPrintingStockOpnameMutationItemModel(
                    1,
                    "color",
                    "construction",
                    "grade",
                    "motif",
                    1,
                    1,
                    "packagingType",
                    "packagingUnit",
                    1,
                    "productionOrderNo",
                    "productionOrderType",
                    1,
                    1,
                    "processType",
                    "remark",
                    "unit",
                    "uomUnit",
                    1,
                    "trackType",
                    "trackName",
                    "trackBox",
                    1,
                    1,
                    "productSKUCode",
                    1,
                    1,
                    "productPackingCode",
                    "typeOut"

                    );

                var stockOpnameMutationItems = new List<DyeingPrintingStockOpnameMutationItemModel>();
                stockOpnameMutationItems.Add(stockOpnameMutationItem);

                return new DyeingPrintingStockOpnameMutationModel(DyeingPrintingArea.GUDANGJADI, "BON_NO", DateTimeOffset.Now, DyeingPrintingArea.SO, stockOpnameMutationItems);

            }
        }

        [Fact]
        public async Task Should_Success_SO_Create()
        {
            //Arrange
            var stockOpnameRepo = new Mock<IDyeingPrintingStockOpnameRepository>();
            var stockOpnameProductionOrderRepo = new Mock<IDyeingPrintingStockOpnameProductionOrderRepository>();
            var stockOpnameMutationRepo = new Mock<IDyeingPrintingStockOpnameMutationRepository>();
            var stockOpnameMutationItemRepo = new Mock<IDyeingPrintingStockOpnameMutationItemRepository>();
            

            stockOpnameMutationRepo
                .Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingStockOpnameMutationModel>()))
                .ReturnsAsync(1);

            stockOpnameMutationRepo
                .Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingStockOpnameMutationModel>() { }.AsQueryable());

            stockOpnameMutationRepo
                .Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingStockOpnameMutationModel>() { modelMutation }.AsQueryable());

            var serviceProviderMock = GetServiceProvider(stockOpnameRepo.Object, stockOpnameProductionOrderRepo.Object, stockOpnameMutationRepo.Object, stockOpnameMutationItemRepo.Object);

            serviceProviderMock
                .Setup(sp => sp.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider() { TimezoneOffset = 7, Token = "Token", Username = "Username" });

            

            var service = GetService(serviceProviderMock.Object);

            //Act
            var result = await service.Create(viewModel);

            //Assert
            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_SO_Create_existing()
        {
            //Arrange
            var stockOpnameRepo = new Mock<IDyeingPrintingStockOpnameRepository>();
            var stockOpnameProductionOrderRepo = new Mock<IDyeingPrintingStockOpnameProductionOrderRepository>();
            var stockOpnameMutationRepo = new Mock<IDyeingPrintingStockOpnameMutationRepository>();
            var stockOpnameMutationItemRepo = new Mock<IDyeingPrintingStockOpnameMutationItemRepository>();


            stockOpnameMutationRepo
                .Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingStockOpnameMutationModel>()))
                .ReturnsAsync(1);

            stockOpnameMutationItemRepo
                .Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingStockOpnameMutationItemModel>()))
                .ReturnsAsync(1);

            stockOpnameMutationRepo
                .Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingStockOpnameMutationModel>() { modelMutation }.AsQueryable());

            //stockOpnameMutationRepo
            //    .Setup(s => s.ReadAllIgnoreQueryFilter())
            //    .Returns(new List<DyeingPrintingStockOpnameMutationModel>() { modelMutation }.AsQueryable());

            var serviceProviderMock = GetServiceProvider(stockOpnameRepo.Object, stockOpnameProductionOrderRepo.Object, stockOpnameMutationRepo.Object, stockOpnameMutationItemRepo.Object);

            serviceProviderMock
                .Setup(sp => sp.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider() { TimezoneOffset = 7, Token = "Token", Username = "Username" });



            var service = GetService(serviceProviderMock.Object);

            //Act
            var result = await service.Create(viewModel);

            //Assert
            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Adj_Create()
        {
            //Arrange
            var stockOpnameRepo = new Mock<IDyeingPrintingStockOpnameRepository>();
            var stockOpnameProductionOrderRepo = new Mock<IDyeingPrintingStockOpnameProductionOrderRepository>();
            var stockOpnameMutationRepo = new Mock<IDyeingPrintingStockOpnameMutationRepository>();
            var stockOpnameMutationItemRepo = new Mock<IDyeingPrintingStockOpnameMutationItemRepository>();


            stockOpnameMutationRepo
                .Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingStockOpnameMutationModel>()))
                .ReturnsAsync(1);

            stockOpnameMutationRepo
                .Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingStockOpnameMutationModel>() { }.AsQueryable());

            stockOpnameMutationRepo
                .Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingStockOpnameMutationModel>() { modelMutation }.AsQueryable());

            var serviceProviderMock = GetServiceProvider(stockOpnameRepo.Object, stockOpnameProductionOrderRepo.Object, stockOpnameMutationRepo.Object, stockOpnameMutationItemRepo.Object);

            serviceProviderMock
                .Setup(sp => sp.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider() { TimezoneOffset = 7, Token = "Token", Username = "Username" });



            var service = GetService(serviceProviderMock.Object);
            var vm = viewModel;
            vm.Type = "ADJ OUT";
            //Act
            var result = await service.Create(vm);

            //Assert
            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_Read()
        {
            //Arrange
            var stockOpnameRepo = new Mock<IDyeingPrintingStockOpnameRepository>();
            var stockOpnameProductionOrderRepo = new Mock<IDyeingPrintingStockOpnameProductionOrderRepository>();
            var stockOpnameMutationRepo = new Mock<IDyeingPrintingStockOpnameMutationRepository>();
            var stockOpnameMutationItemRepo = new Mock<IDyeingPrintingStockOpnameMutationItemRepository>();

            stockOpnameMutationRepo
                 .Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingStockOpnameMutationModel>() { modelMutation }.AsQueryable());

            stockOpnameMutationItemRepo
                 .Setup(s => s.ReadAll())
                 .Returns(modelMutation.DyeingPrintingStockOpnameMutationItems.AsQueryable());

            var service = GetService(GetServiceProvider(stockOpnameRepo.Object, stockOpnameProductionOrderRepo.Object, stockOpnameMutationRepo.Object, stockOpnameMutationItemRepo.Object).Object);

            //Act
            var result = service.Read(1, 25, "", "{}", "");

            //Assert
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task Should_Success_ReadById()
        {
            //Arrange
            var stockOpnameRepo = new Mock<IDyeingPrintingStockOpnameRepository>();
            var stockOpnameProductionOrderRepo = new Mock<IDyeingPrintingStockOpnameProductionOrderRepository>();
            var stockOpnameMutationRepo = new Mock<IDyeingPrintingStockOpnameMutationRepository>();
            var stockOpnameMutationItemRepo = new Mock<IDyeingPrintingStockOpnameMutationItemRepository>();

            //var mm = modelMutation;
            //mm.Id = 1;
            stockOpnameMutationRepo
                .Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(modelMutation);

            //stockOpnameMutationItemRepo
            //     .Setup(s => s.ReadAll())
            //     .Returns(modelMutation.DyeingPrintingStockOpnameMutationItems.AsQueryable());

            var service = GetService(GetServiceProvider(stockOpnameRepo.Object, stockOpnameProductionOrderRepo.Object, stockOpnameMutationRepo.Object, stockOpnameMutationItemRepo.Object).Object);

            //Act
            var result = await service.ReadById(1);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GetMonitoringSO_Mutation()
        {
            //Arrange
            var stockOpnameRepo = new Mock<IDyeingPrintingStockOpnameRepository>();
            var stockOpnameProductionOrderRepo = new Mock<IDyeingPrintingStockOpnameProductionOrderRepository>();
            var stockOpnameMutationRepo = new Mock<IDyeingPrintingStockOpnameMutationRepository>();
            var stockOpnameMutationItemRepo = new Mock<IDyeingPrintingStockOpnameMutationItemRepository>();

            stockOpnameMutationRepo
                 .Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingStockOpnameMutationModel>() { modelMutation }.AsQueryable());

            stockOpnameMutationItemRepo
                 .Setup(s => s.ReadAll())
                 .Returns(modelMutation.DyeingPrintingStockOpnameMutationItems.AsQueryable());

            var service = GetService(GetServiceProvider(stockOpnameRepo.Object, stockOpnameProductionOrderRepo.Object, stockOpnameMutationRepo.Object, stockOpnameMutationItemRepo.Object).Object);

            //Act
            var result = service.GetMonitoringSO(DateTimeOffset.MinValue, DateTimeOffset.MaxValue, 1, 1,7);

            //Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Should_Success_GetExcelMonitoring()
        {
            var stockOpnameRepo = new Mock<IDyeingPrintingStockOpnameRepository>();
            var stockOpnameProductionOrderRepo = new Mock<IDyeingPrintingStockOpnameProductionOrderRepository>();
            var stockOpnameMutationRepo = new Mock<IDyeingPrintingStockOpnameMutationRepository>();
            var stockOpnameMutationItemRepo = new Mock<IDyeingPrintingStockOpnameMutationItemRepository>();

            stockOpnameMutationRepo
                 .Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingStockOpnameMutationModel>() { modelMutation }.AsQueryable());

            stockOpnameMutationItemRepo
                 .Setup(s => s.ReadAll())
                 .Returns(modelMutation.DyeingPrintingStockOpnameMutationItems.AsQueryable());
            var service = GetService(GetServiceProvider(stockOpnameRepo.Object, stockOpnameProductionOrderRepo.Object, stockOpnameMutationRepo.Object, stockOpnameMutationItemRepo.Object).Object);

            var result =  service.GenerateExcelMonitoring(DateTimeOffset.MinValue, DateTimeOffset.MaxValue, 1, 1, 7);

            Assert.NotNull(result);
        }

    }
}
