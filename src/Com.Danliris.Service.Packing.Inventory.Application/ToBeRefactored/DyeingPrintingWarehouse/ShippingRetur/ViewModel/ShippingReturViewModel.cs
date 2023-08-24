using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.ShippingRetur.ViewModel
{
    
    public class ShippingReturViewModel : BaseViewModel, IValidatableObject
    {
        
        //private readonly IDPShippingReturService _service;
        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string ShippingType { get; set; }
        public string Shift { get; set; }
        public bool ReturType { get; set; }
        public ICollection<ShippingReturItemViewModel> ShippingProductionOrders { get; set; }


        //public ShippingReturViewModel(IDPShippingReturService service)
        //{
        //    _service = service;
        //}
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Date == default(DateTimeOffset))
            {
                yield return new ValidationResult("Tanggal harus diisi", new List<string> { "Date" });
            }
            else
            {
                if (Id == 0 && !(Date >= DateTimeOffset.UtcNow || ((DateTimeOffset.UtcNow - Date).TotalDays <= 1 && (DateTimeOffset.UtcNow - Date).TotalDays >= 0)))
                {
                    yield return new ValidationResult("Tanggal Harus Lebih Besar atau Sama Dengan Hari Ini", new List<string> { "Date" });
                }
            }
            int Count = 0;
            string DetailErrors = "[";

            if (ShippingProductionOrders.Count == 0)
            {
                yield return new ValidationResult("Detail Harus Diisi", new List<string> { "ShippingProductionOrders" });
            }
            else
            {
                foreach (var item in ShippingProductionOrders)
                {
                    DetailErrors += "{";
                    if (ReturType)
                    {
                        if (string.IsNullOrEmpty(item.ProductPackingCode))
                        {
                            Count++;
                            DetailErrors += "ProductPackingCode: 'Barcode Harus Diisi!',";
                        }
                    }
                    //if (ReturType) {
                    //    var barcode = _service.CheckBarcode(item.ProductPackingCode);
                    //    if (barcode == 0)
                    //    {
                    //        Count++;
                    //        DetailErrors += "ProductPackingCode: 'No Kereta Harus Diisi!',";
                    //    }
                    //}

                    if (string.IsNullOrEmpty(item.DeliveryOrderReturNo))
                    {
                        Count++;
                        DetailErrors += "DeliveryOrderReturNo: 'DO Retur Harus Diisi!',";
                    }

                    if (item.ProductionOrder == null || item.ProductionOrder.Id == 0)
                    {
                        Count++;
                        DetailErrors += "ProductionOrder: 'SPP Harus Diisi!',";
                    }

                    if (item.InputQuantity == 0)
                    {
                        Count++;
                        DetailErrors += "InputQuantity: 'Qty Terima Harus Lebih dari 0!',";
                    }

                    if (item.InputQtyPacking <= 0)
                    {
                        Count++;
                        DetailErrors += "InputQtyPacking: 'Qty Packing Terima Harus Lebih dari 0!',";
                    }

                    if (item.PackingLength == 0)
                    {
                        Count++;
                        DetailErrors += "PackingLength: 'Qty Packing Terima Harus Lebih dari 0!',";
                    }

                    DetailErrors += "}, ";
                }
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "ShippingProductionOrders" });

        }
    }
}
