using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.PackingList
{
    public class GarmentPackingListMonitoringService : IGarmentPackingListMonitoringService
    {
        private readonly IGarmentPackingListRepository repository;
        private readonly IIdentityProvider _identityProvider;

        public GarmentPackingListMonitoringService(IServiceProvider serviceProvider)
        {
            repository = serviceProvider.GetService<IGarmentPackingListRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        private List<GarmentPackingListMonitoringViewModel> GetData(int buyerAgentId, string invoiceType, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
        {
            var query = repository.ReadAll();

            if (buyerAgentId > 0)
            {
                query = query.Where(w => w.BuyerAgentId == buyerAgentId);
            }

            if (!string.IsNullOrWhiteSpace(invoiceType))
            {
                query = query.Where(w => w.InvoiceType == invoiceType);
            }

            dateFrom = dateFrom ?? DateTimeOffset.MinValue;
            dateTo = dateTo ?? DateTimeOffset.MaxValue;

            query = query.Where(w => w.Date >= dateFrom && w.Date <= dateTo);

            var selectedQuery = query.Select(s => new GarmentPackingListMonitoringViewModel
            {
                id = s.Id,
                invoiceNo = s.InvoiceNo,
                date = s.Date,
                buyerAgentCode = s.BuyerAgentCode,
                buyerAgentName = s.BuyerAgentName,
                sectionCode = s.SectionCode,
                truckingDate = s.TruckingDate,
                exportEstimationDate = s.ExportEstimationDate,
                destination = s.Destination,
                lcNo = s.LCNo,
                issuedBy = s.IssuedBy,
                grossWeight = s.GrossWeight,
                nettWeight = s.NettWeight,
                totalCarton = s.TotalCartons
            })
            .OrderBy(o => o.date)
            .ToList();

            return selectedQuery;
        }

        public ListResult<GarmentPackingListMonitoringViewModel> GetReportData(int buyerAgentId, string invoiceType, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
        {
            var data = GetData(buyerAgentId, invoiceType, dateFrom, dateTo);
            var total = data.Count;

            return new ListResult<GarmentPackingListMonitoringViewModel>(data, 1, total, total);
        }

        public MemoryStreamResult GenerateExcel(int buyerAgentId, string buyerAgent, string invoiceType, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
        {
            var data = GetData(buyerAgentId, invoiceType, dateFrom, dateTo);

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No Invoice", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal Packing List", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer Agent", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Seksi", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal Trucking", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal Perkiraan Export", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Destination", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "LC No", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Issued By", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Gross Weight", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nett Weight", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Total Carton", DataType = typeof(double) });

            if (data.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", 0, 0, 0);
            }
            else
            {
                foreach (var d in data)
                {
                    dt.Rows.Add(d.invoiceNo, d.date.ToString("MM/dd/yyyy", new CultureInfo("us-US")), $"{d.buyerAgentCode} - {d.buyerAgentName}", d.sectionCode, d.truckingDate.ToString("MM/dd/yyyy", new CultureInfo("us-US")), d.exportEstimationDate.ToString("MM/dd/yyyy", new CultureInfo("us-US")), d.destination, d.lcNo, d.issuedBy, d.grossWeight, d.nettWeight, d.totalCarton);
                }
            }

            buyerAgent = buyerAgentId == 0 ? "" : $" {buyerAgent}";
            invoiceType = string.IsNullOrWhiteSpace(invoiceType) ? "" : $" {invoiceType}";
            var dateFromString = dateFrom == null ? "" : $" from {DateTimeToString(dateFrom.GetValueOrDefault())}";
            var dateToString = dateTo == null ? "" : $" to {DateTimeToString(dateTo.GetValueOrDefault())}";

            var excel = Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Packing List") }, true);
            var filename = $"Monitoring Packing List{buyerAgent}{invoiceType}{dateFromString}{dateToString}.xlsx";

            return new MemoryStreamResult(excel, filename);
        }

        private string DateTimeToString(DateTimeOffset dateTime)
        {
            return dateTime.ToOffset(new TimeSpan(_identityProvider.TimezoneOffset, 0, 0)).ToString("dd MMMM yyyy");
        }
    }
}
