using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.InsuranceDisposition;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.InsuranceDisposition;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.InsuranceDisposition;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.InsuranceDisposition
{
    public class GarmentShippingInsuranceDispositionServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingInsuranceDispositionRepository repository)
        {
            HttpResponseMessage message = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var HttpClientService = new Mock<IHttpClientService>();
            HttpClientService
                .Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(message);

            HttpClientService
                .Setup(x => x.GetAsync(It.IsRegex($"^master/garment-insurances")))
                .ReturnsAsync(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new
                    {
                        apiVersion = "1.0",
                        statusCode = 200,
                        message = "Ok",
                        data = JsonConvert.SerializeObject(new Insurance { })
                    }))
                });

            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInsuranceDispositionRepository)))
                .Returns(repository);
            spMock
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(HttpClientService.Object);
            return spMock;
        }

        public Mock<IServiceProvider> GetServiceProvider_Error(IGarmentShippingInsuranceDispositionRepository repository)
        {
            HttpResponseMessage message = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var HttpClientService = new Mock<IHttpClientService>();
            HttpClientService
                .Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(message);

            HttpClientService
               .Setup(x => x.GetAsync(It.IsRegex($"^master/garment-insurances")))
               .ReturnsAsync(new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
               {
                   Content = new StringContent(JsonConvert.SerializeObject(new
                   {
                       apiVersion = "1.0",
                       statusCode = 500,
                       message = "Ok",
                       data = JsonConvert.SerializeObject(new Insurance { })
                   }))
               });

            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInsuranceDispositionRepository)))
                .Returns(repository);
            spMock
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(HttpClientService.Object);
            return spMock;
        }

        protected GarmentShippingInsuranceDispositionService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingInsuranceDispositionService(serviceProvider);
        }

        protected GarmentShippingInsuranceDispositionViewModel ViewModel
        {
            get
            {
                return new GarmentShippingInsuranceDispositionViewModel
                {
                    items = new List<GarmentShippingInsuranceDispositionItemViewModel>()
                    {
                        new GarmentShippingInsuranceDispositionItemViewModel()
                    },
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentShippingInsuranceDispositionRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingInsuranceDispositionModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInsuranceDispositionModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentShippingInsuranceDispositionModel("", "", DateTimeOffset.Now, "", 1, "", "", 1, "", new List<GarmentShippingInsuranceDispositionItemModel>());

            var repoMock = new Mock<IGarmentShippingInsuranceDispositionRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInsuranceDispositionModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var items = new HashSet<GarmentShippingInsuranceDispositionItemModel> { new GarmentShippingInsuranceDispositionItemModel(DateTimeOffset.Now, "", "", 1, 1, "", "", 1, 1, 1, 1, 1, 1, 1), new GarmentShippingInsuranceDispositionItemModel(DateTimeOffset.Now, "", "", 2, 2, "", "", 2, 2, 2, 2, 2, 2, 2) };
            var model = new GarmentShippingInsuranceDispositionModel("", "", DateTimeOffset.Now, "", 1, "", "", 1, "",  items);
            
            var repoMock = new Mock<IGarmentShippingInsuranceDispositionRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var repoMock = new Mock<IGarmentShippingInsuranceDispositionRepository>();
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentShippingInsuranceDispositionModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var repoMock = new Mock<IGarmentShippingInsuranceDispositionRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_Get_InsuranceViewModel()
        {
            var items = new HashSet<GarmentShippingInsuranceDispositionItemModel> { new GarmentShippingInsuranceDispositionItemModel(DateTimeOffset.Now, "", "", 1, 1, "", "", 1, 1, 1, 1, 1, 1, 1), new GarmentShippingInsuranceDispositionItemModel(DateTimeOffset.Now, "", "", 2, 2, "", "", 2, 2, 2, 2, 2, 2, 2) };
            var model = new GarmentShippingInsuranceDispositionModel("", "", DateTimeOffset.Now, "", 1, "", "", 1, "", items);

            var repoMock = new Mock<IGarmentShippingInsuranceDispositionRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);
            var result = service.GetInsurance(It.IsAny<int>());

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Null_Get_InsuranceViewModel()
        {
            var items = new HashSet<GarmentShippingInsuranceDispositionItemModel> { new GarmentShippingInsuranceDispositionItemModel(DateTimeOffset.Now, "", "", 1, 1, "", "", 1, 1, 1, 1, 1, 1, 1), new GarmentShippingInsuranceDispositionItemModel(DateTimeOffset.Now, "", "", 2, 2, "", "", 2, 2, 2, 2, 2, 2, 2) };
            var model = new GarmentShippingInsuranceDispositionModel("", "", DateTimeOffset.Now, "", 1, "", "", 1, "", items);

            var repoMock = new Mock<IGarmentShippingInsuranceDispositionRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);


            var service = GetService(GetServiceProvider_Error(repoMock.Object).Object);
            var result = service.GetInsurance(It.IsAny<int>());

            Assert.Null(result);
        }
    }
}
