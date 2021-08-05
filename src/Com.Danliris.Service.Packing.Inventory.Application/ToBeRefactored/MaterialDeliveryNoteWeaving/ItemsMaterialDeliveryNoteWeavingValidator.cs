using FluentValidation;
using FluentValidation.Validators;

namespace Com.Danliris.Service.Packing.Inventory.Application
{
    public class ItemsMaterialDeliveryNoteWeavingValidator : AbstractValidator<ItemsMaterialDeliveryNoteWeavingViewModel>
    {
        public ItemsMaterialDeliveryNoteWeavingValidator()
        {
            RuleFor(data => data.itemNoSOP).NotNull().WithMessage("Harus Memiliki No SOP!");
            RuleFor(data => data.itemMaterialName).NotNull().WithMessage("Harus Memiliki Nama Barang!");
            RuleFor(data => data.itemGrade).NotNull().WithMessage("Harus Memiliki Grade!");
            RuleFor(data => data.itemType).NotNull().WithMessage("Harus Memiliki Jenis!");
            RuleFor(data => data.itemCode).NotNull().WithMessage("Harus Memiliki Kode!");
            RuleFor(data => data.inputBale).NotEmpty().WithMessage("Harus Memiliki Bale!");
            RuleFor(data => data.inputPiece).NotEmpty().WithMessage("Harus Memiliki Piece!");
            RuleFor(data => data.inputMeter).NotEmpty().WithMessage("Harus Memiliki Meter!");
            RuleFor(data => data.inputKg).NotEmpty().WithMessage("Harus Memiliki Kg!");
        }
    }
}