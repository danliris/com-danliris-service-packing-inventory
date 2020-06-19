using Com.Danliris.Service.Packing.Inventory.Application;
using Com.Danliris.Service.Packing.Inventory.Application.Master.ProductSKU;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using MockQueryable.Moq;
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
                    Code = "Code",
                    Name ="Name",
                    CategoryId =1,
                    Description = "Description",
                    UOMId =1
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


        private IndexQueryParam queryParam
        {
            get
            {
                return new IndexQueryParam();
            }
        }
        

        [Fact]
        public async Task Create_Return_SUccess()
        {
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

            var newProductSKUModel = productSKUModel;
            newProductSKUModel.SetName("NewName");
            newProductSKUModel.SetCode("NewCode");
            productSKURepository.Setup(s => s.ReadAll())
               .Returns(new List<ProductSKUModel>() { newProductSKUModel }.AsQueryable());

            productSKURepository.Setup(s => s.InsertAsync(It.IsAny<ProductSKUModel>()))
              .ReturnsAsync(1);


            var service = GetService(GetServiceProvider(
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            var result = await service.Create(formDto);
            Assert.True(0 < result);

        }



        [Fact]
        public async Task Create__with_Duplicate_Name_Throws_ServiceValidationException()
        {
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();
            var newProductSKUModel = productSKUModel;
            newProductSKUModel.SetName("Name");
            productSKURepository.Setup(s => s.ReadAll())
               .Returns(new List<ProductSKUModel>() { newProductSKUModel }.AsQueryable());

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
        public async Task Create_with_Duplicate_Code_Throws_ServiceValidationException()
        {
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

            var newProductSKUModel = productSKUModel;
            newProductSKUModel.SetCode("Code");
           
            productSKURepository.Setup(s => s.ReadAll())
               .Returns(new List<ProductSKUModel>() { newProductSKUModel }.AsQueryable());

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


        [Fact]
        public async Task GetById_Return_Null()
        {
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();


            productSKURepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
              .ReturnsAsync(()=>null);

            var service = GetService(GetServiceProvider(
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            var result = await service.GetById(1);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetIndex_Return_Success()
        {
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

            
            productSKURepository.Setup(s => s.ReadAll())
                  .Returns(new List<ProductSKUModel>() { productSKUModel }.AsQueryable().BuildMock().Object);

            categoryRepository.Setup(s => s.ReadAll())
           .Returns(new List<CategoryModel>() { categoryModel }.AsQueryable().BuildMock().Object);


            unitOfMeasurementRepository.Setup(s => s.ReadAll())
           .Returns(new List<UnitOfMeasurementModel>() { unitOfMeasurementModel }.AsQueryable().BuildMock().Object);


            var service = GetService(GetServiceProvider(
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            var result = await service.GetIndex(queryParam);
            Assert.NotNull(result);
        }


        [Fact]
        public async Task Update_Return_Success()
        {
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

            var newProductSKUModel = productSKUModel;
            newProductSKUModel.SetCode("newCode");
            newProductSKUModel.SetName("newName");
            productSKURepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(productSKUModel);
            productSKURepository.Setup(s => s.ReadAll())
                  .Returns(new List<ProductSKUModel>() { newProductSKUModel }.AsQueryable());

            productSKURepository.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductSKUModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            var result = await service.Update(1,formDto);
            Assert.True(0<result);
        }

        [Fact]
        public async Task Update_with_duplicateCode_Throws_ServiceValidationException()
        {
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

            var newProductSKUModel = productSKUModel;
            newProductSKUModel.SetCode("Code");

            productSKURepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(productSKUModel);
                  

            productSKURepository.Setup(s => s.ReadAll())
                  .Returns(new List<ProductSKUModel>() { newProductSKUModel }.AsQueryable());

            productSKURepository.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductSKUModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            await Assert.ThrowsAsync<ServiceValidationException>(() => service.Update(1, formDto));
        }

        [Fact]
        public async Task Update_with_duplicate_Name_Throws_ServiceValidationException()
        {
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

            var newProductSKUModel = productSKUModel;
            newProductSKUModel.SetName("Name");
            productSKURepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(productSKUModel);
            productSKURepository.Setup(s => s.ReadAll())
                  .Returns(new List<ProductSKUModel>() { newProductSKUModel }.AsQueryable());

            productSKURepository.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductSKUModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            await Assert.ThrowsAsync<ServiceValidationException>(() => service.Update(1, formDto));
        }
    }
    
}
