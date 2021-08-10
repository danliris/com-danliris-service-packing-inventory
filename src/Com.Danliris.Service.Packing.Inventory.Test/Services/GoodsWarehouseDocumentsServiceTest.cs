using Com.Danliris.Service.Packing.Inventory.Application.GoodsWarehouse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Moq;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Service
{
    public class GoodsWarehouseDocumentsServiceTest
    {
        public GoodsWarehouseDocumentsService GetService(IServiceProvider serviceProvider)
        {
            return new GoodsWarehouseDocumentsService(serviceProvider);
        }
        private IndexViewModel ViewModel
        {
            get
            {
                return new IndexViewModel
                {
                    Date = new DateTimeOffset(new DateTime(2020, 04, 04)),
                    Group = "PAGI",
                    Activities = "KELUAR",
                    Mutation = "AWAL",
                    NoOrder = "Order001",
                    Construction = "PC 001 010",
                    Motif = "Batik",
                    Color = "Biru",
                    Grade = "A",
                    QtyPacking = "1 Rolls",
                    Qty = "1",
                    Packaging = "Rolls",
                    Satuan = "Meter",
                    Balance = "10.000",
                    Zona = "PROD"
                };
            }
        }

        public Mock<IServiceProvider> GetServiceProvider(IGoodsWarehouseDocumentsService service)
        {
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(s => s.GetService(typeof(IGoodsWarehouseDocumentsService)))
                .Returns(service);

            return spMock;
        }

        //[Fact]
        //public void Should_Success_GetExcel()
        //{
        //    //setup
        //    var repoMock = new Mock<IGoodsWarehouseDocumentsService>();
        //    var service = GetService(GetServiceProvider(repoMock.Object).Object);

        //    //act
        //    var result = service.GetExcel(new DateTimeOffset(new DateTime(2020, 04, 04)), "PAGI", "PROD", 7);

        //    //assert
        //    Assert.NotNull(result);
        //}

        //[Fact]
        //public void Should_Success_GetData()
        //{
        //    List<IndexViewModel> resultData = new List<IndexViewModel>();
        //    resultData.Add(ViewModel);
        //    var repoMock = new Mock<IGoodsWarehouseDocumentsService>();
        //    repoMock.Setup(s => s.GetList(It.IsAny<DateTimeOffset>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
        //        .Returns(resultData);

        //    var service = GetService(GetServiceProvider(repoMock.Object).Object);

        //    var result = service.GetList(It.IsAny<DateTimeOffset>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>());

        //    Assert.NotEmpty(result);
        //}
    }
}
