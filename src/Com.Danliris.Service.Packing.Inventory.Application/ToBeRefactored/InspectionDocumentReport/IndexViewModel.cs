using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.InspectionDocumentReport
{
    public class IndexViewModel
    {
        public int Index { get; set; }
        public DateTimeOffset DateReport { get; set; }
        public string GroupText { get; set; }
        public string UnitText { get; set; }
        public string KeluarKe { get; set; }
        public string NoSpp { get; set; }
        public string NoKereta { get; set; }
        public string Material { get; set; }
        public string Keterangan { get; set; }
        public string Status { get; set; }
        public string Lebar { get; set; }
        public string Motif { get; set; }
        public string Warna { get; set; }
        public string Mtr { get; set; }
        public string Yds { get; set; }
    }
}
