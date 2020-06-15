using Com.Danliris.Service.Packing.Inventory.Application.ProductPacking;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductPacking;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductSKU;
using Moq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services
{
    public class ProductPackingServiceTest
    {
        public ProductPackingService GetService(IServiceProvider serviceProvider)
        {
            return new ProductPackingService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IProductPackingRepository productPackingRepository,
                                                        IProductSKURepository productSKURepository)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock.Setup(s => s.GetService(typeof(IProductPackingRepository)))
                .Returns(productPackingRepository);

            serviceProviderMock.Setup(s => s.GetService(typeof(IProductSKURepository)))
                .Returns(productSKURepository);

            return serviceProviderMock;
        }


        private ProductPackingFormViewModel ViewModel
        {
            get
            {
                return new ProductPackingFormViewModel()
                {

                    PackingType = "PackingType",
                    Quantity =1,
                    SKUId =1,

                };
            }
        }


        private ProductPackingModel productPackingModel
        {
            get
            {
                return new ProductPackingModel("Code",ViewModel.PackingType,1,0);
            }
        }



        private ProductSKUModel productSKUModelmodel
        {
            get
            {
                return new ProductSKUModel("Code", "Composition", "Construction", "Design","A","1",  "FABRIC", "UOMUnit","1", "WovenType", "YarnType1", "YarnType2");
            }
        }

       


        [Fact]
        public async Task Should_Success_Create()
        {
            var productPackingRepositoryMock = new Mock<IProductPackingRepository>();
            var productSKURepository = new Mock<IProductSKURepository>();

            productPackingRepositoryMock.Setup(s => s.InsertAsync(It.IsAny<ProductPackingModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(productPackingRepositoryMock.Object, productSKURepository.Object).Object);

           await service.Create(ViewModel);
        }


        [Fact]
        public async Task Should_Success_Delete()
        {
            var productPackingRepositoryMock = new Mock<IProductPackingRepository>();
            var productSKURepository = new Mock<IProductSKURepository>();

            productPackingRepositoryMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(productPackingRepositoryMock.Object, productSKURepository.Object).Object);

            await service.Delete(1);
        }

        [Fact]
        public async Task Should_Success_ReadById()
        {
            var productPackingRepositoryMock = new Mock<IProductPackingRepository>();
            var productSKURepository = new Mock<IProductSKURepository>();

            productPackingRepositoryMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(productPackingModel);

            var service = GetService(GetServiceProvider(productPackingRepositoryMock.Object, productSKURepository.Object).Object);

            await service.ReadById(1);
        }

        [Fact]
        public async Task Should_Success_Update()
        {
            var productPackingRepositoryMock = new Mock<IProductPackingRepository>();
            var productSKURepository = new Mock<IProductSKURepository>();

            productPackingRepositoryMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductPackingModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(productPackingRepositoryMock.Object, productSKURepository.Object).Object);

            await service.Update(1, ViewModel);
        }


        [Fact]
        public void Should_Success_ReadByKeyword()
        {
            var productPackingRepositoryMock = new Mock<IProductPackingRepository>();
            var productSKURepository = new Mock<IProductSKURepository>();
           
            productPackingRepositoryMock.Setup(s => s.ReadAll())
                .Returns(new List<ProductPackingModel>() {  }.AsQueryable());

            productSKURepository.Setup(s => s.ReadAll())
               .Returns(new List<ProductSKUModel>() { productSKUModelmodel }.AsQueryable());

            var service = GetService(GetServiceProvider(productPackingRepositoryMock.Object, productSKURepository.Object).Object);

           var result =  service.ReadByKeyword("Name","",1,2);
            Assert.NotNull(result);

        }



    }
}
