using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.AcceptingPackaging
{
    public class AcceptingPackagingViewModel : BaseViewModel
    {
        public string NoBon { get; set; }
        public ProductionOrder NoSpp { get; set; }
        public Unit Unit { get; set; }
        public Material MaterialObject { get; set; }
        public string Motif { get; set; }
        public string Warna { get; set; }
        public string Grade { get; set; }
        public string Satuan { get; set; }
        public decimal Saldo { get; set; }
        public string Shift { get; set; }
        public double Mtr { get; set; }
        public double Yds { get; set; }
        public decimal PackagingQty { get; set; }
        public string PackagingUnit { get; set; }
        public bool IsPrinting { get; set; }
        public AcceptingPackagingViewModel()
        {

        }
        public AcceptingPackagingViewModel(DyeingPrintingAreaMovementModel source)
        {
            Active = source.Active;
            Saldo = source.Balance;
            Satuan = source.UOMUnit;
            NoSpp = new ProductionOrder
            {
                Id = source.ProductionOrderId,
                Code = source.ProductionOrderCode,
                No = source.ProductionOrderNo,
                Type = source.ProductionOrderType
            };
            CreatedAgent = source.CreatedAgent;
            DeletedAgent = source.DeletedAgent;
            CreatedBy = source.CreatedBy;
            CreatedUtc = source.CreatedUtc;
            DeletedBy = source.DeletedBy;
            DeletedUtc = source.DeletedUtc;
            Grade = source.Grade;
            Id = source.Id;
            IsDeleted = source.IsDeleted;
            LastModifiedAgent = source.LastModifiedAgent;
            LastModifiedBy = source.LastModifiedBy;
            LastModifiedUtc = source.LastModifiedUtc;
            MaterialObject = new Material
            {
                Id = source.MaterialId,
                Code = source.MaterialCode,
                Name = source.MaterialName
            };
            Motif = source.Motif;
            NoBon = source.BonNo;
            Unit = new Unit
            {
                Code = source.UnitCode,
                Id = source.UnitId,
                Name = source.UnitName
            };
            Warna = source.Color;
            Shift = source.Shift;
            Mtr = source.MeterLength;
            Yds = source.YardsLength;
            IsPrinting = source.UnitName.ToLower().Equals("printing");
            PackagingQty = source.PackagingQty;
            PackagingUnit = source.PackagingUnit;
        }

    }
}
