using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class InspectionDocumentReportModel : StandardEntity
    {
        public InspectionDocumentReportModel()
        {

        }

        public InspectionDocumentReportModel(int index, DateTime dateReport, string groupText, string unitText, string keluarKe, string noSpp, string noKereta, string material, string keterangan, string status, string lebar, string motif, string warna, string mtr, string yds)
        {
            Index = index;
            DateReport = dateReport;
            GroupText = groupText;
            UnitText = unitText;
            KeluarKe = keluarKe;
            NoSpp = noSpp;
            NoKereta = noKereta;
            Material = material;
            Keterangan = keterangan;
            Status = status;
            Lebar = lebar;
            Motif = motif;
            Warna = warna;
            Mtr = mtr;
            Yds = yds;
        }

        public int Index { get; set; }
        public DateTime DateReport { get; set; }
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
