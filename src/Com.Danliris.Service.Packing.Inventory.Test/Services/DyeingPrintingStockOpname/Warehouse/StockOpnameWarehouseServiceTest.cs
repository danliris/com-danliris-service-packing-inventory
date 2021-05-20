using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname;
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
 public   class StockOpnameWarehouseServiceTest
    {
        public StockOpnameWarehouseService GetService(IServiceProvider serviceProvider)
        {
            return new StockOpnameWarehouseService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingStockOpnameRepository stockOpnameRepo, IDyeingPrintingStockOpnameProductionOrderRepository stockOpnameProductionOrderRepo)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock
                .Setup(s => s.GetService(typeof(IDyeingPrintingStockOpnameRepository)))
                .Returns(stockOpnameRepo);

            spMock
                .Setup(s => s.GetService(typeof(IDyeingPrintingStockOpnameProductionOrderRepository)))
                .Returns(stockOpnameProductionOrderRepo);
          
            return spMock;
        }

        private StockOpnameWarehouseViewModel viewModel
        {
            get
            {
                return new StockOpnameWarehouseViewModel()
                {
                    Id = 1,
                    Area = "GUDANG JADI",
                    BonNo = "TR.GJ.20.001",
                    Date = DateTimeOffset.Now,
                    Type = DyeingPrintingArea.STOCK_OPNAME,
                    WarehousesProductionOrders = new List<StockOpnameWarehouseProductionOrderViewModel>()
                    {
                        new StockOpnameWarehouseProductionOrderViewModel()
                        {
                            Id = 1,
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "SLD",
                                Id = 62,
                                Type = "SOLID",
                                No = "F/2020/000"
                            },
                            PackingInstruction = "a",
                            Construction = "a",
                            Unit = "a",
                            Buyer = "a",
                            Color = "a",
                            Motif = "a",
                            UomUnit = "a",
                            Remark = "a",
                            Grade = "a",
                            Status = "a",
                            Balance = 50,
                            PreviousBalance = 100,
                            InputId = 2,
                            ProductionOrderNo = "asd",
                            Material = new Material(){
                                Id=1,
                            Code="Code",
                            Name="Name"
                            },
                            GradeProduct =new GradeProduct()
                            {
                                Code="Code",
                                Type="A"
                            },
                            Uom =new UnitOfMeasurement()
                            {
                                Id=1,
                                Unit=""
                            },
                            BalanceRemains=1,
                            BuyerId=1,
                            DocumentNo="DocumentNo",
                            MaterialConstruction =new MaterialConstruction()
                            {
                                Id=1,
                                Code="Code",
                                Name="Name"
                            },
                            MaterialWidth="MaterialWidth",
                            PackagingLength=1,
                            YarnMaterial=new YarnMaterial()
                            {
                                Id=1,
                                Name="Name"
                            },
                            PreviousQtyPacking=1,
                            ProcessType=new ProcessType()
                            {
                                Id=1,
                                Name="Name"
                            },
                            ProcessTypeName="ProcessTypeName",
                            ProcessTypeId=1,
                            YarnMaterialName="YarnMaterialName",
                            YarnMaterialId=1,
                            
                            MtrLength = 10,
                            YdsLength = 10,
                            Quantity = 10,
                            PackagingType = "s",
                            PackagingUnit = "a",
                            PackagingQty = 10,
                            QtyOrder = 10
                                                    }
                    }
                };
            }
        }

        private DyeingPrintingStockOpnameModel model
        {
            get
            {
                var stockOpnameProductionOrder = new DyeingPrintingStockOpnameProductionOrderModel(1, 1, "buyer", "color", "construction", "documentNo", "A", 1, "MaterialConstructionName", 1, "MaterialName", "MaterialWitdh", "Motif", "PackingInstruction", 1, 1, "PackagingType", "PackagingUnit", 1, "ProductionorederName", "productionOrderType", 1, 1, "ProcessTypeName", 1, "yarnMaterialName", "Remark", "Status", "Unit", "UomUnit");

                var stockOpnameProductionOrders = new List<DyeingPrintingStockOpnameProductionOrderModel>();
                stockOpnameProductionOrders.Add(stockOpnameProductionOrder);

                return new DyeingPrintingStockOpnameModel(DyeingPrintingArea.GUDANGJADI, "BON_NO", DateTimeOffset.Now, DyeingPrintingArea.STOCK_OPNAME, stockOpnameProductionOrders);
               
            }
        }


        [Fact]
        public async Task Should_Success_Create()
        {
            //Arrange
            var stockOpnameRepo = new Mock<IDyeingPrintingStockOpnameRepository>();
            var stockOpnameProductionOrderRepo = new Mock<IDyeingPrintingStockOpnameProductionOrderRepository>();
            
            stockOpnameRepo
                .Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingStockOpnameModel>()))
                .ReturnsAsync(1);

            stockOpnameRepo
                .Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingStockOpnameModel>() { }.AsQueryable());

            stockOpnameRepo
                .Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingStockOpnameModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(stockOpnameRepo.Object, stockOpnameProductionOrderRepo.Object).Object);

            //Act
            var result = await service.Create(viewModel);

            //Assert
            Assert.NotEqual(0, result);
        }


        [Fact]
        public async Task Should_Success_Create_When_Existing_Data()
        {
            //Arrange
            var stockOpnameRepo = new Mock<IDyeingPrintingStockOpnameRepository>();
            var stockOpnameProductionOrderRepo = new Mock<IDyeingPrintingStockOpnameProductionOrderRepository>();

            //stockOpnameRepo
            //   .Setup(s => s.GetDbSet())
            //   .Returns(new List<DyeingPrintingStockOpnameModel>() { model }.AsQueryable());

            stockOpnameProductionOrderRepo
                .Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingStockOpnameProductionOrderModel>()))
                .ReturnsAsync(1);

            stockOpnameRepo
                .Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingStockOpnameModel>() { model }.AsQueryable());

            stockOpnameRepo
                .Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingStockOpnameModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(stockOpnameRepo.Object, stockOpnameProductionOrderRepo.Object).Object);

            //Act
            var result = await service.Create(viewModel);
           

            //Assert
            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_Read()
        {  
            //Arrange
            var stockOpnameRepo = new Mock<IDyeingPrintingStockOpnameRepository>();
            var stockOpnameProductionOrderRepo = new Mock<IDyeingPrintingStockOpnameProductionOrderRepository>();

            stockOpnameRepo
                 .Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingStockOpnameModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(stockOpnameRepo.Object, stockOpnameProductionOrderRepo.Object).Object);

            //Act
            var result = service.Read(1, 25, "{}", "{}", null);

            //Assert
            Assert.NotEmpty(result.Data);
        }


        [Fact]
        public void Should_Success_Read_By_Keyword()
        {
            //Arrange
            var stockOpnameRepo = new Mock<IDyeingPrintingStockOpnameRepository>();
            var stockOpnameProductionOrderRepo = new Mock<IDyeingPrintingStockOpnameProductionOrderRepository>();

            stockOpnameRepo
                 .Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingStockOpnameModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(stockOpnameRepo.Object, stockOpnameProductionOrderRepo.Object).Object);

            //Act
            var result = service.Read("");

            //Assert
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task Should_Success_ReadById()
        {
            //Arrange
            var stockOpnameRepo = new Mock<IDyeingPrintingStockOpnameRepository>();
            var stockOpnameProductionOrderRepo = new Mock<IDyeingPrintingStockOpnameProductionOrderRepository>();

            stockOpnameRepo
                .Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(stockOpnameRepo.Object, stockOpnameProductionOrderRepo.Object).Object);

            //Act
            var result = await service.ReadById(1);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Reurn_Null_ReadById()
        {
            //Arrange
            var stockOpnameRepo = new Mock<IDyeingPrintingStockOpnameRepository>();
            var stockOpnameProductionOrderRepo = new Mock<IDyeingPrintingStockOpnameProductionOrderRepository>();

            stockOpnameRepo
                .Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(()=>null);

            var service = GetService(GetServiceProvider(stockOpnameRepo.Object, stockOpnameProductionOrderRepo.Object).Object);

            //Act
            var result = await service.ReadById(1);

            //Assert
            Assert.Null(result);
        }


        [Fact]
        public async Task Should_Success_Delete()
        {
            //Arrange
            var stockOpnameRepo = new Mock<IDyeingPrintingStockOpnameRepository>();
            var stockOpnameProductionOrderRepo = new Mock<IDyeingPrintingStockOpnameProductionOrderRepository>();

            stockOpnameRepo
                .Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            stockOpnameRepo
              .Setup(s => s.DeleteAsync(It.IsAny<int>()))
              .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(stockOpnameRepo.Object, stockOpnameProductionOrderRepo.Object).Object);

            //Act
            var result = await service.Delete(1);

            //Assert
            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Update()
        {
            //Arrange
            var stockOpnameRepo = new Mock<IDyeingPrintingStockOpnameRepository>();
            var stockOpnameProductionOrderRepo = new Mock<IDyeingPrintingStockOpnameProductionOrderRepository>();
            var newModel = model;
            newModel.DyeingPrintingStockOpnameProductionOrders.FirstOrDefault().SetBalance(2, "", "");

            var vm = viewModel;

            vm.WarehousesProductionOrders.FirstOrDefault().Balance = 1;

            stockOpnameRepo
                .Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            stockOpnameRepo
                .Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingStockOpnameModel>()))
                .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(stockOpnameRepo.Object, stockOpnameProductionOrderRepo.Object).Object);

            //Act
            var result = await service.Update(1, vm);

            //Assert
            Assert.NotEqual(0, result);
        }
    }
}
