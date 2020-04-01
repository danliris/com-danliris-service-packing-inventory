using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ProductPacking
{
    public class ProductPackingFormValidator : AbstractValidator<ProductPackingFormViewModel>
    {
        public ProductPackingFormValidator()
        {
            RuleFor(viewModel => viewModel.PackingType).NotEmpty().WithMessage("Jenis Pack tidak boleh kosong!");
            RuleFor(viewModel => viewModel.Quantity.GetValueOrDefault()).GreaterThan(0).WithMessage("Kuantiti harus lebih besar dari 0!");
            RuleFor(viewModel => viewModel.SKUId.GetValueOrDefault()).GreaterThan(0).WithMessage("SKU harus diisi!");
        }
    }
}
