using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShippingNoteCreditAdvice4MII;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNoteCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShipingNoteCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingNote;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.Monitoring
{
    public class GarmentShippingNoteCreditAdviceMIIMonitoringServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingNoteCreditAdviceRepository carepository, IGarmentShippingNoteRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingNoteCreditAdviceRepository)))
                .Returns(carepository);
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingNoteRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentShippingNoteCreditAdviceMIIMonitoringService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingNoteCreditAdviceMIIMonitoringService(serviceProvider);
        }

        [Fact]
        public void GetReportData_DN_Success()
        {
            var model = new GarmentShippingNoteCreditAdviceModel(1, "NOTA DEBIT", "DN230001", DateTimeOffset.Now, "", "", 1, 1, 0, DateTimeOffset.Now, 1, 1, "", "", "", 1, "", "", "", 1, 1, 1, 1, DateTimeOffset.Now, "");

            var model1 = new GarmentShippingNoteModel(GarmentShippingNoteTypeEnum.DN, "DN230001", DateTimeOffset.Now, 1, "", "", "", "", DateTimeOffset.Now, 1, "", "", "", 1, 1, null)
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentShippingNoteCreditAdviceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteCreditAdviceModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingNoteRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteModel>() { model1 }.AsQueryable());

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.SendAsync(HttpMethod.Get, It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { data = new List<GarmentCurrency> { new GarmentCurrency() { code = "usd" } } }))
                });

            var spMock = GetServiceProvider(repoMock.Object, repoMock1.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);

            var service = GetService(spMock.Object);

            var result = service.GetReportData(DateTime.MinValue, DateTime.MaxValue, 0);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void GetReportData_CN_Success()
        {
            var model = new GarmentShippingNoteCreditAdviceModel(1, "NOTA KREDIT", "CN230001", DateTimeOffset.Now, "", "", 1, 1, 0, DateTimeOffset.Now, 1, 1, "", "", "", 1, "", "", "", 1, 1, 1, 1, DateTimeOffset.Now, "");

            var model1 = new GarmentShippingNoteModel(GarmentShippingNoteTypeEnum.DN, "CN230001", DateTimeOffset.Now, 1, "", "", "", "", DateTimeOffset.Now, 1, "", "", "", 1, 1, null)
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentShippingNoteCreditAdviceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteCreditAdviceModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingNoteRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteModel>() { model1 }.AsQueryable());

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.SendAsync(HttpMethod.Get, It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { data = new List<GarmentCurrency> { new GarmentCurrency() { code = "usd" } } }))
                });

            var spMock = GetServiceProvider(repoMock.Object, repoMock1.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);

            var service = GetService(spMock.Object);

            var result = service.GetReportData(DateTime.MinValue, DateTime.MaxValue, 0);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void GenerateExcel_DN_Success()
        {
            var model = new GarmentShippingNoteCreditAdviceModel(1, "NOTA DEBIT", "DN230001", DateTimeOffset.Now, "", "", 1, 1, 0, DateTimeOffset.Now, 1, 1, "", "", "", 1, "", "", "", 1, 1, 1, 1, DateTimeOffset.Now, "");

            var model1 = new GarmentShippingNoteModel(GarmentShippingNoteTypeEnum.DN, "DN230001", DateTimeOffset.Now, 1, "", "", "", "", DateTimeOffset.Now, 1, "", "", "", 1, 1, null)
            {
                Id = 1
            };


            var repoMock = new Mock<IGarmentShippingNoteCreditAdviceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteCreditAdviceModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingNoteRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteModel>() { model1 }.AsQueryable());

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.SendAsync(HttpMethod.Get, It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { data = new List<GarmentCurrency> { new GarmentCurrency() { code = "usd" } } }))
                });

            var spMock = GetServiceProvider(repoMock.Object, repoMock1.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);

            var service = GetService(spMock.Object);

            var result = service.GenerateExcel(DateTime.MinValue, DateTime.MaxValue, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_CN_Success()
        {
            var model = new GarmentShippingNoteCreditAdviceModel(1, "NOTA KREDIT", "CN230001", DateTimeOffset.Now, "", "", 1, 1, 0, DateTimeOffset.Now, 1, 1, "", "", "", 1, "", "", "", 1, 1, 1, 1, DateTimeOffset.Now, "");

            var model1 = new GarmentShippingNoteModel(GarmentShippingNoteTypeEnum.DN, "CN230001", DateTimeOffset.Now, 1, "", "", "", "", DateTimeOffset.Now, 1, "", "", "", 1, 1, null)
            {
                Id = 1
            };


            var repoMock = new Mock<IGarmentShippingNoteCreditAdviceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteCreditAdviceModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingNoteRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteModel>() { model1 }.AsQueryable());

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.SendAsync(HttpMethod.Get, It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { data = new List<GarmentCurrency> { new GarmentCurrency() { code = "usd" } } }))
                });

            var spMock = GetServiceProvider(repoMock.Object, repoMock1.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);

            var service = GetService(spMock.Object);

            var result = service.GenerateExcel(DateTime.MinValue, DateTime.MaxValue, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var repoMock = new Mock<IGarmentShippingNoteCreditAdviceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteCreditAdviceModel>().AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingNoteRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteModel>().AsQueryable());

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.SendAsync(HttpMethod.Get, It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.InternalServerError));

            var spMock = GetServiceProvider(repoMock.Object, repoMock1.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);

            var service = GetService(spMock.Object);

            var result = service.GenerateExcel(null, null, 7);

            Assert.NotNull(result);
        }
    }
}
