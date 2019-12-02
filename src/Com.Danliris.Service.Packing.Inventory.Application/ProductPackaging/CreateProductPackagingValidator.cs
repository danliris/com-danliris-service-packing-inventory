using FluentValidation;

namespace Com.Danliris.Service.Packing.Inventory.Application.ProductPackaging
{
    public class CreateProductSKUValidator : AbstractValidator<CreateProductPackagingViewModel>
    {
        public CreateProductSKUValidator()
        {
            RuleFor(viewModel => viewModel.Currency).Must(currency => currency != null && currency.Id.GetValueOrDefault() > 0).WithMessage("Mata Uang harus diisi atau tidak valid!");
            RuleFor(viewModel => viewModel.Quantity).GreaterThan(0).WithMessage("Kuantiti harus lebih dari 0!");
            RuleFor(viewModel => viewModel.SKU).Must(sku => sku != null && sku.Id.GetValueOrDefault() > 0).WithMessage("SKU harus diisi atau tidak valid");
            RuleFor(viewModel => viewModel.UOM).Must(uom => uom != null && uom.Id.GetValueOrDefault() > 0).WithMessage("Satuan harus diisi atau tidak valid!");
        }

        // private bool ValidateComposition(CreateProductPackagingViewModel viewModel, string composition)
        // {
        //     if(!string.IsNullOrWhiteSpace(viewModel.ProductType) && viewModel.ProductType.ToUpper() == "FABRIC")
        //     {
        //         return !string.IsNullOrWhiteSpace(composition);
        //     }

        //     return string.IsNullOrWhiteSpace(composition);
        // }

        // private bool ValidateConstruction(CreateProductPackagingViewModel viewModel, string construction)
        // {
        //     if(!string.IsNullOrWhiteSpace(viewModel.ProductType) && (viewModel.ProductType.ToUpper() == "FABRIC" || viewModel.ProductType.ToUpper() == "GREIGE"))
        //     {
        //         return !string.IsNullOrWhiteSpace(construction);
        //     }

        //     return string.IsNullOrWhiteSpace(construction);
        // }

        // private bool ValidateDesign(CreateProductPackagingViewModel viewModel, string design)
        // {
        //     if(!string.IsNullOrWhiteSpace(viewModel.ProductType) && viewModel.ProductType.ToUpper() == "FABRIC")
        //     {
        //         return !string.IsNullOrWhiteSpace(design);
        //     }

        //     return string.IsNullOrWhiteSpace(design);
        // }

        // private bool ValidateGrade(CreateProductPackagingViewModel viewModel, string grade)
        // {
        //     if(!string.IsNullOrWhiteSpace(viewModel.ProductType) && (viewModel.ProductType.ToUpper() == "FABRIC" || viewModel.ProductType.ToUpper() == "GREIGE"))
        //     {
        //         return !string.IsNullOrWhiteSpace(grade);
        //     }

        //     return string.IsNullOrWhiteSpace(grade);
        // }

        // private bool ValidateLot(CreateProductPackagingViewModel viewModel, string lot)
        // {
        //     if(!string.IsNullOrWhiteSpace(viewModel.ProductType) && viewModel.ProductType.ToUpper() == "BENANG")
        //     {
        //         return !string.IsNullOrWhiteSpace(lot);
        //     }

        //     return string.IsNullOrWhiteSpace(lot);
        // }

        // private bool ValidateWidth(CreateProductPackagingViewModel viewModel, string width)
        // {
        //     if(!string.IsNullOrWhiteSpace(viewModel.ProductType) && (viewModel.ProductType.ToUpper() == "FABRIC" || viewModel.ProductType.ToUpper() == "GREIGE"))
        //     {
        //         return !string.IsNullOrWhiteSpace(width);
        //     }

        //     return string.IsNullOrWhiteSpace(width);
        // }

        // private bool ValidateWovenType(CreateProductPackagingViewModel viewModel, string wovenType)
        // {
        //     if(!string.IsNullOrWhiteSpace(viewModel.ProductType) && viewModel.ProductType.ToUpper() == "GREIGE")
        //     {
        //         return !string.IsNullOrWhiteSpace(wovenType);
        //     }

        //     return string.IsNullOrWhiteSpace(wovenType);
        // }

        // private bool ValidateYarnType1(CreateProductPackagingViewModel viewModel, string yarnType1)
        // {
        //     if(!string.IsNullOrWhiteSpace(viewModel.ProductType) && (viewModel.ProductType.ToUpper() == "BENANG" || viewModel.ProductType.ToUpper() == "GREIGE"))
        //     {
        //         return !string.IsNullOrWhiteSpace(yarnType1);
        //     }

        //     return string.IsNullOrWhiteSpace(yarnType1);
        // }

        // private bool ValidateYarnType2(CreateProductPackagingViewModel viewModel, string yarnType2)
        // {
        //     if(!string.IsNullOrWhiteSpace(viewModel.ProductType) && viewModel.ProductType.ToUpper() == "BENANG")
        //     {
        //         return !string.IsNullOrWhiteSpace(yarnType2);
        //     }

        //     return string.IsNullOrWhiteSpace(yarnType2);
        // }
    }
}