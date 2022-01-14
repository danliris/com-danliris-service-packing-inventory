using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentRecapOmzetReport;
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
    public class GarmentRecapOmzetReportServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingInvoiceRepository repository, IGarmentPackingListRepository plrepository, IGarmentShippingInvoiceItemRepository repositoryItem)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
               .Returns(plrepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceItemRepository)))
                .Returns(repositoryItem);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentRecapOmzetReportService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentRecapOmzetReportService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success()
        {
            var items = new List<GarmentShippingInvoiceItemModel>
            {
                new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "MENS SHIRT", "", "", "comodesc", "comodesc", "comodesc", 1, "PCS", 1, 1, 1, "USD", 1, "C10", 1, 1)
                    {
                       GarmentShippingInvoiceId = 1
                    },
                new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "LADIES BLOUSE", "", "", "comodesc", "comodesc", "comodesc", 1, "SETS", 1, 1, 1, "USD", 1, "C10", 1, 1)
                    {
                       GarmentShippingInvoiceId = 1
                    },
                 new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "BOYS SHIRT", "", "", "comodesc", "comodesc", "comodesc", 1, "PCS", 1, 1, 1, "USD", 1, "C10", 1, 1)
                    {
                       GarmentShippingInvoiceId = 1
                    },
            };
     
            var model = new GarmentShippingInvoiceModel(1, "DL/219999", DateTimeOffset.Now, "", "", 1, "A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "", 1, "", 1, "123456", DateTimeOffset.Now,
                                                        "", DateTimeOffset.Now, "", "", items, 1, "", "", "", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null)
            {
                Id = 1
            };

            var model1 = new GarmentPackingListModel("DL/219999", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, true, true, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false)
            {
                Id = 1
            };
            
            var repoMock = new Mock<IGarmentShippingInvoiceRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model1 }.AsQueryable());

            var repoMock3 = new Mock<IGarmentShippingInvoiceItemRepository>();
            repoMock3.Setup(s => s.ReadAll())
                .Returns(items.AsQueryable());


            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { data = new GarmentDetailCurrency() { code = "USD" } }))
                });

            var spMock = GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock3.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);
      
            var service = GetService(spMock.Object);

            var result = service.GetReportData(DateTime.MinValue, DateTime.MaxValue, 0);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void GetReportData_InternalServer_Error()
        {
            var items = new List<GarmentShippingInvoiceItemModel>
            {
                new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "MENS SHIRT", "", "", "comodesc", "comodesc", "comodesc", 1, "PCS", 1, 1, 1, "USD", 1, "C10", 1, 1)
                    {
                       GarmentShippingInvoiceId = 1
                    },
                new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "LADIES BLOUSE", "", "", "comodesc", "comodesc", "comodesc", 1, "SETS", 1, 1, 1, "USD", 1, "C10", 1, 1)
                    {
                       GarmentShippingInvoiceId = 1
                    },
                 new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "BOYS SHIRT", "", "", "comodesc", "comodesc", "comodesc", 1, "PCS", 1, 1, 1, "USD", 1, "C10", 1, 1)
                    {
                       GarmentShippingInvoiceId = 1
                    },
            };

            var model = new GarmentShippingInvoiceModel(1, "DL/219999", DateTimeOffset.Now, "", "", 1, "A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "", 1, "", 1, "123456", DateTimeOffset.Now,
                                                        "", DateTimeOffset.Now, "", "", items, 1, "", "", "", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null)
            {
                Id = 1
            };

            var model1 = new GarmentPackingListModel("DL/219999", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, true, true, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false)
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentShippingInvoiceRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model1 }.AsQueryable());

            var repoMock3 = new Mock<IGarmentShippingInvoiceItemRepository>();
            repoMock3.Setup(s => s.ReadAll())
                .Returns(items.AsQueryable());


            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.InternalServerError));

            var spMock = GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock3.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);

            var service = GetService(spMock.Object);

            var result = service.GetReportData(DateTime.MinValue, DateTime.MaxValue, 0);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void GenerateExcel_Success()
        {
            var items = new List<GarmentShippingInvoiceItemModel>
            {
                new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "MENS SHIRT", "", "", "comodesc", "comodesc", "comodesc", 1, "PCS", 1, 1, 1, "USD", 1, "C10", 1, 1)
                    {
                       GarmentShippingInvoiceId = 1
                    },
                new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "LADIES BLOUSE", "", "", "comodesc", "comodesc", "comodesc", 1, "SETS", 1, 1, 1, "USD", 1, "C10", 1, 1)
                    {
                       GarmentShippingInvoiceId = 1
                    },
                 new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "BOYS SHIRT", "", "", "comodesc", "comodesc", "comodesc", 1, "PCS", 1, 1, 1, "USD", 1, "C10", 1, 1)
                    {
                       GarmentShippingInvoiceId = 1
                    },
                 new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "BOYS SHIRT", "", "", "comodesc", "comodesc", "comodesc", 1, "SETS", 1, 1, 1, "USD", 1, "C10", 1, 1)
                    {
                       GarmentShippingInvoiceId = 2
                    },
                 new GarmentShippingInvoiceItemModel("", "", 1, "", 1, 1, "BOYS SHIRT", "", "", "comodesc", "comodesc", "comodesc", 1, "PCS", 1, 1, 1, "USD", 1, "C10", 1, 1)
                    {
                       GarmentShippingInvoiceId = 2
                    },
            };

            var model = new List<GarmentShippingInvoiceModel>
            {
                new GarmentShippingInvoiceModel(1, "DL/219999", DateTimeOffset.Now, "", "", 1, "A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "", 1, "", 1, "123456", DateTimeOffset.Now,
                                                "", DateTimeOffset.Now, "", "", items, 1, "", "", "", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null)
                    {
                      Id = 1
                    },
                new GarmentShippingInvoiceModel(2, "DL/218888", DateTimeOffset.Now, "", "", 1, "A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "", 1, "", 1, "123456", DateTimeOffset.Now,
                                                "", DateTimeOffset.Now, "", "", items, 1, "", "", "", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null)
                    {
                      Id = 2
                    },

            };

            var model1 = new List<GarmentPackingListModel>
            {
                new GarmentPackingListModel("DL/219999", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, true, true, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false)
                    {
                       Id = 1
                    },
                new GarmentPackingListModel("DL/218888", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, true, true, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false)
                    {
                       Id = 2
                    },
            };

        var repoMock = new Mock<IGarmentShippingInvoiceRepository>();

            repoMock.Setup(s => s.ReadAll())
                .Returns(model.AsQueryable());

            var repoMock1 = new Mock<IGarmentPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(model1.AsQueryable());
            var repoMock3 = new Mock<IGarmentShippingInvoiceItemRepository>();
            repoMock3.Setup(s => s.ReadAll())
                .Returns(items.AsQueryable());

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { data = new GarmentDetailCurrency() { code = "USD" } }))
                });

            var spMock = GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock3.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);
            var service = GetService(spMock.Object);

            var result = service.GenerateExcel(DateTime.MinValue, DateTime.MaxValue, 0);

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

            var repoMock3 = new Mock<IGarmentShippingInvoiceItemRepository>();

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { data = new GarmentDetailCurrency() { code = "usd" } }))
                });

            var spMock = GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock3.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);
            var service = GetService(spMock.Object);

            var result = service.GenerateExcel(null, null, 0);

            Assert.NotNull(result);
        }

    }
}
