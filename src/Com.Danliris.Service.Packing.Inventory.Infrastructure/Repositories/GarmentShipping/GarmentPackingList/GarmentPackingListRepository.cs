using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListRepository : IGarmentPackingListRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentPackingListModel> _dbSet;

        public GarmentPackingListRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentPackingListModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet
                .Include(i => i.Items)
                    .ThenInclude(i => i.Details)
                        .ThenInclude(i => i.Sizes)
                .Include(i => i.Measurements)
                .FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, UserAgent);

            foreach (var item in model.Items)
            {
                item.FlagForDelete(_identityProvider.Username, UserAgent);
                foreach (var detail in item.Details)
                {
                    detail.FlagForDelete(_identityProvider.Username, UserAgent);
                    foreach (var size in detail.Sizes)
                    {
                        size.FlagForDelete(_identityProvider.Username, UserAgent);
                    }
                }
            }

            foreach (var measurement in model.Measurements)
            {
                measurement.FlagForDelete(_identityProvider.Username, UserAgent);
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentPackingListModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);

            foreach (var item in model.Items)
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);
                foreach (var detail in item.Details)
                {
                    detail.FlagForCreate(_identityProvider.Username, UserAgent);
                    foreach (var size in detail.Sizes)
                    {
                        size.FlagForCreate(_identityProvider.Username, UserAgent);
                    }
                }
            }

            foreach (var measurement in model.Measurements)
            {
                measurement.FlagForCreate(_identityProvider.Username, UserAgent);
            }

            _dbSet.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentPackingListModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public Task<GarmentPackingListModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .Include(i => i.Items)
                    .ThenInclude(i => i.Details)
                        .ThenInclude(i => i.Sizes)
                .Include(i => i.Measurements)
                .Include(i => i.StatusActivities)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentPackingListModel model)
        {
            var modelToUpdate = _dbSet
                .Include(i => i.Items)
                    .ThenInclude(i => i.Details)
                        .ThenInclude(i => i.Sizes)
                .Include(i => i.Measurements)
                .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetPackingListType(model.PackingListType, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSectionId(model.SectionId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSectionCode(model.SectionCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDate(model.Date, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPaymentTerm(model.PaymentTerm, _identityProvider.Username, UserAgent);
            modelToUpdate.SetLCNo(model.LCNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetLCDate(model.LCDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIssuedBy(model.IssuedBy, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerAgentId(model.BuyerAgentId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerAgentCode(model.BuyerAgentCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerAgentName(model.BuyerAgentName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDestination(model.Destination, _identityProvider.Username, UserAgent);
            modelToUpdate.SetShipmentMode(model.ShipmentMode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetTruckingDate(model.TruckingDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetTruckingEstimationDate(model.TruckingEstimationDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetExportEstimationDate(model.ExportEstimationDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetFabricCountryOrigin(model.FabricCountryOrigin, _identityProvider.Username, UserAgent);
            modelToUpdate.SetFabricComposition(model.FabricComposition, _identityProvider.Username, UserAgent);
            modelToUpdate.SetOmzet(model.Omzet, _identityProvider.Username, UserAgent);
            modelToUpdate.SetAccounting(model.Accounting, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRemarkMd(model.RemarkMd, _identityProvider.Username, UserAgent);

            modelToUpdate.SetGrossWeight(model.GrossWeight, _identityProvider.Username, UserAgent);
            modelToUpdate.SetNettWeight(model.NettWeight, _identityProvider.Username, UserAgent);
            modelToUpdate.SetTotalCartons(model.TotalCartons, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSayUnit(model.SayUnit, _identityProvider.Username, UserAgent);

            modelToUpdate.SetShippingMark(model.ShippingMark, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSideMark(model.SideMark, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRemark(model.Remark, _identityProvider.Username, UserAgent);

            modelToUpdate.SetStatus(model.Status, _identityProvider.Username, UserAgent);

            foreach (var itemToUpdate in modelToUpdate.Items)
            {
                var item = model.Items.FirstOrDefault(i => i.Id == itemToUpdate.Id);
                if (item != null)
                {
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
                    itemToUpdate.SetAVG_GW(item.AVG_GW, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetAVG_NW(item.AVG_NW, _identityProvider.Username, UserAgent);

                    foreach (var detailToUpdate in itemToUpdate.Details)
                    {
                        var detail = item.Details.FirstOrDefault(d => d.Id == detailToUpdate.Id);
                        if (detail != null)
                        {
                            detailToUpdate.SetCarton1(detail.Carton1, _identityProvider.Username, UserAgent);
                            detailToUpdate.SetCarton2(detail.Carton2, _identityProvider.Username, UserAgent);
                            detailToUpdate.SetColour(detail.Colour, _identityProvider.Username, UserAgent);
                            detailToUpdate.SetCartonQuantity(detail.CartonQuantity, _identityProvider.Username, UserAgent);
                            detailToUpdate.SetQuantityPCS(detail.QuantityPCS, _identityProvider.Username, UserAgent);
                            detailToUpdate.SetTotalQuantity(detail.TotalQuantity, _identityProvider.Username, UserAgent);

                            detailToUpdate.SetLength(detail.Length, _identityProvider.Username, UserAgent);
                            detailToUpdate.SetWidth(detail.Width, _identityProvider.Username, UserAgent);
                            detailToUpdate.SetHeight(detail.Height, _identityProvider.Username, UserAgent);
                            detailToUpdate.SetCartonsQuantity(detail.CartonsQuantity, _identityProvider.Username, UserAgent);

                            foreach (var sizeToUpdate in detailToUpdate.Sizes)
                            {
                                var size = detail.Sizes.FirstOrDefault(s => s.Id == sizeToUpdate.Id);
                                if (size != null)
                                {
                                    sizeToUpdate.SetSizeId(size.SizeId, _identityProvider.Username, UserAgent);
                                    sizeToUpdate.SetSize(size.Size, _identityProvider.Username, UserAgent);
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
                }
                else
                {
                    itemToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);
                }

            }

            foreach (var item in model.Items.Where(w => w.Id == 0))
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);
                foreach (var detail in item.Details)
                {
                    detail.FlagForCreate(_identityProvider.Username, UserAgent);
                    foreach (var size in detail.Sizes)
                    {
                        size.FlagForCreate(_identityProvider.Username, UserAgent);
                    }
                }
                modelToUpdate.Items.Add(item);
            }

            foreach (var measurementToUpdate in modelToUpdate.Measurements)
            {
                var measurement = model.Measurements.FirstOrDefault(m => m.Id == measurementToUpdate.Id);
                if (measurement != null)
                {
                    measurementToUpdate.SetLength(measurement.Length, _identityProvider.Username, UserAgent);
                    measurementToUpdate.SetWidth(measurement.Width, _identityProvider.Username, UserAgent);
                    measurementToUpdate.SetHeight(measurement.Height, _identityProvider.Username, UserAgent);
                    measurementToUpdate.SetCartonsQuantity(measurement.CartonsQuantity, _identityProvider.Username, UserAgent);
                }
                else
                {
                    measurementToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);
                }
            }

            foreach (var measurement in model.Measurements.Where(w => w.Id == 0))
            {
                measurement.FlagForCreate(_identityProvider.Username, UserAgent);
                modelToUpdate.Measurements.Add(measurement);
            }

            return _dbContext.SaveChangesAsync();
        }


		public IQueryable<GarmentPackingListModel> ReadNotUsedAsync()
		{
			return _dbSet
				.Where(s=>s.IsUsed == false);
		}

        public Task<GarmentPackingListModel> ReadByInvoiceNoAsync(string no)
        {
            return _dbSet
                .Include(i => i.Items)
                    .ThenInclude(i => i.Details)
                        .ThenInclude(i => i.Sizes)
                .Include(i => i.Measurements)
                .FirstOrDefaultAsync(s => s.InvoiceNo==no);
        }

        public IQueryable<GarmentPackingListModel> Query => _dbSet.AsQueryable();

        public Task<int> SaveChanges()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
