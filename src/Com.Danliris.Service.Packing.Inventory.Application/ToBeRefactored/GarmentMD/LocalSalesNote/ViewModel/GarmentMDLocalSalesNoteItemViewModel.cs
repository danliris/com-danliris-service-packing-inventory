using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentMD.LocalSalesNote.ViewModel
{
    public class GarmentMDLocalSalesNoteItemViewModel : BaseViewModel
    {
        public int localSalesContractId { get; set; }
        public string comodityName { get; set; }
        public double quantity { get; set; }
        public UnitOfMeasurement uom { get; set; }
        public double price { get; set; }

        public double remQty { get; set; }
        public string remark { get; set; }
        public ICollection<GarmentMDLocalSalesNoteDetailViewModel> details { get; set; }
    }
}
