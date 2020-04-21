using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Com.Moonlay.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.FabricQualityControl
{
    public class FabricQualityControlRepository : IFabricQualityControlRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<FabricQualityControlModel> _fabricQCDbSet;
        private readonly DbSet<FabricGradeTestModel> _fabricGradeTestDbSet;
        private readonly DbSet<CriteriaModel> _criteriaDbSet;
        private readonly IIdentityProvider _identityProvider;

        public FabricQualityControlRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _fabricQCDbSet = dbContext.Set<FabricQualityControlModel>();
            _fabricGradeTestDbSet = dbContext.Set<FabricGradeTestModel>();
            _criteriaDbSet = dbContext.Set<CriteriaModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _fabricQCDbSet.Include(s => s.FabricGradeTests).FirstOrDefault(entity => entity.Id == id);
            model.FlagForDelete(_identityProvider.Username, UserAgent);
            foreach (var item in model.FabricGradeTests)
            {
                item.FlagForDelete(_identityProvider.Username, UserAgent);
            }
            _fabricQCDbSet.Update(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<FabricQualityControlModel> GetDbSet()
        {
            return _fabricQCDbSet;
        }

        public Task<int> InsertAsync(FabricQualityControlModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            foreach (var fabricGradeTest in model.FabricGradeTests)
            {
                fabricGradeTest.FlagForCreate(_identityProvider.Username, UserAgent);
            }
            _fabricQCDbSet.Add(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<FabricQualityControlModel> ReadAll()
        {
            return _fabricQCDbSet.Include(s => s.FabricGradeTests).ThenInclude(s => s.Criteria).AsNoTracking();
        }

        public IQueryable<FabricQualityControlModel> ReadAllIgnoreQueryFilter()
        {
            return _fabricQCDbSet.Include(s => s.FabricGradeTests).ThenInclude(s => s.Criteria).IgnoreQueryFilters().AsNoTracking();
        }

        public Task<FabricQualityControlModel> ReadByIdAsync(int id)
        {
            return _fabricQCDbSet.Include(s => s.FabricGradeTests).ThenInclude(s => s.Criteria).FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, FabricQualityControlModel model)
        {
            var modelToUpdate = _fabricQCDbSet.Include(s => s.FabricGradeTests).ThenInclude(s => s.Criteria).FirstOrDefault(entity => entity.Id == id);
            modelToUpdate.SetDateIm(model.DateIm, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDyeingPrintingAreaInput(model.DyeingPrintingAreaInputId, model.DyeingPrintingAreaInputBonNo, model.DyeingPrintingAreaInputProductionOrderId, 
                model.ProductionOrderNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetGroup(model.Group, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIsUsed(model.IsUsed, _identityProvider.Username, UserAgent);
            modelToUpdate.SetMachineNoIm(model.MachineNoIm, _identityProvider.Username, UserAgent);
            modelToUpdate.SetOperatorIm(model.OperatorIm, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPointLimit(model.PointLimit, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPointSystem(model.PointSystem, _identityProvider.Username, UserAgent);

            foreach (var item in modelToUpdate.FabricGradeTests)
            {
                var localModel = model.FabricGradeTests.FirstOrDefault(s => s.Id == item.Id);
                if (localModel == null)
                {
                    item.FlagForDelete(_identityProvider.Username, UserAgent);

                }
                else
                {
                    item.SetAvalLength(localModel.AvalLength, _identityProvider.Username, UserAgent);
                    item.SetFabricGradeTest(localModel.FabricGradeTest, _identityProvider.Username, UserAgent);
                    item.SetFinalArea(localModel.FinalArea, _identityProvider.Username, UserAgent);
                    item.SetFinalGradeTest(localModel.FinalGradeTest, _identityProvider.Username, UserAgent);
                    item.SetFinalLength(localModel.FinalLength, _identityProvider.Username, UserAgent);
                    item.SetFinalScore(localModel.FinalScore, _identityProvider.Username, UserAgent);
                    item.SetGrade(localModel.Grade, _identityProvider.Username, UserAgent);
                    item.SetInitLength(localModel.InitLength, _identityProvider.Username, UserAgent);
                    item.SetItemIndex(localModel.ItemIndex, _identityProvider.Username, UserAgent);
                    item.SetPcsNo(localModel.PcsNo, _identityProvider.Username, UserAgent);
                    item.SetPointLimit(localModel.PointLimit, _identityProvider.Username, UserAgent);
                    item.SetPointSystem(localModel.PointSystem, _identityProvider.Username, UserAgent);
                    item.SetSampleLength(localModel.SampleLength, _identityProvider.Username, UserAgent);
                    item.SetScore(localModel.Score, _identityProvider.Username, UserAgent);
                    item.SetType(localModel.Type, _identityProvider.Username, UserAgent);
                    item.SetWidth(localModel.Width, _identityProvider.Username, UserAgent);

                    foreach (var criteria in item.Criteria)
                    {
                        var localCriteria = localModel.Criteria.FirstOrDefault(s => s.Id == criteria.Id);

                        if (localCriteria != null)
                        {
                            criteria.SetCode(localCriteria.Code);
                            criteria.SetGroup(localCriteria.Group);
                            criteria.SetIndex(localCriteria.Index);
                            criteria.SetName(localCriteria.Name);
                            criteria.SetScoreA(localCriteria.ScoreA);
                            criteria.SetScoreB(localCriteria.ScoreB);
                            criteria.SetScoreC(localCriteria.ScoreC);
                            criteria.SetScoreD(localCriteria.ScoreD);
                        }
                        else
                        {
                            _criteriaDbSet.Remove(criteria);
                        }
                    }

                    foreach (var newCriteria in localModel.Criteria.Where(s => s.Id == 0))
                    {
                        item.Criteria.Add(newCriteria);
                    }
                }
            }

            foreach(var item in model.FabricGradeTests.Where(s => s.Id == 0))
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);
                modelToUpdate.FabricGradeTests.Add(item);
            }

            return _dbContext.SaveChangesAsync();
        }
    }
}
