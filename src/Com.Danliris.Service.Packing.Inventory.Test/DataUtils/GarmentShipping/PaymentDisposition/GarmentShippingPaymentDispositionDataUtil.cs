using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.PaymentDisposition;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.PaymentDisposition;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.PaymentDisposition
{
    public class GarmentShippingPaymentDispositionDataUtil : BaseDataUtil<GarmentShippingPaymentDispositionRepository, GarmentShippingPaymentDispositionModel>
    {
        public GarmentShippingPaymentDispositionDataUtil(GarmentShippingPaymentDispositionRepository repository) : base(repository)
        {
        }

        public override GarmentShippingPaymentDispositionModel GetModel()
        {
            var bills = new HashSet<GarmentShippingPaymentDispositionBillDetailModel> { new GarmentShippingPaymentDispositionBillDetailModel("",1) };
            var units = new HashSet<GarmentShippingPaymentDispositionUnitChargeModel> { new GarmentShippingPaymentDispositionUnitChargeModel(1, "",1,1) };
            var invoices = new HashSet<GarmentShippingPaymentDispositionInvoiceDetailModel> { new GarmentShippingPaymentDispositionInvoiceDetailModel("",1,1,1,1,1,1,1) };
            var model = new GarmentShippingPaymentDispositionModel("", "", "", "", "", 1, "", "", "", 1, "", "", 1, "", "", 1, "", "", "", "", "", DateTimeOffset.Now, "", 1, 1, 1, "", 1, 1, 1, DateTimeOffset.Now, "", "", true, "", "", DateTimeOffset.Now, "", invoices, bills, units);

            return model;
        }

        public override GarmentShippingPaymentDispositionModel GetEmptyModel()
        {
            var bills = new HashSet<GarmentShippingPaymentDispositionBillDetailModel> { new GarmentShippingPaymentDispositionBillDetailModel(null, 0) };
            var units = new HashSet<GarmentShippingPaymentDispositionUnitChargeModel> { new GarmentShippingPaymentDispositionUnitChargeModel(0, null, 0, 0) };
            var invoices = new HashSet<GarmentShippingPaymentDispositionInvoiceDetailModel> { new GarmentShippingPaymentDispositionInvoiceDetailModel(null, 0, 0, 0, 0, 0, 0, 0) };
            var model = new GarmentShippingPaymentDispositionModel(null, null, null, null, null, 0, null, null, null, 0, null, null, 0, null, null, 0, null, null, null, null, null, DateTimeOffset.MinValue, null, 0, 0, 0, null, 0, 0, 0, DateTimeOffset.MinValue, null, null, true, null, null, DateTimeOffset.MinValue, null, invoices, bills, units);

            return model;
        }

        public GarmentShippingPaymentDispositionModel CopyModel(GarmentShippingPaymentDispositionModel om)
        {
            var bills = new HashSet<GarmentShippingPaymentDispositionBillDetailModel>();
            foreach (var i in om.BillDetails)
            {
                bills.Add(new GarmentShippingPaymentDispositionBillDetailModel(i.BillDescription, i.Amount) { Id = i.Id });
            }
            var units = new HashSet<GarmentShippingPaymentDispositionUnitChargeModel>();
            foreach (var x in om.UnitCharges)
            {
                units.Add(new GarmentShippingPaymentDispositionUnitChargeModel(x.UnitId,x.UnitCode,x.AmountPercentage,x.BillAmount) { Id = x.Id });
            }

            var invoices = new HashSet<GarmentShippingPaymentDispositionInvoiceDetailModel>();
            foreach (var y in om.InvoiceDetails)
            {
                invoices.Add(new GarmentShippingPaymentDispositionInvoiceDetailModel(y.InvoiceNo,y.InvoiceId,y.Quantity,y.Amount,y.Volume,y.GrossWeight,y.ChargeableWeight,y.TotalCarton) { Id = y.Id });
            }
            var model = new GarmentShippingPaymentDispositionModel(om.DispositionNo, om.PaymentType, om.PaymentMethod, om.PaidAt, om.SendBy,
                om.BuyerAgentId, om.BuyerAgentCode, om.BuyerAgentName, om.PaymentTerm, om.ForwarderId, om.ForwarderCode, om.ForwarderName,
                om.CourierId, om.CourierCode, om.CourierName, om.EMKLId, om.EMKLCode, om.EMKLName, om.Address, om.NPWP, om.InvoiceNumber, om.InvoiceDate,
                om.InvoiceTaxNumber, om.BillValue, om.VatValue, om.IncomeTaxId, om.IncomeTaxName, (decimal)om.IncomeTaxRate, om.IncomeTaxValue,
                om.TotalBill, om.PaymentDate, om.Bank, om.AccNo, om.IsFreightCharged, om.FreightBy, om.FreightNo, om.FreightDate, om.Remark, invoices, bills, units) { Id = om.Id };

            return model;
        }
    }
}
