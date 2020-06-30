using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.InpsectionMaterial
{
    public class OutputInspectionMaterialViewModel : BaseViewModel, IValidatableObject
    {
        public OutputInspectionMaterialViewModel()
        {
            InspectionMaterialProductionOrders = new HashSet<OutputInspectionMaterialProductionOrderViewModel>();
        }

        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string DestinationArea { get; set; }
        public bool HasNextAreaDocument { get; set; }
        public string Shift { get; set; }
        public int InputInspectionMaterialId { get; set; }
        public string Group { get; set; }

        public ICollection<OutputInspectionMaterialProductionOrderViewModel> InspectionMaterialProductionOrders { get; set; }

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

            if ((Id == 0 && InspectionMaterialProductionOrders.Where(s => s.IsSave).Count() == 0) || (Id != 0 && InspectionMaterialProductionOrders.Count() == 0))
            {
                yield return new ValidationResult("SPP harus Diisi", new List<string> { "InspectionMaterialProductionOrder" });
            }
            else
            {
                foreach (var item in InspectionMaterialProductionOrders)
                {
                    DetailErrors += "{";

                    if ((item.ProductionOrderDetails.Count == 0 && item.IsSave) || (Id != 0 && item.ProductionOrderDetails.Count == 0))
                    {
                        Count++;
                        DetailErrors += "ProductionOrderDetail: 'SPP harus Diisi',";
                    }
                    else
                    {
                        DetailErrors += "ProductionOrderDetails : [ ";
                        foreach (var detail in item.ProductionOrderDetails)
                        {
                            DetailErrors += "{";
                            if (detail.Balance == 0)
                            {
                                Count++;
                                DetailErrors += "Balance: 'Qty Keluar Harus Lebih dari 0!',";
                            }

                            //if (detail.Balance > item.BalanceRemains)
                            //{
                            //    Count++;
                            //    DetailErrors += "Balance: 'Jumlah Qty Keluar tidak boleh melebihi Sisa Saldo',";
                            //}

                            if (DestinationArea == "GUDANG AVAL")
                            {
                                if (string.IsNullOrEmpty(detail.AvalType))
                                {
                                    Count++;
                                    DetailErrors += "AvalType: 'Macam Barang Tidak Boleh Kosong',";
                                }

                                
                            }
                            
                            DetailErrors += "}, ";
                        }
                        DetailErrors += "], ";
                    }
                    DetailErrors += "}, ";
                }
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "InspectionMaterialProductionOrders" });
        }
    }
}
