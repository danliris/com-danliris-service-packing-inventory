using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.FabricQualityControl
{
    public class FabricQualityControlValidator : AbstractValidator<FabricQualityControlViewModel>
    {
        public FabricQualityControlValidator()
        {
            RuleFor(data => data.InspectionMaterialId).NotEmpty().WithMessage("Bon No Harus Diisi!");
            When(data => data.InspectionMaterialId > 0, () =>
            {
                RuleFor(s => s.InspectionMaterialBonNo).NotNull().WithMessage("Bon No Tidak Valid!");
            });
            RuleFor(data => data.PointSystem).Must(s => s == 10 || s == 4).WithMessage("Point System harus diisi");
            When(data => data.PointSystem == 4, () =>
            {
                RuleFor(data => data.PointLimit).Must(s => s.HasValue && s > 0).WithMessage("Point Limit harus lebih besar dari 0");
            });
            RuleFor(data => data.DateIm).Must(s => s != default(DateTimeOffset)).WithMessage("Tanggal Harus Diisi!");
            RuleFor(s => s.ShiftIm).NotNull().WithMessage("Shift harus diisi!");
            RuleFor(s => s.OperatorIm).NotNull().WithMessage("Operator harus diisi!");
            RuleFor(s => s.MachineNoIm).NotNull().WithMessage("Nomor Mesin harus diisi!");

            RuleFor(s => s.FabricGradeTests).NotEmpty().WithMessage("Tabel kain harus diisi!");
            RuleForEach(s => s.FabricGradeTests).ChildRules(d =>
            {
                d.RuleFor(s => s.PcsNo).NotNull().WithMessage("Nomor Pcs harus diisi!");
                d.RuleFor(s => s.InitLength).GreaterThan(0).WithMessage("Panjang harus diisi!");
                d.RuleFor(s => s.Width).GreaterThan(0).WithMessage("Lebar kain harus lebih besar dari 0!");
                d.RuleFor(s => s.AvalLength).Must((m, i) => i <= m.InitLength).WithMessage("Panjang Aval tidak boleh lebih dari panjang kain!");
                d.RuleFor(s => s.SampleLength).Must((m, i) => i <= m.InitLength).WithMessage("Panjang Sampel tidak boleh lebih dari panjang kain!");
                d.RuleFor(s => s.AvalLength).Must((m, i) => i + m.SampleLength <= m.InitLength).WithMessage("Jumlah Panjang Aval dan Panjang Sampel tidak boleh lebih dari panjang kain!");
                d.RuleFor(s => s.SampleLength).Must((m, i) => i + m.AvalLength <= m.InitLength).WithMessage("Jumlah Panjang Aval dan Panjang Sampel tidak boleh lebih dari panjang kain!");

            });
        }
    }
}
