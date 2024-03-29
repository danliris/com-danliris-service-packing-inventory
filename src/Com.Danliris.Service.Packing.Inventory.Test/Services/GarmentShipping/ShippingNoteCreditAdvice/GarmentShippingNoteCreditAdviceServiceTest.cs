﻿using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingNoteCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingNoteCreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShipingNoteCreditAdvice;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentShippingNoteCreditAdvice
{
    public class GarmentShippingNoteCreditAdviceServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingNoteCreditAdviceRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingNoteCreditAdviceRepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentShippingNoteCreditAdviceService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingNoteCreditAdviceService(serviceProvider);
        }

        protected GarmentShippingNoteCreditAdviceViewModel ViewModel
        {
            get
            {
                return new GarmentShippingNoteCreditAdviceViewModel
                {
                    bankComission = 0,
                    creditInterest = 0,
                    bankCharges = 0,
                    insuranceCharge = 0,
                    paidAmount = 0
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentShippingNoteCreditAdviceRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingNoteCreditAdviceModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteCreditAdviceModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentShippingNoteCreditAdviceModel(1, "", "", DateTimeOffset.Now, "", "", 1, 1, 0, DateTimeOffset.Now, 1, 1, "", "", "", 1, "", "", "", 0, 0, 0, 0, DateTimeOffset.Now, "");
            var repoMock = new Mock<IGarmentShippingNoteCreditAdviceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteCreditAdviceModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void ReadData_Success()
        {
            var model = new GarmentShippingNoteCreditAdviceModel(1, "", "", DateTimeOffset.Now, "", "", 1, 1, 0, DateTimeOffset.Now, 1, 1, "", "", "", 1, "", "", "", 0, 0, 0, 0, DateTimeOffset.Now, "");
            var repoMock = new Mock<IGarmentShippingNoteCreditAdviceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingNoteCreditAdviceModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.ReadData(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var model = new GarmentShippingNoteCreditAdviceModel(1, "", "", DateTimeOffset.Now, "", "", 1, 1, 0, DateTimeOffset.Now, 1, 1, "", "", "", 1, "", "", "", 0, 0, 0, 0, DateTimeOffset.Now, "");
            var repoMock = new Mock<IGarmentShippingNoteCreditAdviceRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var repoMock = new Mock<IGarmentShippingNoteCreditAdviceRepository>();
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentShippingNoteCreditAdviceModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var repoMock = new Mock<IGarmentShippingNoteCreditAdviceRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
    }
}
