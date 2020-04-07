using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.InspectionDocumentReport
{
    public class FilterInspectionDocumentReport
    {
        public FilterInspectionDocumentReport(string dateReport, string group, string mutasi, string zona, string keterangan)
        {
            DateReport = dateReport;
            Group = group;
            Mutasi = mutasi;
            Zona = zona;
            Keterangan = keterangan;
        }

        public string DateReport { get; set; }
        public string Group { get; set; }
        public string Mutasi { get; set; }
        public string Zona { get; set; }
        public string Keterangan { get; set; }
    }
}
