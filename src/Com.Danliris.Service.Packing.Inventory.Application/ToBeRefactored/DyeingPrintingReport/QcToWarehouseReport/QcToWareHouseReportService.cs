using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingReport.QcToWarehouseReport
{
    public class QcToWarehouseReportService : IQcToWarehouseReportService
    {
        private readonly IDyeingPrintingAreaInputRepository _repository;
        private readonly IDyeingPrintingAreaInputProductionOrderRepository _productionOrderRepository;
        public QcToWarehouseReportService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IDyeingPrintingAreaInputRepository>();
            _productionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaInputProductionOrderRepository>();
            
        }
        public MemoryStream GenerateExcel( DateTime startdate, DateTime finishdate,int offset)
        {
            var list = GetReportQuery( startdate, finishdate,offset);
            var ninputQuantitySolid = 0;
            var ninputQuantityDyeing = 0;
            var ninputQuantityPrinting = 0;
            var ntotinputQuantitySolid = 0;
            var ntotinputQuantityDyeing = 0;
            var ntotinputQuantityPrinting = 0;
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Total White", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Total Dyeing", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Total Printing", DataType = typeof(String) });
            

            if (list.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", ""); // to allow column name to be generated properly for empty data as template
            else
            {
                int index = 0;
                foreach (var item in list)
                {
                    index++;
                    string tgl = item.createdUtc == new DateTime(1970, 1, 1) ? "-" : item.createdUtc.ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    if (item.inputQuantitySolid != 0 )
                    {
                        ninputQuantitySolid = 1;
                    }
                    else
                    {
                        ninputQuantitySolid = 0;
                    }
                    ntotinputQuantitySolid += ninputQuantitySolid;
                    //----
                    if (item.inputQuantityDyeing != 0)
                    {
                        ninputQuantityDyeing = 1;
                    }
                    else
                    {
                        ninputQuantityDyeing = 0;
                    }
                    ntotinputQuantityDyeing += ninputQuantityDyeing;
                    //-----
                    if (item.inputQuantityPrinting != 0)
                    {
                        ninputQuantityPrinting = 1;
                    }
                    else
                    {
                        ninputQuantityPrinting = 0;
                    }
                    ntotinputQuantityPrinting += ninputQuantityPrinting;
                    //----



                    result.Rows.Add(
                           index, tgl,   item.inputQuantitySolid,   item.inputQuantityDyeing, item.inputQuantityPrinting);
                }
                double TotQtySolid = list.Sum(x => x.inputQuantitySolid);
                double TotQtyDyeing = list.Sum(x => x.inputQuantityDyeing);
                double TotQtyPrinting = list.Sum(x => x.inputQuantityPrinting);
                double jmlKuantitiSolid = TotQtySolid/ntotinputQuantitySolid;
                double jmlKuantitiDyeing = TotQtyDyeing / ntotinputQuantityDyeing;
                double jmlKuantitiPrinting = TotQtyPrinting / ntotinputQuantityPrinting;


                result.Rows.Add("","T O T A L", string.Format("{0:N2}", TotQtySolid), string.Format("{0:N2}", TotQtyDyeing), string.Format("{0:N2}", TotQtyPrinting));
                result.Rows.Add("", "AVERAGE", string.Format("{0:N2}", jmlKuantitiSolid), string.Format("{0:N2}", jmlKuantitiDyeing), string.Format("{0:N2}", jmlKuantitiPrinting));

            }
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }

        public List<QcToWarehouseReportViewModel> GetReportData(DateTime startdate, DateTime finishdate, int offset)
        {
            var Query = GetReportQuery(startdate, finishdate, offset);
            return Query.ToList();
        }

        private List<QcToWarehouseReportViewModel> GetReportQuery( DateTime startdate, DateTime finishdate, int offset)
        {
            var dateStart = startdate != DateTime.MinValue ? startdate.Date : DateTime.MinValue;
            var dateTo = finishdate != DateTime.MinValue ? finishdate.Date : DateTime.Now.Date;

            var query = from a in _repository.ReadAll()
                        where
                        a.Date.AddHours(offset).Date >= dateStart.Date && a.Date.AddHours(offset).Date <= dateTo.Date
                        && 
                        a.Area == "GUDANG JADI"
                        select a;

            //DyeingPrintingArea.GUDANGJADI

            //var querya = from a in _repository.ReadAll()
            //            where
            //            a.Date.AddHours(offset).Date >= dateStart.Date && a.Date.AddHours(offset).Date <= dateTo.Date
            //            && 
            //            a.Area == DyeingPrintingArea.GUDANGJADI
            //             select new { 
            //                a.CreatedUtc,
            //                a.Date,
            //                a.BonNo

            //            };
            var joinQuerySolid = from a in query
                            join b in _productionOrderRepository.ReadAll() on a.Id equals b.DyeingPrintingAreaInputId
                            where (b.ProcessTypeName == "PRODUKSI WHITE")

                            select new QcToWarehouseReportViewModel
                            {
                                createdUtc = a.Date.AddHours(offset).Date,
                                inputQuantitySolid = b.InputQuantity,
                                inputQuantityDyeing =0,
                                inputQuantityPrinting = 0,
                                orderType = b.ProcessTypeName
                            };
            var joinQueryDyeing = from a in query
                            join b in _productionOrderRepository.ReadAll() on a.Id equals b.DyeingPrintingAreaInputId
                            where (b.ProcessTypeName == "PRODUKSI DYEING")

                            select new QcToWarehouseReportViewModel
                            {
                                createdUtc = a.Date.AddHours(offset).Date,
                                inputQuantitySolid = 0,
                                inputQuantityDyeing = b.InputQuantity,
                                inputQuantityPrinting = 0,
                                orderType = b.ProcessTypeName
                            };
            var joinQueryPrinting = from a in query
                                  join b in _productionOrderRepository.ReadAll() on a.Id equals b.DyeingPrintingAreaInputId
                                  where (b.ProcessTypeName == "PRODUKSI PRINTING")

                                  select new QcToWarehouseReportViewModel
                                  {
                                      createdUtc = a.Date.AddHours(offset).Date,
                                      inputQuantitySolid = 0,
                                      inputQuantityDyeing = 0,
                                      inputQuantityPrinting = b.InputQuantity,
                                      orderType = b.ProcessTypeName
                                  };
            var result = joinQuerySolid.Concat(joinQueryDyeing).Concat(joinQueryPrinting).AsEnumerable();
            var resultGroup = result.GroupBy(s => new { s.createdUtc }).Select(d => new QcToWarehouseReportViewModel()
            {
               createdUtc = d.Key.createdUtc,
                inputQuantitySolid = d.Sum(e => e.inputQuantitySolid),
                inputQuantityDyeing = d.Sum(e => e.inputQuantityDyeing),
                inputQuantityPrinting = d.Sum(e => e.inputQuantityPrinting),
                //orderType = d.Key.orderType,
             
            });

            return resultGroup.ToList();
           // return joinQuery.OrderByDescending(a => a.createdUtc).ToList();
        }
    }
}
