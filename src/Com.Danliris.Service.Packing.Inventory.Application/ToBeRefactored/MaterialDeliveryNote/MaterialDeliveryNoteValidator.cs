using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNote
{
    public class MaterialDeliveryNoteValidator : AbstractValidator<MaterialDeliveryNoteViewModel>
    {
        public MaterialDeliveryNoteValidator()
        {
            RuleFor(data => data.DateSJ).Must(s => s != default(DateTimeOffset)).WithMessage("Tanggal S.J Harus Diisi!");
            RuleFor(data => data.BonCode).NotNull().WithMessage("Kode Bon Harus Diisi!");

            RuleFor(data => data.DateFrom).Must(s => s != default(DateTimeOffset)).WithMessage("Tanggal Awal Harus Diisi!");

            RuleFor(data => data.DateTo).Must(s => s != default(DateTimeOffset)).WithMessage("Tanggal Akhir Harus Diisi!");

            //.Must(s => s >= DateTimeOffset.UtcNow || ((DateTimeOffset.UtcNow - s).TotalDays <= 1 && (DateTimeOffset.UtcNow - s).TotalDays >= 0)).WithMessage("Tanggal Harus Lebih Besar atau Sama Dengan Hari Ini");

            RuleFor(data => data.DONumber).NotNull().WithMessage("No DO Harus Diisi!");
            RuleFor(data => data.SCNumber).NotNull().WithMessage("No SC Harus Diisi!");
            RuleFor(data => data.FONumber).NotNull().WithMessage("No FO Harus Diisi!");
            RuleFor(data => data.StorageNumber).NotNull().WithMessage("No Gudang Harus Diisi!");
            RuleFor(data => data.Receiver).NotNull().WithMessage("Dikirim Kepada Harus Diisi!");
            RuleFor(data => data.Sender).NotNull().WithMessage("Bagian/Pengirim Harus Diisi!");
            RuleFor(data => data.Remark).NotNull().WithMessage("Keterangan Harus Diisi!");

            RuleFor(data => data.Items).NotNull().WithMessage("SPP Harus Diisi!");

            RuleForEach(s => s.Items).ChildRules(d =>
            {
                d.RuleFor(data => data.NoSOP).NotNull().WithMessage("No SPP Harus Diisi!");
                d.RuleFor(data => data.MaterialName).NotNull().WithMessage("Nama Barang Harus Diisi!");
                d.RuleFor(data => data.InputLot).NotNull().WithMessage("Lot Harus Diisi!");

            });
        }

    }
}
