using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCorrectionNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalPriceCorrectionNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalPriceCorrectionNote;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentShippingLocalPriceCorrectionNote
{
    public class GarmentShippingLocalPriceCorrectionNoteServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingLocalPriceCorrectionNoteRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingLocalPriceCorrectionNoteRepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentShippingLocalPriceCorrectionNoteService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingLocalPriceCorrectionNoteService(serviceProvider);
        }

        protected GarmentShippingLocalPriceCorrectionNoteViewModel ViewModel
        {
            get
            {
                return new GarmentShippingLocalPriceCorrectionNoteViewModel
                {
                    items = new List<GarmentShippingLocalPriceCorrectionNoteItemViewModel>()
                    {
                        new GarmentShippingLocalPriceCorrectionNoteItemViewModel()
                        {
                            isChecked = true
                        }
                    }
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalPriceCorrectionNoteRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingLocalPriceCorrectionNoteModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalPriceCorrectionNoteModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentShippingLocalPriceCorrectionNoteModel("", DateTimeOffset.Now, 1, new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "", "", 1, "", "", "", "", 1, "", "", true, 1, 1, "", true, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", null), "", new List<GarmentShippingLocalPriceCorrectionNoteItemModel>());

            var repoMock = new Mock<IGarmentShippingLocalPriceCorrectionNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalPriceCorrectionNoteModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var items = new List<GarmentShippingLocalPriceCorrectionNoteItemModel>() { new GarmentShippingLocalPriceCorrectionNoteItemModel(1, new GarmentShippingLocalSalesNoteItemModel(1,1, "", "", 1, 1, "", 1, 1, 1, ""), 1) };
            var model = new GarmentShippingLocalPriceCorrectionNoteModel("", DateTimeOffset.Now, 1, new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "", "", 1, "", "", "", "", 1, "", "", true, 1, 1, "", true, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", null), null, items);

            var repoMock = new Mock<IGarmentShippingLocalPriceCorrectionNoteRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalPriceCorrectionNoteRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task ReadPdfById_Success()
        {
            var salesNoteItem = new GarmentShippingLocalSalesNoteItemModel(1,1, "", "", 1, 1, "", 1, 1, 1, "");
            var salesNoteItems = new List<GarmentShippingLocalSalesNoteItemModel>() { salesNoteItem };
            var salesNoteModel = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "", "", 1, "", "", "", "", 1, "", "", true, 1, 1, "", false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", salesNoteItems);

            var items = new HashSet<GarmentShippingLocalPriceCorrectionNoteItemModel> { new GarmentShippingLocalPriceCorrectionNoteItemModel(1, salesNoteItem, 1) };
            var model = new GarmentShippingLocalPriceCorrectionNoteModel("", DateTimeOffset.Now, 1, salesNoteModel, "", items);

            var repoMock = new Mock<IGarmentShippingLocalPriceCorrectionNoteRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { data = new Buyer() }))
                });

            var spMock = GetServiceProvider(repoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);
            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider
                {
                    TimezoneOffset = 7,
                    Token = "INITOKEN",
                    Username = "UserTest"
                });

            var service = GetService(spMock.Object);

            var result = await service.ReadPdfById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ReadPdfById_Master_Null()
        {
            var salesNoteItem = new GarmentShippingLocalSalesNoteItemModel(1,1, "", "", 1, 1, "", 1, 1, 1, "");
            var salesNoteItems = new List<GarmentShippingLocalSalesNoteItemModel>() { salesNoteItem };
            var salesNoteModel = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "", "", 1, "", "", "", "", 1, "", "", true, 1, 1, "", false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", salesNoteItems);

            var items = new HashSet<GarmentShippingLocalPriceCorrectionNoteItemModel> { new GarmentShippingLocalPriceCorrectionNoteItemModel(1, salesNoteItem, 1) };
            var model = new GarmentShippingLocalPriceCorrectionNoteModel("", DateTimeOffset.Now, 1, salesNoteModel, "", items);

            var repoMock = new Mock<IGarmentShippingLocalPriceCorrectionNoteRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.InternalServerError));

            var spMock = GetServiceProvider(repoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);
            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider
                {
                    TimezoneOffset = 7,
                    Token = "INITOKEN",
                    Username = "UserTest"
                });

            var service = GetService(spMock.Object);

            var result = await service.ReadPdfById(1);

            Assert.NotNull(result);
        }
    }
}
