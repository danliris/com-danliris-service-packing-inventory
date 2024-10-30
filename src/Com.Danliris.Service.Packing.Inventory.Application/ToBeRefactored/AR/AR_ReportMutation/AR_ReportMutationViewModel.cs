using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.AR_ReportMutation
{
    public class AR_ReportMutationViewModel
    {

        // Informasi Utama
        public int Bulan { get; set; }
        public string NoInvoice { get; set; }
        public string KodeBuyer { get; set; }
        public DateTime TglTrucking { get; set; }
        public DateTime TglPEB { get; set; }

        // Saldo Awal
        public double SaldoAwalUS { get; set; }
        public double SaldoAwalRate { get; set; }
        public double SaldoAwalIDR { get; set; }

        // Penjualan
        public double PenjualanUS { get; set; }
        public double PenjualanRate { get; set; }
        public double PenjualanIDR { get; set; }

        // JANUARI
        public double KoreksiOmzetJanuariUSD { get; set; }
        public double KoreksiOmzetJanuariIDR { get; set; }
        public double PelunasanJanuariUSD { get; set; }
        public double PelunasanJanuariIDR { get; set; }
        public double SaldoAkhirJanuariUSD { get; set; }
        public double SaldoAkhirJanuariIDR { get; set; }

        // FEBRUARI
        public double KoreksiOmzetFebruariUSD { get; set; }
        public double KoreksiOmzetFebruariIDR { get; set; }
        public double PelunasanFebruariUSD { get; set; }
        public double PelunasanFebruariIDR { get; set; }
        public double SaldoAkhirFebruariUSD { get; set; }
        public double SaldoAkhirFebruariIDR { get; set; }

        // MARET
        public double KoreksiOmzetMaretUSD { get; set; }
        public double KoreksiOmzetMaretIDR { get; set; }
        public double PelunasanMaretUSD { get; set; }
        public double PelunasanMaretIDR { get; set; }
        public double SaldoAkhirMaretUSD { get; set; }
        public double SaldoAkhirMaretIDR { get; set; }

        // APRIL
        public double KoreksiOmzetAprilUSD { get; set; }
        public double KoreksiOmzetAprilIDR { get; set; }
        public double PelunasanAprilUSD { get; set; }
        public double PelunasanAprilIDR { get; set; }
        public double SaldoAkhirAprilUSD { get; set; }
        public double SaldoAkhirAprilIDR { get; set; }

        // MEI
        public double KoreksiOmzetMeiUSD { get; set; }
        public double KoreksiOmzetMeiIDR { get; set; }
        public double PelunasanMeiUSD { get; set; }
        public double PelunasanMeiIDR { get; set; }
        public double SaldoAkhirMeiUSD { get; set; }
        public double SaldoAkhirMeiIDR { get; set; }

        // JUNI
        public double KoreksiOmzetJuniUSD { get; set; }
        public double KoreksiOmzetJuniIDR { get; set; }
        public double PelunasanJuniUSD { get; set; }
        public double PelunasanJuniIDR { get; set; }
        public double SaldoAkhirJuniUSD { get; set; }
        public double SaldoAkhirJuniIDR { get; set; }

        // JULI
        public double KoreksiOmzetJuliUSD { get; set; }
        public double KoreksiOmzetJuliIDR { get; set; }
        public double PelunasanJuliUSD { get; set; }
        public double PelunasanJuliIDR { get; set; }
        public double SaldoAkhirJuliUSD { get; set; }
        public double SaldoAkhirJuliIDR { get; set; }

        // AGUSTUS
        public double KoreksiOmzetAgustusUSD { get; set; }
        public double KoreksiOmzetAgustusIDR { get; set; }
        public double PelunasanAgustusUSD { get; set; }
        public double PelunasanAgustusIDR { get; set; }
        public double SaldoAkhirAgustusUSD { get; set; }
        public double SaldoAkhirAgustusIDR { get; set; }

        // SEPTEMBER
        public double KoreksiOmzetSeptemberUSD { get; set; }
        public double KoreksiOmzetSeptemberIDR { get; set; }
        public double PelunasanSeptemberUSD { get; set; }
        public double PelunasanSeptemberIDR { get; set; }
        public double SaldoAkhirSeptemberUSD { get; set; }
        public double SaldoAkhirSeptemberIDR { get; set; }

        // OKTOBER
        public double KoreksiOmzetOktoberUSD { get; set; }
        public double KoreksiOmzetOktoberIDR { get; set; }
        public double PelunasanOktoberUSD { get; set; }
        public double PelunasanOktoberIDR { get; set; }
        public double SaldoAkhirOktoberUSD { get; set; }
        public double SaldoAkhirOktoberIDR { get; set; }

        // NOVEMBER
        public double KoreksiOmzetNovemberUSD { get; set; }
        public double KoreksiOmzetNovemberIDR { get; set; }
        public double PelunasanNovemberUSD { get; set; }
        public double PelunasanNovemberIDR { get; set; }
        public double SaldoAkhirNovemberUSD { get; set; }
        public double SaldoAkhirNovemberIDR { get; set; }

        // DESEMBER
        public double KoreksiOmzetDesemberUSD { get; set; }
        public double KoreksiOmzetDesemberIDR { get; set; }
        public double PelunasanDesemberUSD { get; set; }
        public double PelunasanDesemberIDR { get; set; }
        public double SaldoAkhirDesemberUSD { get; set; }
        public double SaldoAkhirDesemberIDR { get; set; }
    }

}

