﻿using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Packaging;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services
{
    public class OutputPackagingServiceTest
    {
        public OutputPackagingService GetService(IServiceProvider serviceProvider)
        {
            return new OutputPackagingService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaOutputRepository repository, IDyeingPrintingAreaMovementRepository movementRepo,
           IDyeingPrintingAreaSummaryRepository summaryRepo, IDyeingPrintingAreaInputProductionOrderRepository sppRepo, IDyeingPrintingAreaInputRepository inputRepo)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementRepository)))
                .Returns(movementRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaSummaryRepository)))
                .Returns(summaryRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(sppRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputRepository)))
                .Returns(inputRepo);

            return spMock;
        }
        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaOutputRepository repository, IDyeingPrintingAreaMovementRepository movementRepo,
           IDyeingPrintingAreaSummaryRepository summaryRepo, IDyeingPrintingAreaInputProductionOrderRepository sppRepo)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementRepository)))
                .Returns(movementRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaSummaryRepository)))
                .Returns(summaryRepo);
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(sppRepo);

            return spMock;
        }

        private OutputPackagingViewModel ViewModel
        {
            get
            {
                return new OutputPackagingViewModel()
                {
                    Area = "PACKING",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "pas",
                    Group = "A",
                    BonNoInput = "s",
                    HasNextAreaDocument = false,
                    DestinationArea = "TRANSIT",
                    InputPackagingId = 1,
                    PackagingProductionOrders = new List<OutputPackagingProductionOrderViewModel>()
                    {
                        new OutputPackagingProductionOrderViewModel()
                        {
                            Balance = 1,
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            Remark = "remar",
                            Status = "Ok",
                            Motif = "sd",
                            PackingInstruction = "d",
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "sd",
                                Id = 1,
                                Type = "sd",
                                No = "sd"
                            },
                            Unit = "s",
                            UomUnit = "d"
                        }
                    }
                };
            }
        }

        private DyeingPrintingAreaOutputModel Model
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.HasNextAreaDocument, ViewModel.DestinationArea,
                   ViewModel.Group, ViewModel.PackagingProductionOrders.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area, ViewModel.DestinationArea, ViewModel.HasNextAreaDocument, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                    s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark, s.Grade, s.Status, s.Balance, s.Id, s.BuyerId)).ToList());
            }
        }

        private DyeingPrintingAreaOutputModel EmptyDetailModel
        {
            get
            {
                return new DyeingPrintingAreaOutputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.HasNextAreaDocument, ViewModel.DestinationArea,
                  ViewModel.Group, new List<DyeingPrintingAreaOutputProductionOrderModel>());
            }
        }
        private InputPackagingViewModel InputViewModel
        {
            get
            {
                return new InputPackagingViewModel()
                {
                    Area = "PACKING",
                    BonNo = "s",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "PAGI",
                    Group = "A",
                    PackagingProductionOrders = new List<InputPackagingProductionOrdersViewModel>
                    {
                        new InputPackagingProductionOrdersViewModel()
                        {
                            Balance = 1,
                            Buyer = "s",
                            CartNo = "1",
                            Color = "red",
                            Construction = "sd",
                            Grade = "s",
                            HasOutputDocument = false,
                            IsChecked = false,
                            Motif = "sd",
                            PackingInstruction = "d",
                            ProductionOrder = new ProductionOrder()
                            {
                                Code = "sd",
                                Id = 1,
                                Type = "sd",
                                No = "sd"
                            },
                            Unit = "s",
                            UomUnit = "d",
                            QtyOrder = 123,
                            ProductionOrderNo ="sd"
                        }
                    }
                };
            }
        }

        private DyeingPrintingAreaInputModel InputModel
        {
            get
            {
                return new DyeingPrintingAreaInputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.Group, ViewModel.PackagingProductionOrders.Select(s =>
                    new DyeingPrintingAreaInputProductionOrderModel(ViewModel.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                    s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, ViewModel.HasNextAreaDocument, s.QtyOrder, s.Grade, s.Balance, s.BuyerId)).ToList());
            }
        }

        [Fact]
        public async Task Should_Success_Create()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.PackagingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputPackagingId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel> {
                    new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, true, 10, "A",1)
                }.AsQueryable());

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>()
                {
                    new DyeingPrintingAreaInputModel(new DateTimeOffset(DateTime.Now),"PACKING","PAGI","s","A",
                    new List<DyeingPrintingAreaInputProductionOrderModel>{
                        new DyeingPrintingAreaInputProductionOrderModel("PACKING",1,"sd","sd","a","a","a","a","a","a","a","a",10,true,10,"A",1)
                    })
                }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_GetListInputSPP()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.PackagingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputPackagingId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            var inputspp = new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, true, 10, "A", 1);
            inputspp.Id = 1;
            inputspp.DyeingPrintingAreaInputId = 1;
            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel> {
                    inputspp
                }.AsQueryable());

            var inputbon = new DyeingPrintingAreaInputModel(new DateTimeOffset(DateTime.Now), "PACKING", "PAGI", "s", "A",
                    new List<DyeingPrintingAreaInputProductionOrderModel>{
                        inputspp
                    });
            inputbon.Id = 1;

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>()
                {
                    inputbon
                }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object).Object);

            var result = service.ReadSppInFromPack(0,10,string.Empty,string.Empty,"s");

            //Assert.NotEqual(0, result);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Should_Success_Delete()
        {
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();
            var inputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var outputRepoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var outputProductionOrderRepoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            var tes = new DyeingPrintingAreaOutputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.HasNextAreaDocument, ViewModel.DestinationArea,
                   ViewModel.Group, ViewModel.PackagingProductionOrders.Select(s =>
                    new DyeingPrintingAreaOutputProductionOrderModel(ViewModel.Area, ViewModel.DestinationArea, false, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.ProductionOrder.OrderQuantity, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                    s.Unit, s.Color, s.Motif, s.UomUnit, s.Remark, s.Grade, s.Status, s.Balance, s.Id, s.BuyerId)).ToList());
            tes.Id = 1;
            foreach (var i in tes.DyeingPrintingAreaOutputProductionOrders)
            {
                i.Id = 1;
                i.DyeingPrintingAreaInputProductionOrderId = 1;
                i.DyeingPrintingAreaOutputId = 1;
            }
            outputRepoMock.Setup(o => o.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);
            outputRepoMock.Setup(o => o.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { tes }.AsQueryable());
            outputRepoMock.Setup(o => o.GetDbSet())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { tes }.AsQueryable());

            var testinput = new DyeingPrintingAreaInputModel(ViewModel.Date, ViewModel.Area, ViewModel.Shift, ViewModel.BonNo, ViewModel.Group, ViewModel.PackagingProductionOrders.Select(s =>
                    new DyeingPrintingAreaInputProductionOrderModel(ViewModel.Area, s.ProductionOrder.Id, s.ProductionOrder.No, s.ProductionOrder.Type, s.PackingInstruction, s.CartNo, s.Buyer, s.Construction,
                    s.Unit, s.Color, s.Motif, s.UomUnit, s.Balance, false, s.QtyOrder, s.Grade, s.Balance, s.BuyerId)).ToList());

            foreach (var j in testinput.DyeingPrintingAreaInputProductionOrders)
            {
                j.Id = 1;
            }

            inputProductionOrderRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(1);
            inputProductionOrderRepoMock.Setup(s => s.ReadAll())
                .Returns(testinput.DyeingPrintingAreaInputProductionOrders.AsQueryable());

            

            outputProductionOrderRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputProductionOrderModel>()))
                .ReturnsAsync(1);

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);



            var service = GetService(GetServiceProvider(outputRepoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, inputProductionOrderRepoMock.Object, inputRepoMock.Object).Object);


            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
        [Fact]
        public async Task Should_Success_Create_Packing()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.PackagingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputPackagingId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel> {
                    new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, true, 10, "A",1)
                }.AsQueryable());

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>()
                {
                    new DyeingPrintingAreaInputModel(new DateTimeOffset(DateTime.Now),"PACKING","PAGI","s","A",
                    new List<DyeingPrintingAreaInputProductionOrderModel>{
                        new DyeingPrintingAreaInputProductionOrderModel("PACKING",1,"sd","sd","a","a","a","a","a","a","a","a",10,true,10,"A",1)
                    })
                }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object).Object);
            var vm = ViewModel;
            vm.DestinationArea = "PACKING";
            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Should_Success_Create_Aval()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();

            repoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaOutputModel>()))
                .ReturnsAsync(1);

            repoMock.Setup(s => s.ReadAllIgnoreQueryFilter())
                .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            movementRepoMock.Setup(s => s.InsertAsync(It.IsAny<DyeingPrintingAreaMovementModel>()))
                 .ReturnsAsync(1);
            var item = ViewModel.PackagingProductionOrders.FirstOrDefault();
            summaryRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaSummaryModel>() {
                     new DyeingPrintingAreaSummaryModel(ViewModel.Date, ViewModel.Area, "IN", ViewModel.InputPackagingId, ViewModel.BonNo, item.ProductionOrder.Id,
                     item.ProductionOrder.No, item.CartNo, item.Buyer, item.Construction,item.Unit, item.Color,item.Motif,item.UomUnit, item.Balance)
                 }.AsQueryable());

            summaryRepoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<DyeingPrintingAreaSummaryModel>()))
                 .ReturnsAsync(1);

            sppRepoMock.Setup(s => s.UpdateFromOutputAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            sppRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel> {
                    new DyeingPrintingAreaInputProductionOrderModel("PACKING", 1, "sd", "sd", "a", "a", "a", "a", "a", "a", "a", "a", 10, true, 10, "A",1)
                }.AsQueryable());

            inputRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>()
                {
                    new DyeingPrintingAreaInputModel(new DateTimeOffset(DateTime.Now),"PACKING","PAGI","s","A",
                    new List<DyeingPrintingAreaInputProductionOrderModel>{
                        new DyeingPrintingAreaInputProductionOrderModel("PACKING",1,"sd","sd","a","a","a","a","a","a","a","a",10,true,10,"A",1)
                    })
                }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputRepoMock.Object).Object);
            var vm = ViewModel;
            vm.DestinationArea = "GUDANG JADI";
            var result = await service.Create(vm);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_Read()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();


            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }
        [Fact]
        public void Should_Success_ReadBonOutFromPack()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();
            var inputBonRepoMock = new Mock<IDyeingPrintingAreaInputRepository>();


            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputModel>() { Model }.AsQueryable());

            inputBonRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputModel>() { InputModel }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object, inputBonRepoMock.Object).Object);

            var result = service.ReadBonOutFromPack(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }


        [Fact]
        public async Task Should_Success_ReadById()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Null_ReadById()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(default(DyeingPrintingAreaOutputModel));

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task Should_Success_GenerateExcel()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object).Object);

            var result = await service.GenerateExcel(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Empty_GenerateExcel()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputRepository>();
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var summaryRepoMock = new Mock<IDyeingPrintingAreaSummaryRepository>();
            var sppRepoMock = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);

            var service = GetService(GetServiceProvider(repoMock.Object, movementRepoMock.Object, summaryRepoMock.Object, sppRepoMock.Object).Object);

            var result = await service.GenerateExcel(1);

            Assert.NotNull(result);
        }
    }
}
