using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.AmendLetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.AmendLetterOfCredit;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.AmendLetterOfCredit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.AmendLetterOfCredit
{
    public class GarmentAmendLetterOfCreditServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentAmendLetterOfCreditRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentAmendLetterOfCreditRepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentAmendLetterOfCreditService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentAmendLetterOfCreditService(serviceProvider);
        }

        protected GarmentAmendLetterOfCreditViewModel ViewModel
        {
            get
            {
                return new GarmentAmendLetterOfCreditViewModel
                {
                };
            }
        }

        // Command 30-01-2024
        //[Fact]
        //public async Task Create_Success()
        //{
        //    var repoMock = new Mock<IGarmentAmendLetterOfCreditRepository>();
        //    repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingAmendLetterOfCreditModel>()))
        //        .ReturnsAsync(1);
        //    repoMock.Setup(s => s.ReadAll())
        //        .Returns(new List<GarmentShippingAmendLetterOfCreditModel>().AsQueryable());

        //    var service = GetService(GetServiceProvider(repoMock.Object).Object);

        //    var result = await service.Create(ViewModel);

        //    Assert.NotEqual(0, result);
        //}

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentShippingAmendLetterOfCreditModel("no", 1, 1, DateTimeOffset.Now, "", 2);

            var repoMock = new Mock<IGarmentAmendLetterOfCreditRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingAmendLetterOfCreditModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var model = new GarmentShippingAmendLetterOfCreditModel("no", 1, 1, DateTimeOffset.Now, "", 2);

            var repoMock = new Mock<IGarmentAmendLetterOfCreditRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        // Command 30-01-2024
        //[Fact]
        //public async Task Update_Success()
        //{
        //    var repoMock = new Mock<IGarmentAmendLetterOfCreditRepository>();
        //    repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentShippingAmendLetterOfCreditModel>()))
        //        .ReturnsAsync(1);

        //    var service = GetService(GetServiceProvider(repoMock.Object).Object);

        //    var result = await service.Update(1, ViewModel);

        //    Assert.NotEqual(0, result);
        //}

        // Command 30-01-2024
        //[Fact]
        //public async Task Delete_Success()
        //{
        //    var repoMock = new Mock<IGarmentAmendLetterOfCreditRepository>();
        //    repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
        //        .ReturnsAsync(1);

        //    var service = GetService(GetServiceProvider(repoMock.Object).Object);

        //    var result = await service.Delete(1);

        //    Assert.NotEqual(0, result);
        //}
    }
}
