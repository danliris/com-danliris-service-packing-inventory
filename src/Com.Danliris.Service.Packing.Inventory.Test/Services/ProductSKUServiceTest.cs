using Com.Danliris.Service.Packing.Inventory.Application.ProductSKU;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductSKU;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services
{
    public class ProductSKUServiceTest
    {
        public ProductSKUService GetService(IServiceProvider serviceProvider)
        {
            return new ProductSKUService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IProductSKURepository productSKURepository)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock.Setup(s => s.GetService(typeof(IProductSKURepository)))
                .Returns(productSKURepository);


            return serviceProviderMock;
        }
        

        private UpdateProductSKUViewModel updateProductSKUViewModel
        {
            get
            {
                return new UpdateProductSKUViewModel()
                {

                    Composition = "composition",
                    Construction = "construction",
                    Design = "design",
                    Grade = "grade",
                    LotNo = "lotNo",
                    ProductType = "FABRIC",
                    UOMUnit = "UOMUnit",
                    Width = "width",
                    WovenType = "wovenType",
                    YarnType1 = "yarnType1",
                    YarnType2 = "yarnType2"
                };
            }
        }

        private CreateProductSKUViewModel createProductSKUViewModel
        {
            get
            {
                return new CreateProductSKUViewModel()
                {
                   
                        Composition = "composition",
                        Construction = "construction",
                        Design = "design",
                        Grade = "grade",
                        LotNo = "lotNo",
                        ProductType = "FABRIC",
                        UOMUnit = "UOMUnit",
                        Width = "width",
                        WovenType = "wovenType",
                        YarnType1 = "yarnType1",
                        YarnType2 = "yarnType2"
            };
            }
        }

        private ProductSKUModel productSKUModel
        {
            get
            {
                return new ProductSKUModel("Code", "Composition", "Construction", "Design", "A", "1", "FABRIC", "UOMUnit", "1", "WovenType", "YarnType1", "YarnType2");
            }
        }

        [Fact]
        public async Task Should_Success_Create()
        {
            
            var productSKURepository = new Mock<IProductSKURepository>();

            productSKURepository.Setup(s => s.InsertAsync(It.IsAny<ProductSKUModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider( productSKURepository.Object).Object);

            await service.Create(createProductSKUViewModel);
        }

        [Fact]
        public async Task Should_Success_Delete()
        {

            var productSKURepository = new Mock<IProductSKURepository>();

            productSKURepository.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(productSKURepository.Object).Object);

             await service.Delete(1);
           
        }

        [Fact]
        public async Task Should_Success_ReadById()
        {

            var productSKURepository = new Mock<IProductSKURepository>();

            productSKURepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(productSKUModel);

            var service = GetService(GetServiceProvider(productSKURepository.Object).Object);

            await service.ReadById(1);

        }

        [Fact]
        public async Task Should_Success_Update()
        {
            var productSKURepository = new Mock<IProductSKURepository>();

            productSKURepository.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductSKUModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider( productSKURepository.Object).Object);

            await service.Update(1, updateProductSKUViewModel);
        }


        [Fact]
        public void Should_Success_ReadByKeyword()
        {
           
            var productSKURepository = new Mock<IProductSKURepository>();


            productSKURepository.Setup(s => s.ReadAll())
               .Returns(new List<ProductSKUModel>() { productSKUModel }.AsQueryable());

            var service = GetService(GetServiceProvider( productSKURepository.Object).Object);

            var result = service.ReadByKeyword("Composition Construction 1 Design A", "", 1, 2);
            Assert.NotNull(result);
           

        }


    }
}
