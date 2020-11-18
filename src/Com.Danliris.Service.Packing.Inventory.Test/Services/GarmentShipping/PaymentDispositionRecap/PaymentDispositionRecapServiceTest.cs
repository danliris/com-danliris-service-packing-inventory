using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.PaymentDispositionRecap;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDispositionRecap;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDispositionRecap;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.PaymentDispositionRecap
{
    public class PaymentDispositionRecapServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingPaymentDispositionRecapRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingPaymentDispositionRecapRepository)))
                .Returns(repository);

            return spMock;
        }

        protected PaymentDispositionRecapService GetService(IServiceProvider serviceProvider)
        {
            return new PaymentDispositionRecapService(serviceProvider);
        }

        protected PaymentDispositionRecapViewModel ViewModel
        {
            get
            {
                return new PaymentDispositionRecapViewModel
                {
                    items = new List<PaymentDispositionRecapItemViewModel>()
                    {
                        new PaymentDispositionRecapItemViewModel
                        {
                            paymentDisposition = new GarmentShippingPaymentDispositionViewModel
                            {
                                invoiceDetails = new List<GarmentShippingPaymentDispositionInvoiceDetailViewModel>()
                                {
                                    new GarmentShippingPaymentDispositionInvoiceDetailViewModel()
                                    {

                                    }
                                }
                            }
                        }
                    },
                };
            }
        }

        [Fact]
        public async Task Create_Success()
        {
            var repoMock = new Mock<IGarmentShippingPaymentDispositionRecapRepository>();
            repoMock.Setup(s => s.InsertAsync(It.IsAny<GarmentShippingPaymentDispositionRecapModel>()))
                .ReturnsAsync(1);
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingPaymentDispositionRecapModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Create(ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public void Read_Success()
        {
            var model = new GarmentShippingPaymentDispositionRecapModel("", DateTimeOffset.Now, 1, "", "", "", "", new List<GarmentShippingPaymentDispositionRecapItemModel>());

            var repoMock = new Mock<IGarmentShippingPaymentDispositionRecapRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingPaymentDispositionRecapModel>() { model }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.Read(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task ReadById_Success()
        {
            var bills = new HashSet<GarmentShippingPaymentDispositionBillDetailModel> { new GarmentShippingPaymentDispositionBillDetailModel("", 1) { Id = 1 } };
            var units = new HashSet<GarmentShippingPaymentDispositionUnitChargeModel> { new GarmentShippingPaymentDispositionUnitChargeModel(1, "", 1, 1) { Id = 1 } };
            var invoices = new HashSet<GarmentShippingPaymentDispositionInvoiceDetailModel> { new GarmentShippingPaymentDispositionInvoiceDetailModel("", 1, 1, 1, 1, 1, 1, 1) { Id = 1 } };
            var dispoModel = new GarmentShippingPaymentDispositionModel("", "", "", "", "", 1, "", "", "", 1, "", "", 1, "", "", 1, "", "", "", "", "", DateTimeOffset.Now, "", 1, 1, 1, "", 1, 1, 1, DateTimeOffset.Now, "", "", true, "", "", DateTimeOffset.Now, "", "", "", invoices, bills, units) { Id = 1 };

            var item = new GarmentShippingPaymentDispositionRecapItemModel(1, 10) { Id = 1 };
            item.SetPaymentDisposition(dispoModel);
            var items = new HashSet<GarmentShippingPaymentDispositionRecapItemModel> { item };
            var model = new GarmentShippingPaymentDispositionRecapModel("", DateTimeOffset.Now, 1, "", "", "", "", items) { Id = 1 };

            var repoMock = new Mock<IGarmentShippingPaymentDispositionRecapRepository>();
            repoMock.Setup(s => s.ReadByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            var dispoRepoMock = new Mock<IGarmentShippingPaymentDispositionRepository>();
            dispoRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingPaymentDispositionModel> { dispoModel }.AsQueryable());

            var itemsInvoice = new HashSet<GarmentShippingInvoiceItemModel> { new GarmentShippingInvoiceItemModel("ro", "scno", 1, "buyerbrandname", 1, 1, "comocode", "comoname", "comodesc", "comodesc", "comodesc", "comodesc", 1, "pcs", 10, 10, 100, "usd", 1, "unitcode", 3) { Id = 1 } };
            var adjustmentsInvoice = new HashSet<GarmentShippingInvoiceAdjustmentModel> { new GarmentShippingInvoiceAdjustmentModel(1, "fee", 100) { Id = 1 } };
            var unitsInvoice = new HashSet<GarmentShippingInvoiceUnitModel> { new GarmentShippingInvoiceUnitModel(1, "unitcode", 3, 1) { Id = 1 } };
            var invoiceModel = new GarmentShippingInvoiceModel(1, "invoiceno", DateTimeOffset.Now, "from", "to", 1, "buyercode", "buyername", "consignee", "lcno", "issuedby", 1, "sectioncode", "shippingper", DateTimeOffset.Now, "confNo", 1, "staff", 1, "cottn", 1, "mandiri", 10, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", itemsInvoice, 1000, "dddd", "dsdsds", "memo", false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, adjustmentsInvoice, 100000, "aa", "aa", unitsInvoice) { Id = 1 };
            var invoiceRepoMock = new Mock<IGarmentShippingInvoiceRepository>();
            invoiceRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel> { invoiceModel }.AsQueryable());

            var measurements = new HashSet<GarmentPackingListMeasurementModel> { new GarmentPackingListMeasurementModel(1, 1, 1, 1) { Id = 1 } };
            var packingListModel = new GarmentPackingListModel("", "", "", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, DateTimeOffset.Now, false, false, "", "", "", null, 1, 1, 1, measurements, "", "", "", "", "", "", "", false, false, 1, "", GarmentPackingListStatusEnum.CREATED) { Id = 1 };
            var packingListRepoMock = new Mock<IGarmentPackingListRepository>();
            packingListRepoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel> { packingListModel }.AsQueryable());

            var spMock = GetServiceProvider(repoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingPaymentDispositionRepository)))
                .Returns(dispoRepoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceRepository)))
                .Returns(invoiceRepoMock.Object);
            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
                .Returns(packingListRepoMock.Object);

            var service = GetService(spMock.Object);

            var result = await service.ReadById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Update_Success()
        {
            var repoMock = new Mock<IGarmentShippingPaymentDispositionRecapRepository>();
            repoMock.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<GarmentShippingPaymentDispositionRecapModel>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Update(1, ViewModel);

            Assert.NotEqual(0, result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var repoMock = new Mock<IGarmentShippingPaymentDispositionRecapRepository>();
            repoMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.Delete(1);

            Assert.NotEqual(0, result);
        }
    }
}
