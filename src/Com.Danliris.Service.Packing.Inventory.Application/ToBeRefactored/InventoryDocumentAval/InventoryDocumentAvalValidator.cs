using FluentValidation;

namespace Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentAval
{
    public class InventoryDocumentAvalValidator : AbstractValidator<InventoryDocumentAvalViewModel>
    {
        public InventoryDocumentAvalValidator()
        {
            RuleFor(inventoryDocument => inventoryDocument.Id).NotNull().WithMessage("Id Tidak Valid");
            RuleFor(inventoryDocument => inventoryDocument.BonNo).NotNull().WithMessage("No. Bon Harus Diisi");
            RuleFor(inventoryDocument => inventoryDocument.Shift).NotNull().WithMessage("Shift Harus Diisi");
            RuleFor(inventoryDocument => inventoryDocument.UOMUnit).NotNull().WithMessage("Satuan Harus Diisi");
            RuleFor(inventoryDocument => inventoryDocument.ProductionOrderQuantity).NotNull().WithMessage("Qty Satuan Harus Diisi");
            RuleFor(inventoryDocument => inventoryDocument.QtyKg).NotNull().WithMessage("Qty KG Harus Diisi");
        }
    }
}
