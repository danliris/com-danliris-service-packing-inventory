using FluentValidation;

namespace Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentSKU
{
    public class CreateInventoryDocumentSKUValidator : AbstractValidator<CreateInventoryDocumentSKUViewModel>
    {
        public CreateInventoryDocumentSKUValidator()
        {
            RuleFor(inventoryDocument => inventoryDocument.ReferenceNo).NotNull().WithMessage("Nomor Referensi harus diisi!");
            RuleFor(inventoryDocument => inventoryDocument.ReferenceType).NotNull().WithMessage("Jenis Referensi harus diisi!");
            RuleFor(inventoryDocument => inventoryDocument.Storage).NotNull().WithMessage("Gudang harus diisi!");
            When(inventoryDocument => inventoryDocument.Storage != null, () =>
            {
                RuleFor(inventoryDocument => inventoryDocument.Storage.Id).GreaterThan(0).WithMessage("Gudang tidak valid!");
            });
            RuleFor(inventoryDocument => inventoryDocument.Type).NotNull().WithMessage("Jenis Dokumen harus diisi!");

            RuleFor(inventoryDocument => inventoryDocument.Items).NotNull().NotEmpty().WithMessage("Item harus diisi!");
            RuleForEach(inventoryDocument => inventoryDocument.Items).ChildRules(items =>
            {
                items.RuleFor(item => item.Quantity.GetValueOrDefault()).GreaterThan(0).WithMessage("Kuantitas harus lebih besar dari 0!");
                items.RuleFor(item => item.SKUId.GetValueOrDefault()).GreaterThan(0).WithMessage("SKU harus diisi!");
                items.RuleFor(item => item.UOMUnit).NotEmpty().WithMessage("Satuan harus diisi!");
            });
        }
    }
}
