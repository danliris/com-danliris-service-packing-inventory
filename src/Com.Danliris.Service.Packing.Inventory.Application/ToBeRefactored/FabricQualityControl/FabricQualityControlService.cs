using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.FabricQualityControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.FabricQualityControl
{
    public class FabricQualityControlService : IFabricQualityControlService
    {
        private readonly IFabricQualityControlRepository _repository;
        private readonly IDyeingPrintingAreaMovementRepository _dpRepository;
        private readonly IFabricGradeTestRepository _fgtRepository;

        public FabricQualityControlService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IFabricQualityControlRepository>();
            _dpRepository = serviceProvider.GetService<IDyeingPrintingAreaMovementRepository>();
            _fgtRepository = serviceProvider.GetService<IFabricGradeTestRepository>();
        }

        private FabricQualityControlViewModel MapToViewModel(FabricQualityControlModel model, DyeingPrintingAreaMovementModel dpModel)
        {
            FabricQualityControlViewModel vm = new FabricQualityControlViewModel()
            {
                DyeingPrintingAreaMovementId = model.DyeingPrintingAreaMovementId,
                Active = model.Active,
                Buyer = dpModel?.Buyer,
                CartNo = dpModel?.CartNo,
                Code = model.Code,
                Color = dpModel?.Color,
                Construction = dpModel?.Construction,
                CreatedAgent = model.CreatedAgent,
                CreatedBy = model.CreatedBy,
                CreatedUtc = model.CreatedUtc,
                DateIm = model.DateIm,
                DeletedAgent = model.DeletedAgent,
                DeletedBy = model.DeletedBy,
                DeletedUtc = model.DeletedUtc,
                DyeingPrintingAreaMovementBonNo = model.DyeingPrintingAreaMovementBonNo,
                Group = model.Group,
                Id = model.Id,
                IsDeleted = model.IsDeleted,
                IsUsed = model.IsUsed,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedUtc = model.LastModifiedUtc,
                MachineNoIm = model.MachineNoIm,
                OperatorIm = model.OperatorIm,
                OrderQuantity = dpModel?.ProductionOrderQuantity,
                PackingInstruction = dpModel?.PackingInstruction,
                PointLimit = model.PointLimit,
                PointSystem = model.PointSystem,
                ProductionOrderNo = model.ProductionOrderNo,
                ProductionOrderType = dpModel?.ProductionOrderType,
                ShiftIm = dpModel?.Shift,
                UId = model.UId,
                Uom = dpModel?.UOMUnit,
                FabricGradeTests = model.FabricGradeTests.Select(s => new FabricGradeTestViewModel()
                {
                    Active = s.Active,
                    AvalLength = s.AvalLength,
                    CreatedAgent = s.CreatedAgent,
                    CreatedBy = s.CreatedBy,
                    CreatedUtc = s.CreatedUtc,
                    DeletedAgent = s.DeletedAgent,
                    DeletedBy = s.DeletedBy,
                    DeletedUtc = s.DeletedUtc,
                    FabricGradeTest = s.FabricGradeTest,
                    FinalArea = s.FinalArea,
                    FinalGradeTest = s.FinalGradeTest,
                    FinalLength = s.FinalLength,
                    FinalScore = s.FinalScore,
                    Grade = s.Grade,
                    Id = s.Id,
                    InitLength = s.InitLength,
                    IsDeleted = s.IsDeleted,
                    LastModifiedAgent = s.LastModifiedAgent,
                    LastModifiedBy = s.LastModifiedBy,
                    LastModifiedUtc = s.LastModifiedUtc,
                    PcsNo = s.PcsNo,
                    PointLimit = s.PointLimit,
                    PointSystem = s.PointSystem,
                    SampleLength = s.SampleLength,
                    Score = s.Score,
                    Type = s.Type,
                    Width = s.Width,
                    Criteria = s.Criteria.Select(c => new CriteriaViewModel()
                    {
                        Code = c.Code,
                        Group = c.Group,
                        Id = c.Id,
                        Index = c.Index,
                        Name = c.Name,
                        Score = new Score()
                        {
                            A = c.ScoreA,
                            B = c.ScoreB,
                            C = c.ScoreC,
                            D = c.ScoreD
                        }
                    }).ToList()
                }).ToList()
            };

            return vm;
        }

        public Task<int> Create(FabricQualityControlViewModel viewModel)
        {
            do
            {
                viewModel.Code = CodeGenerator.Generate(8);
            } while (_repository.GetDbSet().Any(entity => entity.Code == viewModel.Code));

            var model = new FabricQualityControlModel(viewModel.Code, viewModel.DateIm.GetValueOrDefault(), viewModel.Group, viewModel.IsUsed.GetValueOrDefault(), viewModel.DyeingPrintingAreaMovementId,
                viewModel.DyeingPrintingAreaMovementBonNo, viewModel.ProductionOrderNo,
                viewModel.MachineNoIm, viewModel.OperatorIm, viewModel.PointLimit.GetValueOrDefault(), viewModel.PointSystem.GetValueOrDefault(),
                viewModel.FabricGradeTests.Select((s, i) => 
                    new FabricGradeTestModel(s.AvalLength.GetValueOrDefault(), s.FabricGradeTest.GetValueOrDefault(), s.FinalArea.GetValueOrDefault(), 
                    s.FinalGradeTest.GetValueOrDefault(),s.FinalLength.GetValueOrDefault(), s.FinalScore.GetValueOrDefault(), s.Grade, s.InitLength.GetValueOrDefault(),
                    s.PcsNo, s.PointLimit.GetValueOrDefault(), s.PointSystem.GetValueOrDefault(), s.SampleLength.GetValueOrDefault(), s.Score.GetValueOrDefault(),
                    s.Type, s.Width.GetValueOrDefault(), i, 
                        s.Criteria.Select((d, cInd) => new CriteriaModel(d.Code, d.Group, cInd, d.Name, d.Score.A.GetValueOrDefault(), d.Score.B.GetValueOrDefault(),
                        d.Score.C.GetValueOrDefault(), d.Score.D.GetValueOrDefault())).ToList())).ToList());

            return _repository.InsertAsync(model);
        }

        public Task<int> Delete(int id)
        {
            return _repository.DeleteAsync(id);
        }

        public MemoryStream GenerateExcel(string code, int kanbanId, string productionOrderType, string productionOrderNo, string shiftIm, DateTime? dateFrom, DateTime? dateTo, int offSet)
        {
            var data = GetReport(code, kanbanId, productionOrderType, productionOrderNo, shiftIm, dateFrom, dateTo, offSet);

            data = data.OrderByDescending(x => x.LastModifiedUtc).ToList();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nomor Pemeriksaan Kain", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nomor Bon Inspeksi IM", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nomor Kereta", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nomor Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal IM", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Shift", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Operator IM", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No Mesin IM", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Konstruksi", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jumlah Order (meter)", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Packing Instruction", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nomor PCS", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Panjang PCS (meter)", DataType = typeof(int) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Lebar PCS (meter)", DataType = typeof(int) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nilai", DataType = typeof(int) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Aval (meter)", DataType = typeof(int) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Sampel (meter)", DataType = typeof(int) });

            if (data.Count == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 0, 0, 0, "", 0, 0);
            }
            else
            {
                int index = 1;
                foreach (var item in data)
                {
                    foreach (var detail in item.FabricGradeTests)
                    {
                        dt.Rows.Add(index++, item.Code, item.DyeingPrintingAreaMovementBonNo, item.CartNo, item.ProductionOrderType, item.ProductionOrderNo,
                            item.DateIm.GetValueOrDefault().AddHours(offSet).ToString("dd/MM/yyyy"), item.ShiftIm, item.OperatorIm, item.MachineNoIm,
                            item.Construction, item.Buyer, item.Color, item.OrderQuantity.GetValueOrDefault().ToString(), item.PackingInstruction,
                            detail.PcsNo, detail.InitLength, detail.Width, detail.FinalScore, detail.Grade, detail.AvalLength, detail.SampleLength);
                    }
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Pemeriksaan Kain") }, true);
        }

        public Task<List<FabricQCGradeTestsViewModel>> GetForSPP(string no)
        {
            IQueryable<FabricQCGradeTestsViewModel> data;
            
            if (string.IsNullOrEmpty(no))
            {
                data = from qc in _repository.GetDbSet().AsNoTracking()
                       join dp in _dpRepository.ReadAll()
                       on qc.DyeingPrintingAreaMovementId equals dp.Id
                       select new FabricQCGradeTestsViewModel
                       {
                           OrderNo = qc.ProductionOrderNo,
                           OrderQuantity = dp.ProductionOrderQuantity
                       };


            }
            else
            {
                data = from fabricqc in _repository.GetDbSet().AsNoTracking()
                       join fabricgt in _fgtRepository.GetDbSet().AsNoTracking() on fabricqc.Id equals fabricgt.FabricQualityControlId
                       join dp in _dpRepository.ReadAll()
                       on fabricqc.DyeingPrintingAreaMovementId equals dp.Id
                       where fabricqc.ProductionOrderNo == no
                       select new FabricQCGradeTestsViewModel
                       {
                           OrderNo = fabricqc.ProductionOrderNo,
                           OrderQuantity = dp.ProductionOrderQuantity,
                           Grade = fabricgt.Grade
                       };


            }
            return data.AsNoTracking().ToListAsync();
        }

        public ListResult<FabricQualityControlViewModel> GetReport(int page, int size, string code, int dyeingPrintintMovementId, string productionOrderType, 
            string productionOrderNo, string shiftIm, DateTime? dateFrom, DateTime? dateTo, int offSet)
        {
            var queries = GetReport(code, dyeingPrintintMovementId, productionOrderType, productionOrderNo, shiftIm, dateFrom, dateTo, offSet);

            return new ListResult<FabricQualityControlViewModel>(queries, 1, queries.Count, queries.Count);
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "DyeingPrintingAreaMovementBonNo", "ProductionOrderNo"
            };
            query = QueryHelper<FabricQualityControlModel>.Search(query, SearchAttributes, keyword);
            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<FabricQualityControlModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<FabricQualityControlModel>.Order(query, OrderDictionary);

            var data = (from qcData in query.Skip((page - 1) * size).Take(size)
                        join dpData in _dpRepository.ReadAll()
                        on qcData.DyeingPrintingAreaMovementId equals dpData.Id into indexData
                        from dpData in indexData.DefaultIfEmpty()
                        select new IndexViewModel()
                        {
                            Id = qcData.Id,
                            CartNo = dpData.CartNo,
                            Code = qcData.Code,
                            DateIm = qcData.DateIm,
                            DyeingPrintingAreaMovementBonNo = qcData.DyeingPrintingAreaMovementBonNo,
                            IsUsed = qcData.IsUsed,
                            MachineNoIm = qcData.MachineNoIm,
                            OperatorIm = qcData.OperatorIm,
                            ProductionOrderNo = qcData.ProductionOrderNo,
                            ProductionOrderType = dpData.ProductionOrderType,
                            ShiftIm = dpData.Shift
                        });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<FabricQualityControlViewModel> ReadById(int id)
        {
            var model = await _repository.ReadByIdAsync(id);
            if (model == null)
                return null;

            var dpData = await _dpRepository.ReadByIdAsync(model.DyeingPrintingAreaMovementId);

            FabricQualityControlViewModel vm = MapToViewModel(model, dpData);

            return vm;
        }

        public Task<int> Update(int id, FabricQualityControlViewModel viewModel)
        {
            var model = new FabricQualityControlModel(viewModel.Code, viewModel.DateIm.GetValueOrDefault(), viewModel.Group, viewModel.IsUsed.GetValueOrDefault(), viewModel.DyeingPrintingAreaMovementId,
                viewModel.DyeingPrintingAreaMovementBonNo, viewModel.ProductionOrderNo,
                viewModel.MachineNoIm, viewModel.OperatorIm, viewModel.PointLimit.GetValueOrDefault(), viewModel.PointSystem.GetValueOrDefault(),
                viewModel.FabricGradeTests.Select((s, i) =>
                    new FabricGradeTestModel(s.AvalLength.GetValueOrDefault(), s.FabricGradeTest.GetValueOrDefault(), s.FinalArea.GetValueOrDefault(),
                    s.FinalGradeTest.GetValueOrDefault(), s.FinalLength.GetValueOrDefault(), s.FinalScore.GetValueOrDefault(), s.Grade, s.InitLength.GetValueOrDefault(),
                    s.PcsNo, s.PointLimit.GetValueOrDefault(), s.PointSystem.GetValueOrDefault(), s.SampleLength.GetValueOrDefault(), s.Score.GetValueOrDefault(),
                    s.Type, s.Width.GetValueOrDefault(), i,
                        s.Criteria.Select((d, cInd) => new CriteriaModel(d.Code, d.Group, cInd, d.Name, d.Score.A.GetValueOrDefault(), d.Score.B.GetValueOrDefault(),
                        d.Score.C.GetValueOrDefault(), d.Score.D.GetValueOrDefault())).ToList())).ToList());

            return _repository.UpdateAsync(id, model);
        }

        private List<FabricQualityControlViewModel> GetReport(string code, int dyeingPrintingMovementId, string productionOrderType, string productionOrderNo, string shiftIm, DateTime? dateFrom, DateTime? dateTo, int offSet)
        {

            IQueryable<FabricQualityControlModel> query = _repository.GetDbSet().Include(s => s.FabricGradeTests).AsNoTracking();
            IQueryable<DyeingPrintingAreaMovementModel> dpQuery = _dpRepository.ReadAll();
            IEnumerable<FabricQualityControlViewModel> fabricQCs;

            if (!string.IsNullOrEmpty(code))
                query = query.Where(x => x.Code == code);

            if (dyeingPrintingMovementId != -1)
                query = query.Where(x => x.DyeingPrintingAreaMovementId == dyeingPrintingMovementId);

            if (!string.IsNullOrEmpty(productionOrderType))
                dpQuery = dpQuery.Where(x => x.ProductionOrderType == productionOrderType);

            if (!string.IsNullOrEmpty(productionOrderNo))
                query = query.Where(x => x.ProductionOrderNo == productionOrderNo);

            if (!string.IsNullOrEmpty(shiftIm))
                dpQuery = dpQuery.Where(x => x.Shift == shiftIm);

            if (dateFrom == null && dateTo == null)
            {
                query = query
                    .Where(x => DateTimeOffset.UtcNow.AddDays(-30).Date <= x.DateIm.AddHours(offSet).Date
                        && x.DateIm.AddHours(offSet).Date <= DateTime.UtcNow.Date);
            }
            else if (dateFrom == null && dateTo != null)
            {
                query = query
                    .Where(x => dateTo.Value.AddDays(-30).Date <= x.DateIm.AddHours(offSet).Date
                        && x.DateIm.AddHours(offSet).Date <= dateTo.Value.Date);
            }
            else if (dateTo == null && dateFrom != null)
            {
                query = query
                    .Where(x => dateFrom.Value.Date <= x.DateIm.AddHours(offSet).Date
                        && x.DateIm.AddHours(offSet).Date <= dateFrom.Value.AddDays(30).Date);
            }
            else
            {
                query = query
                    .Where(x => dateFrom.Value.Date <= x.DateIm.AddHours(offSet).Date
                        && x.DateIm.AddHours(offSet).Date <= dateTo.Value.Date);
            }

            fabricQCs = (from qc in query
                         join dp in dpQuery
                         on qc.DyeingPrintingAreaMovementId equals dp.Id
                         select new FabricQualityControlViewModel()
                         {
                             Code = qc.Code,
                             DyeingPrintingAreaMovementBonNo = qc.DyeingPrintingAreaMovementBonNo,
                             CartNo = dp.CartNo,
                             ProductionOrderType = dp.ProductionOrderType,
                             ProductionOrderNo = qc.ProductionOrderNo,
                             DateIm = qc.DateIm,
                             ShiftIm = dp.Shift,
                             OperatorIm = qc.OperatorIm,
                             MachineNoIm = qc.MachineNoIm,
                             Construction = dp.Construction,
                             Buyer = dp.Buyer,
                             Color = dp.Color,
                             OrderQuantity = dp.ProductionOrderQuantity,
                             PackingInstruction = dp.PackingInstruction,
                             FabricGradeTests = qc.FabricGradeTests.Select(y => new FabricGradeTestViewModel()
                             {
                                 PcsNo = y.PcsNo,
                                 InitLength = y.InitLength,
                                 Width = y.Width,
                                 FinalScore = y.FinalScore,
                                 Grade = y.Grade,
                                 AvalLength = y.AvalLength,
                                 SampleLength = y.SampleLength
                             }).ToList(),
                             LastModifiedUtc = qc.LastModifiedUtc

                         });

            

            return fabricQCs.ToList();
        }
    }
}
