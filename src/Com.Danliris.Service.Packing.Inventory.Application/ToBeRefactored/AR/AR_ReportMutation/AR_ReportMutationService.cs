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

        public async Task<List<AR_ReportMutationViewModel>>GetQuery(DateTime dateFrom,DateTime dateTo)
        {
            return new List<AR_ReportMutationViewModel>();
        }

        public async Task<MemoryStream> GetExcel()
        {



            DataTable result = new DataTable();

            

            // Membuat kolom-kolom yang dibutuhkan
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Membuat Header Awal
                worksheet.Cells[3, 1].Value = "Bulan";
                worksheet.Cells[3, 2].Value = "No Invoice";
                worksheet.Cells[3, 3].Value = "Kode Buyer";
                worksheet.Cells[3, 4].Value = "Tgl Trucking";
                worksheet.Cells[3, 5].Value = "Tgl PEB";

                worksheet.Cells[2, 6].Value = "Saldo Awal";
                worksheet.Cells[2, 6, 2, 8].Merge = true;
                worksheet.Cells[3, 6].Value = "US$";
                worksheet.Cells[3, 7].Value = "Rate";
                worksheet.Cells[3, 8].Value = "IDR";

                worksheet.Cells[2, 9].Value = "Penjualan";
                worksheet.Cells[2, 9, 2, 11].Merge = true;
                worksheet.Cells[3, 9].Value = "US$";
                worksheet.Cells[3, 10].Value = "Rate";
                worksheet.Cells[3, 11].Value = "IDR";

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
                    worksheet.Cells[3, startColumn].Value = "USD";
                    worksheet.Cells[3, startColumn + 1].Value = "IDR";

                    // Pelunasan
                    worksheet.Cells[2, startColumn + 2].Value = "Pelunasan";
                    worksheet.Cells[2, startColumn + 2, 2, startColumn + 3].Merge = true;
                    worksheet.Cells[3, startColumn + 2].Value = "USD";
                    worksheet.Cells[3, startColumn + 3].Value = "IDR";

                    // Saldo Akhir
                    worksheet.Cells[2, startColumn + 4].Value = "Saldo Akhir";
                    worksheet.Cells[2, startColumn + 4, 2, startColumn + 5].Merge = true;
                    worksheet.Cells[3, startColumn + 4].Value = "USD";
                    worksheet.Cells[3, startColumn + 5].Value = "IDR";

                    // Pindahkan ke kolom berikutnya untuk bulan selanjutnya
                    startColumn += 6;
                }

                // Styling untuk semua header
                worksheet.Cells["A1:CE3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A1:CE3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells["A1:CE3"].Style.Font.Bold = true;

                //Add Data
                worksheet.Cells["A5"].LoadFromDataTable(result, true, OfficeOpenXml.Table.TableStyles.Light16);

                var stream = new MemoryStream();
                package.SaveAs(stream);

                return stream;

            }


        }
    }
}
