using Com.Danliris.Service.Packing.Inventory.Application.Master.UpsertMaster;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Master;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Master.UpsertMaster
{
    public class UpsertMasterServiceTest
    {
        public UpsertMasterService GetService(IServiceProvider serviceProvider)
        {
            return new UpsertMasterService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(
           IWeftTypeRepository weftTypeRepository
          )
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock.Setup(s => s.GetService(typeof(IWeftTypeRepository)))
                .Returns(weftTypeRepository);

            return serviceProviderMock;
        }

        [Fact]
        public async Task UpsertUOM_Return_Success()
        {
            var weftTypeRepository = new Mock<IWeftTypeRepository>();


            var service = GetService(GetServiceProvider(
               weftTypeRepository.Object
               ).Object);

            //var result = await service.UpsertUOM("anystring");
            await Assert.ThrowsAsync<NotImplementedException>(() => service.UpsertUOM("anystring"));
        }

        [Fact]
        public async Task UpsertWeftType_Return_Success()
        {
            var weftTypeRepository = new Mock<IWeftTypeRepository>();


            var service = GetService(GetServiceProvider(
               weftTypeRepository.Object
               ).Object);

            //var result = await service.UpsertUOM("anystring");
            await Assert.ThrowsAsync<NotImplementedException>(() => service.UpsertWeftType("anystring"));
        }
    }
}
