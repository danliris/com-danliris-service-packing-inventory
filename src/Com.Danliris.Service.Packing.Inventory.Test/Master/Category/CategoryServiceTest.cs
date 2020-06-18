using Com.Danliris.Service.Packing.Inventory.Application;
using Com.Danliris.Service.Packing.Inventory.Application.Master.Category;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.Category
{
    public class CategoryServiceTest
    {
        public CategoryService GetService(IServiceProvider serviceProvider)
        {
            return new CategoryService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IRepository<CategoryModel> categoryRepository)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock.Setup(s => s.GetService(typeof(IRepository<CategoryModel>)))
                .Returns(categoryRepository);

            return serviceProviderMock;
        }

        private FormDto formDto
        {
            get
            {
                return new FormDto()
                {
                    Name ="Name",
                    
                };
              
            }
        }

        private CategoryModel categoryModel
        {
            get
            {
                CategoryModel category = new CategoryModel();
                category.SetName("Name");

                return category;

            }
        }

        

        [Fact]
        public async Task Should_Success_Create()
        {
            var categoryRepository = new Mock<IRepository<CategoryModel>>();

            categoryRepository.Setup(s => s.ReadAll())
               .Returns(new List<CategoryModel>() { }.AsQueryable());

            var service = GetService(GetServiceProvider(categoryRepository.Object).Object);

            var result = await service.Create(formDto);
        }

        [Fact]
        public async Task CreateThrowServiceValidationException()
        {
            var categoryRepository = new Mock<IRepository<CategoryModel>>();

            categoryRepository.Setup(s => s.ReadAll())
               .Returns(new List<CategoryModel>() {categoryModel }.AsQueryable());

            var service = GetService(GetServiceProvider(categoryRepository.Object).Object);
            await Assert.ThrowsAsync<ServiceValidationException>(() => service.Create(formDto));
        
        }

        [Fact]
        public async Task Delete_Success()
        {
            var categoryRepository = new Mock<IRepository<CategoryModel>>();

            categoryRepository.Setup(s => s.DeleteAsync(It.IsAny<int>()))
               .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(categoryRepository.Object).Object);

            var result = await service.Delete(1);
        }

        [Fact]
        public async Task GetById_Return_Success()
        {
            var categoryRepository = new Mock<IRepository<CategoryModel>>();

            categoryRepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(categoryModel);

            var service = GetService(GetServiceProvider(categoryRepository.Object).Object);

            var result = await service.GetById(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetById_Return_Null()
        {
            var categoryRepository = new Mock<IRepository<CategoryModel>>();

            categoryRepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(()=>null);

            var service = GetService(GetServiceProvider(categoryRepository.Object).Object);

            var result = await service.GetById(1);
            Assert.Null(result);
        }


       [Fact]
        public async Task GetIndex_Return_Success()
        {
            var categoryRepository = new Mock<IRepository<CategoryModel>>();

            var service = GetService(GetServiceProvider(categoryRepository.Object).Object);

            categoryRepository.Setup(s => s.ReadAll())
                 .Returns(new List<CategoryModel>() { categoryModel, categoryModel }.AsQueryable());
            
            //Create object
            var orderData = new
            {
                Name = "desc",
            };

            string oder = JsonConvert.SerializeObject(orderData);

            IndexQueryParam queryParam = new IndexQueryParam()
            {
                page=1,
                size =25,
                keyword ="Name",
                order = oder
            };

            //var result = await service.GetIndex(queryParam);
            //Assert.NotNull(result);
        }


        [Fact]
        public async Task Update_Return_Success()
        {
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            var service = GetService(GetServiceProvider(categoryRepository.Object).Object);
           
            categoryRepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                 .ReturnsAsync(categoryModel);

            categoryRepository.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<CategoryModel>()))
                 .ReturnsAsync(1);

            var result = await service.Update(1,formDto);
            Assert.True(0 < result);
        }

        [Fact]
        public async Task Upsert_Return_SuccessCreate()
        {
            var categoryRepository = new Mock<IRepository<CategoryModel>>();

            var service = GetService(GetServiceProvider(categoryRepository.Object).Object);

            categoryRepository.Setup(s => s.ReadAll())
                .Returns(new List<CategoryModel>() { categoryModel, categoryModel }.AsQueryable());

            var result = await service.Upsert(formDto);
         
        }

        [Fact]
        public async Task Upsert_Return_Success()
        {
            var categoryRepository = new Mock<IRepository<CategoryModel>>();
            var service = GetService(GetServiceProvider(categoryRepository.Object).Object);
           
            categoryRepository.Setup(s => s.ReadAll())
                .Returns(new List<CategoryModel>() { categoryModel }.AsQueryable());
            var result = await service.Upsert(new FormDto() { Name ="New Name"});

        }

    }
}
