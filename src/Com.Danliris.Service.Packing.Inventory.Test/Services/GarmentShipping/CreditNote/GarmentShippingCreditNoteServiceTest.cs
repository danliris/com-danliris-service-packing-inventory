﻿using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingNote;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentShippingCreditNote
{
    public class GarmentShippingCreditNoteServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingNoteRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingNoteRepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentShippingCreditNoteService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingCreditNoteService(serviceProvider);
        }

        protected GarmentShippingCreditNoteViewModel ViewModel
        {
            get
            {
                return new GarmentShippingCreditNoteViewModel
                {
                    items = new List<GarmentShippingNoteItemViewModel>()
                    {
                        new GarmentShippingNoteItemViewModel()
                    }
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentShippingNoteRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingNoteModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentShippingNoteModel(GarmentShippingNoteTypeEnum.CN, "", DateTimeOffset.Now, 1, "", "", "", "", DateTimeOffset.Now, 0, null, null, 1, new List<GarmentShippingNoteItemModel>());

            var repoMock = new Mock<IGarmentShippingNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var items = new List<GarmentShippingNoteItemModel>() { new GarmentShippingNoteItemModel("", 1, "", 1) };
            var model = new GarmentShippingNoteModel(GarmentShippingNoteTypeEnum.CN, "", DateTimeOffset.Now, 1, "", "", "", "", DateTimeOffset.Now, 0, null, null, 1, items);

            var repoMock = new Mock<IGarmentShippingNoteRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var repoMock = new Mock<IGarmentShippingNoteRepository>();
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentShippingNoteModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var repoMock = new Mock<IGarmentShippingNoteRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task ReadPdfById_Success_USD()
        {
            var items = new HashSet<GarmentShippingNoteItemModel> { new GarmentShippingNoteItemModel("", 2, "USD", 1) };
            var model = new GarmentShippingNoteModel(GarmentShippingNoteTypeEnum.CN, "", DateTimeOffset.Now, 1, "", "", "", "", DateTimeOffset.Now, 1, "", "", 1, items);

            var repoMock = new Mock<IGarmentShippingNoteRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.GetAsync(It.Is<string>(i => i.Contains("master/garment-buyers"))))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { data = new Buyer() }))
                });

            var spMock = GetServiceProvider(repoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);

            var service = GetService(spMock.Object);

            var result = await service.ReadPdfById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ReadPdfById_Success_USD1()
        {
            var items = new HashSet<GarmentShippingNoteItemModel> { new GarmentShippingNoteItemModel("", 2, "USD", 1.5) };
            var model = new GarmentShippingNoteModel(GarmentShippingNoteTypeEnum.CN, "", DateTimeOffset.Now, 1, "", "", "", "", DateTimeOffset.Now, 1, "", "", 1.5, items);

            var repoMock = new Mock<IGarmentShippingNoteRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.GetAsync(It.Is<string>(i => i.Contains("master/garment-buyers"))))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { data = new Buyer() }))
                });

            var spMock = GetServiceProvider(repoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);

            var service = GetService(spMock.Object);

            var result = await service.ReadPdfById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ReadPdfById_Success_USD2()
        {
            var items = new HashSet<GarmentShippingNoteItemModel> { new GarmentShippingNoteItemModel("", 2, "USD", 1.18) };
            var model = new GarmentShippingNoteModel(GarmentShippingNoteTypeEnum.CN, "", DateTimeOffset.Now, 1, "", "", "", "", DateTimeOffset.Now, 1, "", "", 1.18, items);

            var repoMock = new Mock<IGarmentShippingNoteRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.GetAsync(It.Is<string>(i => i.Contains("master/garment-buyers"))))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { data = new Buyer() }))
                });

            var spMock = GetServiceProvider(repoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);

            var service = GetService(spMock.Object);

            var result = await service.ReadPdfById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ReadPdfById_Success_USD3()
        {
            var items = new HashSet<GarmentShippingNoteItemModel> { new GarmentShippingNoteItemModel("", 2, "USD", 1.74) };
            var model = new GarmentShippingNoteModel(GarmentShippingNoteTypeEnum.CN, "", DateTimeOffset.Now, 1, "", "", "", "", DateTimeOffset.Now, 1, "", "", 1.74, items);

            var repoMock = new Mock<IGarmentShippingNoteRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.GetAsync(It.Is<string>(i => i.Contains("master/garment-buyers"))))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { data = new Buyer() }))
                });

            var spMock = GetServiceProvider(repoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);

            var service = GetService(spMock.Object);

            var result = await service.ReadPdfById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ReadPdfById_Success_IDR()
        {
            var items = new HashSet<GarmentShippingNoteItemModel> { new GarmentShippingNoteItemModel("", 1, "IDR", 1) };
            var model = new GarmentShippingNoteModel(GarmentShippingNoteTypeEnum.CN, "", DateTimeOffset.Now, 1, "", "", "", "", DateTimeOffset.Now, 1, "", "", 1, items);

            var repoMock = new Mock<IGarmentShippingNoteRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.GetAsync(It.Is<string>(i => i.Contains("master/garment-buyers"))))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { data = new Buyer() }))
                });

            var spMock = GetServiceProvider(repoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);

            var service = GetService(spMock.Object);

            var result = await service.ReadPdfById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ReadPdfById_Master_Null()
        {
            var items = new HashSet<GarmentShippingNoteItemModel> { new GarmentShippingNoteItemModel("", 1, "", 1) };
            var model = new GarmentShippingNoteModel(GarmentShippingNoteTypeEnum.CN, "", DateTimeOffset.Now, 1, "", "", "", "", DateTimeOffset.Now, 1, "", "", 1, items);

            var repoMock = new Mock<IGarmentShippingNoteRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var httpMock = new Mock<IHttpClientService>();
            httpMock.Setup(s => s.GetAsync(It.Is<string>(i => i.Contains("master/garment-buyers"))))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.InternalServerError));

            var spMock = GetServiceProvider(repoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(httpMock.Object);

            var service = GetService(spMock.Object);

            var result = await service.ReadPdfById(1);

            Assert.NotNull(result);
        }
    }
}
