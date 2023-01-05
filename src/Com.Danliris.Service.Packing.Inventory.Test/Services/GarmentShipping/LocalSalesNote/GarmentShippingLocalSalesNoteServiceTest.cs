using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.GarmentShippingLocalSalesNote
{
    public class GarmentShippingLocalSalesNoteServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingLocalSalesNoteRepository repository)
        {
            HttpResponseMessage message = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var HttpClientService = new Mock<IHttpClientService>();
            HttpClientService
                .Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(message);

            HttpClientService
                .Setup(x => x.GetAsync(It.IsRegex($"^master/garment-leftover-warehouse-buyers")))
                .ReturnsAsync(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new
                    {
                        apiVersion = "1.0",
                        statusCode = 200,
                        message = "Ok",
                        data = JsonConvert.SerializeObject(new Buyer { })
                    }))
                });

            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingLocalSalesNoteRepository)))
                .Returns(repository);
            spMock
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(HttpClientService.Object);
            return spMock;
        }

        public Mock<IServiceProvider> GetServiceProvider_Error(IGarmentShippingLocalSalesNoteRepository repository)
        {
            HttpResponseMessage message = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var HttpClientService = new Mock<IHttpClientService>();
            HttpClientService
                .Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(message);

            HttpClientService
               .Setup(x => x.GetAsync(It.IsRegex($"^master/garment-leftover-warehouse-buyers")))
               .ReturnsAsync(new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
               {
                   Content = new StringContent(JsonConvert.SerializeObject(new
                   {
                       apiVersion = "1.0",
                       statusCode = 500,
                       message = "Ok",
                       data = JsonConvert.SerializeObject(new Buyer { })
                   }))
               });

            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingLocalSalesNoteRepository)))
                .Returns(repository);
            spMock
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(HttpClientService.Object);
            return spMock;
        }

        protected GarmentShippingLocalSalesNoteService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingLocalSalesNoteService(serviceProvider);
        }

        protected GarmentShippingLocalSalesNoteViewModel ViewModel
        {
            get
            {
                return new GarmentShippingLocalSalesNoteViewModel
                {
                    items = new List<GarmentShippingLocalSalesNoteItemViewModel>()
                    {
                        new GarmentShippingLocalSalesNoteItemViewModel()
                    }
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingLocalSalesNoteModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "", "", 1, "", "", "", "", 1, "", "", true, 1, 1, "",false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", 1, "", "", new List<GarmentShippingLocalSalesNoteItemModel>());

            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var items = new List<GarmentShippingLocalSalesNoteItemModel>() { new GarmentShippingLocalSalesNoteItemModel(1,1, "", "", 1, 1, "", 1, 1, 1, "", "") };
            var model = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "", "", 1, "", "", "", "", 1, "", "", true, 1, 1, "", false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", 1, "", "", items);
            var item = new GarmentShippingLocalSalesNoteItemModel();
            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentShippingLocalSalesNoteModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Should_Success_Get_BuyerViewModel()
        {
            var items = new List<GarmentShippingLocalSalesNoteItemModel>() { new GarmentShippingLocalSalesNoteItemModel(1,1, "", "", 1, 1, "", 1, 1, 1, "", "") };
            var model = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "", "", 1, "", "", "", "", 1, "", "", true, 1, 1,"", false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", 1, "", "", items);

            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);
            var result = service.GetBuyer(It.IsAny<int>());

            Assert.NotNull(result);
        }

        [Fact]
        public void ReadShippingLocalSalesNoteListNow_Success()
        {
            var model1 = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "A99", "", 1, "", "", "", "", 1, "", "", true, 1, 1, "", true, true,true,"","", DateTimeOffset.Now, DateTimeOffset.Now, true,true,"" , 1, "", "", new List<GarmentShippingLocalSalesNoteItemModel>())
            {
                Id = 1
            };


            var repoMock1 = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>() { model1 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock1.Object).Object);

            var result = service.ReadShippingLocalSalesNoteListNow(DateTimeOffset.Now.Month, DateTimeOffset.Now.Year);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void Should_Null_Get_BuyerViewModel()
        {
            var items = new List<GarmentShippingLocalSalesNoteItemModel>() { new GarmentShippingLocalSalesNoteItemModel(1,1, "", "", 1, 1, "", 1, 1, 1, "", "") };
            var model = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "", "", 1, "", "", "", "", 1, "", "", true, 1, 1, "", false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", 1, "", "", items);

            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);


            var service = GetService(GetServiceProvider_Error(repoMock.Object).Object);
            var result = service.GetBuyer(It.IsAny<int>());

            Assert.Null(result);
        }

        [Fact]
        public void ReadForFinance_Success()
        {
            var item = new GarmentShippingLocalSalesNoteItemModel(1, 1, "code", "name", 1, 1, "uom", 1, 1, 1, "uom", "");
            var items = new List<GarmentShippingLocalSalesNoteItemModel>();
            items.Add(item);
            var model = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "", "", 1, "", "", "", "", 1, "", "", true, 1, 1, "", false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", 1, "", "", items);

            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.ReadSalesNoteForFinance("now", model.Date.Month,model.Date.Year,model.BuyerCode);

            Assert.NotEmpty(result.ToList());

            int month = 0;
            int year = 0;
            if (model.Date.Month == 12)
            {
                month = 1;
                year = model.Date.Year+1;
            }
            else {
                month = model.Date.Month+1;
                year = model.Date.Year;
            }

            var result2 = service.ReadSalesNoteForFinance("", month, year, null);
            //var result2 = service.ReadSalesNoteForFinance("", model.Date.Month, model.Date.Year, null);

            Assert.NotEmpty(result2.ToList());
        }
        
        [Fact]
        public async Task ApproveShipping_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ApproveShippingAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ApproveShipping(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task ApproveFinance_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ApproveFinanceAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ApproveFinance(1);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task RejectShipping_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.RejectShippingAsync(It.IsAny<int>(), It.IsAny<GarmentShippingLocalSalesNoteModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.RejectedShipping(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task RejectFinance_Success()
        {
            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.RejectFinanceAsync(It.IsAny<int>(), It.IsAny<GarmentShippingLocalSalesNoteModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.RejectedFinance(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void ReadLocalDebtor_Success()
        {
            var item = new GarmentShippingLocalSalesNoteItemModel(1, 1, "code", "name", 1, 1, "uom", 1, 1, 1, "uom", "");
            var items = new List<GarmentShippingLocalSalesNoteItemModel>();
            items.Add(item);
            var model = new GarmentShippingLocalSalesNoteModel("", 1, "", "", DateTimeOffset.Now, 1, "", "", 1, "", "", "", "", 1, "", "", true, 1, 1, "", false, false, false, null, null, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", 1, "", "", items);

            var repoMock = new Mock<IGarmentShippingLocalSalesNoteRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingLocalSalesNoteModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.ReadLocalSalesDebtor("now", model.Date.Month, model.Date.Year);

            Assert.NotEmpty(result.ToList());

            int month = 0;
            int year = 0;
            if (model.Date.Month == 12)
            {
                month = 1;
                year = model.Date.Year + 1;
            }
            else
            {
                month = model.Date.Month + 1;
                year = model.Date.Year;
            }

            var result2 = service.ReadLocalSalesDebtor("", month, year);

            Assert.NotEmpty(result2.ToList());
        }
    }
}
