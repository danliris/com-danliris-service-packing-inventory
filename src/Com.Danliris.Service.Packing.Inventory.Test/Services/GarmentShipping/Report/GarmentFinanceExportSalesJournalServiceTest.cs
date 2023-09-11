using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Report;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.Report
{
    public class GarmentFinanceExportSalesJournalServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingInvoiceRepository repository, IGarmentPackingListRepository plrepository)
        {
            var spMock = new Mock<IServiceProvider>();
            var httpClientService = new Mock<IHttpClientService>();
            HttpResponseMessage messageC = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            messageC.Content = new StringContent("{\"apiVersion\":\"1.0\",\"statusCode\":200,\"message\":\"Ok\",\"data\":{\"Rate\":14500.0,\"Uid\":\"no\",\"Date\":\"2018-10-20T17:00:00\",\"Code\":\"USD\"},\"info\":{\"count\":1,\"page\":1,\"size\":1,\"total\":1,\"order\":{\"Date\":\"desc\"},\"select\":[\"Rate\"]}}");

            var HttpClientService = new Mock<IHttpClientService>();
            HttpClientService
                .Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(messageC);

            //HttpClientService
            //    .Setup(x => x.GetAsync(It.IsRegex($"^master/garment-currencies/sales-debtor-currencies")))
            //    .ReturnsAsync(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            //    {
            //        Content = new StringContent(JsonConvert.SerializeObject(new
            //        {
            //            apiVersion = "1.0",
            //            statusCode = 200,
            //            message = "Ok",
            //            data = JsonConvert.SerializeObject(new GarmentCurrency { })
            //        }))
            //    });

            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
               .Returns(plrepository);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            //spMock
            //    .Setup(x => x.GetService(typeof(IHttpClientService)))
            //    .Returns(HttpClientService.Object);

            return spMock;
        }

        protected GarmentFinanceExportSalesJournalService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentFinanceExportSalesJournalService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success()
        {
            var model = new GarmentShippingInvoiceModel(1, "", DateTimeOffset.Now, "", "", 1, "A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "", 1, "", 1, "", DateTimeOffset.Now,
                                                        "", DateTimeOffset.Now, "", "", null, 1, 1, "", "", "", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null, 1, 1)
            {
                Id = 1
            };

            var model1 = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false, false, false, "")
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentShippingInvoiceRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model1 }.AsQueryable());

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.SendAsync(HttpMethod.Get, It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { data = new List<GarmentCurrency> { new GarmentCurrency() { code = "usd" } } }))
                });

            //var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);
            var spMock = GetServiceProvider(repoMock.Object, repoMock1.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);

            var service = GetService(spMock.Object);
            var result = service.GetReportData(model1.TruckingDate.Date, model1.TruckingDate.Date, 7);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GenerateExcel_Success()
        {
            var model = new GarmentShippingInvoiceModel(1, "", DateTimeOffset.Now, "", "", 1, "A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "", 1, "", 1, "", DateTimeOffset.Now,
                                                        "", DateTimeOffset.Now, "", "", null, 1, 1, "", "", "", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null, 1, 1)
            {
                Id = 1
            };

            var model1 = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false, false, false, "")
            {
                Id = 1
            };
            var repoMock = new Mock<IGarmentShippingInvoiceRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model1 }.AsQueryable());

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.SendAsync(HttpMethod.Get, It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { data = new List<GarmentCurrency> { new GarmentCurrency() { code = "usd" } } }))
                });

            //var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);
            var spMock = GetServiceProvider(repoMock.Object, repoMock1.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);

            var service = GetService(spMock.Object);

            var result = service.GenerateExcel(model1.TruckingDate.Date, model1.TruckingDate.Date, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var repoMock = new Mock<IGarmentShippingInvoiceRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>().AsQueryable());

            var repoMock1 = new Mock<IGarmentPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>().AsQueryable());

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.SendAsync(HttpMethod.Get, It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { data = new List<GarmentCurrency> { new GarmentCurrency() { code = "usd" } } }))
                });

            //var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);
            var spMock = GetServiceProvider(repoMock.Object, repoMock1.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);

            var service = GetService(spMock.Object);

            var result = service.GenerateExcel(DateTime.MinValue.Date, DateTime.MinValue.Date, 7);

            Assert.NotNull(result);
        }
    }
}
