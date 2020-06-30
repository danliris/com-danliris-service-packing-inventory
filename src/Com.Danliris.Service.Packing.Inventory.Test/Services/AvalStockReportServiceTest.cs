using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AvalStockReport;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services
{
    public class AvalStockReportServiceTest
    {
        public AvalStockReportService GetService(IServiceProvider serviceProvider)
        {
            return new AvalStockReportService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaMovementRepository movementRepo)
        {
            var spMock = new Mock<IServiceProvider>();

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaMovementRepository)))
                .Returns(movementRepo);

            return spMock;
        }

        private AvalStockReportViewModel ViewModel
        {
            get
            {
                return new AvalStockReportViewModel()
                {
                    AvalType = "type",
                    EndAvalQuantity = 1,
                    EndAvalWeightQuantity = 1,
                    InAvalQuantity = 2,
                    InAvalWeightQuantity = 2,
                    OutAvalQuantity = 1,
                    OutAvalWeightQuantity = 1,
                    StartAvalQuantity = 0,
                    StartAvalWeightQuantity = 0
                };
            }
        }

        private DyeingPrintingAreaMovementModel ModelTransform
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow, "GUDANG AVAL", "TRANSFORM", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                    "unit", 1, 1, ViewModel.InAvalQuantity, ViewModel.InAvalWeightQuantity, ViewModel.AvalType);
            }
        }

        private DyeingPrintingAreaMovementModel ModelOut
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow, "GUDANG AVAL", "OUT", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", 1, 1, ViewModel.OutAvalQuantity, ViewModel.OutAvalWeightQuantity, ViewModel.AvalType);
            }
        }

        private DyeingPrintingAreaMovementModel ModelAwalOut
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow.AddDays(-1), "GUDANG AVAL", "OUT", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", 1, 1, ViewModel.StartAvalQuantity, ViewModel.StartAvalWeightQuantity, ViewModel.AvalType);
            }
        }
        private DyeingPrintingAreaMovementModel ModelAwalTransform
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow.AddDays(-1), "GUDANG AVAL", "TRANSFORM", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", 1, 1, ViewModel.StartAvalQuantity, ViewModel.StartAvalWeightQuantity, ViewModel.AvalType);
            }
        }
        private DyeingPrintingAreaMovementModel ModelAwalTransform2
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow.AddDays(-2), "GUDANG AVAL", "TRANSFORM", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", 1, 1, ViewModel.StartAvalQuantity, ViewModel.StartAvalWeightQuantity, ViewModel.AvalType);
            }
        }
        private DyeingPrintingAreaMovementModel ModelAwalTransform3
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow.AddDays(-2), "GUDANG AVAL", "OUT", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", 1, 1, ViewModel.StartAvalQuantity, ViewModel.StartAvalWeightQuantity, ViewModel.AvalType);
            }
        }


        [Fact]
        public void Should_Success_GetData()
        {
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();

            var data = new List<DyeingPrintingAreaMovementModel>() { ModelAwalOut, ModelAwalTransform, ModelTransform, ModelOut, ModelAwalTransform2 };
            movementRepoMock.Setup(s => s.ReadAll())
                 .Returns(data.AsQueryable());

            var service = GetService(GetServiceProvider(movementRepoMock.Object).Object);

            var result = service.GetReportData(DateTimeOffset.UtcNow, 7);

            Assert.NotEmpty(result.Data);
        }


        [Fact]
        public void Should_Success_GetExcelData()
        {
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();

            var data = new List<DyeingPrintingAreaMovementModel>() { ModelAwalOut, ModelAwalTransform, ModelTransform, ModelOut };
            movementRepoMock.Setup(s => s.ReadAll())
                 .Returns(data.AsQueryable());

            var service = GetService(GetServiceProvider(movementRepoMock.Object).Object);

            var result = service.GenerateExcel(DateTimeOffset.UtcNow, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Success_GetExcelData_Empty()
        {
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();

            var data = new List<DyeingPrintingAreaMovementModel>() { ModelAwalOut, ModelAwalTransform, ModelTransform, ModelOut };
            movementRepoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaMovementModel>().AsQueryable());

            var service = GetService(GetServiceProvider(movementRepoMock.Object).Object);

            var result = service.GenerateExcel(DateTimeOffset.UtcNow, 7);

            Assert.NotNull(result);
        }

    }


}
