using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingStockOpname;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.DyeingPrintingStockOpname.Warehouse
{
    public class StockOpnameSummaryServiceTest
    {
        public StockOpnameSummaryService GetService(IServiceProvider serviceProvider)
        {
            return new StockOpnameSummaryService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider( IDyeingPrintingStockOpnameSummaryRepository stockOpnameSummaryRepo)
        {
            var spMock = new Mock<IServiceProvider>();
            //spMock
            //    .Setup(s => s.GetService(typeof(IDyeingPrintingStockOpnameRepository)))
            //    .Returns(stockOpnameRepo);

            //spMock
            //    .Setup(s => s.GetService(typeof(IDyeingPrintingStockOpnameProductionOrderRepository)))
            //    .Returns(stockOpnameProductionOrderRepo);

            //spMock.Setup(s => s.GetService(typeof(IDyeingPrintingStockOpnameMutationRepository)))
            //    .Returns(stockOpnameMutationRepo);

            //spMock.Setup(s => s.GetService(typeof(IDyeingPrintingStockOpnameMutationItemRepository)))
            //    .Returns(stockOpnameMutationItemRepo);

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingStockOpnameSummaryRepository)))
                .Returns(stockOpnameSummaryRepo);

            return spMock;

        }

        private DyeingPrintingStockOpnameSummaryModel modelSummary
        {
            get
            {
                return new DyeingPrintingStockOpnameSummaryModel(
                    1,
                    1,
                    1,
                    "buyer",
                    "color",
                    "construction",
                    "grade",
                    1,
                    "materialConstructionName",
                    1,
                    "materialName",
                    "materialWidth",
                    "mottif",
                    "packingConstruction",
                    1,
                    1,
                    1,
                    "packagingType",
                    "packagingUnit",
                    1,
                    "OrderNo",
                    "OrederType",
                    1,
                    1,
                    "processTypeName",
                    1,
                    "yarnMaterialName",
                    "remark",
                    "status",
                    "unit",
                    "uomUnit",
                    1,
                    1,
                    "productSkuCode",
                    1,
                    "productPackingCode",
                    1,
                    "trackType",
                    "trackName",
                    "trackBox",
                    DateTime.Now,
                    "Description",
                    ""


                    );

             

            }
        }

        private StockOpnameTrackViewModel viewModel
        {
            get
            {
                return new StockOpnameTrackViewModel()
                {
                    ProductPackingCode = "productPackingCode",
                    Grade = "a",
                    PackagingLength = 1,
                    Items = new List<TrackingViewModel>() {
                        new TrackingViewModel()
                        {
                            Balance = 1,
                            PackagingQtyRemains = 1,
                            PackagingQtySplit = 1,
                            PackingQtyDiff =1,
                            Track = new Track()
                            { 
                                Id = 1,
                                Name = "Jalur",
                                Type = "A",
                                Box = "1"
                               
                            }
                        },
                        new TrackingViewModel()
                        {
                            Balance = 1,
                            PackagingQtyRemains = 1,
                            PackagingQtySplit = 1,
                            PackingQtyDiff =1,
                            Track = new Track()
                            {
                                Id = 2,
                                Name = "Jalur",
                                Type = "A",
                                Box = "2"

                            }
                        }

                    }
                    


                };


            }
        }

        [Fact]
        public void Should_Success_GetDataUpdateTrack()
        {
            //Arrange
            var stockOpnameSummaryRepo = new Mock<IDyeingPrintingStockOpnameSummaryRepository>();

            stockOpnameSummaryRepo
                 .Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingStockOpnameSummaryModel>() { modelSummary }.AsQueryable());

            //var vm = model2.DyeingPrintingStockOpnameProductionOrders;

            var service = GetService(GetServiceProvider(stockOpnameSummaryRepo.Object).Object);

            //Act
            var result = service.GetDataUpdateTrack(1, "productPackingCode", 1);

            //Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Should_Success_ReadById()
        {
            //Arrange
            var stockOpnameSummaryRepo = new Mock<IDyeingPrintingStockOpnameSummaryRepository>();
            var model = modelSummary;
            model.Id = 1;
            stockOpnameSummaryRepo
                 .Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                 .ReturnsAsync(model);

            
            var service = GetService(GetServiceProvider(stockOpnameSummaryRepo.Object).Object);

            //Act
            var result = await service.ReadById(1);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Success_GenerateExcel()
        {
            var stockOpnameSummaryRepo = new Mock<IDyeingPrintingStockOpnameSummaryRepository>();

            stockOpnameSummaryRepo
                 .Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingStockOpnameSummaryModel>() { modelSummary }.AsQueryable());

            var service = GetService(GetServiceProvider(stockOpnameSummaryRepo.Object).Object);

            var result = service.GenerateExcelMonitoring(1, "productPackingCode",1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Empty_GenerateExcel()
        {
            var stockOpnameSummaryRepo = new Mock<IDyeingPrintingStockOpnameSummaryRepository>();

            stockOpnameSummaryRepo
                 .Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingStockOpnameSummaryModel>() { modelSummary }.AsQueryable());

            var service = GetService(GetServiceProvider(stockOpnameSummaryRepo.Object).Object);

            var result = service.GenerateExcelMonitoring(1, "productPackingCode", 3);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Success_Update_Track()
        {
            //Arrange
            var stockOpnameSummaryRepo = new Mock<IDyeingPrintingStockOpnameSummaryRepository>();
            //var newModel = model;
            //newModel.DyeingPrintingStockOpnameProductionOrders.FirstOrDefault().SetBalance(2, "", "");

            var vm = viewModel;

            //vm.WarehousesProductionOrders.FirstOrDefault().Balance = 1;

            //stockOpnameRepo
            //    .Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
            //    .ReturnsAsync(model);

            //stockOpnameRepo
            //    .Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingStockOpnameModel>()))
            //    .ReturnsAsync(1);
            stockOpnameSummaryRepo
                 .Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                 .ReturnsAsync(modelSummary);
            stockOpnameSummaryRepo
                .Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingStockOpnameSummaryModel>()))
                .ReturnsAsync(1);

            stockOpnameSummaryRepo
                .Setup(s => s.InsertAsync( It.IsAny<DyeingPrintingStockOpnameSummaryModel>()))
                .ReturnsAsync(1);



            var service = GetService(GetServiceProvider(stockOpnameSummaryRepo.Object).Object);

            //Act
            var result = await service.Update(1, vm);

            //Assert
            Assert.NotEqual(0, result);
        }



        [Fact]
        public async Task Should_Success_Update_FindTrack()
        {
            //Arrange
            var stockOpnameSummaryRepo = new Mock<IDyeingPrintingStockOpnameSummaryRepository>();
            //var newModel = model;
            //newModel.DyeingPrintingStockOpnameProductionOrders.FirstOrDefault().SetBalance(2, "", "");

            var vm = viewModel;
            vm.Items[1].Track.Id = 1;

            //vm.WarehousesProductionOrders.FirstOrDefault().Balance = 1;

            //stockOpnameRepo
            //    .Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
            //    .ReturnsAsync(model);

            //stockOpnameRepo
            //    .Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingStockOpnameModel>()))
            //    .ReturnsAsync(1);
            stockOpnameSummaryRepo
                 .Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                 .ReturnsAsync(modelSummary);
            stockOpnameSummaryRepo
                .Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingStockOpnameSummaryModel>()))
                .ReturnsAsync(1);

            stockOpnameSummaryRepo
                .Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingStockOpnameSummaryModel>()))
                .ReturnsAsync(1);

            stockOpnameSummaryRepo
                .Setup(s => s.GetDbSet())
                .Returns(new List<DyeingPrintingStockOpnameSummaryModel>() { modelSummary }.AsQueryable());



            var service = GetService(GetServiceProvider(stockOpnameSummaryRepo.Object).Object);

            //Act
            var result = await service.Update(1, vm);

            //Assert
            Assert.NotEqual(0, result);
        }

    }
}
