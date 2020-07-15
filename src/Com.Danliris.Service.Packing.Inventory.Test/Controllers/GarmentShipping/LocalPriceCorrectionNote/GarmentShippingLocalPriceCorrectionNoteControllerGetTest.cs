using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCorrectionNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingLocalPriceCorrectionNote
{
    public class GarmentShippingLocalPriceCorrectionNoteControllerGetTest : GarmentShippingLocalPriceCorrectionNoteControllerTest
    {
        private IndexViewModel IndexViewModel
        {
            get {
                return new IndexViewModel
                {
                    id = 1,
                    correctionNoteNo = "",
                    salesNote = new SalesNote()
                };
            }
        }

        [Fact]
        public void Get_Ok()
        {
            var serviceMock = new Mock<IGarmentShippingLocalPriceCorrectionNoteService>();
            serviceMock
                .Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<IndexViewModel>(new List<IndexViewModel>()
                {
                    new IndexViewModel
                    {
                        id = IndexViewModel.id,
                        correctionNoteNo = IndexViewModel.correctionNoteNo,
                        correctionDate = IndexViewModel.correctionDate,
                        salesNote = new SalesNote
                        {
                            id = IndexViewModel.salesNote.id,
                            noteNo = IndexViewModel.salesNote.noteNo,
                            buyer = IndexViewModel.salesNote.buyer,
                            date = IndexViewModel.salesNote.date,
                            tempo = IndexViewModel.salesNote.tempo,
                            dispositionNo = IndexViewModel.salesNote.dispositionNo
                        }
                    }
                }, 1, 1, 1));
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Get_Exception_InternalServerError()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentShippingLocalPriceCorrectionNoteService>();
            serviceMock
                .Setup(s => s.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService);
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
