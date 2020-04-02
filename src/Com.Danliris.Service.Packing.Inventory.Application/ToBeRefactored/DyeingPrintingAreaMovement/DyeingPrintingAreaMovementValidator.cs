using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.DyeingPrintingAreaMovement
{
    public class DyeingPrintingAreaMovementValidator : AbstractValidator<DyeingPrintingAreaMovementViewModel>
    {
        public DyeingPrintingAreaMovementValidator()
        {
            RuleFor(data => data.Area).NotNull().WithMessage("Harus Memiliki Area!");
            RuleFor(data => data.Date).Must(s => s != default).WithMessage("Tanggal Harus Diisi!");
            RuleFor(data => data.Shift).NotNull().WithMessage("Shift Harus Diisi!");
            RuleFor(data => data.ProductionOrder).NotNull().WithMessage("SPP Harus Diisi!");
            When(data => data.ProductionOrder != null, () =>
            {
                RuleFor(s => s.ProductionOrder.Id).GreaterThan(0).WithMessage("SPP Tidak Valid!");
                RuleFor(s => s.ProductionOrder.No).NotNull().WithMessage("SPP Tidak Valid!");
            });
            RuleFor(data => data.CartNo).NotNull().WithMessage("No Kereta Harus Diisi!");
            RuleFor(data => data.Material).NotNull().WithMessage("Material Harus Diisi!");
            When(data => data.Material != null, () =>
            {
                RuleFor(s => s.Material.Id).GreaterThan(0).WithMessage("Material Tidak Valid!");
                RuleFor(s => s.Material.Name).NotNull().WithMessage("Konstruksi Material Tidak Valid!");
            });
            RuleFor(data => data.MaterialConstruction).NotNull().WithMessage("Konstruksi Material Harus Diisi!");
            When(data => data.MaterialConstruction != null, () =>
            {
                RuleFor(s => s.MaterialConstruction.Id).GreaterThan(0).WithMessage("Konstruksi Material Tidak Valid!");
                RuleFor(s => s.MaterialConstruction.Name).NotNull().WithMessage("Konstruksi Material Tidak Valid!");
            });
            RuleFor(data => data.MaterialWidth).NotNull().WithMessage("Lebar Material Harus Diisi!");
            RuleFor(data => data.Unit).NotNull().WithMessage("Unit Harus Diisi!");
            When(data => data.Unit != null, () =>
            {
                RuleFor(s => s.Unit.Id).GreaterThan(0).WithMessage("Unit Tidak Valid!");
                RuleFor(s => s.Unit.Name).NotNull().WithMessage("Unit Tidak Valid!");
            });
            RuleFor(data => data.Color).NotNull().WithMessage("Warna Harus Diisi!");
            RuleFor(data => data.Mutation).NotNull().WithMessage("Mutasi Harus Diisi!");
            RuleFor(data => data.UOMUnit).NotNull().WithMessage("Satuan Harus Diisi!");
        }
    }
}
