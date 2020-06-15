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
        public DeliveryOrderMaterialDeliveryNoteWeaving selectedDO { get; set; }
        public string SendTo { get; set; }
        public UnitMaterialDeliveryNoteWeaving Unit { get; set; }
        public BuyerMaterialDeliveryNoteWeaving Buyer { get; set; }
        public string NumberBonOut { get; set; }
        public StorageMaterialDeliveryNoteWeaving Storage { get; set; }
        public string Remark { get; set; }

        public ICollection<ItemsMaterialDeliveryNoteWeavingViewModel> ItemsMaterialDeliveryNoteWeaving { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //if (DateSJ== null || DateSJ == default(DateTimeOffset))
            if(!DateSJ.HasValue)
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

            int Count = 0;
            string DetailErrors = "[";

            if (ItemsMaterialDeliveryNoteWeaving.Count == 0)
            {
                yield return new ValidationResult("SPP harus Diisi", new List<string> { "ItemsMaterialDeliveryNoteWeaving" });
            }
            else
            {
                foreach (var item in ItemsMaterialDeliveryNoteWeaving)
                {
                    DetailErrors += "{";

                    if (string.IsNullOrEmpty(item.itemNoSOP))
                    {
                        Count++;
                        DetailErrors += "ItemNoSOP: 'No. SOP Harus Diisi!',";
                    }

                    if (string.IsNullOrEmpty(item.itemMaterialName))
                    {
                        Count++;
                        DetailErrors += "ItemMaterialName: 'Nama Barang Harus Diisi!',";
                    }

                    if (string.IsNullOrEmpty(item.itemGrade))
                    {
                        Count++;
                        DetailErrors += "ItemGrade: 'Grade Harus Diisi!',";
                    }

                    if (string.IsNullOrEmpty(item.itemType))
                    {
                        Count++;
                        DetailErrors += "ItemType: 'Jenis Harus Diisi!',";
                    }

                    if (string.IsNullOrEmpty(item.itemCode))
                    {
                        Count++;
                        DetailErrors += "ItemCode: 'Kode Harus Diisi!',";
                    }

                    if (!item.inputBale.HasValue || item.inputBale <= 0)
                    {
                        Count++;
                        DetailErrors += "InputBale: 'Bale Harus Diisi!',";
                    }

                    if (!item.inputPiece.HasValue || item.inputPiece <= 0)
                    {
                        Count++;
                        DetailErrors += "InputPiece: 'Piece Harus Diisi!',";
                    }

                    if (!item.inputMeter.HasValue || item.inputMeter <= 0)
                    {
                        Count++;
                        DetailErrors += "InputMeter: 'Meter Harus Diisi!',";
                    }

                    if (!item.inputKg.HasValue || item.inputKg <= 0)
                    {
                        Count++;
                        DetailErrors += "InputKg: 'Kg Harus Diisi!',";
                    }

                    DetailErrors += "}, ";
                }
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "ItemsMaterialDeliveryNoteWeaving" });
        }
    }
}