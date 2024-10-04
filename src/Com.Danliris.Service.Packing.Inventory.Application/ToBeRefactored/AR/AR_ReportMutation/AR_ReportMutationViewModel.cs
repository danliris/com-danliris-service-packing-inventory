using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.AR_ReportMutation
{
    public class AR_ReportMutationViewModel
    {

        // Informasi Utama
        public string Bulan { get; set; }
        public string NoInvoice { get; set; }
        public string KodeBuyer { get; set; }
        public DateTime? TglTrucking { get; set; }
        public DateTime? TglPEB { get; set; }

        // Saldo Awal
        public decimal SaldoAwalUS { get; set; }
        public decimal SaldoAwalRate { get; set; }
        public decimal SaldoAwalIDR { get; set; }

        // Penjualan
        public decimal PenjualanUS { get; set; }
        public decimal PenjualanRate { get; set; }
        public decimal PenjualanIDR { get; set; }

        // JANUARI
        public decimal KoreksiOmzetJanuariUSD { get; set; }
        public decimal KoreksiOmzetJanuariIDR { get; set; }
        public decimal PelunasanJanuariUSD { get; set; }
        public decimal PelunasanJanuariIDR { get; set; }
        public decimal SaldoAkhirJanuariUSD { get; set; }
        public decimal SaldoAkhirJanuariIDR { get; set; }

        // FEBRUARI
        public decimal KoreksiOmzetFebruariUSD { get; set; }
        public decimal KoreksiOmzetFebruariIDR { get; set; }
        public decimal PelunasanFebruariUSD { get; set; }
        public decimal PelunasanFebruariIDR { get; set; }
        public decimal SaldoAkhirFebruariUSD { get; set; }
        public decimal SaldoAkhirFebruariIDR { get; set; }

        // MARET
        public decimal KoreksiOmzetMaretUSD { get; set; }
        public decimal KoreksiOmzetMaretIDR { get; set; }
        public decimal PelunasanMaretUSD { get; set; }
        public decimal PelunasanMaretIDR { get; set; }
        public decimal SaldoAkhirMaretUSD { get; set; }
        public decimal SaldoAkhirMaretIDR { get; set; }

        // APRIL
        public decimal KoreksiOmzetAprilUSD { get; set; }
        public decimal KoreksiOmzetAprilIDR { get; set; }
        public decimal PelunasanAprilUSD { get; set; }
        public decimal PelunasanAprilIDR { get; set; }
        public decimal SaldoAkhirAprilUSD { get; set; }
        public decimal SaldoAkhirAprilIDR { get; set; }

        // MEI
        public decimal KoreksiOmzetMeiUSD { get; set; }
        public decimal KoreksiOmzetMeiIDR { get; set; }
        public decimal PelunasanMeiUSD { get; set; }
        public decimal PelunasanMeiIDR { get; set; }
        public decimal SaldoAkhirMeiUSD { get; set; }
        public decimal SaldoAkhirMeiIDR { get; set; }

        // JUNI
        public decimal KoreksiOmzetJuniUSD { get; set; }
        public decimal KoreksiOmzetJuniIDR { get; set; }
        public decimal PelunasanJuniUSD { get; set; }
        public decimal PelunasanJuniIDR { get; set; }
        public decimal SaldoAkhirJuniUSD { get; set; }
        public decimal SaldoAkhirJuniIDR { get; set; }

        // JULI
        public decimal KoreksiOmzetJuliUSD { get; set; }
        public decimal KoreksiOmzetJuliIDR { get; set; }
        public decimal PelunasanJuliUSD { get; set; }
        public decimal PelunasanJuliIDR { get; set; }
        public decimal SaldoAkhirJuliUSD { get; set; }
        public decimal SaldoAkhirJuliIDR { get; set; }

        // AGUSTUS
        public decimal KoreksiOmzetAgustusUSD { get; set; }
        public decimal KoreksiOmzetAgustusIDR { get; set; }
        public decimal PelunasanAgustusUSD { get; set; }
        public decimal PelunasanAgustusIDR { get; set; }
        public decimal SaldoAkhirAgustusUSD { get; set; }
        public decimal SaldoAkhirAgustusIDR { get; set; }

        // SEPTEMBER
        public decimal KoreksiOmzetSeptemberUSD { get; set; }
        public decimal KoreksiOmzetSeptemberIDR { get; set; }
        public decimal PelunasanSeptemberUSD { get; set; }
        public decimal PelunasanSeptemberIDR { get; set; }
        public decimal SaldoAkhirSeptemberUSD { get; set; }
        public decimal SaldoAkhirSeptemberIDR { get; set; }

        // OKTOBER
        public decimal KoreksiOmzetOktoberUSD { get; set; }
        public decimal KoreksiOmzetOktoberIDR { get; set; }
        public decimal PelunasanOktoberUSD { get; set; }
        public decimal PelunasanOktoberIDR { get; set; }
        public decimal SaldoAkhirOktoberUSD { get; set; }
        public decimal SaldoAkhirOktoberIDR { get; set; }

        // NOVEMBER
        public decimal KoreksiOmzetNovemberUSD { get; set; }
        public decimal KoreksiOmzetNovemberIDR { get; set; }
        public decimal PelunasanNovemberUSD { get; set; }
        public decimal PelunasanNovemberIDR { get; set; }
        public decimal SaldoAkhirNovemberUSD { get; set; }
        public decimal SaldoAkhirNovemberIDR { get; set; }

        // DESEMBER
        public decimal KoreksiOmzetDesemberUSD { get; set; }
        public decimal KoreksiOmzetDesemberIDR { get; set; }
        public decimal PelunasanDesemberUSD { get; set; }
        public decimal PelunasanDesemberIDR { get; set; }
        public decimal SaldoAkhirDesemberUSD { get; set; }
        public decimal SaldoAkhirDesemberIDR { get; set; }
    }

}

