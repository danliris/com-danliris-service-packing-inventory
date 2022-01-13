using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentCoverLetter
{
    public class GarmentCoverLetterServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentCoverLetterRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentCoverLetterRepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentCoverLetterService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentCoverLetterService(serviceProvider);
        }

        protected GarmentCoverLetterViewModel ViewModel
        {
            get
            {
                return new GarmentCoverLetterViewModel
                {
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentCoverLetterRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingCoverLetterModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCoverLetterModel>().AsQueryable());

            var repoPackingListMock = new Mock<IGarmentPackingListRepository>();
            repoPackingListMock.Setup(s => s.Query)
                .Returns(new List<GarmentPackingListModel>
                {
                    new GarmentPackingListModel() { Id = ViewModel.Id }
                }.AsQueryable());

            var spMock = GetServiceProvider(repoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
                .Returns(repoPackingListMock.Object);
            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            var service = GetService(spMock.Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Create_PackingList_NotFound_Error()
        {
            var repoMock = new Mock<IGarmentCoverLetterRepository>();

            var repoPackingListMock = new Mock<IGarmentPackingListRepository>();
            repoPackingListMock.Setup(s => s.Query)
                .Returns(new List<GarmentPackingListModel>
                {
                    new GarmentPackingListModel() { Id = int.MaxValue }
                }.AsQueryable());

            var spMock = GetServiceProvider(repoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
                .Returns(repoPackingListMock.Object);

            var service = GetService(spMock.Object);

            await Assert.ThrowsAnyAsync<Exception>(() => service.Create(ViewModel));
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentShippingCoverLetterModel(1, 1, "", DateTimeOffset.Now, 1, "", "", "", "", "", "", "", DateTimeOffset.Now, 1, "", "", 1, 1, 1, 1, 1, "", "", "", "", "", "", "", "", "", "", DateTimeOffset.Now, "", 1, "");

            var repoMock = new Mock<IGarmentCoverLetterRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCoverLetterModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var model = new GarmentShippingCoverLetterModel(1, 1, "", DateTimeOffset.Now, 1, "", "", "", "", "", "", "", DateTimeOffset.Now, 1, "", "", 1, 1, 1, 1, 1, "", "", "", "", "", "", "", "", "", "", DateTimeOffset.Now, "", 1, "");

            var repoMock = new Mock<IGarmentCoverLetterRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var repoMock = new Mock<IGarmentCoverLetterRepository>();
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentShippingCoverLetterModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        /*[Fact]
        public async Task Delete_Success()
        {
            var model = new GarmentShippingCoverLetterModel(1, 1, "", DateTimeOffset.Now, 1, "", "", "", "", "", "", "", DateTimeOffset.Now, 1, "", "", 1, 1, 1, 1, 1, "", "", "", "", "", "", "", "", "", "", DateTimeOffset.Now, "", 1, "") { Id = 1 };

            var repoMock = new Mock<IGarmentCoverLetterRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var repoPackingListMock = new Mock<IGarmentPackingListRepository>();
            repoPackingListMock.Setup(s => s.Query)
                .Returns(new List<GarmentPackingListModel>
                {
                    new GarmentPackingListModel() { Id = model.Id }
                }.AsQueryable());

            var spMock = GetServiceProvider(repoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
                .Returns(repoPackingListMock.Object);
            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            var service = GetService(spMock.Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }*/

        [Fact]
        public async Task Delete_NoStatus_Success()
        {
            var model = new GarmentShippingCoverLetterModel(1, 1, "", DateTimeOffset.Now, 1, "", "", "", "", "", "", "", DateTimeOffset.Now, 1, "", "", 1, 1, 1, 1, 1, "", "", "", "", "", "", "", "", "", "", DateTimeOffset.Now, "", 1, "") { Id = 1 };

            var repoMock = new Mock<IGarmentCoverLetterRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var packingListData = new GarmentPackingListModel() { Id = model.Id };
            packingListData.StatusActivities.Add(new GarmentPackingListStatusActivityModel("", "", GarmentPackingListStatusEnum.APPROVED_SHIPPING.ToString()));

            var repoPackingListMock = new Mock<IGarmentPackingListRepository>();
            repoPackingListMock.Setup(s => s.Query)
                .Returns(new List<GarmentPackingListModel>
                {
                    packingListData
                }.AsQueryable());

            var spMock = GetServiceProvider(repoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
                .Returns(repoPackingListMock.Object);
            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            var service = GetService(spMock.Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delete_PackingList_NotFound_Error()
        {
            var model = new GarmentShippingCoverLetterModel(1, 1, "", DateTimeOffset.Now, 1, "", "", "", "", "", "", "", DateTimeOffset.Now, 1, "", "", 1, 1, 1, 1, 1, "", "", "", "", "", "", "", "", "", "", DateTimeOffset.Now, "", 1, "") { Id = 1 };

            var repoMock = new Mock<IGarmentCoverLetterRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var repoPackingListMock = new Mock<IGarmentPackingListRepository>();
            repoPackingListMock.Setup(s => s.Query)
                .Returns(new List<GarmentPackingListModel>
                {
                    new GarmentPackingListModel() { Id = model.Id + 1 }
                }.AsQueryable());

            var spMock = GetServiceProvider(repoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
                .Returns(repoPackingListMock.Object);
            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            var service = GetService(spMock.Object);

            await Assert.ThrowsAnyAsync<Exception>(() => service.Delete(1));
        }
    }
}
