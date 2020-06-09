﻿using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Transit
{
    public class OutputTransitViewModel : BaseViewModel, IValidatableObject
    {
        public OutputTransitViewModel()
        {
            TransitProductionOrders = new HashSet<OutputTransitProductionOrderViewModel>();
        }

        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string DestinationArea { get; set; }
        public bool HasNextAreaDocument { get; set; }
        public string Shift { get; set; }
        public int InputTransitId { get; set; }
        public string Group { get; set; }
        public ICollection<OutputTransitProductionOrderViewModel> TransitProductionOrders { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Area))
                yield return new ValidationResult("Area harus diisi", new List<string> { "Area" });

            if (Date == default(DateTimeOffset))
            {
                yield return new ValidationResult("Tanggal harus diisi", new List<string> { "Date" });
            }
            else
            {
                if (!(Date >= DateTimeOffset.UtcNow || ((DateTimeOffset.UtcNow - Date).TotalDays <= 1 && (DateTimeOffset.UtcNow - Date).TotalDays >= 0)))
                {
                    yield return new ValidationResult("Tanggal Harus Lebih Besar atau Sama Dengan Hari Ini", new List<string> { "Date" });
                }
            }

            if (string.IsNullOrEmpty(Shift))
                yield return new ValidationResult("Shift harus diisi", new List<string> { "Shift" });

            if (string.IsNullOrEmpty(Group))
                yield return new ValidationResult("Group harus diisi", new List<string> { "Group" });

            if (string.IsNullOrEmpty(DestinationArea))
                yield return new ValidationResult("Tujuan Area Harus Diisi!", new List<string> { "DestinationArea" });

            int Count = 0;
            string DetailErrors = "[";

            if ((Id == 0 && TransitProductionOrders.Where(s => s.IsSave).Count() == 0) || (Id != 0 && TransitProductionOrders.Count() == 0))
            {
                yield return new ValidationResult("SPP harus Diisi", new List<string> { "TransitProductionOrder" });
            }
            else
            {
                foreach (var item in TransitProductionOrders)
                {
                    DetailErrors += "{";

                    if (item.IsSave)
                    {
                        if (item.Balance == 0)
                        {
                            Count++;
                            DetailErrors += "Balance: 'Qty Terima Harus Lebih dari 0!',";
                        }

                        if (item.Balance > item.BalanceRemains)
                        {
                            Count++;
                            DetailErrors += "Balance: 'Jumlah Qty Keluar tidak boleh melebihi Sisa Saldo',";
                        }
                    }


                    DetailErrors += "}, ";
                }
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "TransitProductionOrders" });
        }
    }
}
