﻿using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalSalesContract;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalSalesContract;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.LocalSalesContract
{
    public class GarmentShippingLocalSalesContractServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingLocalSalesContractRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingLocalSalesContractRepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentShippingLocalSalesContractService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingLocalSalesContractService(serviceProvider);
        }

        protected GarmentShippingLocalSalesContractViewModel ViewModel
        {
            get
            {
                return new GarmentShippingLocalSalesContractViewModel
                {
                    items = new List<GarmentShippingLocalSalesContractItemViewModel>()
                    {
                        new GarmentShippingLocalSalesContractItemViewModel()
                    }
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalSalesContractRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingLocalSalesContractModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesContractModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentShippingLocalSalesContractModel("", DateTimeOffset.Now, 1, "", "", "", "", "", "", 1, "", "", "", "", true, 1, false, new List<GarmentShippingLocalSalesContractItemModel>());

            var repoMock = new Mock<IGarmentShippingLocalSalesContractRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesContractModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var items = new List<GarmentShippingLocalSalesContractItemModel>() { new GarmentShippingLocalSalesContractItemModel(1, "", "", 1, 1,1, "", 1) };
            var model = new GarmentShippingLocalSalesContractModel("", DateTimeOffset.Now, 1, "", "", "", "", "", "", 1, "", "", "", "", true, 1, false, items);
            var item = new GarmentShippingLocalSalesContractItemModel();
            var repoMock = new Mock<IGarmentShippingLocalSalesContractRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalSalesContractRepository>();
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentShippingLocalSalesContractModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalSalesContractRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
    }
}
