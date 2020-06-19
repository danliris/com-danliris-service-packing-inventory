using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentPacking;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.InventoryDocumentPacking;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services
{
    public class InventoryDocumentPackingServiceTest
    {
        public InventoryDocumentPackingService GetService(IServiceProvider serviceProvider)
        {
            return new InventoryDocumentPackingService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IInventoryDocumentPackingRepository inventoryDocumentPackingRepository)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock.Setup(s => s.GetService(typeof(IInventoryDocumentPackingRepository)))
                .Returns(inventoryDocumentPackingRepository);


            return serviceProviderMock;
        }


        private CreateInventoryDocumentPackingViewModel ViewModel
        {
            get
            {
                return new CreateInventoryDocumentPackingViewModel()
                {
                    Items = new List<CreateInventoryDocumentPackingItemViewModel>()
                    {
                        new CreateInventoryDocumentPackingItemViewModel()
                        {
                            SKUId =0,
                            Quantity=1,
                            UOMUnit = "UOMUnit"
                        }
                    },
                    ReferenceNo = "1",
                    ReferenceType = "ReferenceType",
                    Remark = "Remark",
                    Type = "Type",
                    Storage = new Storage()
                    {
                        Id = 1,
                        Code = "Code",
                        Name = "name",
                        Unit = new UnitStorage()
                        {
                            Division = new DivisionStorage()
                            {
                                Name = "name",

                            },
                            Name = "name"

                        },

                    },


                };
            }
        }


        private InventoryDocumentPackingModel inventoryDocumentPackingModel
        {
            get
            {
                //Create object
                var data = new
                {
                    Code = "Code",
                };
                string storage = JsonConvert.SerializeObject(data);

                return new InventoryDocumentPackingModel("Code", DateTimeOffset.UtcNow,
                    new List<InventoryDocumentPackingItemModel>()
                        {
                            new InventoryDocumentPackingItemModel(1,1,0,"uomUnit")
                        }, "np<", "tye", "re", storage, 1, "t");
            }
        }

        [Fact]
        public async Task Should_Success_Create()
        {
            var inventoryDocumentPackingRepository = new Mock<IInventoryDocumentPackingRepository>();

            inventoryDocumentPackingRepository.Setup(s => s.InsertAsync(It.IsAny<InventoryDocumentPackingModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inventoryDocumentPackingRepository.Object).Object);

            await service.Create(ViewModel);
        }

        [Fact]
        public async Task Should_Success_ReadById()
        {
            var inventoryDocumentPackingRepository = new Mock<IInventoryDocumentPackingRepository>();

            inventoryDocumentPackingRepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(inventoryDocumentPackingModel);

            var service = GetService(GetServiceProvider(inventoryDocumentPackingRepository.Object).Object);

            await service.ReadById(1);
        }

        [Fact]
        public void Should_Success_ReadByKeyword()
        {
            var inventoryDocumentPackingRepository = new Mock<IInventoryDocumentPackingRepository>();

            inventoryDocumentPackingRepository.Setup(s => s.ReadAll())
                .Returns(new List<InventoryDocumentPackingModel>() { inventoryDocumentPackingModel}.AsQueryable());


            var service = GetService(GetServiceProvider(inventoryDocumentPackingRepository.Object).Object);

            string order = @"{""Code"":""desc""}";

            var result = service.ReadByKeyword("Code", order, 1, 2);
            Assert.True(0 < result.Data.Count);

        }

        [Fact]
        public void Should_Success_ReadByKeyword_with_EmptyOrder()
        {
            var inventoryDocumentPackingRepository = new Mock<IInventoryDocumentPackingRepository>();

            inventoryDocumentPackingRepository.Setup(s => s.ReadAll())
                .Returns(new List<InventoryDocumentPackingModel>() { inventoryDocumentPackingModel }.AsQueryable());


            var service = GetService(GetServiceProvider(inventoryDocumentPackingRepository.Object).Object);

            var result = service.ReadByKeyword("Code", "", 1, 2);
            Assert.True(0 < result.Data.Count);

        }

    }
}
