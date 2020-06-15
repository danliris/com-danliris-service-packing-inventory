using Com.Danliris.Service.Packing.Inventory.Application;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNoteWeaving;
using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNoteWeaving;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNoteWeaving;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.MaterialDeliveryNoteWeaving
{
    public class MaterialDeliveryNoteWeavingServiceTest
    {
        public MaterialDeliveryNoteWeavingService GetService(IServiceProvider serviceProvider)
        {
            return new MaterialDeliveryNoteWeavingService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IMaterialDeliveryNoteWeavingRepository materialDeliveryNoteWeavingRepository,
                                                       IItemsMaterialDeliveryNoteWeavingRepository itemsMaterialDeliveryNoteWeavingRepository)
        {
            var spMock = new Mock<IServiceProvider>();

            spMock.Setup(s => s.GetService(typeof(IMaterialDeliveryNoteWeavingRepository)))
                .Returns(materialDeliveryNoteWeavingRepository);
            spMock.Setup(s => s.GetService(typeof(IItemsMaterialDeliveryNoteWeavingRepository)))
                .Returns(itemsMaterialDeliveryNoteWeavingRepository);

            return spMock;
        }

        private MaterialDeliveryNoteWeavingViewModel ViewModel
        {
            get
            {
                return new MaterialDeliveryNoteWeavingViewModel()
                {
                    Code = "Code",
                    DateSJ = DateTimeOffset.Now,
                    selectedDO = new DeliveryOrderMaterialDeliveryNoteWeaving(),
                    SendTo = "SendTo",
                    Unit =new UnitMaterialDeliveryNoteWeaving(),
                    Buyer =new BuyerMaterialDeliveryNoteWeaving(),
                    NumberBonOut = "NumberBonOut",
                    Storage = new StorageMaterialDeliveryNoteWeaving()
                    {
                        Code="Code"
                    },
                    Remark = "Remark",
                    ItemsMaterialDeliveryNoteWeaving =new List<ItemsMaterialDeliveryNoteWeavingViewModel>()
                    {
                        new ItemsMaterialDeliveryNoteWeavingViewModel()
                    }
                };

            }
        }


        private Data.MaterialDeliveryNoteWeavingModel model
        {
            get
            {
                return new Data.MaterialDeliveryNoteWeavingModel(
                     ViewModel.Code,
                     DateTimeOffset.Now,
                     1,
                     ViewModel.selectedDO.DOSalesNo,
                     ViewModel.SendTo,
                     1,
                     ViewModel.Unit.Name,
                     1,
                     ViewModel.Buyer.Code,
                     ViewModel.Buyer.Name,
                     ViewModel.NumberBonOut,
                     ViewModel.Storage.Id,
                     ViewModel.Storage.Code,
                     ViewModel.Storage.Name,
                     ViewModel.Remark,
                     new List<ItemsMaterialDeliveryNoteWeavingModel>()
                     {
                         new ItemsMaterialDeliveryNoteWeavingModel()
                     }
                    )
                {

                };
            }
        }


        [Fact]
        public async Task Should_Success_Create()
        {
            var materialDeliveryNoteWeavingRepository = new Mock<IMaterialDeliveryNoteWeavingRepository>();
            var itemsMaterialDeliveryNoteWeavingRepository = new Mock<IItemsMaterialDeliveryNoteWeavingRepository>();

            itemsMaterialDeliveryNoteWeavingRepository.Setup(s => s.InsertAsync(It.IsAny<ItemsMaterialDeliveryNoteWeavingModel>()))
                 .ReturnsAsync(1);

            materialDeliveryNoteWeavingRepository.Setup(s => s.InsertAsync(It.IsAny<Data.MaterialDeliveryNoteWeavingModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(materialDeliveryNoteWeavingRepository.Object, itemsMaterialDeliveryNoteWeavingRepository.Object).Object);

            await service.Create(ViewModel);
        }

        [Fact]
        public async Task Should_Success_Delete()
        {
            var materialDeliveryNoteWeavingRepository = new Mock<IMaterialDeliveryNoteWeavingRepository>();
            var itemsMaterialDeliveryNoteWeavingRepository = new Mock<IItemsMaterialDeliveryNoteWeavingRepository>();

            materialDeliveryNoteWeavingRepository.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);
            var service = GetService(GetServiceProvider(materialDeliveryNoteWeavingRepository.Object, itemsMaterialDeliveryNoteWeavingRepository.Object).Object);

            await service.Delete(1);

        }

        [Fact]
        public async Task Should_Success_ReadById()
        {
            var materialDeliveryNoteWeavingRepository = new Mock<IMaterialDeliveryNoteWeavingRepository>();
            var itemsMaterialDeliveryNoteWeavingRepository = new Mock<IItemsMaterialDeliveryNoteWeavingRepository>();

            
            materialDeliveryNoteWeavingRepository.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(materialDeliveryNoteWeavingRepository.Object, itemsMaterialDeliveryNoteWeavingRepository.Object).Object);

            await service.ReadById(1);
        }

        [Fact]
        public async Task Should_Success_Update()
        {
            var materialDeliveryNoteWeavingRepository = new Mock<IMaterialDeliveryNoteWeavingRepository>();
            var itemsMaterialDeliveryNoteWeavingRepository = new Mock<IItemsMaterialDeliveryNoteWeavingRepository>();


            materialDeliveryNoteWeavingRepository.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<Data.MaterialDeliveryNoteWeavingModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(materialDeliveryNoteWeavingRepository.Object, itemsMaterialDeliveryNoteWeavingRepository.Object).Object);

            await service.Update(1,ViewModel);
        }


        [Fact]
        public void Should_Success_ReadByKeyword()
        {
            var materialDeliveryNoteWeavingRepository = new Mock<IMaterialDeliveryNoteWeavingRepository>();
            var itemsMaterialDeliveryNoteWeavingRepository = new Mock<IItemsMaterialDeliveryNoteWeavingRepository>();


            materialDeliveryNoteWeavingRepository.Setup(s => s.ReadAll())
                .Returns(new List<Data.MaterialDeliveryNoteWeavingModel>() { model}.AsQueryable());

            var service = GetService(GetServiceProvider(materialDeliveryNoteWeavingRepository.Object, itemsMaterialDeliveryNoteWeavingRepository.Object).Object);

            service.ReadByKeyword("BonCode", "", 1, 1, "filter");
            Assert.True(true);
        }


    }
}
