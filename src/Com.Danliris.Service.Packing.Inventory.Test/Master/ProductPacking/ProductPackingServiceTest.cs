using Com.Danliris.Service.Packing.Inventory.Application;
using Com.Danliris.Service.Packing.Inventory.Application.Master.ProductPacking;
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
           IRepository<CategoryModel> categoryRepository,
           IRepository<UnitOfMeasurementModel> unitOfMeasurementRepository
          )
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock.Setup(s => s.GetService(typeof(IRepository<ProductPackingModel>)))
                .Returns(productPackingRepository);

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
                    Name ="Name",
                    Code ="Code",
                    Description = "Description",
                    PackingSize=1,
                    ProductSKUId =1,
                    UOMId =1
                };

            }
        }

   

        private ProductPackingModel productPackingModel
        {
            get
            {
                return new ProductPackingModel(1,1,1,"Code","Name","Description");
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
                return new UnitOfMeasurementModel("Unit");
            }
        }

        private CategoryModel categoryModel
        {
            get
            {
                return new CategoryModel("name","code");
            }
        }

        [Fact]
        public void Validate_Default_FormDto()
        {
            FormDto dto = new FormDto();
            var result = dto.Validate(null);
            Assert.True(0 < result.Count());

        }

        [Fact]
        public void ValidateFormDto_when_InvalidCode()
        {
            FormDto dto = new FormDto()
            {
                Code = "XXX"
            };
            var result = dto.Validate(null);
            Assert.True(0 < result.Count());

        }

        [Fact]
        public async Task Should_Success_Create()
        {
            
            var productPackingRepository = new Mock<IRepository<ProductPackingModel>>();
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();
            var categoryRepository =new Mock<IRepository<CategoryModel>>();
            productPackingRepository.Setup(s => s.ReadAll())
               .Returns(new List<ProductPackingModel>() { productPackingModel }.AsQueryable());

            productPackingRepository.Setup(s => s.InsertAsync(It.IsAny<ProductPackingModel>()))
              .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(
                productPackingRepository.Object,
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);


            var result = await service.Create(new FormDto() { Name ="newName",Code ="NewCode",ProductSKUId =2});
            Assert.Equal(1,result);
        }

        [Fact]
        public async Task Create_Throws_ServiceValidationException_When_Code_Duplicate()
        {

            var productPackingRepository = new Mock<IRepository<ProductPackingModel>>();
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            productPackingRepository.Setup(s => s.ReadAll())
               .Returns(new List<ProductPackingModel>() { productPackingModel }.AsQueryable());

            productPackingRepository.Setup(s => s.InsertAsync(It.IsAny<ProductPackingModel>()))
              .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(
                productPackingRepository.Object,
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            await Assert.ThrowsAsync<Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities.ServiceValidationException>(() => service.Create(new FormDto() { Name = "newName", Code = "Code", ProductSKUId = 2 }));
        }

        [Fact]
        public async Task Create_Throws_ServiceValidationException_When_Name_Duplicate()
        {

            var productPackingRepository = new Mock<IRepository<ProductPackingModel>>();
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            productPackingRepository.Setup(s => s.ReadAll())
               .Returns(new List<ProductPackingModel>() { productPackingModel }.AsQueryable());

            productPackingRepository.Setup(s => s.InsertAsync(It.IsAny<ProductPackingModel>()))
              .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(
                productPackingRepository.Object,
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            await Assert.ThrowsAsync<Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities.ServiceValidationException>(() => service.Create(new FormDto() { Name = "Name", Code = "NewCode", ProductSKUId = 2 }));
        }

        [Fact]
        public async Task Create_Throws_ServiceValidationException_When_SKU_Duplicate()
        {

            var productPackingRepository = new Mock<IRepository<ProductPackingModel>>();
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            productPackingRepository.Setup(s => s.ReadAll())
               .Returns(new List<ProductPackingModel>() { productPackingModel }.AsQueryable());

            productPackingRepository.Setup(s => s.InsertAsync(It.IsAny<ProductPackingModel>()))
              .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(
                productPackingRepository.Object,
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            await Assert.ThrowsAsync<Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities.ServiceValidationException>(() => service.Create(new FormDto() { Name = "newName", Code = "NewCode", ProductSKUId = 1,PackingSize=1 }));
        }


        [Fact]
        public async Task Create_Throws_ServiceValidationException()
        {

            var productPackingRepository = new Mock<IRepository<ProductPackingModel>>();
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();

            var newProductPackingModel = productPackingModel;
          
            productPackingRepository.Setup(s => s.ReadAll())
               .Returns(new List<ProductPackingModel>() { new ProductPackingModel(1,1,1,"Code", " Unit","description") }.AsQueryable());

            productSKURepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
              .ReturnsAsync(productSKUModel);

            unitOfMeasurementRepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(unitOfMeasurementModel);

            categoryRepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(categoryModel);

            var service = GetService(GetServiceProvider(
                productPackingRepository.Object,
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            await Assert.ThrowsAsync<ServiceValidationException>(() => service.Create(formDto));
          
        }

        [Fact]
        public async Task GetById_Return_Success()
        {

            var productPackingRepository = new Mock<IRepository<ProductPackingModel>>();
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();
            
            productPackingRepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(productPackingModel);

            productSKURepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
              .ReturnsAsync(productSKUModel);

            unitOfMeasurementRepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(unitOfMeasurementModel);

            categoryRepository
                .Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(categoryModel);

            var service = GetService(GetServiceProvider(
                productPackingRepository.Object,
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            var result = await service.GetById(1);
            Assert.NotNull(result);

        }

        [Fact]
        public async Task GetById_Return_Null()
        {

            var productPackingRepository = new Mock<IRepository<ProductPackingModel>>();
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

            productPackingRepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(()=>null);

            categoryRepository
                .Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(categoryModel);


            var service = GetService(GetServiceProvider(
                productPackingRepository.Object,
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            var result = await service.GetById(1);
            Assert.Null(result);

        }

        [Fact]
        public async Task Delete_success()
        {

            var productPackingRepository = new Mock<IRepository<ProductPackingModel>>();
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

            productPackingRepository.Setup(s => s.DeleteAsync(It.IsAny<int>()))
               .ReturnsAsync(1);

            categoryRepository
                .Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(categoryModel);

            var service = GetService(GetServiceProvider(
                productPackingRepository.Object,
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            var result = await service.Delete(1);
            Assert.True(0 <result);

        }

       

        [Fact]
        public async Task Update_success()
        {

            var productPackingRepository = new Mock<IRepository<ProductPackingModel>>();
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

            var productPackingModel = new ProductPackingModel();
            productPackingModel.SetCode("code");
            productPackingModel.SetName("name");
            productPackingModel.SetDescription("description");
            productPackingModel.SetPackingSize(1);
            productPackingModel.SetProductSKU(1);
            productPackingModel.SetUOM(1);

            productPackingRepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(productPackingModel);

            productPackingRepository.Setup(s => s.ReadAll())
               .Returns(new List<ProductPackingModel>() { productPackingModel }.AsQueryable());

            productPackingRepository
                .Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductPackingModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(
                productPackingRepository.Object,
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            var newFormDto = new FormDto()
            {
                Name ="newName",
                Code ="newCode",
                ProductSKUId =2,
                PackingSize =2,
                Description ="new Description",
                UOMId =2
            };
            var result = await service.Update(1, newFormDto);
            Assert.True(0 < result);

        }

        [Fact]
        public async Task Update_When_Code_Dupliacte_Throws_ValidationException()
        {

            var productPackingRepository = new Mock<IRepository<ProductPackingModel>>();
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

            
            productPackingRepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(productPackingModel);

            productPackingRepository.Setup(s => s.ReadAll())
               .Returns(new List<ProductPackingModel>() { productPackingModel }.AsQueryable());

            productPackingRepository
                .Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductPackingModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(
                productPackingRepository.Object,
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            var newFormDto = new FormDto()
            {
                Name = "newName",
                Code = "Code",
                ProductSKUId = 2,
                PackingSize = 2,
                Description = "new Description",
                UOMId = 2
            };
          
            await Assert.ThrowsAsync<Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities.ServiceValidationException>(() => service.Update(1, newFormDto));
           
        }

        [Fact]
        public async Task Update_When_Name_Dupliacte_Throws_ValidationException()
        {

            var productPackingRepository = new Mock<IRepository<ProductPackingModel>>();
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

            productPackingRepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(productPackingModel);

            productPackingRepository.Setup(s => s.ReadAll())
               .Returns(new List<ProductPackingModel>() { productPackingModel }.AsQueryable());

            productPackingRepository
                .Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductPackingModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(
                productPackingRepository.Object,
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            var newFormDto = new FormDto()
            {
                Name = "Name",
                Code = "NewCode",
                ProductSKUId = 2,
                PackingSize = 2,
                Description = "new Description",
                UOMId = 2
            };

            await Assert.ThrowsAsync<Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities.ServiceValidationException>(() => service.Update(1, newFormDto));

        }

        [Fact]
        public async Task Update_When_SkuID_Duplicate_Throws_ValidationException()
        {

            var productPackingRepository = new Mock<IRepository<ProductPackingModel>>();
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();

            productPackingRepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(new ProductPackingModel(2,2,2,"Code","name","Description"));

            productPackingRepository.Setup(s => s.ReadAll())
               .Returns(new List<ProductPackingModel>() { productPackingModel }.AsQueryable());

            productPackingRepository
                .Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductPackingModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(
                productPackingRepository.Object,
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            var newFormDto = new FormDto()
            {
                Name = "NewName",
                Code = "NewCode",
                ProductSKUId = 1,
                PackingSize = 1,
                Description = "new Description",
                UOMId = 1
            };

            await Assert.ThrowsAsync<Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities.ServiceValidationException>(() => service.Update(1, newFormDto));

        }


        [Fact]
        public async Task GetIndex_Return_success()
        {

            var productPackingRepository = new Mock<IRepository<ProductPackingModel>>();
            var productSKURepository = new Mock<IRepository<ProductSKUModel>>();
            var unitOfMeasurementRepository = new Mock<IRepository<UnitOfMeasurementModel>>();
            var categoryRepository = new Mock<IRepository<CategoryModel>>();

            var productPackingModel = new ProductPackingModel(1, 1, 1, "Code", "Name","description")
            {
                Id = 1,
                Active = true,

            };

            productPackingRepository.Setup(s => s.ReadAll())
               .Returns(new List<ProductPackingModel>() { productPackingModel }.AsQueryable().BuildMock().Object);

            var productSKUModel = new ProductSKUModel("Code", "Name", 1, 1, "Description", true)
            {
                Id = 1,
                Active = true,

            };
            productSKURepository.Setup(s => s.ReadAll())
               .Returns(new List<ProductSKUModel>() { productSKUModel }.AsQueryable().BuildMock().Object); ;


            var unitOfMeasurementModel = new UnitOfMeasurementModel("Unit")
            {
                Id = 1,
                Active = true
            };

            unitOfMeasurementRepository.Setup(s => s.ReadAll())
               .Returns(new List<UnitOfMeasurementModel>() { unitOfMeasurementModel }.AsQueryable().BuildMock().Object);


            var service = GetService(GetServiceProvider(
                productPackingRepository.Object,
                productSKURepository.Object,
                categoryRepository.Object,
                unitOfMeasurementRepository.Object
                ).Object);

            IndexQueryParam queryParam = new IndexQueryParam()
            {
                order = "",
                page = 1,
                size = 1,
                keyword = "Name"
            };

            var result = await service.GetIndex(queryParam);
            Assert.NotNull(result);

        }
    }
}
