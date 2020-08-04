using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentLocalSalesOmzet
{
    public class GarmentLocalSalesOmzetService : IGarmentLocalSalesOmzetService
    {
        private readonly IGarmentShippingLocalSalesNoteRepository repository;
        private readonly IGarmentShippingLocalSalesNoteItemRepository itemrepository;
        private readonly IIdentityProvider _identityProvider;

        public GarmentLocalSalesOmzetService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentShippingLocalSalesNoteRepository>();
            itemrepository = serviceProvider.GetService<IGarmentShippingLocalSalesNoteItemRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

       public IQueryable<GarmentLocalSalesOmzetViewModel> GetData(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
           var query = repository.ReadAll();
           var queryItem = itemrepository.ReadAll();
      
            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;
            
            query = query.Where(w => w.Date.AddHours(offset).Date >= DateFrom.Date && w.Date.AddHours(offset).Date <= DateTo.Date);

            query = query.OrderBy(w => w.BuyerCode).ThenBy(w => w.Date);

            var newQ = (from a in query
                        join b in queryItem on a.Id equals b.LocalSalesNoteId

                        group new { Amt = Convert.ToDecimal(b.Quantity) * Convert.ToDecimal(b.Price) } by new { a.NoteNo, a.Date, a.BuyerCode, a.BuyerName, a.UseVat } into G

                        select new GarmentLocalSalesOmzetViewModel
                        {
                            LSNo = G.Key.NoteNo,
                            LSDate = G.Key.Date,
                            BuyerCode = G.Key.BuyerCode,
                            BuyerName = G.Key.BuyerName,
                            DPP = Math.Round(G.Sum(m => m.Amt), 2),
                            UseVat = G.Key.UseVat == true ? "YA" : "TIDAK",
                            PPN = G.Key.UseVat == true ? (Convert.ToDecimal(0.1) * Math.Round(G.Sum(m => m.Amt), 2)) : 0,
                            Total = G.Key.UseVat == true ? (Convert.ToDecimal(1.1) * Math.Round(G.Sum(m => m.Amt), 2)) : Math.Round(G.Sum(m => m.Amt), 2),
                        });                      
            return newQ;
        }

        public List<GarmentLocalSalesOmzetViewModel> GetReportData(DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetData(dateFrom, dateTo, offset);
            Query = Query.OrderBy(b => b.BuyerCode).ThenBy(b => b.LSDate);
            return Query.ToList();
        }

        public MemoryStream GenerateExcel(DateTime? dateFrom, DateTime? dateTo, int offset)
       {

            var Query = GetData(dateFrom, dateTo, offset);
            DataTable result = new DataTable();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Nota", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Nota", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Buyer", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "Pakai PPN", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "D P P", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "P P N", DataType = typeof(string) });
            result.Columns.Add(new DataColumn() { ColumnName = "T O T A L", DataType = typeof(string) });
            
            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "");
            else
            {
                int index = 0;
                foreach (var d in Query)
                {
                    index++;

                    string GLSDate = d.LSDate == new DateTime(1970, 1, 1) ? "-" : d.LSDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string AmntDPP = string.Format("{0:N2}", d.DPP);
                    string AmntPPN = string.Format("{0:N2}", d.PPN);
                    string AmntTOT = string.Format("{0:N2}", d.Total);

                    result.Rows.Add(index, d.LSNo, GLSDate, d.BuyerCode, d.BuyerName, d.UseVat, AmntDPP, AmntPPN, AmntTOT);
                }
            }
          
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Sheet1") }, true);
        }
    }
}
