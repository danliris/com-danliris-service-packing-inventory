using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentPaymentDispositionReport;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.InsuranceDisposition;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalReturnNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalPriceCuttingNote;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDisposition;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.Monitoring
{
    public class GarmentPaymentDispositionReportServiceTest
    {
        public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingPaymentDispositionRepository repository)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingPaymentDispositionRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentPaymentDispositionReportService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentPaymentDispositionReportService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success()
        {
            var bills = new List<GarmentShippingPaymentDispositionBillDetailModel>
                {
                     new GarmentShippingPaymentDispositionBillDetailModel("", 6000)
                         {
                           PaymentDispositionId = 1
                         },
                     new GarmentShippingPaymentDispositionBillDetailModel("", 4000)
                         {
                           PaymentDispositionId = 1
                         },       
                };

            var invoices = new List<GarmentShippingPaymentDispositionInvoiceDetailModel>
                {
                     new GarmentShippingPaymentDispositionInvoiceDetailModel("DL/1", 1, 1, 1, 1, 1, 1, 1)
                         {
                           PaymentDispositionId = 1
                         },
                     new GarmentShippingPaymentDispositionInvoiceDetailModel("DL/2", 2, 1, 1, 1, 1, 1, 1)
                         {
                           PaymentDispositionId = 1
                         },
                };

            var units = new List<GarmentShippingPaymentDispositionUnitChargeModel>
                {
                     new GarmentShippingPaymentDispositionUnitChargeModel(1, "C2A", 15, 1500)
                         {
                           PaymentDispositionId = 1
                         },
                     new GarmentShippingPaymentDispositionUnitChargeModel(2, "C2B", 25, 2500)
                         {
                           PaymentDispositionId = 1
                         },
                     new GarmentShippingPaymentDispositionUnitChargeModel(3, "C2C", 20, 2000)
                         {
                           PaymentDispositionId = 1
                         },
                     new GarmentShippingPaymentDispositionUnitChargeModel(4, "C1A", 10, 1000)
                         {
                           PaymentDispositionId = 1
                         },
                     new GarmentShippingPaymentDispositionUnitChargeModel(5, "C1B", 30, 3000)
                         {
                           PaymentDispositionId = 1
                         },
                };

            var payments = new List<GarmentShippingPaymentDispositionPaymentDetailModel>
                {
                     new GarmentShippingPaymentDispositionPaymentDetailModel(DateTimeOffset.Now, "", 6000)
                         {
                           PaymentDispositionId = 1
                         },
                     new GarmentShippingPaymentDispositionPaymentDetailModel(DateTimeOffset.Now, "", 4000)
                         {
                           PaymentDispositionId = 1
                         },
                };

            var model = new GarmentShippingPaymentDispositionModel("", "", "", "", "", 1, "", "", "", 1, "", "", 1, "", "", 1, "", "", "", "", "", DateTimeOffset.Now, "", 1, 1, 1, "", 1, 1, 1, DateTimeOffset.Now, "", "", true, "", "", DateTimeOffset.Now, "", "", "", invoices, bills, units, payments)
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentShippingPaymentDispositionRepository>();
            repoMock.Setup(s => s.ReadAll())               
                .Returns(new List<GarmentShippingPaymentDispositionModel>() { model }.AsQueryable());

            repoMock.Setup(s => s.ReadUnitAll())
              .Returns(units.AsQueryable());

            repoMock.Setup(s => s.ReadInvAll())
              .Returns(invoices.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GetReportData(null, DateTime.MinValue, DateTime.Now, 0);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GenerateExcel_Success()
        {
            var bills = new List<GarmentShippingPaymentDispositionBillDetailModel>
                {
                     new GarmentShippingPaymentDispositionBillDetailModel("", 6000)
                         {
                           PaymentDispositionId = 1
                         },
                     new GarmentShippingPaymentDispositionBillDetailModel("", 4000)
                         {
                           PaymentDispositionId = 1
                         },
                };

            var invoices = new List<GarmentShippingPaymentDispositionInvoiceDetailModel>
                {
                     new GarmentShippingPaymentDispositionInvoiceDetailModel("DL/1", 1, 1, 1, 1, 1, 1, 1)
                         {
                           PaymentDispositionId = 1
                         },
                     new GarmentShippingPaymentDispositionInvoiceDetailModel("DL/2", 2, 1, 1, 1, 1, 1, 1)
                         {
                           PaymentDispositionId = 1
                         },
                };

            var units = new List<GarmentShippingPaymentDispositionUnitChargeModel>
                {
                     new GarmentShippingPaymentDispositionUnitChargeModel(1, "C2A", 15, 1500)
                         {
                           PaymentDispositionId = 1
                         },
                     new GarmentShippingPaymentDispositionUnitChargeModel(2, "C2B", 25, 2500)
                         {
                           PaymentDispositionId = 1
                         },
                     new GarmentShippingPaymentDispositionUnitChargeModel(3, "C2C", 20, 2000)
                         {
                           PaymentDispositionId = 1
                         },
                     new GarmentShippingPaymentDispositionUnitChargeModel(4, "C1A", 10, 1000)
                         {
                           PaymentDispositionId = 1
                         },
                     new GarmentShippingPaymentDispositionUnitChargeModel(5, "C1B", 30, 3000)
                         {
                           PaymentDispositionId = 1
                         },
                };

            var payments = new List<GarmentShippingPaymentDispositionPaymentDetailModel>
                {
                     new GarmentShippingPaymentDispositionPaymentDetailModel(DateTimeOffset.Now, "", 6000)
                         {
                           PaymentDispositionId = 1
                         },
                     new GarmentShippingPaymentDispositionPaymentDetailModel(DateTimeOffset.Now, "", 4000)
                         {
                           PaymentDispositionId = 1
                         },
                };

            var model = new GarmentShippingPaymentDispositionModel("", "", "", "", "", 1, "", "", "", 1, "", "", 1, "", "", 1, "", "", "", "", "", DateTimeOffset.Now, "", 1, 1, 1, "", 1, 1, 1, DateTimeOffset.Now, "", "", true, "", "", DateTimeOffset.Now, "", "", "", invoices, bills, units, payments)
            {
                Id = 1
            };

            var repoMock = new Mock<IGarmentShippingPaymentDispositionRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingPaymentDispositionModel>() { model }.AsQueryable());

            repoMock.Setup(s => s.ReadUnitAll())
              .Returns(units.AsQueryable());

            repoMock.Setup(s => s.ReadInvAll())
             .Returns(invoices.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GenerateExcel(null, DateTime.MinValue, DateTime.Now, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {           
            var repoMock = new Mock<IGarmentShippingPaymentDispositionRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingPaymentDispositionModel>().AsQueryable());

            repoMock.Setup(s => s.ReadUnitAll())
             .Returns(new List<GarmentShippingPaymentDispositionUnitChargeModel>().AsQueryable());

            repoMock.Setup(s => s.ReadInvAll())
              .Returns(new List<GarmentShippingPaymentDispositionInvoiceDetailModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GenerateExcel(null, DateTime.MinValue, DateTime.Now, 7);

            Assert.NotNull(result);
        }
    }
}
