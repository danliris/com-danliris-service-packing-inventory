using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System.Globalization;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using Com.Danliris.Service.Packing.Inventory.Application.Helper;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.OrderStatusReport
{
    public class OrderStatusReportService : IOrderStatusReportService
    {
        protected readonly IHttpClientService _http;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _productionOrderRepository;
        private readonly IDyeingPrintingAreaOutputProductionOrderRepository _productionOutRepository;
        private readonly IServiceProvider _serviceProvider;
        public OrderStatusReportService(IServiceProvider serviceProvider)
        {
            _productionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            _productionOutRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
            _serviceProvider = serviceProvider;
        }
        public async Task<MemoryStream> GenerateExcel(DateTime startdate, DateTime finishdate, int orderTypeId, string orderTypeName)
        {
            var list = await GetReportQuery(startdate, finishdate, orderTypeId);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "SPP", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Target Kirim Ke Buyer(m)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Sisa Belum Turun Kanban (m)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Belum Produksi (m)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Sedang Produksi (m)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Sedang QC (m)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Sudah Produksi (m)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Sudah Dikirim ke Gudang Jadi (m)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Sudah Dikirim ke Buyer (m)", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Sisa Belum Kirim ke Buyer", DataType = typeof(double) });

            int index = 0;
            if (list.ToArray().Count() == 0)
                result.Rows.Add("", "", 0, 0, 0, 0, 0, 0, 0, 0, 0); // to allow column name to be generated properly for empty data as template
            else
            {
                foreach (var item in list)
                {
                    index++;
                    result.Rows.Add(
                           index, item.productionOrderNo, item.targetQty, item.remainingQty, item.preProductionQty, item.inProductionQty, 
                           item.qcQty, item.producedQty, item.sentGJQty, item.sentBuyerQty, item.remainingSentQty);
                }
            }
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                var type = orderTypeName != null ? orderTypeName : "-";
                worksheet.Cells["A1"].Value = "LAPORAN STATUS ORDER BERDASARKAN DELIVERY";
                worksheet.Cells["A2"].Value = "JENIS ORDER : " + type;
                worksheet.Cells["A3"].Value = "TANGGAL AWAL : " + startdate.ToShortDateString() + "  TANGGAL AKHIR : " + finishdate.ToShortDateString();
                
                worksheet.Cells["A" + 1 + ":F" + 1 + ""].Merge = true;
                worksheet.Cells["A" + 2 + ":F" + 2 + ""].Merge = true;
                worksheet.Cells["A" + 3 + ":F" + 3 + ""].Merge = true;
                worksheet.Cells["A" + 4 + ":F" + 4 + ""].Merge = true;
                worksheet.Cells["A" + 1 + ":F" + 5 + ""].Style.Font.Bold = true;
                worksheet.Cells["A5"].LoadFromDataTable(result, true);
                worksheet.Cells["A" + 10 + ":K" + (index + 5) + ""].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A" + 10 + ":K" + (index + 5) + ""].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A" + 10 + ":K" + (index + 5) + ""].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A" + 10 + ":K" + (index + 5) + ""].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["A" + 1 + ":K" + (index + 5) + ""].AutoFitColumns();


                var stream = new MemoryStream();
                package.SaveAs(stream);
                return stream;
            }
            //return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }

        public async Task<List<OrderStatusReportViewModel>> GetReportData(DateTime startdate, DateTime finishdate, int orderTypeId)
        {
            var Query = await GetReportQuery(startdate, finishdate, orderTypeId);
            return Query;
        }

        private async Task<List<OrderStatusReportViewModel> >GetReportQuery(DateTime startdate, DateTime finishdate, int orderTypeId)
        {
            var dateStart = startdate != DateTime.MinValue ? startdate.Date : DateTime.MinValue;
            var dateTo = finishdate != DateTime.MinValue ? finishdate.Date : DateTime.Now.Date;
            var spp = await GetDataSPP(dateStart, dateTo, orderTypeId);
            var sppResults = spp.data;
            List<long> sppIds = new List<long>();
            if (sppResults.Count > 0)
            {
                foreach (var ids in sppResults)
                {
                    sppIds.Add(ids.OrderId);
                }
            }
            var queryIn = from a in _productionOrderRepository.ReadAll()
                        where sppIds.Contains(a.ProductionOrderId) 
                          select new OrderStatusReportViewModel
                        {
                            productionOrderNo = a.ProductionOrderNo,
                            targetQty = 0,
                            productionOrderId=a.ProductionOrderId,
                            sentBuyerQty =0,
                            inProductionQty =0,
                            qcQty = a.Area == "INSPECTION MATERIAL" ? Convert.ToDecimal(a.InputQuantity) : 0,
                            sentGJQty= a.Area=="GUDANG JADI" ? Convert.ToDecimal(a.InputQuantity):0
                        };
            var queryOut = from b in _productionOutRepository.ReadAll() 
                           where sppIds.Contains(b.ProductionOrderId)
                           select new OrderStatusReportViewModel
                            {
                                productionOrderNo = b.ProductionOrderNo,
                                productionOrderId = b.ProductionOrderId,
                                sentBuyerQty= b.Area == "SHIPPING"? Convert.ToDecimal(b.Balance) : 0,
                                targetQty=0,
                                inProductionQty= Convert.ToDecimal(b.Balance),
                                qcQty= b.Area=="INSPECTION MATERIAL" ? Convert.ToDecimal(b.Balance)*(-1) : 0,
                                sentGJQty = 0,
                           };

            var joinQuery = queryIn.ToList().Union(queryOut.ToList());
            var dataList = from data in joinQuery.ToList()
                           group data by new { data.productionOrderNo, data.productionOrderId }
                           into groupdata
                           select new OrderStatusReportViewModel
                           {
                               productionOrderNo = groupdata.Key.productionOrderNo,
                               sentBuyerQty = groupdata.Sum(a => a.sentBuyerQty),
                               targetQty = groupdata.Sum(a => a.targetQty),
                               inProductionQty = groupdata.Sum(a => a.inProductionQty),
                               qcQty= groupdata.Sum(a => a.qcQty),
                               producedQty = groupdata.Sum(a => a.qcQty),
                               sentGJQty = groupdata.Sum(a => a.sentGJQty),
                               productionOrderId =groupdata.Key.productionOrderId,
                           };

            var noOrders = dataList.Select(no => no.productionOrderNo).Distinct().ToList();
            var productionData = await GetDataProduction(noOrders);
            var productionResults = productionData.data;
            var kanbanPretreatment = await GetDataPretreatmentKanban(noOrders);
            var kanbanPretreatmentResults = kanbanPretreatment.data;
            List<OrderStatusReportViewModel> newListData = new List<OrderStatusReportViewModel>();
            foreach(var data in dataList)
            {
                var inProd = productionResults.Where(a => a.noorder == data.productionOrderNo).FirstOrDefault();
                var target= sppResults.Where(x=>x.OrderId== data.productionOrderId).FirstOrDefault();
                var kanban = kanbanPretreatmentResults.Where(a => a.SPPNo == data.productionOrderNo).FirstOrDefault();
                data.targetQty = target.OrderQuantity;
                data.inProductionQty = inProd!=null ? Convert.ToDecimal(inProd.qtyin) : 0;
                data.preProductionQty = data.targetQty - data.inProductionQty >= 0? data.targetQty - data.inProductionQty:0 ;
                data.remainingSentQty = data.targetQty - data.sentBuyerQty >= 0 ? data.targetQty - data.sentBuyerQty : 0;
                data.remainingQty= kanban!=null ? data.targetQty - kanban.MaterialLength >= 0 ? data.targetQty - kanban.MaterialLength : 0 : data.targetQty;

                newListData.Add(data);
            }
            return newListData.ToList();
        }

        public async Task<SPPResult> GetDataSPP(DateTime startdate, DateTime finishdate, int orderTypeId)
        {
            SPPResult spp = new SPPResult();

            var filters = new
            {
                startdate,
                finishdate,
                orderTypeId
            };
            var salesUri = $"sales/production-orders/for-status-order-report?startdate={startdate}&&finishdate={finishdate}&&orderTypeId={orderTypeId}";
            IHttpClientService httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));
            var garmentProductionUri = ApplicationSetting.SalesEndpoint + salesUri;
            var response = await httpClient.SendAsync(HttpMethod.Get, garmentProductionUri, new StringContent(JsonConvert.SerializeObject(filters), Encoding.Unicode, "application/json"));


            if (response.IsSuccessStatusCode)
            {
                var contentString = await response.Content.ReadAsStringAsync();
                Dictionary<string, object> content = JsonConvert.DeserializeObject<Dictionary<string, object>>(contentString);
                var dataString = content.GetValueOrDefault("data").ToString();

                var listdata = JsonConvert.DeserializeObject<List<OrderQuantityForStatusOrder>>(dataString);

                foreach (var i in listdata)
                {
                    spp.data.Add(i);
                }
            }

            return spp;
        }

        public async Task<ProductionResult> GetDataProduction(List<string> orderno)
        {
            ProductionResult spp = new ProductionResult();

            var filter = string.Join(",", orderno.Distinct());
            var dpUri = $"GetProductionOsthoffStatusOrder?noorder={filter}";
            //var filter= string.Join(",", orderno.Distinct());
            IHttpClientService httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));
            var garmentProductionUri = ApplicationSetting.DyeingPrintingEndpoint + dpUri;
            var response = await httpClient.SendAsync(HttpMethod.Get, garmentProductionUri, new StringContent(JsonConvert.SerializeObject(filter), Encoding.Unicode, "application/json"));


            if (response.IsSuccessStatusCode)
            {
                var contentString = await response.Content.ReadAsStringAsync();
                Dictionary<string, object> content = JsonConvert.DeserializeObject<Dictionary<string, object>>(contentString);
                var dataString = content.GetValueOrDefault("data").ToString();

                var listdata = JsonConvert.DeserializeObject<List<ProductionViewModel>>(dataString);

                foreach (var i in listdata)
                {
                    spp.data.Add(i);
                }
            }

            return spp;
        }

        public async Task<ProductionResult> GetDataPretreatmentKanban(List<string> orderno)
        {
            ProductionResult spp = new ProductionResult();

            var filter = string.Join(",", orderno.Distinct());
            var dpUri = $"GetKanbanPretreatmentBySPP?noorder={filter}";
            IHttpClientService httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));
            var garmentProductionUri = ApplicationSetting.DyeingPrintingEndpoint + dpUri;
            var response = await httpClient.SendAsync(HttpMethod.Get, garmentProductionUri, new StringContent(JsonConvert.SerializeObject(filter), Encoding.Unicode, "application/json"));


            if (response.IsSuccessStatusCode)
            {
                var contentString = await response.Content.ReadAsStringAsync();
                Dictionary<string, object> content = JsonConvert.DeserializeObject<Dictionary<string, object>>(contentString);
                var dataString = content.GetValueOrDefault("data").ToString();

                var listdata = JsonConvert.DeserializeObject<List<ProductionViewModel>>(dataString);

                foreach (var i in listdata)
                {
                    spp.data.Add(i);
                }
            }

            return spp;
        }
    }
}
