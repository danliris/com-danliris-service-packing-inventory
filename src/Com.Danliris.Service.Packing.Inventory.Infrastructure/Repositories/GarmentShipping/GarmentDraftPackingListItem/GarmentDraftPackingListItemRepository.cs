using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentDraftPackingListItem;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentDraftPackingListItem
{
    public class GarmentDraftPackingListItemRepository : IGarmentDraftPackingListItemRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentDraftPackingListItemModel> _dbSet;

        public GarmentDraftPackingListItemRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentDraftPackingListItemModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }
        public IQueryable<GarmentDraftPackingListItemModel> Query => throw new NotImplementedException();

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet.Include(i => i.Details)
                        .ThenInclude(i => i.Sizes)
                        .FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, UserAgent);

            foreach (var detail in model.Details)
            {
                detail.FlagForDelete(_identityProvider.Username, UserAgent);
                foreach (var size in detail.Sizes)
                {
                    size.FlagForDelete(_identityProvider.Username, UserAgent);
                }
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentDraftPackingListItemModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);
            foreach (var detail in model.Details)
            {
                detail.FlagForCreate(_identityProvider.Username, UserAgent);
                foreach (var size in detail.Sizes)
                {
                    size.FlagForCreate(_identityProvider.Username, UserAgent);
                }
            }
            
            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentDraftPackingListItemModel> ReadAll()
        {
            return _dbSet.Include(i => i.Details).AsNoTracking();
        }

        public Task<GarmentDraftPackingListItemModel> ReadByIdAsync(int id)
        {
            return _dbSet.Include(i => i.Details)
                .ThenInclude(i => i.Sizes)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> SaveChanges()
        {
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateAsync(int id, GarmentDraftPackingListItemModel item)
        {
            var itemToUpdate = _dbSet.Include(i => i.Details)
                .ThenInclude(i => i.Sizes)
                .FirstOrDefault(s => s.Id == id);
            
            itemToUpdate.SetRONo(item.RONo, _identityProvider.Username, UserAgent);
            itemToUpdate.SetSCNo(item.SCNo, _identityProvider.Username, UserAgent);
            itemToUpdate.SetBuyerBrandId(item.BuyerBrandId, _identityProvider.Username, UserAgent);
            itemToUpdate.SetBuyerBrandName(item.BuyerBrandName, _identityProvider.Username, UserAgent);
            itemToUpdate.SetComodityId(item.ComodityId, _identityProvider.Username, UserAgent);
            itemToUpdate.SetComodityCode(item.ComodityCode, _identityProvider.Username, UserAgent);
            itemToUpdate.SetComodityName(item.ComodityName, _identityProvider.Username, UserAgent);
            itemToUpdate.SetComodityDescription(item.ComodityDescription, _identityProvider.Username, UserAgent);
            itemToUpdate.SetQuantity(item.Quantity, _identityProvider.Username, UserAgent);
            itemToUpdate.SetUomId(item.UomId, _identityProvider.Username, UserAgent);
            itemToUpdate.SetUomUnit(item.UomUnit, _identityProvider.Username, UserAgent);
            itemToUpdate.SetPriceRO(item.PriceRO, _identityProvider.Username, UserAgent);
            itemToUpdate.SetPrice(item.Price, _identityProvider.Username, UserAgent);
            itemToUpdate.SetPriceFob(item.PriceFOB, _identityProvider.Username, UserAgent);
            itemToUpdate.SetPriceCmt(item.PriceCMT, _identityProvider.Username, UserAgent);
            itemToUpdate.SetAmount(item.Amount, _identityProvider.Username, UserAgent);
            itemToUpdate.SetValas(item.Valas, _identityProvider.Username, UserAgent);
            itemToUpdate.SetUnitId(item.UnitId, _identityProvider.Username, UserAgent);
            itemToUpdate.SetUnitCode(item.UnitCode, _identityProvider.Username, UserAgent);
            itemToUpdate.SetArticle(item.Article, _identityProvider.Username, UserAgent);
            itemToUpdate.SetOrderNo(item.OrderNo, _identityProvider.Username, UserAgent);
            itemToUpdate.SetDescription(item.Description, _identityProvider.Username, UserAgent);
            itemToUpdate.SetDescriptionMd(item.DescriptionMd, _identityProvider.Username, UserAgent);
            itemToUpdate.SetRemarks(item.Remarks, _identityProvider.Username, UserAgent);

            foreach (var detailToUpdate in itemToUpdate.Details)
            {
                var detail = item.Details.FirstOrDefault(d => d.Id == detailToUpdate.Id);
                if (detail != null)
                {
                    detailToUpdate.SetCarton1(detail.Carton1, _identityProvider.Username, UserAgent);
                    detailToUpdate.SetCarton2(detail.Carton2, _identityProvider.Username, UserAgent);
                    detailToUpdate.SetColour(detail.Colour, _identityProvider.Username, UserAgent);
                    detailToUpdate.SetStyle(detail.Style, _identityProvider.Username, UserAgent);
                    detailToUpdate.SetCartonQuantity(detail.CartonQuantity, _identityProvider.Username, UserAgent);
                    detailToUpdate.SetQuantityPCS(detail.QuantityPCS, _identityProvider.Username, UserAgent);
                    detailToUpdate.SetTotalQuantity(detail.TotalQuantity, _identityProvider.Username, UserAgent);

                    detailToUpdate.SetLength(detail.Length, _identityProvider.Username, UserAgent);
                    detailToUpdate.SetWidth(detail.Width, _identityProvider.Username, UserAgent);
                    detailToUpdate.SetHeight(detail.Height, _identityProvider.Username, UserAgent);

                    detailToUpdate.SetGrossWeight(detail.GrossWeight, _identityProvider.Username, UserAgent);
                    detailToUpdate.SetNetWeight(detail.NetWeight, _identityProvider.Username, UserAgent);
                    detailToUpdate.SetNetNetWeight(detail.NetNetWeight, _identityProvider.Username, UserAgent);

                    detailToUpdate.SetIndex(detail.Index, _identityProvider.Username, UserAgent);

                    foreach (var sizeToUpdate in detailToUpdate.Sizes)
                    {
                        var size = detail.Sizes.FirstOrDefault(s => s.Id == sizeToUpdate.Id);
                        if (size != null)
                        {
                            sizeToUpdate.SetSizeId(size.SizeId, _identityProvider.Username, UserAgent);
                            sizeToUpdate.SetSize(size.Size, _identityProvider.Username, UserAgent);
                            sizeToUpdate.SetSizeIdx(size.SizeIdx, _identityProvider.Username, UserAgent);
                            sizeToUpdate.SetQuantity(size.Quantity, _identityProvider.Username, UserAgent);
                        }
                        else
                        {
                            sizeToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);
                        }
                    }

                    foreach (var size in detail.Sizes.Where(w => w.Id == 0))
                    {
                        size.FlagForCreate(_identityProvider.Username, UserAgent);
                        detailToUpdate.Sizes.Add(size);
                    }
                }
                else
                {
                    detailToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);
                }
            }

            foreach (var detail in item.Details.Where(w => w.Id == 0))
            {
                detail.FlagForCreate(_identityProvider.Username, UserAgent);
                foreach (var size in detail.Sizes)
                {
                    size.FlagForCreate(_identityProvider.Username, UserAgent);
                }
                itemToUpdate.Details.Add(detail);
            }
            
            return _dbContext.SaveChangesAsync();
        }
    }
}
