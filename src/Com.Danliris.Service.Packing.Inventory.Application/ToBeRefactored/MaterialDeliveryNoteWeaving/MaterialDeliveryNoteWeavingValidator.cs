using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNoteWeaving
{
    public class MaterialDeliveryNoteWeavingValidator : AbstractValidator<MaterialDeliveryNoteWeavingViewModel>
    {
        public MaterialDeliveryNoteWeavingValidator()
        {
            RuleFor(data => data.DateSJ).Must(s => s != default).WithMessage("Tanggal Harus Diisi!");
            RuleFor(data => data.SendTo).NotNull().WithMessage("Harus Memiliki Penerima!");
            RuleFor(data => data.selectedDO).Must(s => s.Id > 0);
            RuleFor(data => data.Unit).Must(s => s.Id > 0);
            RuleFor(data => data.Buyer).Must(s => s.Id > 0);
            RuleFor(data => data.NumberBonOut).NotNull().WithMessage("Nomor Harus Diisi!");
            RuleFor(data => data.Storage).Must(s => s.Id > 0);
            RuleForEach(data => data.ItemsMaterialDeliveryNoteWeaving).SetValidator(new ItemsMaterialDeliveryNoteWeavingValidator());
            RuleFor(data => data.ItemsMaterialDeliveryNoteWeaving).Must(s => s.Count > 0);
        }
    }
}
