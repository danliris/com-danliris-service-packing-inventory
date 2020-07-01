using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNote
{
    public class MaterialDeliveryNoteViewModel : BaseViewModel, IValidatableObject
    {

        public MaterialDeliveryNoteViewModel()
        {
            Items = new HashSet<ItemsViewModel>();
        }

        public string Code { get; set; }
        public DateTimeOffset DateSJ { get; set; }
        public string BonCode { get; set; }
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }
        public DeliveryOrderMaterialDeliveryNoteWeaving DONumber { get; set; }
        public string FONumber { get; set; }
        public BuyerMaterialDeliveryNoteWeaving buyer { get; set; }
        public string Remark { get; set; }
        public SalesContract salesContract { get; set; }
        public UnitMaterialDeliveryNoteWeaving unit { get; set; }
        public StorageMaterialDeliveryNoteWeaving storage { get; set; }
        public ICollection<ItemsViewModel> Items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateSJ == default(DateTimeOffset))
            {
                yield return new ValidationResult("Tanggal S.J harus diisi", new List<string> { "DateSJ" });
            }

            if (string.IsNullOrEmpty(BonCode))
            {
                yield return new ValidationResult("Kode Bon harus diisi", new List<string> { "BonCode" });
            }

            if (DateFrom == default(DateTimeOffset))
            {
                yield return new ValidationResult("Tanggal Awal harus diisi", new List<string> { "DateFrom" });
            }

            if (DateTo == default(DateTimeOffset))
            {
                yield return new ValidationResult("Tanggal Akhir harus diisi", new List<string> { "DateTo" });
            }
            else
            {
                if (!(DateTo >= DateFrom || ((DateFrom - DateTo).TotalDays <= 1 && (DateFrom - DateTo).TotalDays >= 0)))
                {
                    yield return new ValidationResult("Tanggal Akhir harus lebih besar dari Tanggal Awal", new List<string> { "DateFrom" });
                }
            }
            
            if (DONumber == null || DONumber.Id == 0)
            {
                yield return new ValidationResult("Nomor DO harus diisi", new List<string> { "DONumber" });
            }

            if (salesContract == null || salesContract.SalesContractId == 0)
            {
                yield return new ValidationResult("Nomor SC harus diisi", new List<string> { "SalesContract" });
            }

            if (string.IsNullOrEmpty(FONumber))
            {
                yield return new ValidationResult("Nomor FO harus diisi", new List<string> { "FONumber" });
            }

            if (storage == null || storage.Id == 0)
            {
                yield return new ValidationResult("Nomor Gudang harus diisi", new List<string> { "Storage" });
            }

            if (buyer == null || buyer.Id == 0)
            {
                yield return new ValidationResult("Dikirim Kepada harus diisi", new List<string> { "Buyer" });
            }

            if (unit == null || unit.Id == 0)
            {
                yield return new ValidationResult("Bagian/Pengirim harus diisi", new List<string> { "Unit" });
            }

            int Count = 0;
            string DetailErrors = "[";

            if (Items.Count == 0)
            {
                yield return new ValidationResult("SOP harus Diisi", new List<string> { "Item" });
            }
            else
            {
                foreach (var item in Items)
                {
                    DetailErrors += "{";

                    if (string.IsNullOrEmpty(item.NoSOP))
                    {
                        Count++;
                        DetailErrors += "NoSOP: 'No. SOP Harus Diisi!',";
                    }

                    if (string.IsNullOrEmpty(item.MaterialName))
                    {
                        Count++;
                        DetailErrors += "MaterialName: 'Nama Barang Harus Diisi!',";
                    }

                    if (string.IsNullOrEmpty(item.InputLot))
                    {
                        Count++;
                        DetailErrors += "InputLot: 'Lot Harus Diisi!',";
                    }

                    if (item.WeightBruto <= 0)
                    {
                        Count++;
                        DetailErrors += "WeightBruto: 'Bruto Harus Lebih dari 0!',";
                    }

                    Regex r = new Regex("^[a-zA-Z]*$");

                    if (string.IsNullOrEmpty(item.WeightDOS))
                    {
                        Count++;
                        DetailErrors += "WeightDOS: 'DOS Harus Diisi!',";
                    }
                    else if (r.IsMatch(item.WeightDOS))
                    {
                        Count++;
                        DetailErrors += "WeightDOS: 'DOS Harus Angka!',";
                    }

                    if (string.IsNullOrEmpty(item.WeightCone))
                    {
                        //
                        Count++;
                        DetailErrors += "WeightCone: 'Cone Harus Diisi!',";
                    }
                    else if (r.IsMatch(item.WeightCone))
                    {
                        Count++;
                        DetailErrors += "WeightCone: 'Cone Harus Angka!',";
                    }

                    if (item.WeightBale <= 0)
                    {
                        Count++;
                        DetailErrors += "WeightBale: 'Bale Harus Lebih dari 0!',";
                    }

                    DetailErrors += "}, ";
                }

            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "Items" });
        }
    }
}