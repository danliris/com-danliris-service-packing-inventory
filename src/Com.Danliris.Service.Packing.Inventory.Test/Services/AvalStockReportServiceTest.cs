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
                    EndAvalWeightQuantity = 2,
                    InAvalQuantity = 5,
                    InAvalWeightQuantity = 6,
                    OutAvalQuantity = 3,
                    OutAvalWeightQuantity = 4,
                    StartAvalQuantity = 7,
                    StartAvalWeightQuantity = 8
                };
            }
        }

        private DyeingPrintingAreaMovementModel ModelTransform
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow, "DL", "GUDANG AVAL", "TRANSFORM", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                    "unit", 1, 1, "type", 1, "a", "a", ViewModel.InAvalQuantity, ViewModel.InAvalWeightQuantity, ViewModel.AvalType);
            }
        }

        private DyeingPrintingAreaMovementModel ModelOut
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow, "DL", "GUDANG AVAL", "OUT", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", 1, 1, "type", 1, "a", "a", ViewModel.OutAvalQuantity, ViewModel.OutAvalWeightQuantity, ViewModel.AvalType);
            }
        }

        private DyeingPrintingAreaMovementModel ModelAdjOut
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow, "DL", "GUDANG AVAL", "ADJ OUT", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", 1, 1, "type", 1, "a", "a", ViewModel.OutAvalQuantity, ViewModel.OutAvalWeightQuantity, ViewModel.AvalType);
            }
        }

        private DyeingPrintingAreaMovementModel ModelAdjIn
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow, "DL", "GUDANG AVAL", "ADJ IN", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", 1, 1, "type", 1, "a", "a", ViewModel.OutAvalQuantity, ViewModel.OutAvalWeightQuantity, ViewModel.AvalType);
            }
        }

        private DyeingPrintingAreaMovementModel ModelAwalOut
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow.AddDays(-1), "DL", "GUDANG AVAL", "OUT", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", 1, 1, "type", 1, "a", "a", ViewModel.StartAvalQuantity, ViewModel.StartAvalWeightQuantity, ViewModel.AvalType);
            }
        }

        private DyeingPrintingAreaMovementModel ModelAwalAdjOut
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow.AddDays(-1), "DL", "GUDANG AVAL", "ADJ OUT", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", 1, 1, "type", 1, "a", "a", ViewModel.StartAvalQuantity, ViewModel.StartAvalWeightQuantity, ViewModel.AvalType);
            }
        }

        private DyeingPrintingAreaMovementModel ModelAwalAdjIn
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow.AddDays(-1), "DL", "GUDANG AVAL", "ADJ IN", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", 1, 1, "type", 1, "a", "a", ViewModel.StartAvalQuantity, ViewModel.StartAvalWeightQuantity, ViewModel.AvalType);
            }
        }

        private DyeingPrintingAreaMovementModel ModelAwalTransform
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow.AddDays(-1), "DL", "GUDANG AVAL", "TRANSFORM", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", 1, 1, "type", 1, "a", "a", ViewModel.StartAvalQuantity, ViewModel.StartAvalWeightQuantity, ViewModel.AvalType);
            }
        }
        private DyeingPrintingAreaMovementModel ModelAwalTransform2
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow.AddDays(-2), "DL", "GUDANG AVAL", "TRANSFORM", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", 1, 1, "type", 1, "a", "a", ViewModel.StartAvalQuantity, ViewModel.StartAvalWeightQuantity, ViewModel.AvalType);
            }
        }
        private DyeingPrintingAreaMovementModel ModelAwalTransform3
        {
            get
            {
                return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow.AddDays(-2), "DL", "GUDANG AVAL", "OUT", 1, "no", 1, "no", "car", "uu", "cos", "unit", "coo", "motif",
                     "unit", 1, 1, "type", 1, "a", "a", ViewModel.StartAvalQuantity, ViewModel.StartAvalWeightQuantity, ViewModel.AvalType);
            }
        }


        [Fact]
        public void Should_Success_GetData()
        {
            var movementRepoMock = new Mock<IDyeingPrintingAreaMovementRepository>();

            var data = new List<DyeingPrintingAreaMovementModel>() { ModelAwalOut, ModelAwalTransform, ModelTransform, ModelOut, ModelAwalTransform2, ModelAwalTransform3, ModelAdjIn, ModelAdjOut,
                ModelAwalAdjIn, ModelAwalAdjOut};
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

            var data = new List<DyeingPrintingAreaMovementModel>() { ModelAwalOut, ModelAwalTransform, ModelTransform, ModelOut, ModelAdjIn, ModelAdjOut, ModelAwalAdjIn, ModelAwalAdjOut };
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
