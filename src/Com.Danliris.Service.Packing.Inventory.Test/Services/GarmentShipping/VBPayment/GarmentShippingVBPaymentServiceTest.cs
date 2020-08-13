using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.VBPayment;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.VBPayment;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.VBPayment;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.VBPayment
{
    public class GarmentShippingVBPaymentServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingVBPaymentRepository repository)
        {
            HttpResponseMessage message = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingVBPaymentRepository)))
                .Returns(repository);

            return spMock;
        }

        protected GarmentShippingVBPaymentService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingVBPaymentService(serviceProvider);
        }

        protected GarmentShippingVBPaymentViewModel ViewModel
        {
            get
            {
                return new GarmentShippingVBPaymentViewModel
                {
                    units = new List<GarmentShippingVBPaymentUnitViewModel>()
                    {
                        new GarmentShippingVBPaymentUnitViewModel()
                    },
                    invoices= new List<GarmentShippingVBPaymentInvoiceViewModel>()
                    {
                        new GarmentShippingVBPaymentInvoiceViewModel()
                    },
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentShippingVBPaymentRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingVBPaymentModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingVBPaymentModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentShippingVBPaymentModel("", DateTimeOffset.Now, "", 1, "", "", 1, "", "", 1, "", "", "", "", 1, 1, DateTimeOffset.Now, 1, "", 1, new List<GarmentShippingVBPaymentUnitModel>(), new List<GarmentShippingVBPaymentInvoiceModel>());

            var repoMock = new Mock<IGarmentShippingVBPaymentRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingVBPaymentModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var units = new HashSet<GarmentShippingVBPaymentUnitModel> { new GarmentShippingVBPaymentUnitModel(1, "", "", 1) };
            var invoices = new HashSet<GarmentShippingVBPaymentInvoiceModel> { new GarmentShippingVBPaymentInvoiceModel(1, "") };
            var model = new GarmentShippingVBPaymentModel("", DateTimeOffset.Now, "", 1, "", "", 1, "", "", 1, "", "", "", "", 1, 1, DateTimeOffset.Now, 1, "", 1, units,invoices);
            
            var repoMock = new Mock<IGarmentShippingVBPaymentRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var repoMock = new Mock<IGarmentShippingVBPaymentRepository>();
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentShippingVBPaymentModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var repoMock = new Mock<IGarmentShippingVBPaymentRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }

    }
}
