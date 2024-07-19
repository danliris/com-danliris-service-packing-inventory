using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentDetailOmzetByUnitReport;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentReceiptSubconOmzetByUnitReport;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentReceiptSubconPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentReceiptSubconPackingList;
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
    public class GarmentReceiptSubconOmzetByUnitReportServiceServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentReceiptSubconPackingListRepository plrepository, IGarmentReceiptSubconPackingListItemRepository repositoryItem)
        {
            var spMock = new Mock<IServiceProvider>();

            spMock.Setup(s => s.GetService(typeof(IGarmentReceiptSubconPackingListRepository)))
               .Returns(plrepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentReceiptSubconPackingListItemRepository)))
                .Returns(repositoryItem);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentReceiptSubconOmzetByUnitReportService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentReceiptSubconOmzetByUnitReportService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success()
        {
            var items = new List<GarmentReceiptSubconPackingListItemModel>
                {

                     new GarmentReceiptSubconPackingListItemModel("2310001", "SC001", 1, "BUYER BRAND 1", 1, "MS", "MENS SHIRT", "MS 001", "MKT 01", 20, 1, "PCS", 40000, 40000, 0, 0, 800000, "IDR", 1, "C1A", "ART 01", "ORDER 01", "DESC 01", "DESC 01", "REMARK", "PO001", null, 20)
                         {
                           PackingListId = 1
                         },
                     new GarmentReceiptSubconPackingListItemModel("232001", "SC002", 1, "BUYER BRAND 2", 2, "LB", "LADIES BLOUSE", " LB 001", "MKT 01", 15, 1, "PCS", 50000, 50000, 0, 0, 750000, "IDR", 2, "C1B", "ART 02", "ORDER 02", "DESC 01", "DESC 01", "REMARK", "PO002", null, 15)
                            {
                           PackingListId = 1
                         },
                };

            var model = new GarmentReceiptSubconPackingListModel(1, "23/LBJ/0001", DateTimeOffset.Now, 1, "SC001", 1, "LBJ", "PENJUALAN BARANG JADI", 1, "BYR001", "BUYER 001", "NPWP BUYER", "PAYMENT TERM", true, true, items, 10, 10, 10, 1, true, true, "", DateTimeOffset.Now, true, "MD01", DateTimeOffset.Now, 15000, "", true, "SHP01", DateTimeOffset.Now, "", "")                                                                                                                                                                                                                                            //   string rejectReason, string rejectTo)
            {
                Id = 1
            };

            var repoMock1 = new Mock<IGarmentReceiptSubconPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentReceiptSubconPackingListModel>() { model }.AsQueryable());

            var repoMock2 = new Mock<IGarmentReceiptSubconPackingListItemRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(items.AsQueryable());

            var spMock = GetServiceProvider(repoMock1.Object, repoMock2.Object);

            var service = GetService(spMock.Object);

            var result = service.GetReportData(null, DateTime.MinValue, DateTime.MaxValue, 0);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void GenerateExcel_Success()
        {
            var items = new List<GarmentReceiptSubconPackingListItemModel>
                {

                     new GarmentReceiptSubconPackingListItemModel("2310001", "SC001", 1, "BUYER BRAND 1", 1, "MS", "MENS SHIRT", "MS 001", "MKT 01", 20, 1, "PCS", 40000, 40000, 0, 0, 800000, "IDR", 1, "C1A", "ART 01", "ORDER 01", "DESC 01", "DESC 01", "REMARK", "PO001", null, 20)
                         {
                           PackingListId = 1
                         },
                     new GarmentReceiptSubconPackingListItemModel("232001", "SC002", 1, "BUYER BRAND 2", 2, "LB", "LADIES BLOUSE", " LB 001", "MKT 01", 15, 1, "PCS", 50000, 50000, 0, 0, 750000, "IDR", 2, "C1B", "ART 02", "ORDER 02", "DESC 01", "DESC 01", "REMARK", "PO002", null, 15)
                            {
                           PackingListId = 1
                         },
                };

            var model = new GarmentReceiptSubconPackingListModel(1, "23/LBJ/0001", DateTimeOffset.Now, 1, "SC001", 1, "LBJ", "PENJUALAN BARANG JADI", 1, "BYR001", "BUYER 001", "NPWP BUYER", "PAYMENT TERM", true, true, items, 10, 10, 10, 1, true, true, "", DateTimeOffset.Now, true, "MD01", DateTimeOffset.Now, 15000, "", true, "SHP01", DateTimeOffset.Now, "", "")                                                                                                                                                                                                                                            //   string rejectReason, string rejectTo)
            {
                Id = 1
            };

            var repoMock1 = new Mock<IGarmentReceiptSubconPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentReceiptSubconPackingListModel>() { model }.AsQueryable());

            var repoMock2 = new Mock<IGarmentReceiptSubconPackingListItemRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(items.AsQueryable());

            var spMock = GetServiceProvider(repoMock1.Object, repoMock2.Object);

            var service = GetService(spMock.Object);

            var result = service.GenerateExcel(null, DateTime.MinValue, DateTime.MaxValue, 0);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {           
            var repoMock1 = new Mock<IGarmentReceiptSubconPackingListRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentReceiptSubconPackingListModel>().AsQueryable());

            var repoMock2 = new Mock<IGarmentReceiptSubconPackingListItemRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentReceiptSubconPackingListItemModel>().AsQueryable());

            var spMock = GetServiceProvider(repoMock1.Object, repoMock2.Object);

            var service = GetService(spMock.Object);

            var result = service.GenerateExcel(null, DateTime.MinValue, DateTime.MaxValue, 0);

            Assert.NotNull(result);
        }
    }
}
