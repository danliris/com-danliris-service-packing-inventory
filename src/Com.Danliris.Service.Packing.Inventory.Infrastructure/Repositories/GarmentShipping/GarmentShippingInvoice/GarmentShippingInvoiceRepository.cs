using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.EntityFrameworkCore;
using System;
using Com.Moonlay.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInvoice
{
	public class GarmentShippingInvoiceRepository : IGarmentShippingInvoiceRepository
	{
		private const string USER_AGENT = "Repository";
		private readonly PackingInventoryDbContext _dbContext;
		private readonly DbSet<GarmentShippingInvoiceModel> _garmentshippingInvoiceDbSet;
		private readonly DbSet<GarmentPackingListModel> _garmentpackingListDbSet;
		private readonly IIdentityProvider _identityProvider;
		public GarmentShippingInvoiceRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
		{
			_dbContext = dbContext;
			_garmentshippingInvoiceDbSet = dbContext.Set<GarmentShippingInvoiceModel>();
			_garmentpackingListDbSet = dbContext.Set<GarmentPackingListModel>();
			_identityProvider = serviceProvider.GetService<IIdentityProvider>();
		}

		public Task<int> DeleteAsync(int id)
		{
			var model = _garmentshippingInvoiceDbSet
				.Include(i => i.Items)
				.Include(i => i.GarmentShippingInvoiceAdjustment)
				.FirstOrDefault(s => s.Id == id);

			model.FlagForDelete(_identityProvider.Username, USER_AGENT);
			var packingListModel = _garmentpackingListDbSet.FirstOrDefault(entity => entity.Id == model.PackingListId);
			packingListModel.SetIsUsed(false, _identityProvider.Username, USER_AGENT);

			foreach (var item in model.Items)
			{
				item.FlagForDelete(_identityProvider.Username, USER_AGENT);
				
			}

			foreach (var ajd in model.GarmentShippingInvoiceAdjustment)
			{
				ajd.FlagForDelete(_identityProvider.Username, USER_AGENT);
			}

            foreach (var unit in model.GarmentShippingInvoiceUnit)
            {
                unit.FlagForDelete(_identityProvider.Username, USER_AGENT);
            }

            return _dbContext.SaveChangesAsync();
		}
		public Task<int> InsertAsync(GarmentShippingInvoiceModel model)
		{
			model.FlagForCreate(_identityProvider.Username, USER_AGENT);
			var packingListModel = _garmentpackingListDbSet.FirstOrDefault(entity => entity.Id == model.PackingListId);
			packingListModel.SetIsUsed(true, _identityProvider.Username, USER_AGENT);
			foreach (var item in model.Items)
			{
				item.FlagForCreate(_identityProvider.Username, USER_AGENT);

			}

			foreach (var adjustment in model.GarmentShippingInvoiceAdjustment)
			{
				adjustment.FlagForCreate(_identityProvider.Username, USER_AGENT);
			}

            foreach (var unit in model.GarmentShippingInvoiceUnit)
            {
                unit.FlagForCreate(_identityProvider.Username, USER_AGENT);
            }

            _garmentshippingInvoiceDbSet.Add(model);

			return _dbContext.SaveChangesAsync();

		}

		public IQueryable<GarmentShippingInvoiceModel> ReadAll()
		{
			return _garmentshippingInvoiceDbSet.AsNoTracking();
		}

		public Task<GarmentShippingInvoiceModel> ReadByIdAsync(int id)
		{
			return _garmentshippingInvoiceDbSet
				.Include(i => i.Items)
				.Include(i=>i.GarmentShippingInvoiceUnit)	
				.Include(i => i.GarmentShippingInvoiceAdjustment)
				.FirstOrDefaultAsync(s => s.Id == id);
		}

		public Task<int> UpdateAsync(int id, GarmentShippingInvoiceModel model)
		{
			var modelToUpdate = _garmentshippingInvoiceDbSet
				.Include(i => i.Items)
                .Include(i => i.GarmentShippingInvoiceUnit)
                .Include(i => i.GarmentShippingInvoiceAdjustment)
				.FirstOrDefault(s => s.Id == id);

			modelToUpdate.SetFrom(model.From, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetTo(model.To, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetConsignee(model.Consignee, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetShippingPer(model.ShippingPer, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetSailingDate(model.SailingDate, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetConfirmationOfOrderNo(model.ConfirmationOfOrderNo, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetShippingStaffId(model.ShippingStaffId, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetShippingStaff(model.ShippingStaff, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetFabricTypeId(model.FabricTypeId, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetFabricType(model.FabricType, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetBankAccountId(model.BankAccountId, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetBankAccount(model.BankAccount, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetPaymentDue(model.PaymentDue, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetPEBNo(model.PEBNo, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetPEBDate(model.PEBDate, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetNPENo(model.NPENo, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetNPEDate(model.NPEDate, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetBL(model.BL, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetBLDate(model.BLDate, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetCO(model.CO, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetCODate(model.CODate, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetCOTP(model.COTP, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetCOTPDate(model.COTPDate, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetDescription(model.Description, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetRemark(model.Remark, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetCPrice(model.CPrice, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetAmountToBePaid(model.AmountToBePaid, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetAmountCA(model.AmountCA, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetMemo(model.Memo, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetTotalAmount(model.TotalAmount, _identityProvider.Username, USER_AGENT);
            modelToUpdate.SetConsigneeAddress(model.ConsigneeAddress, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetDeliverTo(model.DeliverTo, _identityProvider.Username, USER_AGENT);
			modelToUpdate.SetInvoiceDate(model.InvoiceDate, _identityProvider.Username, USER_AGENT);


			foreach (var itemToUpdate in modelToUpdate.Items)
			{
				var item = model.Items.FirstOrDefault(i => i.Id == itemToUpdate.Id);
				if (item != null)
				{
					itemToUpdate.SetQuantity(item.Quantity, _identityProvider.Username, USER_AGENT);
					itemToUpdate.SetPrice(item.Price, _identityProvider.Username, USER_AGENT);
					itemToUpdate.SetAmount(item.Amount, _identityProvider.Username, USER_AGENT);
					itemToUpdate.SetUomUnit(item.UomUnit, _identityProvider.Username, USER_AGENT);
					itemToUpdate.SetUomId(item.UomId, _identityProvider.Username, USER_AGENT);
					itemToUpdate.SetCMTPrice(item.CMTPrice, _identityProvider.Username, USER_AGENT);
					itemToUpdate.SetComodityDesc(item.ComodityDesc, _identityProvider.Username, USER_AGENT);
                    itemToUpdate.SetDesc2(item.Desc2, _identityProvider.Username, USER_AGENT);
                    itemToUpdate.SetDesc3(item.Desc3, _identityProvider.Username, USER_AGENT);
                    itemToUpdate.SetDesc4(item.Desc4, _identityProvider.Username, USER_AGENT);

                }
                else
				{
					itemToUpdate.FlagForDelete(_identityProvider.Username, USER_AGENT);

				}
			}
			foreach (var items in model.Items.Where(w => w.Id == 0))
			{
				modelToUpdate.Items.Add(items);

				items.FlagForCreate(_identityProvider.Username, USER_AGENT);
			}
			foreach (var adjToUpdate in modelToUpdate.GarmentShippingInvoiceAdjustment)
			{
				var adj = model.GarmentShippingInvoiceAdjustment.FirstOrDefault(i => i.Id == adjToUpdate.Id);
				if (adj != null)
				{
					adjToUpdate.SetAdjustmentDescription(adj.AdjustmentDescription, _identityProvider.Username, USER_AGENT);
					adjToUpdate.SetAdjustmentValue(adj.AdjustmentValue, _identityProvider.Username, USER_AGENT);
                    adjToUpdate.SetAdditionalChargesId(adj.AdditionalChargesId, _identityProvider.Username, USER_AGENT);
                }
                else
				{
					adjToUpdate.FlagForDelete(_identityProvider.Username, USER_AGENT);
				}
			}

            foreach (var unitToUpdate in modelToUpdate.GarmentShippingInvoiceUnit)
            {
                var unit = model.GarmentShippingInvoiceUnit.FirstOrDefault(i => i.Id == unitToUpdate.Id);
                if (unit != null)
                {
                    unitToUpdate.SetAmountPercentage(unit.AmountPercentage, _identityProvider.Username, USER_AGENT);
                    unitToUpdate.SetQuantityPercentage(unit.QuantityPercentage, _identityProvider.Username, USER_AGENT);
                    unitToUpdate.SetUnitCode(unit.UnitCode, _identityProvider.Username, USER_AGENT);
                    unitToUpdate.SetUnitId(unit.UnitId, _identityProvider.Username, USER_AGENT);
                }
                else
                {
                    unitToUpdate.FlagForDelete(_identityProvider.Username, USER_AGENT);
                }
            }

            foreach (var items in model.GarmentShippingInvoiceAdjustment.Where(w => w.Id == 0))
			{
				modelToUpdate.GarmentShippingInvoiceAdjustment.Add(items);
			}

            foreach (var unit in model.GarmentShippingInvoiceUnit.Where(w => w.Id == 0))
            {
                modelToUpdate.GarmentShippingInvoiceUnit.Add(unit);
            }


            return _dbContext.SaveChangesAsync();
		}
	}
}
