using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentSKU;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.InventoryDocumentSKU;
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
    public class InventoryDocumentSKUServiceTest
    {
        public InventoryDocumentSKUService GetService(IServiceProvider serviceProvider)
        {
            return new InventoryDocumentSKUService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IInventoryDocumentSKURepository inventoryDocumentSKURepository)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock.Setup(s => s.GetService(typeof(IInventoryDocumentSKURepository)))
                .Returns(inventoryDocumentSKURepository);


            return serviceProviderMock;
        }


        private CreateInventoryDocumentSKUViewModel ViewModel
        {
            get
            {
                return new CreateInventoryDocumentSKUViewModel()
                {
                    Items = new List<CreateInventoryDocumentSKUItemViewModel>()
                    {
                        new CreateInventoryDocumentSKUItemViewModel()
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


        private InventoryDocumentSKUModel inventoryDocumentSKUModel
        {


            get
            {
                //Create object
                var data = new
                {
                    Code = "Code",
                };
                string storage = JsonConvert.SerializeObject(data);

                return new InventoryDocumentSKUModel("Code", DateTimeOffset.UtcNow,
                    new List<InventoryDocumentSKUItemModel>()
                        {
                            new InventoryDocumentSKUItemModel(1,1,"e")
                        }, "np<", "tye", "re", storage, 1, "t");
            }
        }

        [Fact]
        public async Task Should_Success_Create()
        {
            var inventoryDocumentSKURepository = new Mock<IInventoryDocumentSKURepository>();

            inventoryDocumentSKURepository.Setup(s => s.InsertAsync(It.IsAny<InventoryDocumentSKUModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(inventoryDocumentSKURepository.Object).Object);

            await service.Create(ViewModel);
        }

        [Fact]
        public async Task Should_Success_ReadById()
        {
            var inventoryDocumentSKURepository = new Mock<IInventoryDocumentSKURepository>();

            inventoryDocumentSKURepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync( inventoryDocumentSKUModel);

            var service = GetService(GetServiceProvider(inventoryDocumentSKURepository.Object).Object);

            await service.ReadById(1);
        }

        [Fact]
        public void Should_Success_ReadByKeyword()
        {
            var inventoryDocumentSKURepository = new Mock<IInventoryDocumentSKURepository>();

            inventoryDocumentSKURepository.Setup(s => s.ReadAll())
                .Returns(new List<InventoryDocumentSKUModel>() { inventoryDocumentSKUModel}.AsQueryable());


            var service = GetService(GetServiceProvider(inventoryDocumentSKURepository.Object).Object);

            string order = @"{""Code"":""desc""}";

            var result = service.ReadByKeyword("Code", order, 1, 2);
            Assert.True(0 <result.Data.Count);

        }

        [Fact]
        public void Should_Success_ReadByKeyword_with_EmptyOrder()
        {
            var inventoryDocumentSKURepository = new Mock<IInventoryDocumentSKURepository>();

            inventoryDocumentSKURepository.Setup(s => s.ReadAll())
                .Returns(new List<InventoryDocumentSKUModel>() { inventoryDocumentSKUModel }.AsQueryable());


            var service = GetService(GetServiceProvider(inventoryDocumentSKURepository.Object).Object);

            var result = service.ReadByKeyword("Code", "", 1, 2);
            Assert.NotNull(result);

        }
    }
}
