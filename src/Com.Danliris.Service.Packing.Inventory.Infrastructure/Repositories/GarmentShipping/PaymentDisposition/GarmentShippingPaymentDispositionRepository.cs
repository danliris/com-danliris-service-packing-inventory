using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDisposition
{
    public class GarmentShippingPaymentDispositionRepository : IGarmentShippingPaymentDispositionRepository
    {
        private const string UserAgent = "Repository";
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityProvider;
        private readonly DbSet<GarmentShippingPaymentDispositionModel> _dbSet;
        private readonly DbSet<GarmentShippingPaymentDispositionUnitChargeModel> _dbSetUnit;
        private readonly DbSet<GarmentShippingPaymentDispositionInvoiceDetailModel> _dbSetInv;

        public GarmentShippingPaymentDispositionRepository(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<GarmentShippingPaymentDispositionModel>();
            _dbSetUnit = dbContext.Set<GarmentShippingPaymentDispositionUnitChargeModel>();
            _dbSetInv = dbContext.Set<GarmentShippingPaymentDispositionInvoiceDetailModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public Task<int> DeleteAsync(int id)
        {
            var model = _dbSet
                .Include(i => i.BillDetails)
                .Include(i => i.InvoiceDetails)
                .Include(i => i.UnitCharges)
                .Include(i => i.PaymentDetails)
                .FirstOrDefault(s => s.Id == id);

            model.FlagForDelete(_identityProvider.Username, UserAgent);

            foreach (var unitCharge in model.UnitCharges)
            {
                unitCharge.FlagForDelete(_identityProvider.Username, UserAgent);
            }

            foreach (var bill in model.BillDetails)
            {
                bill.FlagForDelete(_identityProvider.Username, UserAgent);
            }

            foreach (var invoice in model.InvoiceDetails)
            {
                invoice.FlagForDelete(_identityProvider.Username, UserAgent);
            }

            foreach (var payment in model.PaymentDetails)
            {
                payment.FlagForDelete(_identityProvider.Username, UserAgent);
            }

            return _dbContext.SaveChangesAsync();
        }

        public Task<int> InsertAsync(GarmentShippingPaymentDispositionModel model)
        {
            model.FlagForCreate(_identityProvider.Username, UserAgent);

            foreach (var unitCharge in model.UnitCharges)
            {
                unitCharge.FlagForCreate(_identityProvider.Username, UserAgent);
            }

            foreach (var bill in model.BillDetails)
            {
                bill.FlagForCreate(_identityProvider.Username, UserAgent);
            }

            foreach (var invoice in model.InvoiceDetails)
            {
                invoice.FlagForCreate(_identityProvider.Username, UserAgent);
            }

            foreach (var payment in model.PaymentDetails)
            {
                payment.FlagForCreate(_identityProvider.Username, UserAgent);
            }

            _dbSet.Add(model);
            return _dbContext.SaveChangesAsync();
        }

        public IQueryable<GarmentShippingPaymentDispositionModel> ReadAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<GarmentShippingPaymentDispositionUnitChargeModel> ReadUnitAll()
        {
            return _dbSetUnit.AsNoTracking();
        }

        public IQueryable<GarmentShippingPaymentDispositionInvoiceDetailModel> ReadInvAll()
        {
            return _dbSetInv.AsNoTracking();
        }

        public Task<GarmentShippingPaymentDispositionModel> ReadByIdAsync(int id)
        {
            return _dbSet
                .Include(i => i.BillDetails)
                .Include(i => i.InvoiceDetails)
                .Include(i => i.UnitCharges)
                .Include(i => i.PaymentDetails)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> UpdateAsync(int id, GarmentShippingPaymentDispositionModel model)
        {
            var modelToUpdate = _dbSet
                .Include(i => i.BillDetails)
                .Include(i => i.InvoiceDetails)
                .Include(i => i.UnitCharges)
                .Include(i => i.PaymentDetails)
                .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetAccNo(model.AccNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBank(model.Bank, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBillValue(model.BillValue, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerAgentCode(model.BuyerAgentCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerAgentId(model.BuyerAgentId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetBuyerAgentName(model.BuyerAgentName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetAddress(model.Address, _identityProvider.Username, UserAgent);
            modelToUpdate.SetForwarderCode(model.ForwarderCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetForwarderId(model.ForwarderId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetInvoiceDate(model.InvoiceDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetInvoiceNumber(model.InvoiceNumber, _identityProvider.Username, UserAgent);
            modelToUpdate.SetInvoiceTaxNumber(model.InvoiceTaxNumber, _identityProvider.Username, UserAgent);
            modelToUpdate.SetForwarderName(model.ForwarderName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetNPWP(model.NPWP, _identityProvider.Username, UserAgent);
            modelToUpdate.SetFreightBy(model.FreightBy, _identityProvider.Username, UserAgent);
            modelToUpdate.SetFreightDate(model.FreightDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetFreightNo(model.FreightNo, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIncomeIncomeTaxRate(model.IncomeTaxRate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIncomeTaxId(model.IncomeTaxId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIncomeTaxName(model.IncomeTaxName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIncomeTaxValue(model.IncomeTaxValue, _identityProvider.Username, UserAgent);
            modelToUpdate.SetIsFreightCharged(model.IsFreightCharged, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPaymentDate(model.PaymentDate, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPaymentMethod(model.PaymentMethod, _identityProvider.Username, UserAgent);
            modelToUpdate.SetPaymentTerm(model.PaymentTerm, _identityProvider.Username, UserAgent);
            modelToUpdate.SetRemark(model.Remark, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSendBy(model.SendBy, _identityProvider.Username, UserAgent);
            modelToUpdate.SetTotalBill(model.TotalBill, _identityProvider.Username, UserAgent);
            modelToUpdate.SetEMKLCode(model.EMKLCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetEMKLName(model.EMKLName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetWareHouseId(model.WareHouseId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetWareHouseCode(model.WareHouseCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetWareHouseName(model.WareHouseName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetEMKLId(model.EMKLId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetCourierCode(model.CourierCode, _identityProvider.Username, UserAgent);
            modelToUpdate.SetCourierId(model.CourierId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetCourierName(model.CourierName, _identityProvider.Username, UserAgent);
            modelToUpdate.SetDestination(model.Destination, _identityProvider.Username, UserAgent);
            modelToUpdate.SetFlightVessel(model.FlightVessel, _identityProvider.Username, UserAgent);
            modelToUpdate.SetVatValue(model.VatValue, _identityProvider.Username, UserAgent);

            foreach (var billToUpdate in modelToUpdate.BillDetails)
            {
                var bill = model.BillDetails.FirstOrDefault(i => i.Id == billToUpdate.Id);
                if (bill != null)
                {
                    billToUpdate.SetAmount(bill.Amount, _identityProvider.Username, UserAgent);
                    billToUpdate.SetBillDescription(bill.BillDescription, _identityProvider.Username, UserAgent);
                    
                }
                else
                {
                    billToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);
                }

            }

            foreach (var bill in model.BillDetails.Where(w => w.Id == 0))
            {
                modelToUpdate.BillDetails.Add(bill);
            }

            foreach (var unitToUpdate in modelToUpdate.UnitCharges)
            {
                var unit = model.UnitCharges.FirstOrDefault(i => i.Id == unitToUpdate.Id);
                if (unit != null)
                {
                    unitToUpdate.SetAmountPercentage(unit.AmountPercentage, _identityProvider.Username, UserAgent);
                    unitToUpdate.SetBillAmount(unit.BillAmount, _identityProvider.Username, UserAgent);
                    unitToUpdate.SetUnitCode(unit.UnitCode, _identityProvider.Username, UserAgent);
                    unitToUpdate.SetUnitId(unit.UnitId, _identityProvider.Username, UserAgent);
                }
                else
                {
                    unitToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);
                }

            }

            foreach (var unit in model.UnitCharges.Where(w => w.Id == 0))
            {
                modelToUpdate.UnitCharges.Add(unit);
            }

            foreach (var invoiceToUpdate in modelToUpdate.InvoiceDetails)
            {
                var invoice = model.InvoiceDetails.FirstOrDefault(i => i.Id == invoiceToUpdate.Id);
                if (invoice != null)
                {
                    invoiceToUpdate.SetAmount(invoice.Amount, _identityProvider.Username, UserAgent);
                    invoiceToUpdate.SetChargeableWeight(invoice.ChargeableWeight, _identityProvider.Username, UserAgent);
                    invoiceToUpdate.SetGrossWeight(invoice.GrossWeight, _identityProvider.Username, UserAgent);
                    invoiceToUpdate.SetQuantity(invoice.Quantity, _identityProvider.Username, UserAgent);
                    invoiceToUpdate.SetInvoiceId(invoice.InvoiceId, _identityProvider.Username, UserAgent);
                    invoiceToUpdate.SetInvoiceNo(invoice.InvoiceNo, _identityProvider.Username, UserAgent);
                    invoiceToUpdate.SetTotalCarton(invoice.TotalCarton, _identityProvider.Username, UserAgent);
                    invoiceToUpdate.SetVolume(invoice.Volume, _identityProvider.Username, UserAgent);

                }
                else
                {
                    invoiceToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);
                }

            }

            foreach (var invoice in model.InvoiceDetails.Where(w => w.Id == 0))
            {
                modelToUpdate.InvoiceDetails.Add(invoice);
            }

            //
            foreach (var paymentToUpdate in modelToUpdate.PaymentDetails)
            {
                var payment = model.PaymentDetails.FirstOrDefault(i => i.Id == paymentToUpdate.Id);
                if (payment != null)
                {
                    paymentToUpdate.SetPaymentDate(payment.PaymentDate, _identityProvider.Username, UserAgent);
                    paymentToUpdate.SetPaymentDescription(payment.PaymentDescription, _identityProvider.Username, UserAgent);
                    paymentToUpdate.SetAmount(payment.Amount, _identityProvider.Username, UserAgent);
                }
                else
                {
                    paymentToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);
                }

            }

            foreach (var payment in model.PaymentDetails.Where(w => w.Id == 0))
            {
                modelToUpdate.PaymentDetails.Add(payment);
            }


            return _dbContext.SaveChangesAsync();
        }
    }
}
