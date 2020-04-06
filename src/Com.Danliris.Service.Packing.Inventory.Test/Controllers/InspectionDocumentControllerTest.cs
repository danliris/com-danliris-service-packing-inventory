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

        private int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }
        private InspectionDocumentReportItemViewModel ViewModel
        {
            get
            {
                string test = "[{'Index':'1','DateReport':'2020-03-16','GroupText':'SIANG','UnitText':'P','KeluarKe':'TRANSIT','NoSPP':'P/2020/0261','NoKereta':'1-1-9','Material':'CD 74 56 44','Keterangan':'NOT OK','Status':'PERBAIKAN','Lebar':'44','Motif':'PRJ200','Warna':'HIJAU','Mtr':'247.00','Yds':'270.22'},{'Index':'2','DateReport':'2020-03-16','GroupText':'SIANG','UnitText':'P','KeluarKe':'TRANSIT','NoSPP':'P/2020/0556','NoKereta':'1-1-6','Material':'CD 94 72 44','Keterangan':'NOT OK','Status':'PERBAIKAN','Lebar':'44','Motif':'TW 1069','Warna':'A','Mtr':'221.00','Yds':'241.77'},{'Index':'3','DateReport':'2020-03-16','GroupText':'SIANG','UnitText':'P','KeluarKe':'TRANSIT','NoSPP':'P/2020/0787','NoKereta':'1-1-10','Material':'CD 94 72 44','Keterangan':'NOT OK','Status':'PERBAIKAN','Lebar':'44','Motif':'Bp 763','Warna':'0','Mtr':'225.00','Yds':'246.15'}]";
                List<InspectionDocumentReportItemViewModel> listModel = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InspectionDocumentReportItemViewModel>>(test);
                return listModel[0];
            }
        }

        //[Fact]
        //public void Should_Get_Success()
        //{
        //    //v
        //    var serviceMock = new Mock<IInspectionDocumentReportService>();
        //    serviceMock.Setup(s => s.GetReport(It.IsAny<DateTimeOffset>(),It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())
        //        .Returns(new ListResult<IndexViewModel>(new List<IndexViewModel>(), 1, 1, 1)));
        //    var service = serviceMock.Object;

        //    var identityProviderMock = new Mock<IIdentityProvider>();
        //    var identityProvider = identityProviderMock.Object;

        //    var controller = GetController(service, identityProvider);
        //    //controller.ModelState.IsValid == false;
        //    var response = controller.GetAll();

        //    Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        //}
    }
}
