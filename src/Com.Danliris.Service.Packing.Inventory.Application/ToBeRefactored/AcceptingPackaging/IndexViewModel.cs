using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Data.Models.AcceptingPackaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.AcceptingPackaging
{
    public class IndexViewModel
    {
        public DateTimeOffset? Date { get; set; }
        public string NoBon { get; set; }
        public string Shift { get; set; }
        public string NoSpp { get; set; }
        public string NoSppId { get; set; }
        public string Unit { get; set; }
        public string UnitId { get; set; }
        public string Material { get; set; }
        public string MaterialId { get; set; }
        public string Motif { get; set; }
        public string Warna { get; set; }
        public string Grade { get; set; }
        public string Mtr { get; set; }
        public string Yds { get; set; }
        public decimal Saldo { get; set; }

        public IndexViewModel()
        {

        }
        public IndexViewModel(AcceptingPackagingViewModel source)
        {
            Date = source.LastModifiedUtc;
            Grade = source.Grade;
            Material = source.MaterialObject.Name;
            Motif = source.Motif;
            Saldo = source.Saldo;
            Shift = source.Shift;
            NoSpp = source.NoSpp.No;
            Mtr = source.Mtr.ToString();
            Yds = source.Yds.ToString();
            NoBon = source.NoBon;
            Unit = source.Unit.Name;
            Warna = source.Warna;
        }
        public IndexViewModel(AcceptingPackagingModel source)
        {
            Date = source.Date;
            Grade = source.Grade;
            Material = source.MaterialObject;
            Motif = source.Motif;
            Saldo = source.Saldo;
            Shift = source.Shift;
            NoSpp = source.OrderNo;
            Mtr = source.Mtr.ToString();
            Yds = source.Yds.ToString();
            NoBon = source.BonNo;
            Unit = source.Unit;
            Warna = source.Warna;
        }
        public IndexViewModel(DyeingPrintingAreaMovementModel source)
        {
            Date = source.LastModifiedUtc;
            Grade = source.Grade;
            Material = source.MaterialName;
            Motif = source.Motif;
            Saldo = source.Balance;
            Shift = source.Shift;
            NoSpp = source.ProductionOrderNo;
            Mtr = source.MeterLength.ToString();
            Yds = source.YardsLength.ToString();
            NoBon = source.BonNo;
            Unit = source.UnitName;
            Warna = source.Color;
        }
    }
}
