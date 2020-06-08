﻿using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNote;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services
{
    public class MaterialDeliveryNoteServiceTest
    {
        public MaterialDeliveryNoteService GetService(IServiceProvider serviceProvider)
        {
            return new MaterialDeliveryNoteService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IMaterialDeliveryNoteRepository materialDeliveryNoteRepository,
                                                        IItemsRepository itemsRepository)
        {
            var spMock = new Mock<IServiceProvider>();

            spMock.Setup(s => s.GetService(typeof(IMaterialDeliveryNoteRepository)))
                .Returns(materialDeliveryNoteRepository);
            spMock.Setup(s => s.GetService(typeof(IItemsRepository)))
                .Returns(itemsRepository);

            return spMock;
        }

        
        private MaterialDeliveryNoteViewModel ViewModel
        {
            get
            {
                return new MaterialDeliveryNoteViewModel()
                {
                    Active = true,
                    Code = "Code",
                    DateSJ = DateTimeOffset.Now,
                    BonCode = "BonCode",
                    DateFrom = DateTimeOffset.Now.AddDays(-2),
                    DateTo = DateTimeOffset.Now.AddDays(1),
                    DONumber = "DONumber",
                    FONumber = "FONumber",
                    Receiver = "Receiver",
                    Remark = "Remark",
                    SCNumber = "SCNumber",
                    Sender = "someone",
                    StorageNumber = "1",
                    Items = new List<ItemsViewModel>()
                    {
                        new ItemsViewModel()
                        {
                            NoSPP ="NoSPP",
                            MaterialName = "MaterialName",
                            InputLot ="InputLot",
                            WeightBruto =1,
                            WeightDOS =1,
                            WeightCone =1,
                            WeightBale =1,
                            GetTotal =1,
                        }
                    }
                };
            }
        }

        

        private Data.MaterialDeliveryNoteModel model
        {
            get
            {
                return new Data.MaterialDeliveryNoteModel(
                     ViewModel.Code,
                     ViewModel.DateSJ,
                     ViewModel.BonCode,
                     ViewModel.DateFrom,
                     ViewModel.DateTo,
                     ViewModel.DONumber,
                     ViewModel.FONumber,
                     ViewModel.Receiver,
                     ViewModel.Remark,
                     ViewModel.SCNumber,
                     ViewModel.Sender,
                     ViewModel.StorageNumber,
                        new List<ItemsModel>()
                        {
                            new ItemsModel("1","materialName","inputLot",1,1,1,1,1)
                        }
                    )
                {

                };
            }
        }



        [Fact]
        public async Task Should_Success_ReadById()
        {
            var materialDeliveryNoteRepositoryMock = new Mock<IMaterialDeliveryNoteRepository>();
            var itemsRepositoryMock = new Mock<IItemsRepository>();


            materialDeliveryNoteRepositoryMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                 .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(materialDeliveryNoteRepositoryMock.Object, itemsRepositoryMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Success_Delete()
        {
            var materialDeliveryNoteRepositoryMock = new Mock<IMaterialDeliveryNoteRepository>();
            var itemsRepositoryMock = new Mock<IItemsRepository>();

            materialDeliveryNoteRepositoryMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(materialDeliveryNoteRepositoryMock.Object, itemsRepositoryMock.Object).Object);
            try
            {
                await service.Delete(1);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }

           
        }


        [Fact]
        public async Task Should_Success_Create()
        {
            var materialDeliveryNoteRepositoryMock = new Mock<IMaterialDeliveryNoteRepository>();
            var itemsRepositoryMock = new Mock<IItemsRepository>();

            itemsRepositoryMock.Setup(s => s.InsertAsync(It.IsAny<ItemsModel>()))
                 .ReturnsAsync(1);

            materialDeliveryNoteRepositoryMock.Setup(s => s.InsertAsync(It.IsAny<Data.MaterialDeliveryNoteModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(materialDeliveryNoteRepositoryMock.Object, itemsRepositoryMock.Object).Object);

            try
            {
                await service.Create(ViewModel);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }


        [Fact]
        public async Task Should_Success_Update()
        {
            var materialDeliveryNoteRepositoryMock = new Mock<IMaterialDeliveryNoteRepository>();
            var itemsRepositoryMock = new Mock<IItemsRepository>();

            materialDeliveryNoteRepositoryMock.Setup(s => s.UpdateAsync(It.IsAny<int>(),It.IsAny<Data.MaterialDeliveryNoteModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(materialDeliveryNoteRepositoryMock.Object, itemsRepositoryMock.Object).Object);

            try
            {
                await service.Update(1,ViewModel);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Should_Success_ReadByKeyword()
        {
            var materialDeliveryNoteRepositoryMock = new Mock<IMaterialDeliveryNoteRepository>();
            var itemsRepositoryMock = new Mock<IItemsRepository>();

            materialDeliveryNoteRepositoryMock.Setup(s => s.ReadAll())
                .Returns(new List<Data.MaterialDeliveryNoteModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(materialDeliveryNoteRepositoryMock.Object, itemsRepositoryMock.Object).Object);

            try
            {
                 service.ReadByKeyword("BonCode","",1,1,"filter");
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

    }


}




