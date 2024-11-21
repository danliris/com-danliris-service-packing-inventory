using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentMD.LocalSalesNote.ViewModel
{
    public class GarmentMDLocalSalesNoteDetailViewModel : BaseViewModel
    {
        public string bonNo { get;  set; }
        public double quantity { get;  set; }
        public UnitOfMeasurement uom { get; set; }
        public string bonFrom { get;  set; }
    }
}
