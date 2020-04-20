using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.AcceptingPackaging
{
    public class AcceptingPackagingModel : StandardEntity
    {
        public AcceptingPackagingModel(string bonNo, string orderNo, string unit, string materialObject, string motif, string warna, string grade, string satuan, decimal saldo, string shift, double mtr, double yds, int idDyeingPrintingMovement)
        {
            BonNo = bonNo;
            OrderNo = orderNo;
            Unit = unit;
            MaterialObject = materialObject;
            Motif = motif;
            Warna = warna;
            Grade = grade;
            Satuan = satuan;
            Saldo = saldo;
            Shift = shift;
            Mtr = mtr;
            Yds = yds;
            IdDyeingPrintingMovement = idDyeingPrintingMovement;
        }
        public AcceptingPackagingModel()
        {

        }
        public DateTimeOffset Date { get; set; }
        public string BonNo { get; set; }
        public string OrderNo { get; set; }
        public string Unit { get; set; }
        public string MaterialObject { get; set; }
        public string Motif { get; set; }
        public string Warna { get; set; }
        public string Grade { get; set; }
        public string Satuan { get; set; }
        public decimal Saldo { get; set; }
        public string Shift { get; set; }
        public double Mtr { get; set; }
        public double Yds { get; set; }
        public int IdDyeingPrintingMovement { get; set; }
        public decimal PackagingQTY { get; set; }
        public string PackagingUnit { get; set; }
    }
}
