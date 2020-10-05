using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.InsuranceDisposition;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.InsuranceDisposition;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.InsuranceDisposition;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.InsuranceDisposition
{
    public class GarmentShippingInsuranceDispositionServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingInsuranceDispositionRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInsuranceDispositionRepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentShippingInsuranceDispositionService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingInsuranceDispositionService(serviceProvider);
        }

        protected GarmentShippingInsuranceDispositionViewModel ViewModel
        {
            get
            {
                return new GarmentShippingInsuranceDispositionViewModel
                {
                    items = new List<GarmentShippingInsuranceDispositionItemViewModel>()
                    {
                        new GarmentShippingInsuranceDispositionItemViewModel()
                    },
                    unitCharge= new List<GarmentShippingInsuranceDispositionUnitChargeViewModel>()
                    {
                        new GarmentShippingInsuranceDispositionUnitChargeViewModel()
                    },
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentShippingInsuranceDispositionRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingInsuranceDispositionModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInsuranceDispositionModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentShippingInsuranceDispositionModel("", "", DateTimeOffset.Now, "", 1, "", "", 1, "", new List<GarmentShippingInsuranceDispositionUnitChargeModel>(), new List<GarmentShippingInsuranceDispositionItemModel>());

            var repoMock = new Mock<IGarmentShippingInsuranceDispositionRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInsuranceDispositionModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var items = new HashSet<GarmentShippingInsuranceDispositionItemModel> { new GarmentShippingInsuranceDispositionItemModel(DateTimeOffset.Now, "", "", 1, 1, "", "", 1, 1), new GarmentShippingInsuranceDispositionItemModel(DateTimeOffset.Now, "", "", 2, 2, "", "", 2, 2) };
            var units = new HashSet<GarmentShippingInsuranceDispositionUnitChargeModel> { new GarmentShippingInsuranceDispositionUnitChargeModel(1, "", 1), new GarmentShippingInsuranceDispositionUnitChargeModel(2, "", 2) };
            var model = new GarmentShippingInsuranceDispositionModel("", "", DateTimeOffset.Now, "", 1, "", "", 1, "", units, items);
            
            var repoMock = new Mock<IGarmentShippingInsuranceDispositionRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var repoMock = new Mock<IGarmentShippingInsuranceDispositionRepository>();
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentShippingInsuranceDispositionModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var repoMock = new Mock<IGarmentShippingInsuranceDispositionRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
    }
}
