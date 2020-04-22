using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Packaging
{
    public class OutputPackagingValidator : AbstractValidator<OutputPackagingViewModel>
    {
        public OutputPackagingValidator()
        {
            RuleFor(data => data.Area).NotNull().WithMessage("Harus Memiliki Area!");
            RuleFor(data => data.Date).Must(s => s != default(DateTimeOffset)).WithMessage("Tanggal Harus Diisi!");
            RuleFor(data => data.DestinationArea).NotNull().WithMessage("Tujuan Area Harus Diisi!");
            
            RuleForEach(data => data.PackagingProductionOrders).ChildRules(d =>
            {
                d.RuleFor(item => item.PackagingQTY).NotNull().WithMessage("QTY Tidak Boleh Kosong!");
                d.RuleFor(item => item.PackagingType).NotNull().WithMessage("Jenis Packaging Tidak Boleh Kosong !");
                d.RuleFor(item => item.PackagingUnit).NotNull().WithMessage("Unit Packaging Tidak Boleh Kosong !");
            });

        }
    }
}
