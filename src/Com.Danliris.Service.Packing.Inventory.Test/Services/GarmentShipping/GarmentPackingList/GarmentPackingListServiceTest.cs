using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListServiceTest
    {
        protected GarmentPackingListService GetService()
        {
            return new GarmentPackingListService();
        }

        protected GarmentPackingListViewModel ViewModel
        {
            get
            {
                return new GarmentPackingListViewModel();
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var service = GetService();

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var service = GetService();

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var service = GetService();

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var service = GetService();

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var service = GetService();

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
    }
}
