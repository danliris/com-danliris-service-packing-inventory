using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalReturnNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalReturnNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalReturnNote;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.LocalReturnNote
{
    public class GarmentShippingLocalReturnNoteServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingLocalReturnNoteRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingLocalReturnNoteRepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentShippingLocalReturnNoteService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingLocalReturnNoteService(serviceProvider);
        }

        protected GarmentShippingLocalReturnNoteViewModel ViewModel
        {
            get
            {
                return new GarmentShippingLocalReturnNoteViewModel
                {
                    items = new List<GarmentShippingLocalReturnNoteItemViewModel>()
                    {
                        new GarmentShippingLocalReturnNoteItemViewModel()
                        {
                            isChecked = true
                        }
                    }
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalReturnNoteRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingLocalReturnNoteModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalReturnNoteModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentShippingLocalReturnNoteModel("", 1, DateTimeOffset.Now, "", new List<GarmentShippingLocalReturnNoteItemModel>());

            var repoMock = new Mock<IGarmentShippingLocalReturnNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalReturnNoteModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var items = new List<GarmentShippingLocalReturnNoteItemModel>() { new GarmentShippingLocalReturnNoteItemModel(1, 1) };
            var model = new GarmentShippingLocalReturnNoteModel("", 1, DateTimeOffset.Now, "", items);

            var repoMock = new Mock<IGarmentShippingLocalReturnNoteRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalReturnNoteRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
    }
}
