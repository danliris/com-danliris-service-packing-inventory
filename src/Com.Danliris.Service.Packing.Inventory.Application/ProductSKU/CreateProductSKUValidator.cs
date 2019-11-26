using System;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Com.Danliris.Service.Packing.Inventory.Application.ProductSKU
{
    public class CreateProductSKUValidator : AbstractValidator<CreateProductSKUViewModel>
    {
        public CreateProductSKUValidator()
        {
            RuleFor(viewModel => viewModel.Composition).Must((viewModel, composition) => ValidateComposition(viewModel, composition)).WithMessage("Komposisi harus diisi!");
            RuleFor(viewModel => viewModel.Construction).Must((viewModel, construction) => ValidateConstruction(viewModel, construction)).WithMessage("Konstruksi harus diisi!");
            RuleFor(viewModel => viewModel.Currency).Must(currency => currency != null && currency.Id.GetValueOrDefault() > 0).WithMessage("Mata Uang harus diisi atau tidak valid!");
            RuleFor(viewModel => viewModel.Design).Must((viewModel, design) => ValidateDesign(viewModel, design)).WithMessage("Design harus diisi!");
            RuleFor(viewModel => viewModel.Grade).Must((viewModel, grade) => ValidateGrade(viewModel, grade)).WithMessage("Grade harus diisi!");
            RuleFor(viewModel => viewModel.Lot).Must((viewModel, lot) => ValidateLot(viewModel, lot)).WithMessage("Nomor Lot harus diisi!");
            RuleFor(viewModel => viewModel.Name).NotEmpty().WithMessage("Nama harus diisi!");
            RuleFor(viewModel => viewModel.ProductType).NotNull().WithMessage("Jenis Barang harus diisi!").NotEmpty().WithMessage("Jenis Barang harus diisi!");
            RuleFor(viewModel => viewModel.UOM).Must(uom => uom != null && uom.Id.GetValueOrDefault() > 0).WithMessage("Satuan harus diisi atau tidak valid!");
            RuleFor(viewModel => viewModel.Width).Must((viewModel, width) => ValidateWidth(viewModel, width)).WithMessage("Lebar harus diisi!");
            RuleFor(viewModel => viewModel.WovenType).Must((viewModel, wovenType) => ValidateWovenType(viewModel, wovenType)).WithMessage("Jenis Anyaman harus diisi!");
            RuleFor(viewModel => viewModel.YarnType1).Must((viewModel, yarnType1) => ValidateYarnType1(viewModel, yarnType1)).WithMessage("Jenis Benang 1 harus diisi!");
            RuleFor(viewModel => viewModel.YarnType2).Must((viewModel, yarnType2) => ValidateYarnType2(viewModel, yarnType2)).WithMessage("Jenis Benang 2 harus diisi!");
        }

        private bool ValidateComposition(CreateProductSKUViewModel viewModel, string composition)
        {
            if(!string.IsNullOrWhiteSpace(viewModel.ProductType) && viewModel.ProductType.ToUpper() == "FABRIC")
            {
                return !string.IsNullOrWhiteSpace(composition);
            }

            return string.IsNullOrWhiteSpace(composition);
        }

        private bool ValidateConstruction(CreateProductSKUViewModel viewModel, string construction)
        {
            if(!string.IsNullOrWhiteSpace(viewModel.ProductType) && (viewModel.ProductType.ToUpper() == "FABRIC" || viewModel.ProductType.ToUpper() == "GREIGE"))
            {
                return !string.IsNullOrWhiteSpace(construction);
            }

            return string.IsNullOrWhiteSpace(construction);
        }

        private bool ValidateDesign(CreateProductSKUViewModel viewModel, string design)
        {
            if(!string.IsNullOrWhiteSpace(viewModel.ProductType) && viewModel.ProductType.ToUpper() == "FABRIC")
            {
                return !string.IsNullOrWhiteSpace(design);
            }

            return string.IsNullOrWhiteSpace(design);
        }

        private bool ValidateGrade(CreateProductSKUViewModel viewModel, string grade)
        {
            if(!string.IsNullOrWhiteSpace(viewModel.ProductType) && (viewModel.ProductType.ToUpper() == "FABRIC" || viewModel.ProductType.ToUpper() == "GREIGE"))
            {
                return !string.IsNullOrWhiteSpace(grade);
            }

            return string.IsNullOrWhiteSpace(grade);
        }

        private bool ValidateLot(CreateProductSKUViewModel viewModel, string lot)
        {
            if(!string.IsNullOrWhiteSpace(viewModel.ProductType) && viewModel.ProductType.ToUpper() == "BENANG")
            {
                return !string.IsNullOrWhiteSpace(lot);
            }

            return string.IsNullOrWhiteSpace(lot);
        }

        private bool ValidateWidth(CreateProductSKUViewModel viewModel, string width)
        {
            if(!string.IsNullOrWhiteSpace(viewModel.ProductType) && (viewModel.ProductType.ToUpper() == "FABRIC" || viewModel.ProductType.ToUpper() == "GREIGE"))
            {
                return !string.IsNullOrWhiteSpace(width);
            }

            return string.IsNullOrWhiteSpace(width);
        }

        private bool ValidateWovenType(CreateProductSKUViewModel viewModel, string wovenType)
        {
            if(!string.IsNullOrWhiteSpace(viewModel.ProductType) && viewModel.ProductType.ToUpper() == "GREIGE")
            {
                return !string.IsNullOrWhiteSpace(wovenType);
            }

            return string.IsNullOrWhiteSpace(wovenType);
        }

        private bool ValidateYarnType1(CreateProductSKUViewModel viewModel, string yarnType1)
        {
            if(!string.IsNullOrWhiteSpace(viewModel.ProductType) && (viewModel.ProductType.ToUpper() == "BENANG" || viewModel.ProductType.ToUpper() == "GREIGE"))
            {
                return !string.IsNullOrWhiteSpace(yarnType1);
            }

            return string.IsNullOrWhiteSpace(yarnType1);
        }

        private bool ValidateYarnType2(CreateProductSKUViewModel viewModel, string yarnType2)
        {
            if(!string.IsNullOrWhiteSpace(viewModel.ProductType) && viewModel.ProductType.ToUpper() == "BENANG")
            {
                return !string.IsNullOrWhiteSpace(yarnType2);
            }

            return string.IsNullOrWhiteSpace(yarnType2);
        }
    }
}