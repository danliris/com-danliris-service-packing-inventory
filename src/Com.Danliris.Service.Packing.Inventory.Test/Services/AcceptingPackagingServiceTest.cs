using Com.Danliris.Service.Packing.Inventory.Application.AcceptingPackaging;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.InspectionMaterial;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Data.Models.AcceptingPackaging;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.AcceptingPackaging;
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
    public class AcceptingPackagingServiceTest
    {
        public AcceptingPackagingService GetService(IServiceProvider serviceProvider)
        {
            return new AcceptingPackagingService(serviceProvider);
        }

        private AcceptingPackagingViewModel ViewModel
        {
            get
            {
                return new AcceptingPackagingViewModel()
                {
                    Active = true,
                    Saldo = 123,
                    Satuan = "string",
                    NoSpp = new ProductionOrder
                    {
                        Id = 0,
                        Code = "string",
                        No = "string",
                        Type = "string"
                    },
                    CreatedAgent = "string",
                    DeletedAgent = "string",
                    CreatedBy = "string",
                    CreatedUtc = DateTime.Now,
                    DeletedBy = "string",
                    DeletedUtc = DateTime.Now,
                    Grade = "string",
                    Id = 0,
                    IsDeleted = false,
                    LastModifiedAgent = "string",
                    LastModifiedBy = "string",
                    LastModifiedUtc = DateTime.Now,
                    MaterialObject = new Material
                    {
                        Id = 0,
                        Code = "string",
                        Name = "string"
                    },
                    Motif = "string",
                    NoBon = "IM.20.0006",
                    Unit = new Unit
                    {
                        Code = "string",
                        Id = 0,
                        Name = "string"
                    },
                    Warna = "string",
                    Shift = "string",
                    Mtr = 111,
                    Yds = 222,
                };
            }
        }

        private AcceptingPackagingModel Model
        {
            get
            {
                return new AcceptingPackagingModel
                {
                    Active = ViewModel.Active,
                    CreatedAgent = ViewModel.CreatedAgent,
                    Saldo = ViewModel.Saldo,
                    Satuan= ViewModel.Satuan,
                    Shift = ViewModel.Shift,
                    BonNo = ViewModel.NoBon,
                    CreatedBy = ViewModel.CreatedBy,
                    Id = ViewModel.Id,
                    CreatedUtc = ViewModel.CreatedUtc,
                    Date = ViewModel.LastModifiedUtc,
                    DeletedAgent = ViewModel.DeletedAgent,
                    DeletedUtc = ViewModel.DeletedUtc,
                    Grade = ViewModel.Grade,
                    IdDyeingPrintingMovement = ViewModel.Id,
                    DeletedBy = ViewModel.DeletedBy,
                    IsDeleted = ViewModel.IsDeleted,
                    LastModifiedAgent = ViewModel.LastModifiedAgent,
                    LastModifiedBy = ViewModel.LastModifiedBy,
                    LastModifiedUtc = ViewModel.LastModifiedUtc,
                    MaterialObject = ViewModel.MaterialObject.Name,
                    Motif = ViewModel.Motif,
                    Mtr = ViewModel.Mtr,
                    OrderNo = ViewModel.NoSpp.No,
                    Unit = ViewModel.Unit.Name,
                    Warna = ViewModel.Warna,
                    Yds = ViewModel.Yds
                };
            }
        }

        private InspectionMaterialViewModel DPViewModel
        {
            get
            {
                return new InspectionMaterialViewModel()
                {
                    Id = 1,
                    Area = "area",
                    Date = DateTimeOffset.UtcNow,
                    Shift = "shift",
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = 1,
                        No = "no",
                        Type = "type"
                    },
                    CartNo = "no",
                    Material = new Material()
                    {
                        Id = 1,
                        Name = "name"
                    },
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Id = 1,
                        Name = "name"
                    },
                    MaterialWidth = "width",
                    Unit = new Unit()
                    {
                        Id = 1,
                        Name = "name"
                    },

                    Color = "color",
                    Mutation = "mutation",
                    UOMUnit = "MTR"
                };
            }
        }
        private DyeingPrintingAreaMovementModel DPModel
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DPViewModel.Area, DPViewModel.BonNo, DPViewModel.Date, DPViewModel.Shift, DPViewModel.ProductionOrder.Id,
                    DPViewModel.ProductionOrder.Code, DPViewModel.ProductionOrder.No, DPViewModel.ProductionOrderQuantity, DPViewModel.ProductionOrder.Type,
                    DPViewModel.Buyer, DPViewModel.PackingInstruction, DPViewModel.CartNo, DPViewModel.Material.Id, DPViewModel.Material.Code, DPViewModel.Material.Name,
                    DPViewModel.MaterialConstruction.Id, DPViewModel.MaterialConstruction.Code, DPViewModel.MaterialConstruction.Name, DPViewModel.MaterialWidth,
                    DPViewModel.Unit.Id, DPViewModel.Unit.Code, DPViewModel.Unit.Name, DPViewModel.Color, DPViewModel.Motif, DPViewModel.Mutation, DPViewModel.Length,
                    DPViewModel.UOMUnit, DPViewModel.Balance,
                    new List<DyeingPrintingAreaMovementHistoryModel>()
                    {
                        new DyeingPrintingAreaMovementHistoryModel(DPViewModel.Date, DPViewModel.Area, DPViewModel.Shift, AreaEnum.IM)
                    });
            }
        }

        public Mock<IServiceProvider> GetServiceProvider(IAcceptingPackagingRepository service, IDyeingPrintingAreaMovementRepository dpService)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IAcceptingPackagingRepository)))
                .Returns(service);

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementRepository)))
                .Returns(dpService);

            return spMock;
        }

        //[Fact]
        //public async Task Should_Success_Create()
        //{
        //    var repoMock = new Mock<IAcceptingPackagingRepository>();
        //    var dpMock = new Mock<IDyeingPrintingAreaMovementRepository>();
        //    repoMock.Setup(s => s.InsertAsync(It.IsAny<int>(),It.IsAny<AcceptingPackagingModel>()))
        //        .ReturnsAsync(1);
        //    //repoMock.Setup(s => s.ReadAll()).Returns(new List<AcceptingPackagingModel>() { new AcceptingPackagingModel(){}}.AsQueryable());

        //    var service = GetService(GetServiceProvider(repoMock.Object, dpMock.Object).Object);

        //    var result = await service.Create(ViewModel);

        //    Assert.NotEqual(0, result);
        //}

        [Fact]
        public async Task Should_Success_Delete()
        {
            var repoMock = new Mock<IAcceptingPackagingRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Model);
            var dpMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            dpMock.Setup(s => s.UpdateFromFabricQualityControlAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(1);
            var fgtMock = new Mock<IAcceptingPackagingRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, dpMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        //[Fact]
        //public async Task Should_Success_Update()
        //{
        //    var repoMock = new Mock<IAcceptingPackagingRepository>();
        //    repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<AcceptingPackagingModel>()))
        //        .ReturnsAsync(1);
        //    var fgtMock = new Mock<IAcceptingPackagingRepository>();
        //    var dpMock = new Mock<IDyeingPrintingAreaMovementRepository>();
        //    var service = GetService(GetServiceProvider(repoMock.Object, dpMock.Object).Object);

        //    var result = await service.Update(1, ViewModel);

        //    Assert.NotEqual(0, result);
        //}

        [Fact]
        public void Should_Success_Read()
        {
            var repoMock = new Mock<IAcceptingPackagingRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<AcceptingPackagingModel>() { Model }.AsQueryable());
            var dpMock = new Mock<IDyeingPrintingAreaMovementRepository>();
            var fgtMock = new Mock<IAcceptingPackagingRepository>();
            var service = GetService(GetServiceProvider(repoMock.Object, dpMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        //[Fact]
        //public void Should_Success_GetForBonNo()
        //{
        //    var repoMock = new Mock<IAcceptingPackagingRepository>();
        //    repoMock.Setup(s => s.ReadByBonNo(Model.BonNo))
        //        .Returns(Model);

        //    var dpMock = new Mock<IDyeingPrintingAreaMovementRepository>();
        //    dpMock.Setup(s => s.ReadAll().Where(x => x.BonNo == Model.BonNo))
        //        .Returns(new List<DyeingPrintingAreaMovementModel> { DPModel}.AsQueryable());


        //    var service = GetService(GetServiceProvider(repoMock.Object, dpMock.Object).Object);

        //    var result = service.ReadByBonNo(Model.BonNo);

        //    Assert.NotNull(result);
        //}
    }
}
