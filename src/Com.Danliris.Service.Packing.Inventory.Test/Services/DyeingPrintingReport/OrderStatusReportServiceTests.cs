using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.OrderStatusReport;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.DyeingPrintingReport;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.DyeingPrintingReport
{
    public class OrderStatusReportServiceTests
    {
        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaInputProductionOrderRepository inRepository, IDyeingPrintingAreaOutputProductionOrderRepository outRepository)
        {
            var httpClientService = new Mock<IHttpClientService>();
            HttpResponseMessage message = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            message.Content = new StringContent("{\"apiVersion\":\"1.0\",\"statusCode\":200,\"message\":\"Ok\",\"data\":[{\"Id\":7,\"noorder\":\"OrderNo\",\"qtyin\":13700.0,\"name\":\"FABRIC\",\"date\":\"2018/10/20\"}],\"info\":{\"count\":1,\"page\":1,\"size\":1,\"total\":2,\"order\":{\"date\":\"desc\"},\"select\":[\"Id\",\"noorder\",\"qtyin\",\"date\"]}}");
            
            httpClientService
                .Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(message);

            httpClientService
                .Setup(x => x.SendAsync(HttpMethod.Get, It.Is<string>(s => s.Contains("GetProductionOsthoffStatusOrder")), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(new OrderStatusReportOsthoffDataUtil().GetResultFormatterOkString()) });
            httpClientService
                .Setup(x => x.SendAsync(HttpMethod.Get, It.Is<string>(s => s.Contains("sales/production-orders/for-status-order-report")), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(new OrderStatusReportSPPDataUtil().GetResultFormatterOkString()) });


            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaInputProductionOrderRepository)))
                .Returns(inRepository);

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
               .Returns(outRepository);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            spMock
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(httpClientService.Object);
            return spMock;
        }

        protected OrderStatusReportService GetService(IServiceProvider serviceProvider)
        {
            return new OrderStatusReportService(serviceProvider);
        }

        [Fact]
        public async void GetReportData_Success()
        {
            var outputProductionOrderModel = new DyeingPrintingAreaOutputProductionOrderModel("INSPECTION MATERIAL", "", false, 1, "no", "", 1, "", "", "", "", "", "", "", "", "", "",
                "", 1, 1, 1, "", 1, "", 1, "", "", "", "", "", 1, "", 1, "", 1, 1, "", false, "", DateTimeOffset.Now, DateTimeOffset.Now, "", 1, "", ""/*, DateTime.Now*/)
            { DyeingPrintingAreaOutputId = 1 };
            var inputProductionOrderModel = new DyeingPrintingAreaInputProductionOrderModel("INSPECTION MATERIAL", 1, "", "", "", "", "", "", "", "", "", "", 0, true, 1) { DyeingPrintingAreaInputId = 1 };

            var repoOutMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoOutMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputProductionOrderModel>() { outputProductionOrderModel }.AsQueryable());

            var repoMock1 = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel>() { inputProductionOrderModel }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock1.Object, repoOutMock.Object).Object);

            var result = await service.GetReportData(DateTime.Now.AddDays(-30), DateTime.Now, outputProductionOrderModel.ProcessTypeId);

            Assert.NotEmpty(result);
        }


        [Fact]
        public async void GenerateExcel_Success()
        {
            var outputProductionOrderModel = new DyeingPrintingAreaOutputProductionOrderModel("INSPECTION MATERIAL", "", false, 1, "no", "", 1, "", "", "", "", "", "", "", "", "", "",
                "", 1, 1, 1, "", 1, "", 1, "", "", "", "", "", 1, "", 1, "", 1, 1, "", false, "", DateTimeOffset.Now, DateTimeOffset.Now, "", 1, "", ""/*, DateTime.Now*/)
            { DyeingPrintingAreaOutputId = 1 };
            var inputProductionOrderModel = new DyeingPrintingAreaInputProductionOrderModel("INSPECTION MATERIAL", 1, "", "", "", "", "", "", "", "", "", "", 0, true, 1) { DyeingPrintingAreaInputId = 1 };

            var repoOutMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoOutMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputProductionOrderModel>() { outputProductionOrderModel }.AsQueryable());

            var repoMock1 = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel>() { inputProductionOrderModel }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock1.Object, repoOutMock.Object).Object);


            var result = await service.GenerateExcel(DateTime.Now.AddDays(-30), DateTime.Now, outputProductionOrderModel.ProcessTypeId);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var repoOutMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();

            repoOutMock.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaOutputProductionOrderModel>().AsQueryable());

            var repoMock1 = new Mock<IDyeingPrintingAreaInputProductionOrderRepository>();

            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<DyeingPrintingAreaInputProductionOrderModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock1.Object, repoOutMock.Object).Object);

            var result = service.GenerateExcel(DateTime.Now, DateTime.Now, 7);

            Assert.NotNull(result);
        }
    }
}
