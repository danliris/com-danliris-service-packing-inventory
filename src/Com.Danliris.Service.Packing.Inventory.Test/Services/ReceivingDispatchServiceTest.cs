using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ReceivingDispatchDocument;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.InventoryDocumentPacking;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.InventoryDocumentSKU;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductPacking;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductSKU;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services
{
    public class ReceivingDispatchServiceTest
    {
        public ReceivingDispatchService GetService(IServiceProvider serviceProvider)
        {
            return new ReceivingDispatchService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IInventoryDocumentSKURepository inventoryDocumentSKURepository,
                                                        IInventoryDocumentPackingRepository inventoryDocumentPackingRepository,
                                                        IProductSKURepository productSKURepository,
                                                        IProductPackingRepository productPackingRepository

                                                        )
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock.Setup(s => s.GetService(typeof(IInventoryDocumentSKURepository)))
               .Returns(inventoryDocumentSKURepository);

            serviceProviderMock.Setup(s => s.GetService(typeof(IInventoryDocumentPackingRepository)))
             .Returns(inventoryDocumentPackingRepository);

            serviceProviderMock.Setup(s => s.GetService(typeof(IProductPackingRepository)))
                .Returns(productPackingRepository);

            serviceProviderMock.Setup(s => s.GetService(typeof(IProductSKURepository)))
                .Returns(productSKURepository);

            return serviceProviderMock;
        }


        private CreateReceivingDispatchDocumentViewModel ViewModel
        {
            get
            {
                return new CreateReceivingDispatchDocumentViewModel()
                {
                    Items =new List<CreateReceivingDispatchDocumentItemViewModel>()
                    {
                        new CreateReceivingDispatchDocumentItemViewModel()
                        {
                            Code ="Code",
                            PackingId =1,
                            PackingType = "PackingType",
                            Quantity =1,
                            SKUId =1,
                            UOMUnit = "UOMUnit",
                            
                        }
                    },
                    Storage = new Storage()
                    {
                        Id = 1,
                        Code = "code",
                        Name = "name",
                        Unit = new UnitStorage()
                        {
                            Division = new DivisionStorage()
                            {
                                Name = "name",

                            },
                            Name = "name"

                        },

                    }
                };
            }
        }

        private InventoryDocumentSKUModel inventoryDocumentSKUModel
        {
            get
            {
                return new InventoryDocumentSKUModel()
                {

                };
            }
        }

        [Fact]
        public async Task Should_Success_Dispatch()
        {

            var inventoryDocumentSKURepository = new Mock<IInventoryDocumentSKURepository>();
            var inventoryDocumentPackingRepository = new Mock<IInventoryDocumentPackingRepository>();
            var productSKURepository = new Mock<IProductSKURepository>();
            var productPackingRepository = new Mock<IProductPackingRepository>();

            inventoryDocumentSKURepository.Setup(s => s.InsertAsync(It.IsAny<InventoryDocumentSKUModel>()))
                .ReturnsAsync(1);

            inventoryDocumentPackingRepository.Setup(s => s.InsertAsync(It.IsAny<InventoryDocumentPackingModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inventoryDocumentSKURepository.Object, inventoryDocumentPackingRepository.Object, productSKURepository.Object, productPackingRepository.Object).Object);

            await service.Dispatch(ViewModel);
        }

        [Fact]
        public async Task Should_Success_Receive()
        {

            var inventoryDocumentSKURepository = new Mock<IInventoryDocumentSKURepository>();
            var inventoryDocumentPackingRepository = new Mock<IInventoryDocumentPackingRepository>();
            var productSKURepository = new Mock<IProductSKURepository>();
            var productPackingRepository = new Mock<IProductPackingRepository>();

            inventoryDocumentSKURepository.Setup(s => s.InsertAsync(It.IsAny<InventoryDocumentSKUModel>()))
                .ReturnsAsync(1);

            inventoryDocumentPackingRepository.Setup(s => s.InsertAsync(It.IsAny<InventoryDocumentPackingModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inventoryDocumentSKURepository.Object, inventoryDocumentPackingRepository.Object, productSKURepository.Object, productPackingRepository.Object).Object);

            await service.Receive(ViewModel);
        }

    }
}
