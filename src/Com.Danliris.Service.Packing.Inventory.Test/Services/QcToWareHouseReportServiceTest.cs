using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Packaging;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.QcToWarehouseReport;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services
{
    public class QcToWarehouseReportServiceTest
    {
        private QcToWarehouseReportViewModel ViewModel
        {
            get
            {
                return new QcToWarehouseReportViewModel
                {
                    orderType = "a",
                    inputQuantitySolid = 0,
                    inputQuantityDyeing = 0,
                    inputQuantityPrinting = 0,
                    createdUtc = DateTime.UtcNow,
                };
            }
        }
       




        public QcToWarehouseReportService GetService(IServiceProvider serviceProvider)
        {
            return new QcToWarehouseReportService(serviceProvider);
        }
        public Mock<IServiceProvider> GetServiceProvider(IQcToWarehouseReportService repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IQcToWarehouseReportService)))
                .Returns(repository);

            return spMock;
        }
     
       

    


       

       

       
    }
}
