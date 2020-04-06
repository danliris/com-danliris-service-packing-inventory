using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.InspectionDocumentReport
{
    public class FilterInspectionDocumentReportValidator : AbstractValidator<FilterInspectionDocumentReport>
    {
        public FilterInspectionDocumentReportValidator()
        {
            RuleFor(data => data.DateReport).NotNull().WithMessage("Date Harus Diisi");
            RuleFor(data => data.Group).NotNull().WithMessage("Group Harus Diisi");
            RuleFor(data => data.Keterangan).NotNull().WithMessage("Keterangan Harus Diisi");
            RuleFor(data => data.Mutasi).NotNull().WithMessage("Mutasi Harus Diisi");
            RuleFor(data => data.Zona).NotNull().WithMessage("Zona Harus Diisi");
        }
    }
}
