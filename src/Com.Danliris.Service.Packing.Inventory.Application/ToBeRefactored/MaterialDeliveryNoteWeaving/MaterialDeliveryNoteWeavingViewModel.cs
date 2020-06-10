using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Application
{
    public class MaterialDeliveryNoteWeavingViewModel : BaseViewModel, IValidatableObject
    {

        public MaterialDeliveryNoteWeavingViewModel()
        {
            ItemsMaterialDeliveryNoteWeaving = new HashSet<ItemsMaterialDeliveryNoteWeavingViewModel>();
        }

        public string Code { get; set; }
        public DateTimeOffset? DateSJ { get; set; }
        public DeliveryOrderSales selectedDO { get; set; }
        public string SendTo { get; set; }
        public Unit Unit { get; set; }
        public Buyer Buyer { get; set; }
        public string NumberBonOut { get; set; }
        public Storage Storage { get; set; }
        public string UnitLength { get; set; }
        public string UnitPacking { get; set; }
        public string Remark { get; set; }

        public ICollection<ItemsMaterialDeliveryNoteWeavingViewModel> ItemsMaterialDeliveryNoteWeaving { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateSJ == default(DateTimeOffset))
            {
                yield return new ValidationResult("Tanggal S.J harus diisi", new List<string> { "DateSJ" });
            }

            if (selectedDO == null || selectedDO.Id == 0)
            {
                yield return new ValidationResult("Nomor DO harus diisi", new List<string> { "DeliveryOrder" });
            }

            if (string.IsNullOrEmpty(SendTo))
            {
                yield return new ValidationResult("Dikirm Kepada harus diisi", new List<string> { "SendTo" });
            }

            if (Unit == null || Unit.Id == 0)
            {
                yield return new ValidationResult("Bagian harus diisi", new List<string> { "Unit" });
            }

            if (Buyer == null || Buyer.Id == 0)
            {
                yield return new ValidationResult("Nama Buyer harus diisi", new List<string> { "Buyer" });
            }

            if (string.IsNullOrWhiteSpace(NumberBonOut))
            {
                yield return new ValidationResult("Nomor harus diisi", new List<string> { "NumberBonOut" });
            }

            if (Storage == null || Storage.Id == 0)
            {
                yield return new ValidationResult("Gudang harus diisi", new List<string> { "Storage" });
            }

            if (string.IsNullOrWhiteSpace(UnitLength))
            {
                yield return new ValidationResult("Satuan Panjang harus diisi", new List<string> { "UnitLength" });
            }

            if (string.IsNullOrWhiteSpace(UnitPacking))
            {
                yield return new ValidationResult("Satuan Packing harus diisi", new List<string> { "UnitLength" });
            }

            int Count = 0;
            string DetailErrors = "[";

            if (ItemsMaterialDeliveryNoteWeaving.Count == 0)
            {
                yield return new ValidationResult("SPP harus Diisi", new List<string> { "ItemsMaterialDeliveryNoteW" });
            }
            else
            {
                foreach (var item in ItemsMaterialDeliveryNoteWeaving)
                {
                    DetailErrors += "{";

                    if (string.IsNullOrEmpty(item.ItemNoSPP))
                    {
                        Count++;
                        DetailErrors += "ItemNoSPP: 'No. SPP Harus Diisi!',";
                    }

                    if (string.IsNullOrEmpty(item.ItemMaterialName))
                    {
                        Count++;
                        DetailErrors += "ItemMaterialName: 'No. Nama Barang Harus Diisi!',";
                    }

                    if (string.IsNullOrEmpty(item.ItemDesign))
                    {
                        Count++;
                        DetailErrors += "ItemDesign: 'Nama Design Harus Diisi!',";
                    }

                    if (string.IsNullOrEmpty(item.ItemType))
                    {
                        Count++;
                        DetailErrors += "ItemType: 'Nama Jenis Harus Diisi!',";
                    }

                    if (string.IsNullOrEmpty(item.ItemCode))
                    {
                        Count++;
                        DetailErrors += "ItemCode: 'Kode Harus Diisi!',";
                    }

                    if (item.InputPacking == 0)
                    {
                        Count++;
                        DetailErrors += "InputPakcing: 'Packing Harus Diisi!',";
                    }

                    if (item.Length == 0)
                    {
                        Count++;
                        DetailErrors += "Length: 'Panjang Harus Diisi!',";
                    }

                    DetailErrors += "}, ";
                }
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "ItemsMaterialDeliveryNoteW" });
        }
    }
}