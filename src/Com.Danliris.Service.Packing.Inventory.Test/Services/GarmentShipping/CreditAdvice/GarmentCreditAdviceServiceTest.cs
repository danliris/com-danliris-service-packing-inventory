using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CreditAdvice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CreditAdvice;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentCreditAdvice
{
    public class GarmentCreditAdviceServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingCreditAdviceRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingCreditAdviceRepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentShippingCreditAdviceService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingCreditAdviceService(serviceProvider);
        }

        protected GarmentShippingCreditAdviceViewModel ViewModel
        {
            get
            {
                return new GarmentShippingCreditAdviceViewModel
                {
                    bankComission = 0,
                    discrepancyFee = 0,
                    btbAmount = 0,
                    btbRatio = 0,
                    btbRate = 0,
                    btbTransfer = 0,
                    btbMaterial = 0,
                    billDays = 0,
                    billAmount = 0,
                    creditInterest = 0,
                    bankCharges = 0
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentShippingCreditAdviceRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingCreditAdviceModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCreditAdviceModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentShippingCreditAdviceModel(1, 1, "", DateTimeOffset.Now, 1, 1, "", "", "", true, "", 1, 1, "", DateTimeOffset.Now, DateTimeOffset.Now, "", 0, 0, 1, DateTimeOffset.Now, 0, 0, 0, 0, 0, 0, 0, "", 1, "", "", 1, "", "", 0, 0, 0, DateTimeOffset.Now, "", DateTimeOffset.Now, 0, "", DateTimeOffset.Now, 0, DateTimeOffset.Now, "");
            var repoMock = new Mock<IGarmentShippingCreditAdviceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCreditAdviceModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void ReadData_Success()
        {
            var model = new GarmentShippingCreditAdviceModel(1, 1, "", DateTimeOffset.Now, 1, 1, "", "", "", true, "", 1, 1, "", DateTimeOffset.Now, DateTimeOffset.Now, "", 0, 0, 1, DateTimeOffset.Now, 0, 0, 0, 0, 0, 0, 0, "", 1, "", "", 1, "", "", 0, 0, 0, DateTimeOffset.Now, "", DateTimeOffset.Now, 0, "", DateTimeOffset.Now, 0, DateTimeOffset.Now, "");
            var repoMock = new Mock<IGarmentShippingCreditAdviceRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCreditAdviceModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.ReadData(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var model = new GarmentShippingCreditAdviceModel(1, 1, "", DateTimeOffset.Now, 1, 1, "", "", "", true, "", 1, 1, "", DateTimeOffset.Now, DateTimeOffset.Now, "", 0, 0, 1, DateTimeOffset.Now, 0, 0, 0, 0, 0, 0, 0, "", 1, "", "", 1, "", "", 0, 0, 0, DateTimeOffset.Now, "", DateTimeOffset.Now, 0, "", DateTimeOffset.Now, 0, DateTimeOffset.Now, "");

            var repoMock = new Mock<IGarmentShippingCreditAdviceRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var repoMock = new Mock<IGarmentShippingCreditAdviceRepository>();
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentShippingCreditAdviceModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var repoMock = new Mock<IGarmentShippingCreditAdviceRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
    }
}
