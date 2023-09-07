using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentCMTSalesReport;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CreditAdvice;
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

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.Monitoring
{
    public class GarmentCMTSalesServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingInvoiceRepository repository, IGarmentPackingListRepository plrepository, IGarmentShippingCreditAdviceRepository carepository, IGarmentShippingInvoiceItemRepository repositoryItem)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
               .Returns(plrepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentShippingCreditAdviceRepository)))
                .Returns(carepository);
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceItemRepository)))
                .Returns(repositoryItem);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentCMTSalesService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentCMTSalesService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success()
        {
            var model = new GarmentShippingInvoiceModel(1, "", DateTimeOffset.Now, "", "", 1, "A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "", 1, "", 1, "", DateTimeOffset.Now,
                                                        "", DateTimeOffset.Now, "", "", null, 1, 1, "", "", "", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null)
            {
                Id = 1
            };

            var model1 = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false, false, false, "")
            {
                Id = 1
            };

            var model2 = new GarmentShippingCreditAdviceModel(1, 1, "", DateTimeOffset.Now, 1, 1, 1, 1, "", "", "", true, "", 1, 1, "", DateTimeOffset.Now, DateTimeOffset.Now, "", 1, 1, 1, DateTimeOffset.Now, 1, 1, 1, 1, 1, 1, 1, "", 1, "", "", "", 1, "", "", "", 1, 1, 1, DateTimeOffset.Now, "", DateTimeOffset.Now, 1, "", DateTimeOffset.Now, 1, DateTimeOffset.Now, "", 1, 1);


            var model3 = new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "", "", "", "", "comodesc", "comodesc", "comodesc", 1, "", 1, 1, 1, "", 1, "C10", 1, 1)
            {
                GarmentShippingInvoiceId = 1
            };


            var repoMock = new Mock<IGarmentShippingInvoiceRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model1 }.AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingCreditAdviceRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCreditAdviceModel>() { model2 }.AsQueryable());


            var repoMock3 = new Mock<IGarmentShippingInvoiceItemRepository>();
            repoMock3.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceItemModel>() { model3 }.AsQueryable());


            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.SendAsync(HttpMethod.Get, It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { data = new List<GarmentCurrency> { new GarmentCurrency() { code = "usd" } } }))
                });

            var spMock = GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object, repoMock3.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);
            var service = GetService(spMock.Object);

            var result = service.GetReportData(model.BuyerAgentCode, DateTime.MinValue, DateTime.MaxValue, 0);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void GenerateExcel_Success()
        {
            var model = new GarmentShippingInvoiceModel(1, "", DateTimeOffset.Now, "", "", 1, "A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "", 1, "", 1, "", DateTimeOffset.Now,
                                                        "", DateTimeOffset.Now, "", "", null, 1, 1, "", "", "", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null)
            {
                Id = 1
            };

            var model1 = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false, false, false, "")
            {
                Id = 1
            };

            var model2 = new GarmentShippingCreditAdviceModel(1, 1, "", DateTimeOffset.Now, 1, 1, 1, 1, "", "", "", true, "", 1, 1, "", DateTimeOffset.Now, DateTimeOffset.Now, "", 1, 1, 1, DateTimeOffset.Now, 1, 1, 1, 1, 1, 1, 1, "", 1, "", "", "", 1, "", "", "", 1, 1, 1, DateTimeOffset.Now, "", DateTimeOffset.Now, 1, "", DateTimeOffset.Now, 1, DateTimeOffset.Now, "", 1, 1);


            var model3 = new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "", "", "", "", "comodesc", "comodesc", "comodesc", 1, "", 1, 1, 1, "", 1, "C10", 1, 1)
            {
                GarmentShippingInvoiceId = 1
            };


            var repoMock = new Mock<IGarmentShippingInvoiceRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model1 }.AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingCreditAdviceRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCreditAdviceModel>() { model2 }.AsQueryable());


            var repoMock3 = new Mock<IGarmentShippingInvoiceItemRepository>();
            repoMock3.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceItemModel>() { model3 }.AsQueryable());


            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.SendAsync(HttpMethod.Get, It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { data = new List<GarmentCurrency> { new GarmentCurrency() { code = "usd" } } }))
                });

            var spMock = GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object, repoMock3.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);
            var service = GetService(spMock.Object);

            var result = service.GenerateExcel(model.BuyerAgentCode, DateTime.MinValue, DateTime.MaxValue, 0);

            Assert.NotNull(result);
        }


        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var model = new GarmentShippingInvoiceModel(1, "", DateTimeOffset.Now, "", "", 1, "A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "", 1, "", 1, "", DateTimeOffset.Now,
                                                        "", DateTimeOffset.Now, "", "", null, 1, 1, "", "", "", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null)
            {
                Id = 1
            };

            var model1 = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false, false, false, "")
            {
                Id = 1
            };

            var model2 = new GarmentShippingCreditAdviceModel(1, 1, "", DateTimeOffset.Now, 1, 1, 1, 1, "", "", "", true, "", 1, 1, "", DateTimeOffset.Now, DateTimeOffset.Now, "", 1, 1, 1, DateTimeOffset.Now, 1, 1, 1, 1, 1, 1, 1, "", 1, "", "", "", 1, "", "", "", 1, 1, 1, DateTimeOffset.Now, "", DateTimeOffset.Now, 1, "", DateTimeOffset.Now, 1, DateTimeOffset.Now, "", 1, 1);


            var model3 = new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "", "", "", "", "comodesc", "comodesc", "comodesc", 1, "", 1, 1, 1, "", 1, "C10", 1, 1)
            {
                GarmentShippingInvoiceId = 1
            };


            var repoMock = new Mock<IGarmentShippingInvoiceRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model1 }.AsQueryable());

            var repoMock2 = new Mock<IGarmentShippingCreditAdviceRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCreditAdviceModel>() { model2 }.AsQueryable());


            var repoMock3 = new Mock<IGarmentShippingInvoiceItemRepository>();
            repoMock3.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceItemModel>() { model3 }.AsQueryable());


            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.SendAsync(HttpMethod.Get, It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { data = new List<GarmentCurrency> { new GarmentCurrency() { code = "usd" } } }))
                });

            var spMock = GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object, repoMock3.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);
            var service = GetService(spMock.Object);

            var result = service.GenerateExcel("n", null, null, 0);

            Assert.NotNull(result);
        }

    }
}
