using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentDebitNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
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
    public class GarmentDebitNoteMonitoringServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingNoteRepository repository, IGarmentShippingNoteItemRepository itemRepository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingNoteRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IGarmentShippingNoteItemRepository)))
                .Returns(itemRepository);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentDebitNoteMonitoringService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentDebitNoteMonitoringService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success()
        {
            var model = new GarmentShippingNoteModel(GarmentShippingNoteTypeEnum.DN, "", DateTimeOffset.Now, 1, "A99", "", "", "", DateTimeOffset.Now, 1, "", "", "", 1, null);

            var model1 = new GarmentShippingNoteItemModel("", 1, "", 1);

            var repoMock = new Mock<IGarmentShippingNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingNoteItemRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteItemModel>() { model1 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);

            var result = service.GetReportData(model.BuyerCode, DateTime.MinValue, DateTime.MaxValue, 0);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GenerateExcel_Success()
        {
            var model = new GarmentShippingNoteModel(GarmentShippingNoteTypeEnum.DN, "", DateTimeOffset.Now, 1, "A99", "", "", "", DateTimeOffset.Now, 1, "", "", "", 1, null);

            var model1 = new GarmentShippingNoteItemModel("", 1, "", 1);

            var repoMock = new Mock<IGarmentShippingNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingNoteItemRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteItemModel>() { model1 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);

            var result = service.GenerateExcel(model.BuyerCode, DateTime.MinValue, DateTime.MaxValue, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var repoMock = new Mock<IGarmentShippingNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteModel>().AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingNoteItemRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteItemModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object).Object);

            var result = service.GenerateExcel(null, null, null, 0);

            Assert.NotNull(result);
        }
        //
        [Fact]
        public void GetReportData_MII_Success()
        {
            var model = new GarmentShippingNoteModel(GarmentShippingNoteTypeEnum.DN, "", DateTimeOffset.Now, 1, "A99", "", "", "", DateTimeOffset.Now, 1, "", "USD", "", 1, null);

            var model1 = new GarmentShippingNoteItemModel("", 1, "USD", 1);

            var repoMock = new Mock<IGarmentShippingNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingNoteItemRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteItemModel>() { model1 }.AsQueryable());

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
            var result = service.GetReportDataMII(DateTime.MinValue, DateTime.MaxValue, 0);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void GenerateExcel_MII_Success()
        {
            var model = new GarmentShippingNoteModel(GarmentShippingNoteTypeEnum.DN, "", DateTimeOffset.Now, 1, "A99", "", "", "", DateTimeOffset.Now, 1, "", "USD", "", 1, null);

            var model1 = new GarmentShippingNoteItemModel("", 1, "USD", 1);

            var repoMock = new Mock<IGarmentShippingNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingNoteItemRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteItemModel>() { model1 }.AsQueryable());

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

            var result = service.GenerateExcelMII(DateTime.MinValue, DateTime.MaxValue, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_MII_Empty_Success()
        {
            var repoMock = new Mock<IGarmentShippingNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteModel>().AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingNoteItemRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteItemModel>().AsQueryable());

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

            var result = service.GenerateExcelMII(null, null, 7);

            Assert.NotNull(result);
        }
    }
}
