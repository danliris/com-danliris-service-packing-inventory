using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.Product
{
    public class CreateProductPackAndSKUValidator : AbstractValidator<CreateProductPackAndSKUViewModel>
    {
        public CreateProductPackAndSKUValidator()
        {
            RuleFor(viewModel => viewModel.Composition).NotEmpty().When(viewModel => viewModel.ProductType?.ToUpper() == "FABRIC").WithMessage("Komposisi tidak boleh kosong!");
            RuleFor(viewModel => viewModel.Construction).NotEmpty().When(viewModel => viewModel.ProductType?.ToUpper() == "FABRIC" || viewModel.ProductType?.ToUpper() == "GREIGE").WithMessage("Konstruksi tidak boleh kosong!");
            RuleFor(viewModel => viewModel.Design).NotEmpty().When(viewModel => viewModel.ProductType?.ToUpper() == "FABRIC").WithMessage("Design tidak boleh kosong!");
            RuleFor(viewModel => viewModel.Grade).NotEmpty().When(viewModel => viewModel.ProductType?.ToUpper() == "FABRIC" || viewModel.ProductType?.ToUpper() == "GREIGE").WithMessage("Grade tidak boleh kosong!");
            RuleFor(viewModel => viewModel.LotNo).NotEmpty().When(viewModel => viewModel.ProductType?.ToUpper() == "YARN").WithMessage("No Lot tidak boleh kosong!");
            RuleFor(viewModel => viewModel.PackType).NotEmpty().When(viewModel => viewModel.ProductType?.ToUpper() == "FABRIC").WithMessage("Jenis Pack tidak boleh kosong!");
            RuleFor(viewModel => viewModel.ProductType).NotEmpty().WithMessage("Jenis Produk tidak boleh kosong!");
            RuleFor(viewModel => viewModel.Quantity).GreaterThan(0).WithMessage("Quantity harus lebih besar dari 0!");
            RuleFor(viewModel => viewModel.UOMUnit).NotEmpty().WithMessage("UOM harus diisi!");
            RuleFor(viewModel => viewModel.Width).NotEmpty().When(viewModel => viewModel.ProductType?.ToUpper() == "FABRIC" || viewModel.ProductType?.ToUpper() == "GREIGE").WithMessage("Lebar harus diisi!");
            RuleFor(viewModel => viewModel.WovenType).NotEmpty().When(viewModel => viewModel.ProductType?.ToUpper() == "GREIGE").WithMessage("Jenis Anyaman harus diisi!");
            RuleFor(viewModel => viewModel.YarnType1).NotEmpty().When(viewModel => viewModel.ProductType?.ToUpper() == "GREIGE" || viewModel.ProductType?.ToUpper() == "YARN").WithMessage("Jenis Benang harus diisi!");
            RuleFor(viewModel => viewModel.YarnType2).NotEmpty().When(viewModel => viewModel.ProductType?.ToUpper() == "GREIGE").WithMessage("Jenis Benang 2 harus diisi!");
        }
    }
}
