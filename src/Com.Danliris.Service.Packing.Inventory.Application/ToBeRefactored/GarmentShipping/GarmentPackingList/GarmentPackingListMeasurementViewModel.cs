using Com.Danliris.Service.Packing.Inventory.Application.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListMeasurementViewModel : BaseViewModel
    {
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double CartonsQuantity { get; set; }
    }
}
