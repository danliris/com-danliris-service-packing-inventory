using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentShippingInvoice
{
    public class GarmentShipingInvoicePackingListServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingInvoiceRepository invrepository, IGarmentPackingListRepository plrepository)

        {
            var spMock = new Mock<IServiceProvider>();

            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceRepository)))
              .Returns(invrepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
                .Returns(plrepository);



            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentShippingInvoiceService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingInvoiceService(serviceProvider);
        }

        [Fact]
        public void ReadShippingPackingListNow()
        {

            var model1 = new GarmentShippingInvoiceModel(1, "", DateTimeOffset.Now, "", "", 1, "A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "",
                                                      1, "", 1, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", "", null, 1, "", "", "",
                                                      false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null)
            {
                Id = 1
            };

            var model2 = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "B10", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, true, true, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", true, true, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false, false, false, "")
            {
                Id = 1
            };


            var repoMock1 = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model1 }.AsQueryable());

            var repoMock2 = new Mock<IGarmentPackingListRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model2 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock1.Object, repoMock2.Object).Object);

            var result = service.ReadShippingPackingListNow(DateTimeOffset.Now.Month, DateTimeOffset.Now.Year);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void ReadShippingPackingList()
        {

            var model1 = new GarmentShippingInvoiceModel(1, "", DateTimeOffset.Now, "", "", 1, "A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "",
                                                      1, "", 1, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", "", null, 1, "", "", "",
                                                      false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null)
            {
                Id = 1
            };

            var model2 = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now.Date.AddMonths(-1), "", "", DateTimeOffset.Now.Date.AddMonths(-1), "", 1, "B10", "", "", "", "", DateTimeOffset.Now.Date.AddMonths(-1), DateTimeOffset.Now.Date.AddMonths(-1), DateTimeOffset.Now.Date.AddMonths(-1), true, true, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", true, true, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false)
            {
                Id = 1
            };


            var repoMock1 = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model1 }.AsQueryable());

            var repoMock2 = new Mock<IGarmentPackingListRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model2 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock1.Object, repoMock2.Object).Object);

            var result = service.ReadShippingPackingList(DateTimeOffset.Now.Month, DateTimeOffset.Now.Year);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void ReadPLForDebtorCardNow()
        {

            var model1 = new GarmentShippingInvoiceModel(1, "", DateTimeOffset.Now, "", "", 1, "A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "",
                                                      1, "", 1, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", "", null, 1, "", "", "",
                                                      false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null)
            {
                Id = 1
            };

            var model2 = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "B10", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, true, true, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", true, true, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false, false, false, "")
            {
                Id = 1
            };


            var repoMock1 = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model1 }.AsQueryable());

            var repoMock2 = new Mock<IGarmentPackingListRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model2 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock1.Object, repoMock2.Object).Object);

            var result = service.ReadShippingPackingListForDebtorCardNow(DateTimeOffset.Now.Month, DateTimeOffset.Now.Year, model2.BuyerAgentCode);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void ReadPLForDebtorCard()
        {

            var model1 = new GarmentShippingInvoiceModel(1, "", DateTimeOffset.Now, "", "", 1, "A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "",
                                                      1, "", 1, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", "", null, 1, "", "", "",
                                                      false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null)
            {
                Id = 1
            };

            var model2 = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now.Date.AddMonths(-1), "", "", DateTimeOffset.Now.Date.AddMonths(-1), "", 1, "B10", "", "", "", "", DateTimeOffset.Now.Date.AddMonths(-1), DateTimeOffset.Now.Date.AddMonths(-1), DateTimeOffset.Now.Date.AddMonths(-1), true, true, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", true, true, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false, false, false, "")
            {
                Id = 1
            };


            var repoMock1 = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model1 }.AsQueryable());

            var repoMock2 = new Mock<IGarmentPackingListRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model2 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock1.Object, repoMock2.Object).Object);

            var result = service.ReadShippingPackingListForDebtorCard(DateTimeOffset.Now.Month, DateTimeOffset.Now.Year, model2.BuyerAgentCode);

            Assert.NotEmpty(result.ToList());
        }
    }
}