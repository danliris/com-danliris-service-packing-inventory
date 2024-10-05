using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.AR_ReportMutation
{
    public class AR_ReportMutationService : IAR_ReportMutationService
    {
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityService;
        private readonly List<string> _alphabets;
        private const string UserAgent = "finance-service";

        public AR_ReportMutationService(PackingInventoryDbContext dbContext, IIdentityProvider identityService)
        {
            _dbContext = dbContext;
            _identityService = identityService;

        }

        public async Task<List<AR_ReportMutationViewModel>> GetQuery(DateTime dateFrom, DateTime dateTo)
        {
            // calculate balance
            var balance = _dbContext.AR_Balances.AsNoTracking()
                .Where(s => s.TruckingDate < dateFrom)
                .Select(s => new AR_ReportMutationViewModel
                {
                    KodeBuyer = s.BuyerAgentCode,
                    NoInvoice = s.InvoiceNo,
                    TglTrucking = s.TruckingDate,
                    TglPEB = s.PEBDate,
                    SaldoAwalRate = s.Rate,
                    SaldoAwalUS = s.Amount,
                    SaldoAwalIDR = s.AmountIDR,
                    Bulan = s.Month
                })
                .GroupBy(x => new { x.KodeBuyer, x.NoInvoice, x.TglTrucking, x.TglPEB, x.Bulan,x.SaldoAwalRate })
                .Select(s => new AR_ReportMutationViewModel
                {
                    KodeBuyer = s.Key.KodeBuyer,
                    NoInvoice = s.Key.NoInvoice,
                    TglTrucking = s.Key.TglTrucking,
                    TglPEB = s.Key.TglPEB,
                    SaldoAwalRate = s.Key.SaldoAwalRate,
                    SaldoAwalUS = Math.Round(s.Sum(x => x.SaldoAwalUS),2),
                    SaldoAwalIDR = Math.Round(s.Sum(x => x.SaldoAwalIDR),2),
                    Bulan = s.Key.Bulan
                })
                .ToList();

            // calculate penjualan
            var penjualan = _dbContext.AR_RecapOmzet.AsNoTracking()
                .Where(s => s.TruckingDate >= dateFrom && s.TruckingDate <= dateTo)
                .Select(s => new AR_ReportMutationViewModel
                {
                    KodeBuyer = s.BuyerAgentCode,
                    NoInvoice = s.InvoiceNo,
                    TglTrucking = s.TruckingDate,
                    TglPEB = s.PEBDate,
                    PenjualanRate = s.Rate,
                    PenjualanUS = s.Amount,
                    PenjualanIDR = s.AmountIDR,
                    Bulan = s.Month
                })
                .GroupBy(x => new { x.KodeBuyer, x.NoInvoice, x.TglTrucking, x.TglPEB, x.Bulan,x.PenjualanRate })
                .Select(s => new AR_ReportMutationViewModel
                {
                    KodeBuyer = s.Key.KodeBuyer,
                    NoInvoice = s.Key.NoInvoice,
                    TglTrucking = s.Key.TglTrucking,
                    TglPEB = s.Key.TglPEB,
                    PenjualanRate = s.Key.PenjualanRate,
                    PenjualanUS = Math.Round(s.Sum(x => x.PenjualanUS),2),
                    PenjualanIDR = Math.Round(s.Sum(x => x.PenjualanIDR),2),
                    Bulan = s.Key.Bulan
                })
                .ToList();

            // Union balance and penjualan
            var queryUnion = balance.Union(penjualan)
                .GroupBy(x => new { x.KodeBuyer, x.NoInvoice, x.TglTrucking, x.TglPEB, x.Bulan })
                .Select(s => new AR_ReportMutationViewModel
                {
                    KodeBuyer = s.Key.KodeBuyer,
                    NoInvoice = s.Key.NoInvoice,
                    TglTrucking = s.Key.TglTrucking,
                    TglPEB = s.Key.TglPEB,
                    SaldoAwalRate = Math.Round(s.Sum(x => x.SaldoAwalRate), 2),
                    SaldoAwalUS = Math.Round(s.Sum(x => x.SaldoAwalUS), 2),
                    SaldoAwalIDR = Math.Round(s.Sum(x => x.SaldoAwalIDR), 2),
                    PenjualanRate = Math.Round(s.Sum(x => x.PenjualanRate), 2),
                    PenjualanUS = Math.Round(s.Sum(x => x.PenjualanUS), 2),
                    PenjualanIDR = Math.Round(s.Sum(x => x.PenjualanIDR), 2),
                    Bulan = s.Key.Bulan
                })
                .ToList();

            // Get all invoice number
            var listInvoice = queryUnion.Select(s => s.NoInvoice).ToHashSet();

            // calculate koreksi omzet by invoice number
            var koreksiOmzet = _dbContext.AR_OmzetCorrections.AsNoTracking()
                .Where(s => listInvoice.Contains(s.InvoiceNo))
                .Select(s => new AR_TempToCalculate
                {
                    NoInvoice = s.InvoiceNo,
                    Amount = s.Amount,
                    Rate = s.Kurs,
                    TotalAmount = s.TotalAmount,
                    Bulan = s.Month
                })
                .GroupBy(x => new {  x.NoInvoice, x.Bulan,x.Rate })
                .Select(s => new AR_TempToCalculate
                {
                    NoInvoice = s.Key.NoInvoice,
                    Bulan = s.Key.Bulan,
                    Rate = s.Key.Rate,
                    TotalAmount = Math.Round(s.Sum(x => x.TotalAmount), 2),
                    Amount = Math.Round(s.Sum(x => x.Amount), 2),
                })
                .ToList();

            //calculate cash in bank by invoice number
            var cashInBank = _dbContext.AR_CashInBank.AsNoTracking()
                .Where(s => listInvoice.Contains(s.InvoiceNo))
                .Select(s => new AR_TempToCalculate
                {
                    NoInvoice = s.InvoiceNo,
                    Amount = s.LiquidAmount,
                    TotalAmount = s.LiquidTotalAmount - s.DifferenceKurs,
                    Bulan = s.Month
                })
                .GroupBy(x => new { x.NoInvoice, x.Bulan })
                .Select(s => new AR_TempToCalculate
                {
                    NoInvoice = s.Key.NoInvoice,
                    Bulan = s.Key.Bulan,
                    Rate = 0,
                    TotalAmount = Math.Round(s.Sum(x => x.TotalAmount), 2),
                    Amount = Math.Round(s.Sum(x => x.Amount), 2),
                })
                .ToList();

            //calculate cmt by invoice number
            var cmt = _dbContext.AR_CMTs.AsNoTracking()
                .Where(s => listInvoice.Contains(s.InvoiceNo))
                .Select(s => new AR_TempToCalculate
                {
                    NoInvoice = s.InvoiceNo,
                    Amount = s.Amount,
                    Rate = s.Kurs,
                    TotalAmount = s.TotalAmount,
                    Bulan = s.Month
                })
                .GroupBy(x => new { x.NoInvoice, x.Bulan, x.Rate ,})
                .Select(s => new AR_TempToCalculate
                {
                    NoInvoice = s.Key.NoInvoice,
                    Bulan = s.Key.Bulan,
                    Rate = s.Key.Rate,
                    TotalAmount = Math.Round(s.Sum(x => x.TotalAmount), 2),
                    Amount = Math.Round(s.Sum(x => x.Amount), 2),
                })
                .ToList();

            //calculate down payment by invoice number
            var downPayment = _dbContext.AR_DownPayments.AsNoTracking()
                .Where(s => listInvoice.Contains(s.InvoiceNo))
                .Select(s => new AR_TempToCalculate
                {
                    NoInvoice = s.InvoiceNo,
                    Amount = s.Amount,
                    Rate = s.Kurs,
                    TotalAmount = s.TotalAmount,
                    Bulan = s.Month
                })
                .GroupBy(x => new { x.NoInvoice, x.Bulan, x.Rate })
                .Select(s => new AR_TempToCalculate
                {
                    NoInvoice = s.Key.NoInvoice,
                    Bulan = s.Key.Bulan,
                    Rate = s.Key.Rate,
                    TotalAmount = Math.Round(s.Sum(x => x.TotalAmount), 2),
                    Amount = Math.Round(s.Sum(x => x.Amount), 2),
                })
                .ToList();

            foreach (var a in queryUnion)
            {
                //find match data from koreksi omzet, cash in bank, cmt, and down payment
                var matchKoreksiOmzet = koreksiOmzet.Where(s => s.NoInvoice == a.NoInvoice);
                var matchCashInBank = cashInBank.Where(s => s.NoInvoice == a.NoInvoice);
                var matchCMT = cmt.Where(s => s.NoInvoice == a.NoInvoice);
                var matchDownPayment = downPayment.Where(s => s.NoInvoice == a.NoInvoice);

                // Calculate cash in bank each month
                if (matchCashInBank.Count() > 0)
                {
                    a.PelunasanJanuariIDR += matchCashInBank.Where(x => x.Bulan == 1).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanJanuariUSD += matchCashInBank.Where(x => x.Bulan == 1).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanFebruariIDR += matchCashInBank.Where(x => x.Bulan == 2).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanFebruariUSD += matchCashInBank.Where(x => x.Bulan == 2).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanMaretIDR += matchCashInBank.Where(x => x.Bulan == 3).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanMaretUSD += matchCashInBank.Where(x => x.Bulan == 3).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanAprilIDR += matchCashInBank.Where(x => x.Bulan == 4).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanAprilUSD += matchCashInBank.Where(x => x.Bulan == 4).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanMeiIDR += matchCashInBank.Where(x => x.Bulan == 5).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanMeiUSD += matchCashInBank.Where(x => x.Bulan == 5).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanJuniIDR += matchCashInBank.Where(x => x.Bulan == 6).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanJuniUSD += matchCashInBank.Where(x => x.Bulan == 6).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanJuliIDR += matchCashInBank.Where(x => x.Bulan == 7).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanJuliUSD += matchCashInBank.Where(x => x.Bulan == 7).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanAgustusIDR += matchCashInBank.Where(x => x.Bulan == 8).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanAgustusUSD += matchCashInBank.Where(x => x.Bulan == 8).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanSeptemberIDR += matchCashInBank.Where(x => x.Bulan == 9).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanSeptemberUSD += matchCashInBank.Where(x => x.Bulan == 9).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanOktoberIDR += matchCashInBank.Where(x => x.Bulan == 10).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanOktoberUSD += matchCashInBank.Where(x => x.Bulan == 10).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanNovemberIDR += matchCashInBank.Where(x => x.Bulan == 11).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanNovemberUSD += matchCashInBank.Where(x => x.Bulan == 11).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanDesemberIDR += matchCashInBank.Where(x => x.Bulan == 12).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanDesemberUSD += matchCashInBank.Where(x => x.Bulan == 12).Select(x => x.Amount).FirstOrDefault();
                }

                // Calculate koreksi omzet each month
                if (matchKoreksiOmzet.Count() > 0)
                {
                    a.KoreksiOmzetJanuariIDR = matchKoreksiOmzet.Where(x => x.Bulan == 1).Select(x => x.TotalAmount).FirstOrDefault();
                    a.KoreksiOmzetJanuariUSD = matchKoreksiOmzet.Where(x => x.Bulan == 1).Select(x => x.Amount).FirstOrDefault();

                    a.KoreksiOmzetFebruariIDR = matchKoreksiOmzet.Where(x => x.Bulan == 2).Select(x => x.TotalAmount).FirstOrDefault();
                    a.KoreksiOmzetFebruariUSD = matchKoreksiOmzet.Where(x => x.Bulan == 2).Select(x => x.Amount).FirstOrDefault();

                    a.KoreksiOmzetMaretIDR = matchKoreksiOmzet.Where(x => x.Bulan == 3).Select(x => x.TotalAmount).FirstOrDefault();
                    a.KoreksiOmzetMaretUSD = matchKoreksiOmzet.Where(x => x.Bulan == 3).Select(x => x.Amount).FirstOrDefault();

                    a.KoreksiOmzetAprilIDR = matchKoreksiOmzet.Where(x => x.Bulan == 4).Select(x => x.TotalAmount).FirstOrDefault();
                    a.KoreksiOmzetAprilUSD = matchKoreksiOmzet.Where(x => x.Bulan == 4).Select(x => x.Amount).FirstOrDefault();

                    a.KoreksiOmzetMeiIDR = matchKoreksiOmzet.Where(x => x.Bulan == 5).Select(x => x.TotalAmount).FirstOrDefault();
                    a.KoreksiOmzetMeiUSD = matchKoreksiOmzet.Where(x => x.Bulan == 5).Select(x => x.Amount).FirstOrDefault();

                    a.KoreksiOmzetJuniIDR = matchKoreksiOmzet.Where(x => x.Bulan == 6).Select(x => x.TotalAmount).FirstOrDefault();
                    a.KoreksiOmzetJuniUSD = matchKoreksiOmzet.Where(x => x.Bulan == 6).Select(x => x.Amount).FirstOrDefault();

                    a.KoreksiOmzetJuliIDR = matchKoreksiOmzet.Where(x => x.Bulan == 7).Select(x => x.TotalAmount).FirstOrDefault();
                    a.KoreksiOmzetJuliUSD = matchKoreksiOmzet.Where(x => x.Bulan == 7).Select(x => x.Amount).FirstOrDefault();

                    a.KoreksiOmzetAgustusIDR = matchKoreksiOmzet.Where(x => x.Bulan == 8).Select(x => x.TotalAmount).FirstOrDefault();
                    a.KoreksiOmzetAgustusUSD = matchKoreksiOmzet.Where(x => x.Bulan == 8).Select(x => x.Amount).FirstOrDefault();

                    a.KoreksiOmzetSeptemberIDR = matchKoreksiOmzet.Where(x => x.Bulan == 9).Select(x => x.TotalAmount).FirstOrDefault();
                    a.KoreksiOmzetSeptemberUSD = matchKoreksiOmzet.Where(x => x.Bulan == 9).Select(x => x.Amount).FirstOrDefault();

                    a.KoreksiOmzetOktoberIDR = matchKoreksiOmzet.Where(x => x.Bulan == 10).Select(x => x.TotalAmount).FirstOrDefault();
                    a.KoreksiOmzetOktoberUSD = matchKoreksiOmzet.Where(x => x.Bulan == 10).Select(x => x.Amount).FirstOrDefault();

                    a.KoreksiOmzetNovemberIDR = matchKoreksiOmzet.Where(x => x.Bulan == 11).Select(x => x.TotalAmount).FirstOrDefault();
                    a.KoreksiOmzetNovemberUSD = matchKoreksiOmzet.Where(x => x.Bulan == 11).Select(x => x.Amount).FirstOrDefault();

                    a.KoreksiOmzetDesemberIDR = matchKoreksiOmzet.Where(x => x.Bulan == 12).Select(x => x.TotalAmount).FirstOrDefault();
                    a.KoreksiOmzetDesemberUSD = matchKoreksiOmzet.Where(x => x.Bulan == 12).Select(x => x.Amount).FirstOrDefault();
                }

                // Calculate CMT each month
                if (matchCMT != null)
                {
                    a.PelunasanJanuariIDR += matchCMT.Where(x => x.Bulan == 1).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanJanuariUSD += matchCMT.Where(x => x.Bulan == 1).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanFebruariIDR += matchCMT.Where(x => x.Bulan == 2).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanFebruariUSD += matchCMT.Where(x => x.Bulan == 2).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanMaretIDR += matchCMT.Where(x => x.Bulan == 3).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanMaretUSD += matchCMT.Where(x => x.Bulan == 3).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanAprilIDR += matchCMT.Where(x => x.Bulan == 4).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanAprilUSD += matchCMT.Where(x => x.Bulan == 4).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanMeiIDR += matchCMT.Where(x => x.Bulan == 5).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanMeiUSD += matchCMT.Where(x => x.Bulan == 5).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanJuniIDR += matchCMT.Where(x => x.Bulan == 6).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanJuniUSD += matchCMT.Where(x => x.Bulan == 6).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanJuliIDR += matchCMT.Where(x => x.Bulan == 7).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanJuliUSD += matchCMT.Where(x => x.Bulan == 7).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanAgustusIDR += matchCMT.Where(x => x.Bulan == 8).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanAgustusUSD += matchCMT.Where(x => x.Bulan == 8).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanSeptemberIDR += matchCMT.Where(x => x.Bulan == 9).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanSeptemberUSD += matchCMT.Where(x => x.Bulan == 9).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanOktoberIDR += matchCMT.Where(x => x.Bulan == 10).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanOktoberUSD += matchCMT.Where(x => x.Bulan == 10).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanNovemberIDR += matchCMT.Where(x => x.Bulan == 11).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanNovemberUSD += matchCMT.Where(x => x.Bulan == 11).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanDesemberIDR += matchCMT.Where(x => x.Bulan == 12).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanDesemberUSD += matchCMT.Where(x => x.Bulan == 12).Select(x => x.Amount).FirstOrDefault();
                }

                // Calculate Down Payment each month
                if (matchDownPayment != null)
                {
                    a.PelunasanJanuariIDR += matchDownPayment.Where(x => x.Bulan == 1).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanJanuariUSD += matchDownPayment.Where(x => x.Bulan == 1).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanFebruariIDR += matchDownPayment.Where(x => x.Bulan == 2).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanFebruariUSD += matchDownPayment.Where(x => x.Bulan == 2).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanMaretIDR += matchDownPayment.Where(x => x.Bulan == 3).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanMaretUSD += matchDownPayment.Where(x => x.Bulan == 3).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanAprilIDR += matchDownPayment.Where(x => x.Bulan == 4).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanAprilUSD += matchDownPayment.Where(x => x.Bulan == 4).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanMeiIDR += matchDownPayment.Where(x => x.Bulan == 5).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanMeiUSD += matchDownPayment.Where(x => x.Bulan == 5).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanJuniIDR += matchDownPayment.Where(x => x.Bulan == 6).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanJuniUSD += matchDownPayment.Where(x => x.Bulan == 6).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanJuliIDR += matchDownPayment.Where(x => x.Bulan == 7).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanJuliUSD += matchDownPayment.Where(x => x.Bulan == 7).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanAgustusIDR += matchDownPayment.Where(x => x.Bulan == 8).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanAgustusUSD += matchDownPayment.Where(x => x.Bulan == 8).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanSeptemberIDR += matchDownPayment.Where(x => x.Bulan == 9).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanSeptemberUSD += matchDownPayment.Where(x => x.Bulan == 9).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanOktoberIDR += matchDownPayment.Where(x => x.Bulan == 10).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanOktoberUSD += matchDownPayment.Where(x => x.Bulan == 10).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanNovemberIDR += matchDownPayment.Where(x => x.Bulan == 11).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanNovemberUSD += matchDownPayment.Where(x => x.Bulan == 11).Select(x => x.Amount).FirstOrDefault();

                    a.PelunasanDesemberIDR += matchDownPayment.Where(x => x.Bulan == 12).Select(x => x.TotalAmount).FirstOrDefault();
                    a.PelunasanDesemberUSD += matchDownPayment.Where(x => x.Bulan == 12).Select(x => x.Amount).FirstOrDefault();
                }

                // Calculate Saldo Akhir for each month
                a.SaldoAkhirJanuariIDR = Math.Round(a.SaldoAwalIDR + a.PenjualanIDR + a.KoreksiOmzetJanuariIDR - a.PelunasanJanuariIDR, 2);
                a.SaldoAkhirJanuariUSD = Math.Round(a.SaldoAwalUS + a.PenjualanUS + a.KoreksiOmzetJanuariUSD - a.PelunasanJanuariUSD, 2);

                a.SaldoAkhirFebruariIDR = Math.Round(a.SaldoAkhirJanuariIDR + a.KoreksiOmzetFebruariIDR - a.PelunasanFebruariIDR, 2);
                a.SaldoAkhirFebruariUSD = Math.Round(a.SaldoAkhirJanuariUSD + a.KoreksiOmzetFebruariUSD - a.PelunasanFebruariUSD, 2);

                a.SaldoAkhirMaretIDR = Math.Round(a.SaldoAkhirFebruariIDR + a.KoreksiOmzetMaretIDR - a.PelunasanMaretIDR, 2);
                a.SaldoAkhirMaretUSD = Math.Round(a.SaldoAkhirFebruariUSD + a.KoreksiOmzetMaretUSD - a.PelunasanMaretUSD, 2);

                a.SaldoAkhirAprilIDR = Math.Round(a.SaldoAkhirMaretIDR + a.KoreksiOmzetAprilIDR - a.PelunasanAprilIDR, 2);
                a.SaldoAkhirAprilUSD = Math.Round(a.SaldoAkhirMaretUSD + a.KoreksiOmzetAprilUSD - a.PelunasanAprilUSD, 2);

                a.SaldoAkhirMeiIDR = Math.Round(a.SaldoAkhirAprilIDR + a.KoreksiOmzetMeiIDR - a.PelunasanMeiIDR, 2);
                a.SaldoAkhirMeiUSD = Math.Round(a.SaldoAkhirAprilUSD + a.KoreksiOmzetMeiUSD - a.PelunasanMeiUSD, 2);

                a.SaldoAkhirJuniIDR = Math.Round(a.SaldoAkhirMeiIDR + a.KoreksiOmzetJuniIDR - a.PelunasanJuniIDR, 2);
                a.SaldoAkhirJuniUSD = Math.Round(a.SaldoAkhirMeiUSD + a.KoreksiOmzetJuniUSD - a.PelunasanJuniUSD, 2);

                a.SaldoAkhirJuliIDR = Math.Round(a.SaldoAkhirJuniIDR + a.KoreksiOmzetJuliIDR - a.PelunasanJuliIDR, 2);
                a.SaldoAkhirJuliUSD = Math.Round(a.SaldoAkhirJuniUSD + a.KoreksiOmzetJuliUSD - a.PelunasanJuliUSD, 2);

                a.SaldoAkhirAgustusIDR = Math.Round(a.SaldoAkhirJuliIDR + a.KoreksiOmzetAgustusIDR - a.PelunasanAgustusIDR, 2);
                a.SaldoAkhirAgustusUSD = Math.Round(a.SaldoAkhirJuliUSD + a.KoreksiOmzetAgustusUSD - a.PelunasanAgustusUSD, 2);

                a.SaldoAkhirSeptemberIDR = Math.Round(a.SaldoAkhirAgustusIDR + a.KoreksiOmzetSeptemberIDR - a.PelunasanSeptemberIDR, 2);
                a.SaldoAkhirSeptemberUSD = Math.Round(a.SaldoAkhirAgustusUSD + a.KoreksiOmzetSeptemberUSD - a.PelunasanSeptemberUSD, 2);

                a.SaldoAkhirOktoberIDR = Math.Round(a.SaldoAkhirSeptemberIDR + a.KoreksiOmzetOktoberIDR - a.PelunasanOktoberIDR, 2);
                a.SaldoAkhirOktoberUSD = Math.Round(a.SaldoAkhirSeptemberUSD + a.KoreksiOmzetOktoberUSD - a.PelunasanOktoberUSD, 2);

                a.SaldoAkhirNovemberIDR = Math.Round(a.SaldoAkhirOktoberIDR + a.KoreksiOmzetNovemberIDR - a.PelunasanNovemberIDR, 2);
                a.SaldoAkhirNovemberUSD = Math.Round(a.SaldoAkhirOktoberUSD + a.KoreksiOmzetNovemberUSD - a.PelunasanNovemberUSD, 2);

                a.SaldoAkhirDesemberIDR = Math.Round(a.SaldoAkhirNovemberIDR + a.KoreksiOmzetDesemberIDR - a.PelunasanDesemberIDR, 2);
                a.SaldoAkhirDesemberUSD = Math.Round(a.SaldoAkhirNovemberUSD + a.KoreksiOmzetDesemberUSD - a.PelunasanDesemberUSD, 2);
            }

            return queryUnion;
        }

        public async Task<MemoryStream> GetExcel(DateTime dateFrom, DateTime dateTo)
        {
       
            var data = await GetQuery(dateFrom,dateTo);
            {
                // Membuat kolom-kolom yang dibutuhkan
                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");
                    // Insert Data
                    DataTable result = new DataTable();

                    // Membuat Header Awal
                    result.Columns.Add(new DataColumn() { ColumnName = "Bulan", DataType = typeof(String) });
                    result.Columns.Add(new DataColumn() { ColumnName = "No Invoice", DataType = typeof(String) });
                    result.Columns.Add(new DataColumn() { ColumnName = "Kode Buyer", DataType = typeof(String) });
                    result.Columns.Add(new DataColumn() { ColumnName = "Tgl Trucking", DataType = typeof(String) });
                    result.Columns.Add(new DataColumn() { ColumnName = "Tgl PEB", DataType = typeof(String) });
                    result.Columns.Add(new DataColumn() { ColumnName = "US$1", DataType = typeof(double) });
                    result.Columns.Add(new DataColumn() { ColumnName = "Rate1", DataType = typeof(double) });
                    result.Columns.Add(new DataColumn() { ColumnName = "IDR1", DataType = typeof(double) });
                    result.Columns.Add(new DataColumn() { ColumnName = "US$2", DataType = typeof(double) });
                    result.Columns.Add(new DataColumn() { ColumnName = "Rate2", DataType = typeof(double) });
                    result.Columns.Add(new DataColumn() { ColumnName = "IDR2", DataType = typeof(double) });

                    int counter = 3;
                    // Define columns
                    for (int i = 0; i < 36; i++)
                    {
                        result.Columns.Add(new DataColumn() { ColumnName = $"USD{counter}", DataType = typeof(double) });
                        result.Columns.Add(new DataColumn() { ColumnName = $"IDR{counter}", DataType = typeof(double) });
                        counter++;
                    }

                    if (data.Count == 0)
                    {
                        // Add blank row
                        result.Rows.Add("", "", "", "", "",
                                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

                    }
                    else
                    {
                        foreach (var d in data)
                        {
                            var tglTrucking = d.TglTrucking != null ? d.TglTrucking.ToString("dd/MM/yyyy") : "";
                            var tglPEB = d.TglPEB != DateTime.MinValue ? d.TglPEB.ToString("dd/MM/yyyy") : "";

                            //map data to row 



                            result.Rows.Add(
                                    d.Bulan,
                                    d.NoInvoice,
                                    d.KodeBuyer,
                                    tglTrucking,
                                    tglPEB,

                                    d.SaldoAwalUS,
                                    d.SaldoAwalRate,
                                    d.SaldoAwalIDR,

                                    d.PenjualanUS,
                                    d.PenjualanRate,
                                    d.PenjualanIDR,

                                    // JANUARI
                                    d.KoreksiOmzetJanuariUSD,
                                    d.KoreksiOmzetJanuariIDR,
                                    d.PelunasanJanuariUSD,
                                    d.PelunasanJanuariIDR,
                                    d.SaldoAkhirJanuariUSD,
                                    d.SaldoAkhirJanuariIDR,

                                    // FEBRUARI
                                    d.KoreksiOmzetFebruariUSD,
                                    d.KoreksiOmzetFebruariIDR,
                                    d.PelunasanFebruariUSD,
                                    d.PelunasanFebruariIDR,
                                    d.SaldoAkhirFebruariUSD,
                                    d.SaldoAkhirFebruariIDR,

                                    // MARET
                                    d.KoreksiOmzetMaretUSD,
                                    d.KoreksiOmzetMaretIDR,
                                    d.PelunasanMaretUSD,
                                    d.PelunasanMaretIDR,
                                    d.SaldoAkhirMaretUSD,
                                    d.SaldoAkhirMaretIDR,

                                    // APRIL
                                    d.KoreksiOmzetAprilUSD,
                                    d.KoreksiOmzetAprilIDR,
                                    d.PelunasanAprilUSD,
                                    d.PelunasanAprilIDR,
                                    d.SaldoAkhirAprilUSD,
                                    d.SaldoAkhirAprilIDR,

                                    // MEI
                                    d.KoreksiOmzetMeiUSD,
                                    d.KoreksiOmzetMeiIDR,
                                    d.PelunasanMeiUSD,
                                    d.PelunasanMeiIDR,
                                    d.SaldoAkhirMeiUSD,
                                    d.SaldoAkhirMeiIDR,

                                    // JUNI
                                    d.KoreksiOmzetJuniUSD,
                                    d.KoreksiOmzetJuniIDR,
                                    d.PelunasanJuniUSD,
                                    d.PelunasanJuniIDR,
                                    d.SaldoAkhirJuniUSD,
                                    d.SaldoAkhirJuniIDR,

                                    // JULI
                                    d.KoreksiOmzetJuliUSD,
                                    d.KoreksiOmzetJuliIDR,
                                    d.PelunasanJuliUSD,
                                    d.PelunasanJuliIDR,
                                    d.SaldoAkhirJuliUSD,
                                    d.SaldoAkhirJuliIDR,

                                    // AGUSTUS
                                    d.KoreksiOmzetAgustusUSD,
                                    d.KoreksiOmzetAgustusIDR,
                                    d.PelunasanAgustusUSD,
                                    d.PelunasanAgustusIDR,
                                    d.SaldoAkhirAgustusUSD,
                                    d.SaldoAkhirAgustusIDR,

                                    // SEPTEMBER
                                    d.KoreksiOmzetSeptemberUSD,
                                    d.KoreksiOmzetSeptemberIDR,
                                    d.PelunasanSeptemberUSD,
                                    d.PelunasanSeptemberIDR,
                                    d.SaldoAkhirSeptemberUSD,
                                    d.SaldoAkhirSeptemberIDR,

                                    // OKTOBER
                                    d.KoreksiOmzetOktoberUSD,
                                    d.KoreksiOmzetOktoberIDR,
                                    d.PelunasanOktoberUSD,
                                    d.PelunasanOktoberIDR,
                                    d.SaldoAkhirOktoberUSD,
                                    d.SaldoAkhirOktoberIDR,

                                    // NOVEMBER
                                    d.KoreksiOmzetNovemberUSD,
                                    d.KoreksiOmzetNovemberIDR,
                                    d.PelunasanNovemberUSD,
                                    d.PelunasanNovemberIDR,
                                    d.SaldoAkhirNovemberUSD,
                                    d.SaldoAkhirNovemberIDR,

                                    // DESEMBER
                                    d.KoreksiOmzetDesemberUSD,
                                    d.KoreksiOmzetDesemberIDR,
                                    d.PelunasanDesemberUSD,
                                    d.PelunasanDesemberIDR,
                                    d.SaldoAkhirDesemberUSD,
                                    d.SaldoAkhirDesemberIDR
                               );

                        }
                    }
                  
                    
                    //worksheet.Cells[3, 1].Value = "Bulan";
                    //worksheet.Cells[3, 2].Value = "No Invoice";
                    //worksheet.Cells[3, 3].Value = "Kode Buyer";
                    //worksheet.Cells[3, 4].Value = "Tgl Trucking";
                    //worksheet.Cells[3, 5].Value = "Tgl PEB";

                    worksheet.Cells[2, 6].Value = "Saldo Awal";
                    worksheet.Cells[2, 6, 2, 8].Merge = true;
                    //worksheet.Cells[3, 6].Value = "US$";
                    //worksheet.Cells[3, 7].Value = "Rate";
                    //worksheet.Cells[3, 8].Value = "IDR";

                    worksheet.Cells[2, 9].Value = "Penjualan";
                    worksheet.Cells[2, 9, 2, 11].Merge = true;
                    //worksheet.Cells[3, 9].Value = "US$";
                    //worksheet.Cells[3, 10].Value = "Rate";
                    //worksheet.Cells[3, 11].Value = "IDR";

                    // Pengulangan untuk setiap bulan dari Januari hingga Desember
                    int startColumn = 12;
                    string[] months = { "JANUARI", "FEBRUARI", "MARET", "APRIL", "MEI", "JUNI",
                                        "JULI", "AGUSTUS", "SEPTEMBER", "OKTOBER", "NOVEMBER", "DESEMBER" };

                    foreach (var month in months)
                    {
                        // Header bulan
                        worksheet.Cells[1, startColumn].Value = month;
                        worksheet.Cells[1, startColumn, 1, startColumn + 5].Merge = true;

                        // Koreksi Omzet
                        worksheet.Cells[2, startColumn].Value = "Koreksi Omzet";
                        worksheet.Cells[2, startColumn, 2, startColumn + 1].Merge = true;
                        //worksheet.Cells[3, startColumn].Value = "USD";
                        //worksheet.Cells[3, startColumn + 1].Value = "IDR";

                        // Pelunasan
                        worksheet.Cells[2, startColumn + 2].Value = "Pelunasan";
                        worksheet.Cells[2, startColumn + 2, 2, startColumn + 3].Merge = true;
                        //worksheet.Cells[3, startColumn + 2].Value = "USD";
                        //worksheet.Cells[3, startColumn + 3].Value = "IDR";

                        // Saldo Akhir
                        worksheet.Cells[2, startColumn + 4].Value = "Saldo Akhir";
                        worksheet.Cells[2, startColumn + 4, 2, startColumn + 5].Merge = true;
                        //worksheet.Cells[3, startColumn + 4].Value = "USD";
                        //worksheet.Cells[3, startColumn + 5].Value = "IDR";

                        // Pindahkan ke kolom berikutnya untuk bulan selanjutnya
                        startColumn += 6;
                     
                    }

                    // Styling untuk semua header
                    worksheet.Cells["A1:CE3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A1:CE3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells["A1:CE3"].Style.Font.Bold = true;

                  

                

                    // Add Data
                    worksheet.Cells["A3"].LoadFromDataTable(result, true, OfficeOpenXml.Table.TableStyles.Light16);

                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    var stream = new MemoryStream();
                    package.SaveAs(stream);

                    return stream;
                }
            };
        }
    }
}
