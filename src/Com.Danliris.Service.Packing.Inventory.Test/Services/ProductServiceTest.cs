using Com.Danliris.Service.Packing.Inventory.Application.Product;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductPacking;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductSKU;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services
{
    public class ProductServiceTest
    {
        public ProductService GetService(IServiceProvider serviceProvider)
        {
            return new ProductService(serviceProvider);
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


        private CreateProductPackAndSKUViewModel ViewModel
        {
            get
            {
                return new CreateProductPackAndSKUViewModel()
                {
                    
                    Composition= "Composition",
                    Construction = "Construction",
                    Design = "Design",
                    Grade ="A",
                    LotNo ="1",
                    PackType = "PackType",
                    ProductType = "FABRIC",
                    Quantity =1,
                    UOMUnit = "UOMUnit",
                    Width ="1",
                    WovenType = "WovenType",
                    YarnType1 = "YarnType1",
                    YarnType2 = "YarnType2",
                    
                };
            }
        }

       

        private ProductSKUModel model
        {
            get
            {
                return new ProductSKUModel(
                  "Code",
                  ViewModel.Composition,
                  ViewModel.Construction,
                  ViewModel.Design,
                  ViewModel.Grade,
                  ViewModel.LotNo,
                  ViewModel.ProductType,
                  ViewModel.UOMUnit,
                  ViewModel.Width,
                  ViewModel.WovenType,
                  ViewModel.YarnType1,
                  ViewModel.YarnType2
                    )
                    ;
            }
        }

        [Fact]
        public async Task Should_Success_Create()
        {
            var productPackingRepositoryMock = new Mock<IProductPackingRepository>();
            var productSKURepository = new Mock<IProductSKURepository>();

            productSKURepository.Setup(s => s.ReadAll())
                 .Returns(new List<ProductSKUModel>() { model}.AsQueryable());

           

            productPackingRepositoryMock.Setup(s => s.InsertAsync(It.IsAny<ProductPackingModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(productPackingRepositoryMock.Object, productSKURepository.Object).Object);

           // await service.CreateProductPackAndSKU(ViewModel);
        }
    }
}
