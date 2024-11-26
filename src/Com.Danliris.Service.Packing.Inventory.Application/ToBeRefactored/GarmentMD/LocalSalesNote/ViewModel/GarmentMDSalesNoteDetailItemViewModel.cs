using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentMD.LocalSalesNote.ViewModel
{
    public class GarmentMDSalesNoteDetailItemViewModel : BaseViewModel
    {
        public double quantity { get;  set; }
        public UnitOfMeasurement uom { get; set; }
        public Comodity comodity { get; set; }
        public string roNo { get; set; }
    }
}
