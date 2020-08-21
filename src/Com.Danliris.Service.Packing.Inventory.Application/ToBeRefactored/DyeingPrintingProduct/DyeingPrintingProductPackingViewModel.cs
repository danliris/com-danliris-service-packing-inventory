using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingProduct
{
    public class DyeingPrintingProductPackingViewModel
    {
        public int Id { get; set; }
        public int ProductSKUId { get; set; }
        public int FabricSKUId { get; set; }
        public string ProductSKUCode { get; set; }
        public int ProductPackingId { get; set; }
        public int FabricPackingId { get; set; }
        public string ProductPackingCode { get; set; }
        public bool HasPrintingProductSKU { get; set; }
        public bool HasPrintingProductPacking { get; set; }
        
        // Anyaman
        public Material Material { get; set; }
        // Konstruksi
        public MaterialConstruction MaterialConstruction { get; set; }
        // Panjang
        public string MaterialWidth { get; set; }
        // Warna
        public string Color { get; set; }
        // Motif
        public string Motif { get; set; }
        //Nomor Benang
        public YarnMaterial YarnMaterial { get; set; }
        // Satuan
        public string UomUnit { get; set; }
        // SPP
        public ProductionOrder ProductionOrder { get; set; }
        // Panjang Per Packing
        public double ProductPackingLength { get; set; }
        // Satuan Packing
        public string ProductPackingType { get; set; }
        // Quantity Packing
        public decimal Quantity { get; set; }
    }
}
