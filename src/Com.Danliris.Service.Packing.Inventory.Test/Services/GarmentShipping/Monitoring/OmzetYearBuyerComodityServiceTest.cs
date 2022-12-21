using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.OmzetYearBuyerComodity;
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
    public class OmzetYearBuyerComodityServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingInvoiceRepository invoiceRepository, IGarmentShippingInvoiceItemRepository invoiceitemRepository, IGarmentPackingListRepository packingListRepository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceRepository)))
                .Returns(invoiceRepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceItemRepository)))
                .Returns(invoiceitemRepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
                .Returns(packingListRepository);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected OmzetYearBuyerComodityService GetService(IServiceProvider serviceProvider)
        {
            return new OmzetYearBuyerComodityService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success()
        {

            var packinglistmodel = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "USA", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, true, true, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", true, true, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false, false, false, "")
            {
                Id = 1
            };

            var invoiceItemModels = new List<GarmentShippingInvoiceItemModel>
                {
                new GarmentShippingInvoiceItemModel("ro", "scno", 1, "buyerbrandname", 10, 1, "comocode", "comoname", "comodesc", "marketing", "comodesc", "comodesc", "comodesc", 1, "PCS", 10, 10, 100, "usd", 1, "unitcode", 3, 1)
                {
                    GarmentShippingInvoiceId = 1
                },
                new GarmentShippingInvoiceItemModel("ro", "scno", 1, "buyerbrandname", 15, 2, "comocode2", "comoname2", "comodesc2", "marketing", "comodesc", "comodesc", "comodesc", 2, "SETS", 20, 20, 300, "usd", 1, "unitcode", 3, 2)
                {
                    GarmentShippingInvoiceId = 1
                },
                new GarmentShippingInvoiceItemModel("ro", "scno", 2, "buyerbrandname2", 10, 1, "comocode", "comoname", "comodesc", "marketing", "comodesc", "comodesc", "comodesc", 1, "PCS", 10, 10, 100, "usd", 1, "unitcode", 3, 3)
                {
                    GarmentShippingInvoiceId = 1
                },
                new GarmentShippingInvoiceItemModel("ro", "scno", 2, "buyerbrandname2", 15, 2, "comocode2", "comoname2", "comodesc2", "marketing", "comodesc", "comodesc", "comodesc", 2, "SETS", 20, 20, 300, "usd", 1, "unitcode", 0, 4)
                {
                    GarmentShippingInvoiceId = 1
                }

            };
            var invoiceUnitModels = new HashSet<GarmentShippingInvoiceUnitModel> {
                new GarmentShippingInvoiceUnitModel(1,"unitcode", 3,1)
                {
                    Id = 1
                }
            };
            var invoiceModel = new GarmentShippingInvoiceModel(1, "invoiceno", DateTimeOffset.Now, "from", "to", 1, "buyercode", "buyername", "consignee", "lcno", "issuedby", 1, "sectioncode", "shippingper", DateTimeOffset.Now, "confNo", 1, "staff", 1, "cottn", 1, "mandiri", 10, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", "", invoiceItemModels, 1000, 1000, "23", "dsdsds", "memo", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 100000, "aa", "aa", invoiceUnitModels)
            {
                Id = 1,
            };


            var repoMock = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { invoiceModel }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingInvoiceItemRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(invoiceItemModels.AsQueryable());

            var repoMock2 = new Mock<IGarmentPackingListRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { packinglistmodel }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object).Object);

            var result = service.GetReportData(packinglistmodel.TruckingDate.Year);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void GenerateExcel_Success()
        {

            var packinglistmodel = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "USA", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, true, true, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", true, true, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false, false, false, "")
            {
                Id = 1
            };

            var invoiceItemModels = new HashSet<GarmentShippingInvoiceItemModel>
            {
                new GarmentShippingInvoiceItemModel("ro", "scno", 1, "buyerbrandname", 10, 1, "comocode", "comoname", "comodesc", "marketing", "comodesc", "comodesc", "comodesc", 1, "PCS", 10, 10, 100, "usd", 1, "unitcode", 3, 1)
                {
                    GarmentShippingInvoiceId = 1
                },
                new GarmentShippingInvoiceItemModel("ro", "scno", 1, "buyerbrandname", 15, 2, "comocode2", "comoname2", "comodesc2", "marketing", "comodesc", "comodesc", "comodesc", 2, "SETS", 20, 20, 300, "usd", 1, "unitcode", 3, 2)
                {
                    GarmentShippingInvoiceId = 1
                },
                new GarmentShippingInvoiceItemModel("ro", "scno", 2, "buyerbrandname2", 10, 1, "comocode", "comoname", "comodesc", "marketing", "comodesc", "comodesc", "comodesc", 1, "PCS", 10, 10, 100, "usd", 1, "unitcode", 3, 3)
                {
                    GarmentShippingInvoiceId = 1
                },
                new GarmentShippingInvoiceItemModel("ro", "scno", 2, "buyerbrandname2", 15, 2, "comocode2", "comoname2", "comodesc2", "marketing", "comodesc", "comodesc", "comodesc", 2, "SETS", 20, 20, 300, "usd", 1, "unitcode", 0, 4)
                {
                    GarmentShippingInvoiceId = 1
                }
            };
            var invoiceUnitModels = new HashSet<GarmentShippingInvoiceUnitModel> {
                new GarmentShippingInvoiceUnitModel(1,"unitcode", 3,1)
                {
                    Id = 1
                }
            };
            var invoiceModel = new GarmentShippingInvoiceModel(1, "invoiceno", DateTimeOffset.Now, "from", "to", 1, "buyercode", "buyername", "consignee", "lcno", "issuedby", 1, "sectioncode", "shippingper", DateTimeOffset.Now, "confNo", 1, "staff", 1, "cottn", 1, "mandiri", 10, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", "", invoiceItemModels, 1000, 1000, "23", "dsdsds", "memo", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 100000, "aa", "aa", invoiceUnitModels)
            {
                Id = 1,
            };

            var repoMock = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { invoiceModel }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingInvoiceItemRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(invoiceItemModels.AsQueryable());

            var repoMock2 = new Mock<IGarmentPackingListRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { packinglistmodel }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object).Object);

            var result = service.GetReportData(packinglistmodel.TruckingDate.Year);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var repoMock = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>().AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingInvoiceItemRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceItemModel>().AsQueryable());

            var repoMock2 = new Mock<IGarmentPackingListRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object).Object);

            var result = service.GenerateExcel(2099);

            Assert.NotNull(result);
        }
    }
}
