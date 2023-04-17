using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using Com.Danliris.Service.Packing.Inventory.Application.Master.ProductPacking;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System.IO;
using System.Data;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse
{
    public class StockOpnameMutationService : IStockOpnameMutationService
    {
        private const string UserAgent = "packing-inventory-service";
        private readonly IDyeingPrintingStockOpnameMutationRepository _stockOpnameMutationRepository;
        private readonly IDyeingPrintingStockOpnameMutationItemRepository _stockOpnameMutationItemsRepository;
        private readonly IDyeingPrintingStockOpnameSummaryRepository _stockOpnameSummaryRepository;
        private readonly IFabricPackingSKUService _fabricPackingSKUService;
        private readonly IProductPackingService _productPackingService;
        private readonly IIdentityProvider _identityProvider;
        private readonly IServiceProvider _serviceProvider;


        public StockOpnameMutationService(IServiceProvider serviceProvider)
        {
            _stockOpnameMutationRepository = serviceProvider.GetService<IDyeingPrintingStockOpnameMutationRepository>();
            _stockOpnameMutationItemsRepository = serviceProvider.GetService<IDyeingPrintingStockOpnameMutationItemRepository>();
            _stockOpnameSummaryRepository = serviceProvider.GetService<IDyeingPrintingStockOpnameSummaryRepository>();
            _fabricPackingSKUService = serviceProvider.GetService<IFabricPackingSKUService>();
            _productPackingService = serviceProvider.GetService<IProductPackingService>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            //_outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
            _serviceProvider = serviceProvider;
        }

        public async Task<int> Create(StockOpnameMutationViewModel viewModel)
        {
            int result = 0;

            //if (viewModel.Type == DyeingPrintingArea.STOCK_OPNAME)
            //{
                result = await CreateMutation(viewModel);
            //}

            return result;
        }

        private async Task<int> CreateMutation(StockOpnameMutationViewModel viewModel)
        {
            DateTime date = DateTime.Today;

            DyeingPrintingStockOpnameMutationModel model;
            int result = 0;
            if (viewModel.Type == "STOCK OPNAME")
            {

                //var data = _stockOpnameMutationRepository.GetDbSet().AsNoTracking();
                model = _stockOpnameMutationRepository.GetDbSet().AsNoTracking().FirstOrDefault(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
                                                                                            s.Date.AddHours(7).ToString("dd/MM/YYYY").Equals(date.AddHours(7).ToString("dd/MM/YYYY")) &&
                                                                                            s.Type == DyeingPrintingArea.SO
                                                                                            && !s.IsDeleted);
            }
            else if (viewModel.Type == "ADJ OUT")
            {
                model = _stockOpnameMutationRepository.GetDbSet().AsNoTracking().FirstOrDefault(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
                                                                                            s.Date.AddHours(7).ToString("dd/MM/YYYY").Equals(date.AddHours(7).ToString("dd/MM/YYYY")) &&
                                                                                            s.Type == DyeingPrintingArea.ADJ_OUT
                                                                                            && !s.IsDeleted);

            }
            else {
                model = null;
            }

            var typeOut = viewModel.Type == "STOCK OPNAME" ? "SO" : "ADJ OUT";

            viewModel.DyeingPrintingStockOpnameMutationItems = viewModel.DyeingPrintingStockOpnameMutationItems.ToList();

            

            if (model == null) {
                int totalCurentYearData = _stockOpnameMutationRepository.ReadAllIgnoreQueryFilter()
                                                .Count(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
                                                            s.CreatedUtc.Year == viewModel.Date.Year && s.Type == DyeingPrintingArea.STOCK_OPNAME);

                string bonNo = GenerateBonNo(totalCurentYearData + 1, viewModel.Date, DyeingPrintingArea.GUDANGJADI, typeOut);

                

                model = new DyeingPrintingStockOpnameMutationModel(DyeingPrintingArea.GUDANGJADI, bonNo, DateTime.Today,
                                                                    typeOut,
                                                                    viewModel.DyeingPrintingStockOpnameMutationItems.Select(s =>
                                                                        new DyeingPrintingStockOpnameMutationItemModel(
                                                                            (double)s.SendQuantity * s.PackagingLength,
                                                                            s.Color,
                                                                            s.Construction,
                                                                            s.Grade,
                                                                            s.Motif,
                                                                            s.SendQuantity,
                                                                            s.PackagingLength,
                                                                            s.PackagingType,
                                                                            s.PackagingUnit,
                                                                            s.ProductionOrder.Id,
                                                                            s.ProductionOrder.No,
                                                                            s.ProductionOrder.Type,
                                                                            s.ProductionOrder.OrderQuantity,
                                                                            s.ProcessTypeId,
                                                                            s.ProcessTypeName,
                                                                            s.Remark,
                                                                            s.Unit,
                                                                            s.UomUnit,
                                                                            s.Track.Id,
                                                                            s.Track.Type,
                                                                            s.Track.Name,
                                                                            s.Track.Box,
                                                                            s.ProductSKUId,
                                                                            s.FabricSKUId,
                                                                            s.ProductSKUCode,
                                                                            s.ProductPackingId,
                                                                            s.FabricPackingId,
                                                                            s.ProductPackingCode,
                                                                            typeOut
                                                                            
                                                                            )).ToList());
                result = await _stockOpnameMutationRepository.InsertAsync(model);
                await _stockOpnameMutationRepository.UpdateAsync(model.Id, model);

            }
            else
            {
                foreach (var item in viewModel.DyeingPrintingStockOpnameMutationItems )
                {
                    var modelItem = new DyeingPrintingStockOpnameMutationItemModel(
                                                                            (double)item.SendQuantity * item.PackagingLength,
                                                                            item.Color,
                                                                            item.Construction,
                                                                            item.Grade,
                                                                            item.Motif,
                                                                            item.SendQuantity,
                                                                            item.PackagingLength,
                                                                            item.PackagingType,
                                                                            item.PackagingUnit,
                                                                            item.ProductionOrder.Id,
                                                                            item.ProductionOrder.No,
                                                                            item.ProductionOrder.Type,
                                                                            item.ProductionOrder.OrderQuantity,
                                                                            item.ProcessTypeId,
                                                                            item.ProcessTypeName,
                                                                            item.Remark,
                                                                            item.Unit,
                                                                            item.UomUnit,
                                                                            item.Track.Id,
                                                                            item.Track.Type,
                                                                            item.Track.Name,
                                                                            item.Track.Box,
                                                                            item.ProductSKUId,
                                                                            item.FabricSKUId,
                                                                            item.ProductSKUCode,
                                                                            item.ProductPackingId,
                                                                            item.FabricPackingId,
                                                                            item.ProductPackingCode,
                                                                            typeOut
                        );

                    modelItem.DyeingPrintingStockOpnameMutationId = model.Id;
                    result += await _stockOpnameMutationItemsRepository.InsertAsync(modelItem);
                }


            }

            foreach (var itemSum in viewModel.DyeingPrintingStockOpnameMutationItems)
            {
                var modelSummary = _stockOpnameSummaryRepository.GetDbSet().AsNoTracking().FirstOrDefault(s => s.ProductPackingCode.Contains(itemSum.ProductPackingCode) && s.TrackId == itemSum.Track.Id && s.PackagingLength == itemSum.PackagingLength);
                var balance = itemSum.PackagingLength * (double)itemSum.SendQuantity;
                await _stockOpnameSummaryRepository.UpdateBalanceOut(modelSummary.Id, balance);
                await _stockOpnameSummaryRepository.UpdateBalanceRemainsOut(modelSummary.Id, balance);
                await _stockOpnameSummaryRepository.UpdatePackingQtyOut(modelSummary.Id, itemSum.SendQuantity);
                await _stockOpnameSummaryRepository.UpdatePackingQtyRemainsOut(modelSummary.Id, itemSum.SendQuantity);
            }
            return result;
        }

        public string GenerateBonNo(int totalPreviousData, DateTimeOffset date, string destinationArea, string typeOut)
        {
            var bonNo = "";
            if (destinationArea == DyeingPrintingArea.GUDANGJADI)
            {
                bonNo = string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.GJ, typeOut, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }


            return bonNo;
        }
        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var validIds = _stockOpnameMutationItemsRepository.ReadAll().Select(entity => entity.DyeingPrintingStockOpnameMutationId).ToList();
            var query = _stockOpnameMutationRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.GUDANGJADI && validIds.Contains(s.Id));

            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingStockOpnameMutationModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingStockOpnameMutationModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingStockOpnameMutationModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new IndexViewModel()
            {
                Id = s.Id,
                Area = s.Area,
                Type = DyeingPrintingArea.STOCK_OPNAME,
                BonNo = s.BonNo,
                Date = s.Date,

            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<StockOpnameMutationViewModel> ReadById(int id)
        {
            var model = await _stockOpnameMutationRepository.ReadByIdAsync(id);
            if (model == null)
                return null;

            StockOpnameMutationViewModel vm = await MapToViewModel(model);

            return vm;
        }

        private async Task<StockOpnameMutationViewModel> MapToViewModel(DyeingPrintingStockOpnameMutationModel model)
        {
            var vm = new StockOpnameMutationViewModel();
            vm = new StockOpnameMutationViewModel
            {

                Active = model.Active,
                Id = model.Id,
                Area = model.Area,
                BonNo = model.BonNo,
                Type = model.Type,
                Bon = new IndexViewModel
                {
                    Area = model.Area,
                    BonNo = model.BonNo,
                    Date = model.Date,
                    Id = model.Id
                },
                CreatedAgent = model.CreatedAgent,
                CreatedBy = model.CreatedBy,
                CreatedUtc = model.CreatedUtc,
                Date = model.Date,
                DeletedAgent = model.DeletedAgent,
                DeletedBy = model.DeletedBy,
                DeletedUtc = model.DeletedUtc,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedUtc = model.LastModifiedUtc,
                DyeingPrintingStockOpnameMutationItems = model.DyeingPrintingStockOpnameMutationItems.Where(x => !x.IsDeleted).Select(s => new StockOpnameMutationItemViewModel() 
                {
                    Active = s.Active,
                    LastModifiedUtc = s.LastModifiedUtc,
                    
                    Color = s.Color,
                    Construction = s.Construction,
                    CreatedAgent = s.CreatedAgent,
                    CreatedBy = s.CreatedBy,
                    CreatedUtc = s.CreatedUtc,
                    DeletedAgent = s.DeletedAgent,
                    DeletedBy = s.DeletedBy,
                    DeletedUtc = s.DeletedUtc,
                    Balance = s.Balance,
                    Grade = s.Grade,
                    Motif = s.Motif,
                    PackagingQty = s.PackagingQty,
                    PackagingLength = s.PackagingLength,
                    PackagingType = s.PackagingType,
                    PackagingUnit = s.PackagingUnit,
                    ProductionOrder = new ProductionOrder()
                    { 
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        Type = s.ProductionOrderType,
                        OrderQuantity = s.ProductionOrderOrderQuantity
                    },
                    Remark = s.Remark,
                    ProcessTypeId = s.ProcessTypeId,
                    ProcessTypeName = s.ProcessTypeName,
                    Unit = s.Unit,
                    UomUnit = s.UomUnit,
                    Track = new Track()
                    { 
                        Id = s.TrackId,
                        Name = s.TrackName,
                        Type = s.TrackType,
                        Box = s.TrackBox
                    },
                    ProductSKUId = s.ProductSKUId,
                    FabricSKUId = s.FabricSKUId,
                    ProductSKUCode = s.ProductSKUCode,
                    ProductPackingId = s.ProductPackingId,
                    FabricPackingId = s.FabricPackingId,
                    ProductPackingCode = s.ProductPackingCode,
                    TypeOut = s.TypeOut,
                    SendQuantity = s.PackagingQty
                }).ToList()    
            };

            return vm;

        }

        public List<ReportSOViewModel> GetMonitoringSO(DateTimeOffset dateFrom, DateTimeOffset dateTo, int productionOrderId, int track, int offset)
        {

            IQueryable<DyeingPrintingStockOpnameMutationModel> stockOpnameMutationQuery;
            IQueryable<DyeingPrintingStockOpnameMutationItemModel> stockOpnameMutationItemsQuery;
            if (dateFrom == DateTimeOffset.MinValue && dateTo == DateTimeOffset.MinValue)
            {
                //stockOpnameMutationQuery = _stockOpnameMutationRepository.ReadAll();
                stockOpnameMutationItemsQuery = _stockOpnameMutationItemsRepository.ReadAll();
            }
            else
            {
                //stockOpnameMutationQuery = _stockOpnameMutationRepository.ReadAll().Where(s =>
                //                    s.CreatedUtc.AddHours(7).Date >= dateFrom.Date && s.CreatedUtc.AddHours(7).Date <= dateTo.Date);
                stockOpnameMutationItemsQuery = _stockOpnameMutationItemsRepository.ReadAll().Where(s =>
                                        s.CreatedUtc.AddHours(7).Date >= dateFrom.Date && s.CreatedUtc.AddHours(7).Date <= dateTo.Date);
            }




            if (productionOrderId != 0)
            {
                stockOpnameMutationItemsQuery = stockOpnameMutationItemsQuery.Where(s => s.ProductionOrderId == productionOrderId);
            }

            if (track != 0)
            {
                stockOpnameMutationItemsQuery = stockOpnameMutationItemsQuery.Where(s => s.TrackId == track);
            }

            //var query = (from a in stockOpnameMutationQuery
            //             join b in stockOpnameMutationItemsQuery on a.Id equals b.DyeingPrintingStockOpnameMutationId
            //             select new ReportSOViewModel()
            //             {
            //                 ProductionOrderId = b.ProductionOrderId,
            //                 ProductionOrderNo = b.ProductionOrderNo,
            //                 ProductPackingCode = b.ProductPackingCode,
            //                 ProcessTypeName = b.ProcessTypeName,
            //                 PackagingUnit = b.PackagingUnit,
            //                 Grade = b.Grade,
            //                 Color = b.Color,
            //                 TrackId = b.TrackId,
            //                 TrackName = b.TrackType + " - " + b.TrackName + " - " + b.TrackBox,
            //                 BonNo = a.BonNo,
            //                 DateIn = a.CreatedUtc.AddHours(7),
            //                 PackagingQty = b.PackagingQty,
            //                 PackingLength = b.PackagingLength,
            //                 InQty = (double)b.PackagingQty * b.PackagingLength
            //             }).ToList();
            //var result = query.GroupBy(s => new { s.ProductPackingCode, s.TrackId }).Select(d => new ReportSOViewModel()
            //{
            //    ProductionOrderId = d.First().ProductionOrderId,
            //    ProductionOrderNo = d.First().ProductionOrderNo,
            //    ProductPackingCode = d.First().ProductPackingCode,
            //    ProcessTypeName = d.First().ProcessTypeName,
            //    PackagingUnit = d.First().PackagingUnit,
            //    Grade = d.First().Grade,
            //    Color = d.First().Color,
            //    //TrackId = d.First().TrackId,
            //    //TrackName = d.First().TrackName +"-"+ d.First().TrackType,
            //    TrackName = d.First().TrackName,
            //    BonNo = d.First().BonNo,
            //    DateIn = d.First().DateIn,
            //    PackagingQty = d.Sum(a => a.PackagingQty),
            //    PackingLength = d.First().PackingLength,
            //    InQty = d.Sum(a => a.InQty)
            //}).OrderBy(o => o.TrackId).ThenBy(o => o.ProductionOrderId).ToList();

            var query = (from  b in stockOpnameMutationItemsQuery 
                         select new ReportSOViewModel()
                         {
                             ProductionOrderId = b.ProductionOrderId,
                             ProductionOrderNo = b.ProductionOrderNo,
                             ProductPackingCode = b.ProductPackingCode,
                             ProcessTypeName = b.ProcessTypeName,
                             PackagingUnit = b.PackagingUnit,
                             Grade = b.Grade,
                             Construction = b.Construction,
                             Motif = b.Motif,
                             Color = b.Color,
                             TrackId = b.TrackId,
                             TrackName = b.TrackType + " - " + b.TrackName + " - " + b.TrackBox,
                             DateIn = b.CreatedUtc.AddHours(7),
                             PackagingQty = b.PackagingQty,
                             PackingLength = b.PackagingLength,
                             InQty = (double)b.PackagingQty * b.PackagingLength
                         }).ToList();
            var result = query.GroupBy(s => new { s.ProductPackingCode, s.TrackId }).Select(d => new ReportSOViewModel()
            {
                ProductionOrderId = d.First().ProductionOrderId,
                ProductionOrderNo = d.First().ProductionOrderNo,
                ProductPackingCode = d.First().ProductPackingCode,
                ProcessTypeName = d.First().ProcessTypeName,
                PackagingUnit = d.First().PackagingUnit,
                Grade = d.First().Grade,
                Color = d.First().Color,
                Construction = d.First().Construction,
                Motif = d.First().Motif,
                //TrackId = d.First().TrackId,
                //TrackName = d.First().TrackName +"-"+ d.First().TrackType,
                TrackName = d.First().TrackName,
                BonNo = d.First().BonNo,
                DateIn = d.First().DateIn,
                PackagingQty = d.Sum(a => a.PackagingQty),
                PackingLength = d.First().PackingLength,
                InQty = d.Sum(a => a.InQty)
            }).OrderBy(o => o.TrackId).ThenBy(o => o.ProductionOrderId).ToList();

            return result;

        }

        public MemoryStream GenerateExcelMonitoring(DateTimeOffset dateFrom, DateTimeOffset dateTo, int productionOrderId, int track, int offset)
        {
            var data = GetMonitoringSO(dateFrom, dateTo, productionOrderId, track, offset);
            DataTable dt = new DataTable();

            //dt.Columns.Add(new DataColumn() { ColumnName = "No Bon", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Barcode", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jalur/Rak", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Packing", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Keluar", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Panjang Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Total Keluar", DataType = typeof(double) });


            if (data.Count() == 0)
            {
                dt.Rows.Add( "", "", "", "", "", "", "", "", "", 0, "", 0);
            }
            else
            {
                decimal packagingQty = 0;
                double total = 0;

                foreach (var item in data)
                {
                    var dateIn = item.DateIn.Equals(DateTimeOffset.MinValue) ? "" : item.DateIn.AddHours(offset).Date.ToString("d");
                    // var sldbegin = item.SaldoBegin;
                    //saldoBegin =+ item.SaldoBegin;
                    dt.Rows.Add( item.ProductionOrderNo, dateIn, item.ProductPackingCode, item.Construction, item.Color, item.Motif,
                        item.Grade, item.TrackName, item.PackagingUnit, item.PackagingQty, item.PackingLength, item.InQty);

                    packagingQty += item.PackagingQty;
                    total += item.InQty;
                }

                dt.Rows.Add("", "", "", "", "", "", "", "", "", packagingQty, "", total);
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, string.Format("Laporan Stock {0}", "SO")) }, true);

        }



    }



    
}
