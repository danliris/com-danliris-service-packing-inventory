using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval
{
    public class OutputAvalViewModel : BaseViewModel, IValidatableObject
    {
        public OutputAvalViewModel()
        {
            AvalItems = new HashSet<OutputAvalItemViewModel>();
        }

        public string Area { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Type { get; set; }
        public string DestinationArea { get; set; }
        public string Shift { get; set; }
        public string BonNo { get; set; }
        public string Group { get; set; }
        public int DeliveryOrdeSalesId { get; set; }
        public string DeliveryOrderSalesNo { get; set; }
        public bool HasNextAreaDocument { get; set; }
        public ICollection<OutputAvalItemViewModel> AvalItems { get; set; }
        public List<OutputAvalDyeingPrintingAreaMovementIdsViewModel> DyeingPrintingMovementIds { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Area))
                yield return new ValidationResult("Area harus diisi", new List<string> { "Area" });

            if (string.IsNullOrEmpty(Type))
                yield return new ValidationResult("Jenis harus diisi", new List<string> { "Type" });

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

            if (string.IsNullOrEmpty(Shift))
                yield return new ValidationResult("Shift harus diisi", new List<string> { "Shift" });

            if (string.IsNullOrEmpty(Group))
                yield return new ValidationResult("Group harus diisi", new List<string> { "Group" });

            if (Type == "OUT" && string.IsNullOrEmpty(DestinationArea))
                yield return new ValidationResult("Tujuan Area Harus Diisi!", new List<string> { "DestinationArea" });

            int Count = 0;
            string DetailErrors = "[";

            if (Type == "OUT")
            {
                if (AvalItems.Count == 0)
                {
                    yield return new ValidationResult("Aval Item harus Diisi", new List<string> { "AvalItem" });
                }
                else
                {
                    foreach (var item in AvalItems)
                    {
                        DetailErrors += "{";

                        if(item.AvalOutQuantity == 0)
                        {
                            Count++;
                            DetailErrors += "AvalOutQuantity: 'Harus Memiliki Qty Kg!',";

                        }

                        if (item.AvalOutSatuan == 0)
                        {
                            Count++;
                            DetailErrors += "AvalOutSatuan: 'Harus Memiliki Qty Satuan!',";

                        }


                        DetailErrors += "}, ";
                    }
                }
            }
            else
            {
                if (AvalItems.Count == 0)
                {
                    yield return new ValidationResult("Aval Item harus Diisi", new List<string> { "AvalItem" });
                }
                else
                {
                    var items = AvalItems.Where(e => e.AvalQuantity != 0 && e.AvalQuantityKg != 0);

                    if (!(items.All(d => d.AvalQuantity > 0 && d.AvalQuantityKg > 0) || items.All(d => d.AvalQuantity < 0 && d.AvalQuantityKg < 0)))
                    {
                        yield return new ValidationResult("Quantity Keluar Satuan dan Qty Keluar Berat harus Positif semua atau Negatif Semua", new List<string> { "AvalItem" });
                    }

                    foreach (var item in AvalItems)
                    {
                        DetailErrors += "{";

                        if (string.IsNullOrEmpty(item.AvalType))
                        {
                            Count++;
                            DetailErrors += "AvalType: 'Nama Barang Harus Diisi!',";
                        }

                        if (item.AvalQuantity == 0)
                        {
                            Count++;
                            DetailErrors += "AvalQuantity: 'Qty Keluar Satuan Tidak Boleh sama dengan 0!',";
                        }

                        if (item.AvalQuantityKg == 0)
                        {
                            Count++;
                            DetailErrors += "AvalQuantityKg: 'Qty Keluar Berat Tidak Boleh sama dengan 0!',";
                        }

                        if (string.IsNullOrEmpty(item.AdjDocumentNo))
                        {
                            Count++;
                            DetailErrors += "AdjDocumentNo: 'No Dokumen Harus Diisi!',";
                        }

                        DetailErrors += "}, ";
                    }
                }
            }



            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "AvalItems" });
        }
    }
}
