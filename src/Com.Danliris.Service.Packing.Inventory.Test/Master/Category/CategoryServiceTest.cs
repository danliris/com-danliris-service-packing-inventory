using Com.Danliris.Service.Packing.Inventory.Application.Master.Category;
using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Moq;
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
               .Returns(new List<CategoryModel>() {  }.AsQueryable());

            var service = GetService(GetServiceProvider(categoryRepository.Object).Object);

            var result = await service.Create(formDto);
        }



        }
}
