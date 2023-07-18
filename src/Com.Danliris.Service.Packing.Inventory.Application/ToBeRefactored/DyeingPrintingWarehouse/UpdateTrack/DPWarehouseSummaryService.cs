using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.UpdateTrack.ViewModel;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse;
using Microsoft.EntityFrameworkCore;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Moonlay.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using System.IO;
using System.Data;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.UpdateTrack
{
    public class DPWarehouseSummaryService : IDPWarehouseSummaryService
    {
        private const string UserAgent = "packing-inventory-service";
        private readonly IIdentityProvider _identityProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<DPWarehouseSummaryModel> _dbSetSummary;
        private readonly DbSet<DPWarehouseMovementModel> _dbSetMovement;

        public DPWarehouseSummaryService(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {

            _dbSetSummary = dbContext.Set<DPWarehouseSummaryModel>();
            _dbSetMovement = dbContext.Set<DPWarehouseMovementModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _serviceProvider = serviceProvider;
            _dbContext = dbContext;
        }

        public List<DPUpdateTrackViewModel> GetDataUpdateTrack(int productionOrderId, string barcode, int trackId)
        {
            IQueryable<DPWarehouseSummaryModel> DPWarehouseSummaryQuery;

            DPWarehouseSummaryQuery = _dbSetSummary.AsNoTracking().Where(x => x.BalanceRemains > 0);

            if (productionOrderId != 0)
            {
                DPWarehouseSummaryQuery = DPWarehouseSummaryQuery.Where(model => model.ProductionOrderId == productionOrderId);
            }

            if (!string.IsNullOrEmpty(barcode))
            {
                DPWarehouseSummaryQuery = DPWarehouseSummaryQuery.Where(model => model.ProductPackingCode.Contains(barcode));
            }

            if (trackId != 0)
            {
                DPWarehouseSummaryQuery = DPWarehouseSummaryQuery.Where(model => model.TrackId == trackId);
            }

            var result = DPWarehouseSummaryQuery.Select(b => new DPUpdateTrackViewModel()
            {
                Id = b.Id,
                ProductionOrderId = b.ProductionOrderId,
                ProductionOrderNo = b.ProductionOrderNo,
                ProductPackingCode = b.ProductPackingCode,
                ProcessTypeName = b.ProcessTypeName,
                PackagingUnit = b.PackagingUnit,

                Grade = b.Grade,
                Color = b.Color,
                Construction = b.Construction,
                Motif = b.Motif,
                TrackId = b.TrackId,
                TrackName = b.TrackName,
                TrackBox = b.TrackBox,
                Track = b.TrackBox != null ? b.TrackType + " - " + b.TrackName + " - " + b.TrackBox : b.TrackType + " - " + b.TrackName,
                //Track = b.TrackType + " - " + b.TrackName ,
                PackagingQty = b.PackagingQtyRemains,
                PackagingLength = b.PackagingLength,
                Balance = b.BalanceRemains,
                Description = b.Description

            }).ToList();

            var totalPacking = result.Sum(x => x.PackagingQty);
            var totalInQty = result.Sum(x => x.Balance);
            result.Add(new DPUpdateTrackViewModel()
            {
                Track = "Total",
                PackagingQty = totalPacking,
                Balance = totalInQty
            });

            return result;

        }

        

        public async Task<DPWarehouseSummaryViewModel> ReadById(int id)
        {
            var model = await _dbSetSummary.FirstOrDefaultAsync(s => s.Id == id);
            if (model == null)
                return null;

            DPWarehouseSummaryViewModel vm = await MapToViewModel(model);

            return vm;
        }
        private async Task<DPWarehouseSummaryViewModel> MapToViewModel(DPWarehouseSummaryModel model)
        {

            var vm = new DPWarehouseSummaryViewModel();
            vm = new DPWarehouseSummaryViewModel()
            {
                Active = model.Active,
                LastModifiedUtc = model.LastModifiedUtc,
                Balance = model.Balance,
                BalanceRemains = model.BalanceRemains,
                BalanceOut = model.BalanceOut,
                Buyer = model.Buyer,
                BuyerId = model.BuyerId,
                Color = model.Color,
                Construction = model.Construction,
                CreatedAgent = model.CreatedAgent,
                CreatedBy = model.CreatedBy,
                CreatedUtc = model.CreatedUtc,
                DeletedAgent = model.DeletedAgent,
                DeletedBy = model.DeletedBy,
                DeletedUtc = model.DeletedUtc,
                Grade = model.Grade,
               
                PackingInstruction = model.PackingInstruction,
                //Remark = model.Remark,
                //Status = model.Status,
                Id = model.Id,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                Motif = model.Motif,

                ProductionOrder = new ProductionOrder()
                {
                    Id = model.ProductionOrderId,
                    No = model.ProductionOrderNo,
                    OrderQuantity = model.ProductionOrderOrderQuantity,
                    Type = model.ProductionOrderType
                },
                Unit = model.Unit,
                MaterialWidth = model.MaterialWidth,
                Material = new Material()
                {
                    Id = model.MaterialId,
                    Name = model.MaterialName
                },
                MaterialConstruction = new MaterialConstruction()
                {
                    Name = model.MaterialConstructionName,
                    Id = model.MaterialConstructionId
                },
                YarnMaterial = new YarnMaterial()
                {
                    Id = model.YarnMaterialId,
                    Name = model.YarnMaterialName
                },
                ProcessType = new ProcessType()
                {
                    Id = model.ProcessTypeId,
                    Name = model.ProcessTypeName
                },
                UomUnit = model.UomUnit,
                Uom = new UnitOfMeasurement()
                {
                    Unit = model.PackagingUnit
                },
                PackagingQty = model.PackagingQty,
                PackagingQtyRemains = model.PackagingQtyRemains,
                PackagingQtyOut = model.PackagingQtyOut,
                PackagingType = model.PackagingType,
                PackagingUnit = model.PackagingUnit,
                ProductionOrderNo = model.ProductionOrderNo,
                QtyOrder = model.ProductionOrderOrderQuantity,

                Track = new Track()
                {
                    Id = model.TrackId,
                    Name = model.TrackName,
                    Type = model.TrackType,
                    Box = model.TrackBox
                },
                ProductPackingCode = model.ProductPackingCode,
                ProductPackingId = model.ProductPackingId,
                ProductSKUId = model.ProductSKUId,
                ProductSKUCode = model.ProductSKUCode,
                TrackName = model.TrackType + " - " + model.TrackName + " - " + model.TrackBox,
                PackagingLength = model.PackagingLength,
                Description = model.Description

            };

            return vm;
        }

        public async Task<int> UpdateTrack(int id, DPTrackViewModel viewModel)
        {

            int Created = 0;
            DPWarehouseSummaryModel dataUpdate;
            var dbModel =  _dbSetSummary.FirstOrDefault(s => s.Id == id);
            using (var transaction = this._dbContext.Database.BeginTransaction()) 
            {
                try
                {
                    for (int i = 0; i < viewModel.Items.Count(); i++)
                    {
                        if (i == 0)
                        {
                            var DPSummary = _dbSetSummary.FirstOrDefault(s => s.Id == id);

                            DPSummary.BalanceRemains = viewModel.Items[i].Balance;
                            DPSummary.PackagingQtyRemains = (decimal)viewModel.Items[i].PackagingQtySplit;
                            DPSummary.SplitQuantity = DPSummary.SplitQuantity + viewModel.Items[i].PackingQtyDiff;
                            EntityExtension.FlagForUpdate(DPSummary, _identityProvider.Username, UserAgent);

                        }
                        else
                        {
                            var dPSummaryN = _dbSetSummary.FirstOrDefault(s => s.ProductPackingCode.Contains(viewModel.ProductPackingCode) && s.TrackId == viewModel.Items[i].Track.Id && s.PackagingLength == viewModel.PackagingLength);

                            if (dPSummaryN != null)
                            {
                                dPSummaryN.Balance = dPSummaryN.Balance + viewModel.Items[i].Balance;
                                dPSummaryN.BalanceRemains = dPSummaryN.BalanceRemains + viewModel.Items[i].Balance;
                                dPSummaryN.PackagingQty = dPSummaryN.PackagingQty + (decimal)viewModel.Items[i].PackagingQtySplit;
                                dPSummaryN.PackagingQtyRemains = dPSummaryN.PackagingQtyRemains + (decimal)viewModel.Items[i].PackagingQtySplit;
                                dPSummaryN.TrackId = viewModel.Items[i].Track.Id;
                                dPSummaryN.TrackName = viewModel.Items[i].Track.Name;
                                dPSummaryN.TrackBox = viewModel.Items[i].Track.Box;
                                dPSummaryN.TrackType = viewModel.Items[i].Track.Type;

                                EntityExtension.FlagForUpdate(dPSummaryN, _identityProvider.Username, UserAgent);

                                var track = viewModel.Items[1].Track.Id;


                            }
                            else
                            {
                                var createDataSummary = new DPWarehouseSummaryModel(
                                                            viewModel.Items[i].Balance,
                                                            viewModel.Items[i].Balance,
                                                            0,
                                                            dbModel.BuyerId,
                                                            dbModel.Buyer,
                                                            "",
                                                            dbModel.Color,
                                                            dbModel.Grade,
                                                            dbModel.Construction,
                                                            dbModel.MaterialConstructionId,
                                                            dbModel.MaterialConstructionName,
                                                            dbModel.MaterialId,
                                                            dbModel.MaterialName,
                                                            dbModel.MaterialWidth,
                                                            dbModel.Motif,
                                                            dbModel.PackingInstruction,
                                                            (decimal)viewModel.Items[i].PackagingQtySplit,
                                                            (decimal)viewModel.Items[i].PackagingQtySplit,
                                                            0,
                                                            dbModel.PackagingLength,
                                                            dbModel.PackagingType,
                                                            dbModel.PackagingUnit,
                                                            dbModel.ProductionOrderId,
                                                            dbModel.ProductionOrderNo,
                                                            dbModel.ProductionOrderType,
                                                            dbModel.ProductionOrderOrderQuantity,
                                                            dbModel.CreatedUtcOrderNo,
                                                            dbModel.ProcessTypeId,
                                                            dbModel.ProcessTypeName,
                                                            dbModel.YarnMaterialId,
                                                            dbModel.YarnMaterialName,
                                                            dbModel.Unit,
                                                            dbModel.UomUnit,
                                                            viewModel.Items[i].Track.Id,
                                                            viewModel.Items[i].Track.Type,
                                                            viewModel.Items[i].Track.Name,
                                                            viewModel.Items[i].Track.Box,
                                                            0,
                                                            dbModel.Description,
                                                            dbModel.ProductSKUId,
                                                            dbModel.FabricSKUId,
                                                            dbModel.ProductSKUCode,
                                                            dbModel.ProductPackingId,
                                                            dbModel.FabricPackingId,
                                                            dbModel.ProductPackingCode,
                                                            dbModel.MaterialOrigin,
                                                            dbModel.Remark,
                                                            dbModel.FinishWidth
                                                            );

                                var track = viewModel.Items[0].Track.Id;
                                createDataSummary.FlagForCreate(_identityProvider.Username, UserAgent);

                                _dbSetSummary.Add(createDataSummary);

                            }

                        }
                    }

                    Created = await _dbContext.SaveChangesAsync();
                    await createMovement(viewModel);
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

        private async Task<int> createMovement(DPTrackViewModel viewModel)
        {
            int count = 0;
            for (int i = 0; i < viewModel.Items.Count(); i++)
            {
                var IdSum = _dbSetSummary.FirstOrDefault(x => x.ProductPackingCode == viewModel.ProductPackingCode && x.TrackId == viewModel.Items[i].Track.Id);
                if (i != 0)
                {
                    var modelMovement = new DPWarehouseMovementModel(
                                DateTime.Now,
                                DyeingPrintingArea.GUDANGJADI,
                                DyeingPrintingArea.MOVE,
                                0,
                                0,
                                DyeingPrintingArea.MOVE,
                                IdSum.Id,
                                IdSum.ProductionOrderId,
                                IdSum.ProductionOrderNo,
                                IdSum.Buyer,
                                IdSum.Construction,
                                IdSum.Unit,
                                IdSum.Color,
                                IdSum.Motif,
                                IdSum.UomUnit,
                                IdSum.Balance,
                                IdSum.Grade,
                                IdSum.ProductionOrderType,
                                IdSum.Remark,
                                IdSum.PackagingType,
                                IdSum.PackagingQty,
                                IdSum.PackagingUnit,
                                IdSum.PackagingLength,
                                IdSum.MaterialOrigin,
                                0,
                                "",
                                "",
                                IdSum.ProductPackingId,
                                IdSum.ProductPackingCode,
                                viewModel.Items[0].Track.Id,
                                viewModel.Items[0].Track.Name,
                                viewModel.Items[0].Track.Box,
                                viewModel.Items[0].Track.Type,
                                viewModel.Items[i].Track.Id,
                                viewModel.Items[i].Track.Name,
                                viewModel.Items[i].Track.Box,
                                viewModel.Items[i].Track.Type,
                                IdSum.Description

                                );

                    modelMovement.FlagForCreate(_identityProvider.Username, UserAgent);

                    _dbSetMovement.Add(modelMovement);

                }





            }

            count = await _dbContext.SaveChangesAsync();
            return count;
        }

        public MemoryStream GenerateExcelMonitoring(int productionOrderId, string barcode, int trackId)
        {
            var data = GetDataUpdateTrack(productionOrderId, barcode, trackId);
            DataTable dt = new DataTable();

            //dt.Columns.Add(new DataColumn() { ColumnName = "No Bon", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Barcode", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Konstruksi", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan Pack", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jalur/Rak", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Packing", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Satuan", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty Total", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(string) });


            if (data.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", 0, 0, 0, "");
            }
            else
            {
                //decimal sumPackagingQty = 0;
                //double totalBalance = 0;


                foreach (var item in data)
                {

                    dt.Rows.Add(item.ProductionOrderNo, item.ProductPackingCode, item.Construction,
                        item.Grade, item.PackagingUnit, item.Track, item.PackagingQty, item.PackagingLength, item.Balance, item.Description);

                    //sumPackagingQty += item.PackagingQty;
                    //totalBalance += item.Balance;


                }

                //dt.Rows.Add("", "", "", "", "", "", sumPackagingQty, 0, totalBalance, "");
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, string.Format("Monitoring Rak/Jalur {0}", "MO")) }, true);

        }


    }
}
