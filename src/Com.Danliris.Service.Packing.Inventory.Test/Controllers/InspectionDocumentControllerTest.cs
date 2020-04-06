using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.InspectionDocumentReport;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers
{
    public class InspectionDocumentControllerTest
    {
        #region Constructor
        private InspectionDocumentController GetController(IInspectionDocumentReportService service, IIdentityProvider identityProvider)
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new InspectionDocumentController(service, identityProvider)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = claimPrincipal.Object
                    }
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer unittesttoken";
            controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            return controller;
        }
        #endregion

        #region Helper
        private int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        private string GetBodyAsString(IActionResult response)
        {
            var result = (string)response.GetType().GetProperty("Value").GetValue(response, null);
            return result;
        }
        #endregion

        #region DummyData
        private List<IndexViewModel> GetBodyAsIndexViewModel(IActionResult response)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<IndexViewModel>>(GetBodyAsString(response),
                    new Newtonsoft.Json.JsonSerializerSettings
                    {
                        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
                    });
            }
            catch(Exception e)
            {
                return new List<IndexViewModel>();
            }
        }
        private List<IndexViewModel> ListViewModelDummy
        {
            get
            {
                string test = "[{'Index':'1','DateReport':'2020-03-16','GroupText':'SIANG','UnitText':'P','KeluarKe':'TRANSIT','NoSPP':'P/2020/0261','NoKereta':'1-1-9','Material':'CD 74 56 44','Keterangan':'NOT OK','Status':'PERBAIKAN','Lebar':'44','Motif':'PRJ200','Warna':'HIJAU','Mtr':'247.00','Yds':'270.22'},{'Index':'2','DateReport':'2020-03-16','GroupText':'SIANG','UnitText':'P','KeluarKe':'TRANSIT','NoSPP':'P/2020/0556','NoKereta':'1-1-6','Material':'CD 94 72 44','Keterangan':'NOT OK','Status':'PERBAIKAN','Lebar':'44','Motif':'TW 1069','Warna':'A','Mtr':'221.00','Yds':'241.77'},{'Index':'3','DateReport':'2020-03-16','GroupText':'SIANG','UnitText':'P','KeluarKe':'TRANSIT','NoSPP':'P/2020/0787','NoKereta':'1-1-10','Material':'CD 94 72 44','Keterangan':'NOT OK','Status':'PERBAIKAN','Lebar':'44','Motif':'Bp 763','Warna':'0','Mtr':'225.00','Yds':'246.15'}]";
                List<IndexViewModel> listModel = Newtonsoft.Json.JsonConvert.DeserializeObject<List<IndexViewModel>>(test);
                return listModel;
            }
        }
        private InspectionDocumentReportItemViewModel ViewModelDummy
        {
            get
            {
                string test = "[{'Index':'1','DateReport':'2020-03-16','GroupText':'SIANG','UnitText':'P','KeluarKe':'TRANSIT','NoSPP':'P/2020/0261','NoKereta':'1-1-9','Material':'CD 74 56 44','Keterangan':'NOT OK','Status':'PERBAIKAN','Lebar':'44','Motif':'PRJ200','Warna':'HIJAU','Mtr':'247.00','Yds':'270.22'},{'Index':'2','DateReport':'2020-03-16','GroupText':'SIANG','UnitText':'P','KeluarKe':'TRANSIT','NoSPP':'P/2020/0556','NoKereta':'1-1-6','Material':'CD 94 72 44','Keterangan':'NOT OK','Status':'PERBAIKAN','Lebar':'44','Motif':'TW 1069','Warna':'A','Mtr':'221.00','Yds':'241.77'},{'Index':'3','DateReport':'2020-03-16','GroupText':'SIANG','UnitText':'P','KeluarKe':'TRANSIT','NoSPP':'P/2020/0787','NoKereta':'1-1-10','Material':'CD 94 72 44','Keterangan':'NOT OK','Status':'PERBAIKAN','Lebar':'44','Motif':'Bp 763','Warna':'0','Mtr':'225.00','Yds':'246.15'}]";
                List<InspectionDocumentReportItemViewModel> listModel = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InspectionDocumentReportItemViewModel>>(test);
                return listModel[0];
            }
        }
        private FilterInspectionDocumentReport FilterMode
        {
            get
            {
                return new FilterInspectionDocumentReport("02 Apr 2020 00:00", "PAGI", "AWAL", "IM","OK");
            }
        }
        private FilterInspectionDocumentReport FilterModeNotFound
        {
            get
            {
                return new FilterInspectionDocumentReport("30 Apr 2020 00:00", "PAGI", "AWAL", "IM", "OK");
            }
        }

        private FilterInspectionDocumentReport FilterModeOtherFormat
        {
            get
            {
                return new FilterInspectionDocumentReport("2020/04/02", "PAGI", "AWAL", "IM", "OK");
            }
        }
        #endregion

        #region TestSection
        [Fact]
        public void Should_Get_InternalServerError_If_AllParameter_Null()
        {
            //v
            var serviceMock = new Mock<IInspectionDocumentReportService>();
            serviceMock.Setup(s => s.GetReport(It.IsAny<DateTimeOffset>(),It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(new List<IndexViewModel>());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            
            var response = controller.GetAll(null,null,null,null,null);
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Get_Success_If_DateReport_OtherFormat()
        {
            //v
            var serviceMock = new Mock<IInspectionDocumentReportService>();
            serviceMock.Setup(s => s.GetReport(It.IsAny<DateTimeOffset>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(ListViewModelDummy);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);

            var response = controller.GetAll(DateTimeOffset.Parse(FilterModeOtherFormat.DateReport), FilterModeOtherFormat.Group, FilterModeOtherFormat.Mutasi, FilterModeOtherFormat.Zona, FilterModeOtherFormat.Keterangan);
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Get_Success_NoContent_If_NoReturnData()
        {
            //v
            var serviceMock = new Mock<IInspectionDocumentReportService>();
            serviceMock.Setup(s => s.GetReport(It.IsAny<DateTimeOffset>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(new List<IndexViewModel>());
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);

            var response = controller.GetAll(DateTimeOffset.Parse(FilterModeNotFound.DateReport), FilterModeNotFound.Group, FilterModeNotFound.Mutasi, FilterModeNotFound.Zona, FilterModeNotFound.Keterangan);
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Get_Success()
        {
            //v
            
            var serviceMock = new Mock<IInspectionDocumentReportService>();
            serviceMock.Setup(s => s.GetReport(It.IsAny<DateTimeOffset>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(ListViewModelDummy);
            var service = serviceMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var controller = GetController(service, identityProvider);
            var response = controller.GetAll(DateTimeOffset.Parse(FilterMode.DateReport), 
                FilterMode.Group,
                FilterMode.Mutasi, 
                FilterMode.Zona,
                FilterMode.Keterangan);
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }
        #endregion
    }
}
