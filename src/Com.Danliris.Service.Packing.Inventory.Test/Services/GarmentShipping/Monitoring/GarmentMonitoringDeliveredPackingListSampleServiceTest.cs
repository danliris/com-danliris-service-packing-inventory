using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShipment;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentPackingList;
using Moq;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Xunit;


namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.Monitoring
{
    public class GarmentMonitoringDeliveredPackingListSampleServiceTest
    {
        private const string ENTITY = "GarmentMonitoringDeliveredPackingListSample";

        public Mock<IServiceProvider> GetServiceProvider(IGarmentPackingListRepository repository, IGarmentPackingListItemRepository packingListItemRepository, IGarmentPackingListDetailRepository packingListDetailRepository, IGarmentPackingListDetailSizeRepository garmentPackingListDetailSizeRepository)
        {
            var spMock = new Mock<IServiceProvider>();

            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListItemRepository)))
                .Returns(packingListItemRepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListDetailRepository)))
                .Returns(packingListDetailRepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListDetailSizeRepository)))
                .Returns(garmentPackingListDetailSizeRepository);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentMonitoringDeliveredPackingListSampleService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentMonitoringDeliveredPackingListSampleService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success()
        {
            var model = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false, true, false, "");
            var model1 = new GarmentPackingListDetailSizeModel(1, "", 1, 1);

            var model2 = new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, 1, 1, null, 1);

            var model3 = new GarmentPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", "", "RO SAMPLE", null);

            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model }.AsQueryable());

            var repoMock3 = new Mock<IGarmentPackingListItemRepository>();
            repoMock3.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListItemModel>() { model3 }.AsQueryable());

            var repoMock2 = new Mock<IGarmentPackingListDetailRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListDetailModel>() { model2 }.AsQueryable());

            var repoMock1 = new Mock<IGarmentPackingListDetailSizeRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListDetailSizeModel>() { model1 }.AsQueryable());

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.SendAsync(HttpMethod.Get, It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));


            var spMock = GetServiceProvider(repoMock.Object, repoMock3.Object, repoMock2.Object, repoMock1.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);
     
            var service = GetService(spMock.Object);

            var result = service.GetReportData(model.InvoiceNo, model.PaymentTerm, DateTimeOffset.MinValue, DateTimeOffset.MaxValue, 0);

            Assert.NotEmpty(result.Data);
        
        }

        [Fact]
        public void GenerateExcel_Success()
        {
            var model = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false, true, false, "");
            var model1 = new GarmentPackingListDetailSizeModel(1, "", 1, 1);

            var model2 = new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, 1, 1, null, 1);

            var model3 = new GarmentPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", "", "RO SAMPLE", null);

            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model }.AsQueryable());

            var repoMock3 = new Mock<IGarmentPackingListItemRepository>();
            repoMock3.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListItemModel>() { model3 }.AsQueryable());

            var repoMock2 = new Mock<IGarmentPackingListDetailRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListDetailModel>() { model2 }.AsQueryable());

            var repoMock1 = new Mock<IGarmentPackingListDetailSizeRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListDetailSizeModel>() { model1 }.AsQueryable());

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.SendAsync(HttpMethod.Get, It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));


            var spMock = GetServiceProvider(repoMock.Object, repoMock3.Object, repoMock2.Object, repoMock1.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);

            var service = GetService(spMock.Object);

            var result = service.GenerateExcel(model.InvoiceNo, model.PaymentTerm, DateTimeOffset.MinValue, DateTimeOffset.MaxValue, 0);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var model = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", null, 1, 1, 1, 1, null, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED, "", false, "", false, true, false, "");
            var model1 = new GarmentPackingListDetailSizeModel(1, "", 1, 1);

            var model2 = new GarmentPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, 1, 1, null, 1);

            var model3 = new GarmentPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", "", "RO SAMPLE", null);

            var repoMock = new Mock<IGarmentPackingListRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model }.AsQueryable());

            var repoMock3 = new Mock<IGarmentPackingListItemRepository>();
            repoMock3.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListItemModel>() { model3 }.AsQueryable());

            var repoMock2 = new Mock<IGarmentPackingListDetailRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListDetailModel>() { model2 }.AsQueryable());

            var repoMock1 = new Mock<IGarmentPackingListDetailSizeRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListDetailSizeModel>() { model1 }.AsQueryable());

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.SendAsync(HttpMethod.Get, It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));


            var spMock = GetServiceProvider(repoMock.Object, repoMock3.Object, repoMock2.Object, repoMock1.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);

            var service = GetService(spMock.Object);

            var result = service.GenerateExcel("empty", model.PaymentTerm, DateTimeOffset.MinValue, DateTimeOffset.MaxValue, 0);

            Assert.NotNull(result);
        }


    }
}
