using Com.Danliris.Service.Packing.Inventory.Application.Master.ProductSKU;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.ProductSKU
{
    public class ProductSKUServiceTest
    {
        public ProductSKUService GetService(IServiceProvider serviceProvider)
        {
            return new ProductSKUService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(
           IRepository<ProductSKUModel> productSKURepository,
           IRepository<CategoryModel> categoryRepository,
           IRepository<UnitOfMeasurementModel> unitOfMeasurementRepository
          )
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock.Setup(s => s.GetService(typeof(IRepository<ProductSKUModel>)))
                .Returns(productSKURepository);

            serviceProviderMock.Setup(s => s.GetService(typeof(IRepository<CategoryModel>)))
             .Returns(categoryRepository);

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

        private ProductSKUModel productSKUModel
        {
            get
            {
                return new ProductSKUModel();
            }
        }

        private CategoryModel categoryModel
        {
            get
            {
                return new CategoryModel();
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
        public async Task Create_Throws_ServiceValidationException()
        {
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

            productSKURepository.Setup(s => s.ReadAll())
               .Returns(new List<ProductSKUModel>() { productSKUModel }.AsQueryable());

            productSKURepository.Setup(s => s.InsertAsync(It.IsAny<ProductSKUModel>()))
              .ReturnsAsync(1);

           
            var service = GetService(GetServiceProvider(
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            await Assert.ThrowsAsync<ServiceValidationException>(() => service.Create(formDto));
        }

        [Fact]
        public async Task Should_Success_Delete()
        {
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

            productSKURepository.Setup(s => s.DeleteAsync(It.IsAny<int>()))
              .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            var result = await service.Delete(1);
            Assert.True(0 < result);
        }

        [Fact]
        public async Task GetById_Return_Success()
        {
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

          
            productSKURepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
              .ReturnsAsync(productSKUModel);

            categoryRepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
           .ReturnsAsync(categoryModel);

            unitOfMeasurementRepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
           .ReturnsAsync(unitOfMeasurementModel);


            var service = GetService(GetServiceProvider(
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            var result = await service.GetById(1);
            Assert.NotNull( result);
        }

    }
}
