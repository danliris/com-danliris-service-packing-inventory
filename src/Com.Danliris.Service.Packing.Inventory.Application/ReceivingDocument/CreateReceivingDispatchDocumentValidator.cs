using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ReceivingDocument
{
    public class CreateReceivingDispatchDocumentValidator : AbstractValidator<CreateReceivingDispatchDocumentViewModel>
    {
        public CreateReceivingDispatchDocumentValidator()
        {
            RuleFor(inventoryDocument => inventoryDocument.Storage).NotNull().WithMessage("Gudang harus diisi!");
            When(inventoryDocument => inventoryDocument.Storage != null, () =>
            {
                RuleFor(inventoryDocument => inventoryDocument.Storage.Id).GreaterThan(0).WithMessage("Gudang tidak valid!");
            });

            RuleFor(inventoryDocument => inventoryDocument.Items).NotNull().NotEmpty().WithMessage("Item harus diisi!");
            RuleForEach(inventoryDocument => inventoryDocument.Items).ChildRules(items =>
            {
                items.RuleFor(item => item.Code).NotEmpty().WithMessage("Kode harus diisi!");
                items.RuleFor(item => item.Quantity.GetValueOrDefault()).GreaterThan(0).WithMessage("Kuantitas harus lebih besar dari 0!");
                items.RuleFor(item => item.SKUId.GetValueOrDefault()).GreaterThan(0).WithMessage("SKU harus diisi!");
                items.RuleFor(item => item.UOMUnit).NotEmpty().WithMessage("Satuan harus diisi!");
            });
        }
    }
}
