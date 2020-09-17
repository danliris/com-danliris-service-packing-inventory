﻿using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services.GarmentShipping.Monitoring
{
    public class GarmentShippingInstructionMonitoringServiceTest
    {
       public Mock<IServiceProvider> GetServiceProvider(IGarmentShippingInstructionRepository repository, IGarmentShippingInvoiceRepository invrepository, IGarmentPackingListRepository plrepository, IGarmentCoverLetterRepository clrepository)

        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInstructionRepository)))
                .Returns(repository);

            spMock.Setup(s => s.GetService(typeof(IGarmentShippingInvoiceRepository)))
              .Returns(invrepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentPackingListRepository)))
                .Returns(plrepository);

            spMock.Setup(s => s.GetService(typeof(IGarmentCoverLetterRepository)))
               .Returns(clrepository);
 
            spMock.Setup(s => s.GetService(typeof(IIdentityProvider)))
                .Returns(new IdentityProvider());

            return spMock;
        }

        protected GarmentShippingInstructionMonitoringService GetService(IServiceProvider serviceProvider)
        {
            return new GarmentShippingInstructionMonitoringService(serviceProvider);
        }

        [Fact]
        public void GetReportData_Success()
        {
            var model = new GarmentShippingInstructionModel("no", 1, DateTimeOffset.Now, 1, "", "", "", "", "", "", "", 1, "", "", "", DateTimeOffset.Now, "", "", "", "", "", "", "", "", 1, "", 1, "", "", "", "", "", DateTimeOffset.Now, "", "", "")
            {
                Id = 1
            };


            var model1 = new GarmentShippingInvoiceModel(1, "", DateTimeOffset.Now, "", "", 1, "A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "",
                                                      1, "", 1, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", null, 1, "", "", "",
                                                      false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null)
            {
                Id = 1
            };


            var model2 = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "B10", "", "", DateTimeOffset.Now, DateTimeOffset.Now, true, true, null, 1, 1, 1, null, "", "", "", "", true)
            {
                Id = 1
            };

            var model3 = new GarmentShippingCoverLetterModel(1, 1, "", DateTimeOffset.Now, 1, "", "", "", "", "", "", "", DateTimeOffset.Now, 1, "", "", 1, 1, 1, 1, 1, "", "", "", "", "", "", "", "", "", "", DateTimeOffset.Now, "", 1, "")
            {
                Id = 1
            };


            var repoMock = new Mock<IGarmentShippingInstructionRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInstructionModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model1 }.AsQueryable());

            var repoMock2 = new Mock<IGarmentPackingListRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model2 }.AsQueryable());

            var repoMock3 = new Mock<IGarmentCoverLetterRepository>();
            repoMock3.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCoverLetterModel>() { model3 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object, repoMock3.Object).Object);

            var result = service.GetReportData(model.BuyerAgentCode, DateTime.MinValue, DateTime.MaxValue, 0);

            Assert.NotEmpty(result.ToList());
        }

        [Fact]
        public void GenerateExcel_Success()
        {
            var model = new GarmentShippingInstructionModel("no", 1, DateTimeOffset.Now, 1, "", "", "", "", "", "", "", 1, "", "", "", DateTimeOffset.Now, "", "", "", "", "", "", "", "", 1, "", 1, "", "", "", "", "", DateTimeOffset.Now, "", "", "")
            {
                Id = 1
            };

            var model1 = new GarmentShippingInvoiceModel(1, "", DateTimeOffset.Now, "", "", 1,"A99", "", "", "", "", 1, "", "", DateTimeOffset.Now, "", 1, "", 1, "",
                                                        1, "", 1, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", null, 1, "", "", "", 
                                                        false, "", DateTimeOffset.Now, "", DateTimeOffset.Now, "", DateTimeOffset.Now, null, 1, "", "", null)
            {
                Id = 1
            };


            var model2 = new GarmentPackingListModel("", "", "DL", 1, "", DateTimeOffset.Now, "", "", DateTimeOffset.Now, "", 1, "B10", "", "", DateTimeOffset.Now, DateTimeOffset.Now, true, true, null, 1, 1, 1, null, "", "", "", "", true)
            {
                Id = 1
            };

            var model3 = new GarmentShippingCoverLetterModel(1, 1, "", DateTimeOffset.Now, 1, "", "", "", "", "", "", "", DateTimeOffset.Now, 1, "", "", 1, 1, 1, 1, 1, "", "", "", "", "", "", "", "", "", "", DateTimeOffset.Now, "", 1, "")
            {
                Id = 1
            };


            var repoMock = new Mock<IGarmentShippingInstructionRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInstructionModel>() { model }.AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>() { model1 }.AsQueryable());

            var repoMock2 = new Mock<IGarmentPackingListRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>() { model2 }.AsQueryable());

            var repoMock3 = new Mock<IGarmentCoverLetterRepository>();
            repoMock3.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCoverLetterModel>() { model3 }.AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object, repoMock3.Object).Object);

            var result = service.GenerateExcel(model.BuyerAgentCode, DateTime.MinValue, DateTime.MaxValue, 7);

            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateExcel_Empty_Success()
        {
            var repoMock = new Mock<IGarmentShippingInstructionRepository>();
            repoMock.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInstructionModel>().AsQueryable());

            var repoMock1 = new Mock<IGarmentShippingInvoiceRepository>();
            repoMock1.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingInvoiceModel>().AsQueryable());

            var repoMock2 = new Mock<IGarmentPackingListRepository>();
            repoMock2.Setup(s => s.ReadAll())
                .Returns(new List<GarmentPackingListModel>().AsQueryable());

            var repoMock3 = new Mock<IGarmentCoverLetterRepository>();
            repoMock3.Setup(s => s.ReadAll())
                .Returns(new List<GarmentShippingCoverLetterModel>().AsQueryable());

            var service = GetService(GetServiceProvider(repoMock.Object, repoMock1.Object, repoMock2.Object, repoMock3.Object).Object);

            var result = service.GenerateExcel(null, null, null, 7);

            Assert.NotNull(result);
        }
    }
}
