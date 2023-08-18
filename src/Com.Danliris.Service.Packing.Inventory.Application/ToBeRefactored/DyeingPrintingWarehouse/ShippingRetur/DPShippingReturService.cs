using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse.DPShipping;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.ShippingIN.ViewModel;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Com.Moonlay.Models;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.ShippingRetur.ViewModel;
using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System.IO;
using System.Data;
using System.Globalization;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.ShippingRetur
{
    public class DPShippingReturService : IDPShippingReturService
    {
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<DPShippingInputModel> _dbSet;
        private readonly DbSet<DPShippingInputItemModel> _dbSetItem;
        private readonly DbSet<DPShippingMovementModel> _dbSetShippingMovement;
        private readonly DbSet<FabricProductPackingModel> _dbSetFabricProduct;
        private readonly DbSet<FabricProductSKUModel> _dbSetFabricProductSku;
        private readonly IIdentityProvider _identityProvider;
        private const string UserAgent = "Repository";

        public DPShippingReturService(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            
            _dbSet = dbContext.Set<DPShippingInputModel>();
            _dbSetItem = dbContext.Set<DPShippingInputItemModel>();
            _dbSetShippingMovement = dbContext.Set<DPShippingMovementModel>();
            _dbSetFabricProduct = dbContext.Set<FabricProductPackingModel>();
            _dbSetFabricProductSku = dbContext.Set<FabricProductSKUModel>();
            _dbContext = dbContext;
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _dbSet.AsNoTracking().Where( x => x.ShippingType == "RETUR BUYER" && !x.IsDeleted);
            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };
            query = QueryHelper<DPShippingInputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DPShippingInputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DPShippingInputModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new IndexViewModel()
            {

                BonNo = s.BonNo,
                Date = s.Date,
                Id = s.Id,
                Shift = s.Shift,
                ShippingType = s.ShippingType,
            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<int> Create(ShippingReturViewModel viewModel)
        {
            int Created = 0;
            using (var transaction = this._dbContext.Database.BeginTransaction()) {

                try
                {
                    var model = _dbSet.AsNoTracking().FirstOrDefault(s => s.Date.Date == viewModel.Date.Date && s.ShippingType == DyeingPrintingArea.RETURBUYER && !s.IsDeleted);
                    var listItem = new List<DPShippingInputItemModel>();
                    if (model == null)
                    {
                        int totalCurrentYearData = _dbSet.AsNoTracking().Count(s => s.CreatedUtc.Year == viewModel.Date.Year);
                        string bonNo = GenerateBonNo(totalCurrentYearData + 1, viewModel.Date);

                        model = new DPShippingInputModel(
                                viewModel.Date,
                                DyeingPrintingArea.RETURBUYER,
                                "DAILY SHIFT",
                                bonNo,
                                viewModel.ShippingProductionOrders.Select(s => new DPShippingInputItemModel(
                                   (int)s.ProductionOrder.Id,
                                   s.ProductionOrder.No,
                                   s.Material.Id,
                                   s.Material.Name,
                                   s.MaterialConstruction.Id,
                                   s.MaterialConstruction.Name,
                                   s.MaterialWidth,
                                   s.BuyerId,
                                   s.Buyer,
                                   s.Construction,
                                   s.Unit,
                                   s.Color,
                                   s.Motif,
                                   s.UomUnit,
                                   s.InputQuantity,
                                    s.InputQuantity,
                                   s.PackingInstruction,
                                   s.ProductionOrder.Type,
                                   s.ProductionOrder.OrderQuantity.ToString(),
                                   s.CreatedUtcOrderNo,
                                   s.Remark,
                                   s.Grade,
                                   s.Description,
                                   
                                   s.PackagingUnit,
                                   s.PackingType,
                                   s.InputQtyPacking,
                                   s.PackingLength,
                                   DyeingPrintingArea.BUYER,
                                   s.Id,
                                   viewModel.ReturType ? s.ProductSKUId : 0,
                                    viewModel.ReturType ? s.FabricProductSKUId : 0,
                                    viewModel.ReturType ? s.ProductPackingCode.Substring(0, 8) : null,
                                    viewModel.ReturType ? s.ProductPackingId : 0,
                                    viewModel.ReturType ? s.FabricPackingId : 0,
                                    viewModel.ReturType ? s.ProductPackingCode : null,
                                   s.ProcessType.Id,
                                   s.ProcessType.Name,
                                   s.YarnMaterial.Id,
                                   s.YarnMaterial.Name,
                                   (double)s.InputQtyPacking,
                                   s.InputQuantity,
                                   s.FinishWidth,
                                   s.DestinationBuyerName,
                                   s.MaterialOrigin,
                                   s.DeliveryOrderReturNo


                                   )).ToList());

                        model.FlagForCreate(_identityProvider.Username, UserAgent);

                        _dbSet.Add(model);

                        foreach (var item in model.DPShippingInputItems)
                        {
                            item.FlagForCreate(_identityProvider.Username, UserAgent);
                           
                            //if (viewModel.ReturType)
                            //{
                            //    var barcodeData = _dbContext.FabricProductPackings.FirstOrDefault(x => x.Code.Contains(item.ProductPackingCode));

                            //    item.ProductSKUId = barcodeData.ProductSKUId;
                            //    item.ProductPackingId = barcodeData.ProductPackingId;
                            //    item.FabricSKUId = barcodeData.FabricProductSKUId;
                            //    item.FabricPackingId = barcodeData.Id;
                            //}
                            
                        }

                        Created = await _dbContext.SaveChangesAsync();
                        await createMovement(model.DPShippingInputItems.ToList());
                    }
                    else 
                    {
                        foreach (var s in viewModel.ShippingProductionOrders)
                        {
                            var modelItem = new DPShippingInputItemModel(
                                    (int)s.ProductionOrder.Id,
                                    s.ProductionOrder.No,
                                    s.Material.Id,
                                    s.Material.Name,
                                    s.MaterialConstruction.Id,
                                    s.MaterialConstruction.Name,
                                    s.MaterialWidth,
                                    s.BuyerId,
                                    s.Buyer,
                                    s.Construction,
                                    s.Unit,
                                    s.Color,
                                    s.Motif,
                                    s.UomUnit,
                                    s.InputQuantity,
                                    s.InputQuantity,
                                    s.PackingInstruction,
                                    s.ProductionOrder.Type,
                                    s.ProductionOrder.OrderQuantity.ToString(),
                                    s.CreatedUtcOrderNo,
                                    s.Remark,
                                    s.Grade,
                                    s.Description,
                                   
                                    s.PackagingUnit,
                                    s.PackingType,
                                    s.InputQtyPacking,
                                    s.PackingLength,
                                    DyeingPrintingArea.BUYER,
                                    model.Id,
                                    viewModel.ReturType ?s.ProductSKUId : 0,
                                    viewModel.ReturType ? s.FabricProductSKUId : 0,
                                    viewModel.ReturType ? s.ProductPackingCode.Substring(0, 8) : null,
                                    viewModel.ReturType ? s.ProductPackingId : 0,
                                    viewModel.ReturType ? s.FabricPackingId : 0,
                                    viewModel.ReturType ? s.ProductPackingCode : null,
                                    s.ProcessType.Id,
                                    s.ProcessType.Name,
                                    s.YarnMaterial.Id,
                                    s.YarnMaterial.Name,
                                    (double)s.InputQtyPacking,
                                    s.InputQuantity,
                                    s.FinishWidth,
                                    s.DestinationBuyerName,
                                    s.MaterialOrigin,
                                    s.DeliveryOrderReturNo
                                );

                            //if (viewModel.ReturType)
                            //{
                            //    var barcodeData = _dbContext.FabricProductPackings.FirstOrDefault(x => x.Code.Contains(modelItem.ProductPackingCode));

                            //    modelItem.ProductSKUId = barcodeData.ProductSKUId;
                            //    modelItem.ProductPackingId = barcodeData.ProductPackingId;
                            //    modelItem.FabricSKUId = barcodeData.FabricProductSKUId;
                            //    modelItem.FabricPackingId = barcodeData.Id;
                            //    modelItem.ProductSKUCode = modelItem.ProductPackingCode.Substring(0, 8);
                            //}

                            modelItem.FlagForCreate(_identityProvider.Username, UserAgent);
                            listItem.Add(modelItem);
                            _dbSetItem.Add(modelItem);
                        }


                        Created = await _dbContext.SaveChangesAsync();
                        await createMovement(listItem);
                    }

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }


                return Created;
        }

        private string GenerateBonNo(int totalPreviousData, DateTimeOffset date)
        {


            return string.Format("{0}.{1}.{2}", DyeingPrintingArea.SP, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));


        }

        private async Task<int> createMovement(List<DPShippingInputItemModel> modelItem)
        {
            int count = 0;

            foreach (var item in modelItem)
            {
                var movement = new DPShippingMovementModel(
                        item.CreatedUtc.Date,
                        DyeingPrintingArea.RETURBUYER,
                        DyeingPrintingArea.IN,
                        item.Id,
                        0,
                        item.DPShippingInputId,
                        item.ProductionOrderId,
                        item.ProductionOrderNo,
                        item.BuyerId,
                        item.BuyerName,
                        item.Construction,
                        item.Unit,
                        item.Color,
                        item.Motif,
                        item.UomUnit,
                        item.Balance,
                        item.Grade,
                        item.ProductionOrderType,
                        item.Remark,
                        item.Description,
                        item.PackagingType,
                        item.PackagingLength,
                        item.PackagingQty,
                        item.PackagingUnit,
                        item.MaterialOrigin,
                        item.ProductPackingCode,
                        item.ProductPackingId,
                        0,
                        "",
                        ""
                    );
                movement.FlagForCreate(_identityProvider.Username, UserAgent);

                _dbSetShippingMovement.Add(movement);

            }

            count = await _dbContext.SaveChangesAsync();
            return count;
        }

        //public int CheckBarcode(string barcode)
        //{
        //    var jumlah  = _dbContext.ProductPackings.Where(x => x.Code.Contains(barcode)).Count();

        //    return jumlah;
        //}
        public ListResult<barcodeViewModel> GetCodeLoader(int page, int size, string filter, string order, string keyword)
        {
            var query = from a in _dbSetFabricProduct
                        join b in _dbSetFabricProductSku on a.FabricProductSKUId equals b.Id
                        select new barcodeViewModel() {
                            Code = a.Code,
                            ProductPackingId = a.ProductPackingId,
                            ProductSKUId = a.ProductSKUId,
                            FabricProductSKUId = a.FabricProductSKUId,
                            FabricPackingId = a.Id,
                            ProductionOrderNo = b.ProductionOrderNo,
                            PackingLength = a.PackingSize
                           
                        }
                        ;
            List<string> SearchAttributes = new List<string>()
            {
                "Code"
            };

            query = QueryHelper<barcodeViewModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<barcodeViewModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<barcodeViewModel>.Order(query, OrderDictionary);
            var data = 
                query.Skip((page - 1) * size).Take(size).Select(s => new barcodeViewModel()
                {
                    Code = s.Code,
                    ProductPackingId = s.ProductPackingId,
                    ProductSKUId = s.ProductSKUId,
                    FabricProductSKUId = s.FabricProductSKUId,
                    FabricPackingId = s.Id,
                    ProductionOrderNo = s.ProductionOrderNo,
                    PackingLength = s.PackingLength
                    


                });

            return new ListResult<barcodeViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<ShippingReturViewModel> ReadById(int id)
        {
            var model = await _dbSet.Include(s => s.DPShippingInputItems).FirstOrDefaultAsync(s => s.Id == id);
            if (model == null)
                return null;

            var vm = MapToViewModel(model);

            return vm;
        }

        private ShippingReturViewModel MapToViewModel(DPShippingInputModel model)
        {
            var vm = new ShippingReturViewModel()
            {
                Active = model.Active,
                Id = model.Id,

                BonNo = model.BonNo,
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
                Shift = model.Shift,
                ShippingType = model.ShippingType,
                ShippingProductionOrders = model.DPShippingInputItems.Select(s => new ShippingReturItemViewModel()
                {
                    Active = s.Active,
                    LastModifiedUtc = s.LastModifiedUtc,
                    Buyer = s.BuyerName,
                    BuyerId = s.BuyerId,
                    Color = s.Color,
                    Construction = s.Construction,
                    CreatedAgent = s.CreatedAgent,
                    CreatedBy = s.CreatedBy,
                    CreatedUtc = s.CreatedUtc,
                    DeletedAgent = s.DeletedAgent,
                    DeletedBy = s.DeletedBy,
                    DeletedUtc = s.DeletedUtc,
                    Remark = s.Remark,
                    PackingInstruction = s.PackingInstruction,

                    Id = s.Id,
                    IsDeleted = s.IsDeleted,
                    LastModifiedAgent = s.LastModifiedAgent,
                    LastModifiedBy = s.LastModifiedBy,
                    PackagingUnit = s.PackagingUnit,
                    Motif = s.Motif,
                    Grade = s.Grade,
                    //DeliveryOrder = new DeliveryOrderSales()
                    //{
                    //    Id = s.DeliveryOrderSalesId,
                    //    No = s.DeliveryOrderSalesNo
                    //},
                    
                    PackagingQty = s.PackagingQty,
                    InputQtyPacking = s.PackagingQty,
                    PackingType = s.PackagingType,
                    Balance = s.Balance,
                    InputQuantity = s.Balance,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        OrderQuantity = Convert.ToDouble(s.ProductionOrderOrderQuantity),
                        Type = s.ProductionOrderType
                    },
                    DeliveryOrderReturNo = s.DeliveryOrderReturNo,
                    MaterialWidth = s.MaterialWidth,
                    MaterialOrigin = s.MaterialOrigin,
                    FinishWidth = s.FinishWidth,
                    Material = new Material()
                    {
                        Id = s.MaterialId,
                        Name = s.MaterialName
                    },
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Name = s.MaterialConstructionName,
                        Id = s.MaterialConstructionId
                    },
                    PackingLength = s.PackagingLength,
                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
                    {
                        Id = s.ProcessTypeId,
                        Name = s.ProcessTypeName
                    },
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = s.YarnMaterialId,
                        Name = s.YarnMaterialName
                    },

                    Unit = s.Unit,
                    UomUnit = s.UomUnit,
                    ProductSKUId = s.ProductSKUId,
                    FabricProductSKUId = s.FabricSKUId,
                    ProductSKUCode = s.ProductSKUCode,
                    ProductPackingId = s.ProductPackingId,
                    FabricPackingId = s.FabricPackingId,
                    ProductPackingCode = s.ProductPackingCode,

                }

                    ).ToList()
            };

            return vm;
        }

        public MemoryStream GenerateExcel(DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet)
        {
            var query = _dbSet.AsNoTracking()
               .Where(s => s.ShippingType == DyeingPrintingArea.RETURBUYER);

            if (dateFrom.HasValue && dateTo.HasValue)
            {
                query = query.Where(s => dateFrom.Value.Date <= s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date &&
                            s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date <= dateTo.Value.Date);
            }
            else if (!dateFrom.HasValue && dateTo.HasValue)
            {
                query = query.Where(s => s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date <= dateTo.Value.Date);
            }
            else if (dateFrom.HasValue && !dateTo.HasValue)
            {
                query = query.Where(s => dateFrom.Value.Date <= s.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date);
            }

            query = query.OrderBy(x => x.BonNo);

            var modelAll = query.Select(s => new
            {

                SppList = s.DPShippingInputItems.Select(d => new
                {
                    s.BonNo,
                    d.DeliveryOrderReturNo,
                    d.ProductionOrderId,
                    d.ProductionOrderNo,
                    s.Date,
                    d.ProductionOrderOrderQuantity,
                    d.Construction,
                    d.MaterialOrigin,
                    d.Unit,
                    d.BuyerName,
                    d.Color,
                    d.Motif,
                    d.Grade,
                    d.Remark,
                    d.InputPackagingQty,
                    d.PackagingUnit,
                    d.InputQuantity,
                    d.UomUnit,
                    d.ProductPackingCode
                })
            });

            var query1 = modelAll;

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn() { ColumnName = "No. Bon", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. Delivery Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Barcode", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal Masuk", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Asal Material", DataType = typeof(string) });
            //dt.Columns.Add(new DataColumn() { ColumnName = "Nama Barang", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Pack", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Pack", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });

            if (query1.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "","", "", "", "",  "", "", "", "", "", "", "", "");
            }
            else
            {
                foreach (var model in query1)
                {
                    //foreach (var item in model.DyeingPrintingAreaInputProductionOrders.Where(d => !d.HasOutputDocument).OrderBy(s => s.ProductionOrderNo))
                    foreach (var item in model.SppList.OrderBy(s => s.ProductionOrderNo))
                    {
                        var dateIn = item.Date.Equals(DateTimeOffset.MinValue) ? "" : item.Date.ToOffset(new TimeSpan(offSet, 0, 0)).Date.ToString("d");

                        dt.Rows.Add(item.BonNo,
                                     item.DeliveryOrderReturNo,
                                     item.ProductionOrderNo,
                                     item.ProductPackingCode,
                                     dateIn,
                                     item.ProductionOrderOrderQuantity,
                                     item.Construction,
                                     item.MaterialOrigin,
                                     item.Unit,
                                     item.BuyerName,
                                     item.Color,
                                     item.Motif,
                                     item.Grade,
                                     item.Remark,
                                     item.InputPackagingQty.ToString("N2", CultureInfo.InvariantCulture),
                                     item.PackagingUnit,
                                     item.InputQuantity.ToString("N2", CultureInfo.InvariantCulture),
                                     item.UomUnit);
                    }
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Shipping") }, true);

        }



    }
}
