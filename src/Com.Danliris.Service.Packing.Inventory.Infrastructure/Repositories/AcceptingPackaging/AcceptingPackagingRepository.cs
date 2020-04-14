using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Data.Models.AcceptingPackaging;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.AcceptingPackaging
{
    public class AcceptingPackagingRepository : IAcceptingPackagingRepository
    {
        private const string UserAgent = "Repository";
        private const AreaEnum Area = AreaEnum.PACK;
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<DyeingPrintingAreaMovementModel> _dyeingPrintingAreaMovementDbSet;
        private readonly DbSet<DyeingPrintingAreaMovementHistoryModel> _historyDbSet;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<AcceptingPackagingModel> _acceptingPackagingDbSet;

        public AcceptingPackagingRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _acceptingPackagingDbSet = dbContext.Set<AcceptingPackagingModel>();
            _historyDbSet = dbContext.Set<DyeingPrintingAreaMovementHistoryModel>();
            _dyeingPrintingAreaMovementDbSet = dbContext.Set<DyeingPrintingAreaMovementModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();

        }

        public Task<int> DeleteAsync(int dyeingPrintingAreaMovementId)
        {
            var model = _historyDbSet.OrderByDescending(x=>x.LastModifiedUtc).FirstOrDefault(entity => entity.DyeingPrintingAreaMovementId == dyeingPrintingAreaMovementId);
            model.FlagForDelete(_identityProvider.Username, UserAgent);
            _historyDbSet.Update(model);
            var modelPrev = _historyDbSet.OrderByDescending(x => x.LastModifiedUtc).Skip(1).FirstOrDefault(entity => entity.DyeingPrintingAreaMovementId == dyeingPrintingAreaMovementId && !entity.IsDeleted);
            var modelMaster = _dyeingPrintingAreaMovementDbSet.Where(x => x.Id == dyeingPrintingAreaMovementId).FirstOrDefault();

            if (modelPrev != null)
            {
                modelMaster.SetArea(modelPrev.Area, _identityProvider.Username, UserAgent);
                modelMaster.SetBalance(modelPrev.Balance, _identityProvider.Username, UserAgent);
                modelMaster.SetGrade(modelPrev.Grade, _identityProvider.Username, UserAgent);
            }
            else
            {
                modelMaster.FlagForDelete(_identityProvider.Username, UserAgent);
            }
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(int idDyeingMovementArea, AcceptingPackagingModel data)
        {
            var masterData = _dyeingPrintingAreaMovementDbSet.Where(x => x.Id == idDyeingMovementArea).FirstOrDefault();
            if (masterData != null)
            {
                masterData.SetBalance(data.Saldo, _identityProvider.Username, UserAgent);
                masterData.SetGrade(data.Grade, _identityProvider.Username, UserAgent);
                masterData.SetArea(Enum.GetName(typeof(AreaEnum), Area),_identityProvider.Username,UserAgent);
                //insert history
                var newHistory = new DyeingPrintingAreaMovementHistoryModel
                {
                    Active = data.Active,
                    CreatedUtc = data.CreatedUtc,
                    CreatedBy = data.CreatedBy,
                    CreatedAgent = data.CreatedAgent,
                    LastModifiedAgent = data.LastModifiedAgent,
                    LastModifiedBy = data.LastModifiedBy,
                    LastModifiedUtc = data.LastModifiedUtc,
                    IsDeleted = data.IsDeleted,
                    DeletedUtc = data.DeletedUtc,
                    DeletedAgent = data.DeletedAgent,
                    DeletedBy = data.DeletedBy,
                    DyeingPrintingAreaMovementId = data.IdDyeingPrintingMovement
                };

                newHistory.SetDate(data.Date, _identityProvider.Username, UserAgent);
                newHistory.SetIndexArea(Area, _identityProvider.Username, UserAgent);
                newHistory.SetShift(data.Shift, _identityProvider.Username, UserAgent);
                newHistory.SetBalance(data.Saldo, _identityProvider.Username, UserAgent);
                newHistory.SetGrade(data.Grade, _identityProvider.Username, UserAgent);
                masterData.DyeingPrintingAreaMovementHistories.Add(newHistory);

                return _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Id/No Bon Tidak Ditemukan");
            }
        }

        public IQueryable<AcceptingPackagingModel> ReadAll() => MappingListDyeingToAccepting(_dyeingPrintingAreaMovementDbSet.Where(x => x.Area == Enum.GetName(typeof(AreaEnum), Area)).AsNoTracking());


        public AcceptingPackagingModel ReadByDyeingPrintingAreaMovement(int dyeingPrintingAreaMovementId) => MappingDyeingToAccepting(_dyeingPrintingAreaMovementDbSet.Where(x => x.Id == dyeingPrintingAreaMovementId).FirstOrDefault());


        public Task<int> UpdateAsync(int dyeingPrintingAreaMovementId, AcceptingPackagingModel data)
        {
            var masterData = _dyeingPrintingAreaMovementDbSet.Where(x => x.Id == dyeingPrintingAreaMovementId).FirstOrDefault();
            if (masterData != null)
            {
                masterData.SetBalance(data.Saldo, _identityProvider.Username, UserAgent);
                masterData.SetGrade(data.Grade, _identityProvider.Username, UserAgent);
                masterData.SetArea(Enum.GetName(typeof(AreaEnum), Area), _identityProvider.Username, UserAgent);

                return _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Id/No Bon Tidak Ditemukan");
            }

        }

        public Task<int> UpdateAsync(string BonNoDyeingPrintingAreaMovement, AcceptingPackagingModel data)
        {
            var dataId = ReadByBonNo(BonNoDyeingPrintingAreaMovement);
            if (dataId != null)
            {
                return UpdateAsync(dataId.Id, data);
            }
            else
            {
                throw new Exception("Id/No Bon Tidak Ditemukan");
            }
        }


        private IQueryable<AcceptingPackagingModel> MappingListDyeingToAccepting(IQueryable<DyeingPrintingAreaMovementModel> source)
        {
            List<AcceptingPackagingModel> result = new List<AcceptingPackagingModel>();
            var sourceList = source.ToList();
            foreach (var item in sourceList)
            {
                result.Add(MappingDyeingToAccepting(item));
            }
            return result.AsQueryable();
        }
        private AcceptingPackagingModel MappingDyeingToAccepting(DyeingPrintingAreaMovementModel item)
        {
            if (item != null)
            {
                return new AcceptingPackagingModel
                {
                    Active = item.Active,
                    Satuan = item.UOMUnit,
                    Saldo = item.Balance,
                    Shift = item.Shift,
                    DeletedAgent = item.DeletedAgent,
                    BonNo = item.BonNo,
                    CreatedAgent = item.CreatedAgent,
                    DeletedBy = item.DeletedBy,
                    CreatedBy = item.CreatedBy,
                    Grade = item.Grade,
                    Id = item.Id,
                    DeletedUtc = item.DeletedUtc,
                    IdDyeingPrintingMovement = item.Id,
                    CreatedUtc = item.CreatedUtc,
                    IsDeleted = item.IsDeleted,
                    LastModifiedAgent = item.LastModifiedAgent,
                    LastModifiedBy = item.LastModifiedBy,
                    LastModifiedUtc = item.LastModifiedUtc,
                    MaterialObject = item.MaterialName,
                    Motif = item.Motif,
                    Mtr = item.MeterLength,
                    OrderNo = item.ProductionOrderNo,
                    Unit = item.UnitName,
                    Warna = item.Color,
                    Yds = item.YardsLength
                };
            }
            else
            {
                return null;
            }
        }

        public AcceptingPackagingModel ReadByBonNo(string bonNo)
        {
            var dyeingMovement = _dyeingPrintingAreaMovementDbSet.Where(x => x.BonNo == bonNo).FirstOrDefault();
            if (dyeingMovement != null)
            {
                return MappingDyeingToAccepting(dyeingMovement);
            }
            else
            {
                throw new Exception("No Bon Tidak Ditemukan");
            }
        }

        public Task<int> InsertAsync(string BonNoDyeingMovementArea, AcceptingPackagingModel data)
        {
            var dataId = ReadByBonNo(BonNoDyeingMovementArea);
            if (dataId != null)
            {
                return InsertAsync(dataId.Id, data);
            }
            else
            {
                return Task.FromResult<int>(0);
            }
        }

        public Task<int> InsertAsync(AcceptingPackagingModel model)
        {
            return InsertAsync(model.BonNo, model);
        }

        public Task<AcceptingPackagingModel> ReadByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
