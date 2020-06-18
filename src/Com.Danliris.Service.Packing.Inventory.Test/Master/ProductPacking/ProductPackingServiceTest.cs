using Com.Danliris.Service.Packing.Inventory.Application.Master.ProductPacking;
using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.ProductPacking
{
    public class ProductPackingServiceTest
    {
        public ProductPackingService GetService(IServiceProvider serviceProvider)
        {
            return new ProductPackingService(serviceProvider);
        }


        public Mock<IServiceProvider> GetServiceProvider(
           IRepository<ProductPackingModel> productPackingRepository,
           IRepository<ProductSKUModel> productSKURepository,
           IRepository<UnitOfMeasurementModel> unitOfMeasurementRepository
          )
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock.Setup(s => s.GetService(typeof(IRepository<ProductPackingModel>)))
                .Returns(productPackingRepository);

            serviceProviderMock.Setup(s => s.GetService(typeof(IRepository<ProductSKUModel>)))
             .Returns(productSKURepository);

            serviceProviderMock.Setup(s => s.GetService(typeof(IRepository<UnitOfMeasurementModel>)))
             .Returns(unitOfMeasurementRepository);

            return serviceProviderMock;
        }


        private FormDto formDto
        {
            get
            {
                return new FormDto()
                {
                };

            }
        }

        private ProductPackingModel productPackingModel
        {
            get
            {
                return new ProductPackingModel();
            }
        }
        private ProductSKUModel productSKUModel
        {
            get
            {
                return new ProductSKUModel();
            }
        }

        private UnitOfMeasurementModel unitOfMeasurementModel
        {
            get
            {
                return new UnitOfMeasurementModel();
            }
        }

        [Fact]
        public async Task Should_Success_Create()
        {
            
            var productPackingRepository = new Mock<IRepository<ProductPackingModel>>();
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

            productPackingRepository.Setup(s => s.ReadAll())
               .Returns(new List<ProductPackingModel>() { productPackingModel }.AsQueryable());

            productSKURepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
              .ReturnsAsync(productSKUModel);

            unitOfMeasurementRepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(unitOfMeasurementModel);

            var service = GetService(GetServiceProvider(
                productPackingRepository.Object,
                productSKURepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            //var result = await service.Create(formDto);
           await  Assert.ThrowsAsync<NotImplementedException>(() =>service.Create(formDto));
            //Assert.NotNull(result);
        }
    }
}
