using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearBuyer;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.PackingList;
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

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.Monitoring
{
    public class OmzetYearBuyerServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingInvoiceRepository invoiceRepository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceRepository)))
                .Returns(invoiceRepository);
            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected OmzetYearBuyerService GetService(IServiceProvider serviceProvider)
        {
            return new OmzetYearBuyerService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success()
        {
            var invoiceItemModels = new HashSet<GarmentShippingInvoiceItemModel> {
                new GarmentShippingInvoiceItemModel("ro", "scno", 1, "buyerbrandname", 1, 1, "comocode", "comoname", "comodesc", 1, "pcs", 10, 10, 100, "usd", 1, "unitcode", 3)
                {
                    Id = 1
                }
            };
            var invoiceModel = new GarmentShippingInvoiceModel(1, "invoiceno", DateTimeOffset.Now, "from", "to", 1, "buyercode", "buyername", "consignee", "lcno", "issuedby", 1, "sectioncode", "shippingper", DateTimeOffset.Now, "confNo", 1, "staff", 1, "cottn", 1, "mandiri", 10, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", invoiceItemModels, 1000, "23", "dsdsds", "memo", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 100000, "aa")
            {
                Id = 1,
            };

            var invoiceRepoMock = new Mock<IGarmentShippingInvoiceRepository>();
            invoiceRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { invoiceModel }.AsQueryable());

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.SendAsync(HttpMethod.Get, It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { data = new List<GarmentCurrency> { new GarmentCurrency() } }))
                });

            var spMock = GetServiceProvider(invoiceRepoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);

            var service = GetService(spMock.Object);

            var result = service.GetReportData(invoiceModel.InvoiceDate.Year);

            Assert.NotEmpty(result.Items);
        }

        [Fact]
        public void GenerateExcel_Success()
        {
            var invoiceItemModels = new HashSet<GarmentShippingInvoiceItemModel> {
                new GarmentShippingInvoiceItemModel("ro", "scno", 1, "buyerbrandname", 1, 1, "comocode", "comoname", "comodesc", 1, "pcs", 10, 10, 100, "usd", 1, "unitcode", 3)
                {
                    Id = 1
                }
            };
            var invoiceModel = new GarmentShippingInvoiceModel(1, "invoiceno", DateTimeOffset.Now, "from", "to", 1, "buyercode", "buyername", "consignee", "lcno", "issuedby", 1, "sectioncode", "shippingper", DateTimeOffset.Now, "confNo", 1, "staff", 1, "cottn", 1, "mandiri", 10, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", invoiceItemModels, 1000, "23", "dsdsds", "memo", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 100000, "aa")
            {
                Id = 1,
            };

            var invoiceRepoMock = new Mock<IGarmentShippingInvoiceRepository>();
            invoiceRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { invoiceModel }.AsQueryable());

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.SendAsync(HttpMethod.Get, It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { data = new List<GarmentCurrency> { new GarmentCurrency() } }))
                });

            var spMock = GetServiceProvider(invoiceRepoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);

            var service = GetService(spMock.Object);

            var result = service.GenerateExcel(invoiceModel.InvoiceDate.Year);

            Assert.NotNull(result.Data);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var invoiceRepoMock = new Mock<IGarmentShippingInvoiceRepository>();
            invoiceRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>().AsQueryable());

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.SendAsync(HttpMethod.Get, It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.InternalServerError));

            var spMock = GetServiceProvider(invoiceRepoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);

            var service = GetService(spMock.Object);

            var result = service.GenerateExcel(2020);

            Assert.NotNull(result.Data);
        }
    }
}
