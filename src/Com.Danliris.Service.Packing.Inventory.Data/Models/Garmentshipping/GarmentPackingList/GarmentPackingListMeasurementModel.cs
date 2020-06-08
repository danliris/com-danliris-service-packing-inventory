using Com.Moonlay.Models;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList
{
    public class GarmentPackingListMeasurementModel : StandardEntity
    {
        public int PackingListId { get; private set; }

        public double Length { get; private set; }
        public double Width { get; private set; }
        public double Height { get; private set; }
        public double CartonsQuantity { get; private set; }

        public GarmentPackingListMeasurementModel()
        {
        }

        public GarmentPackingListMeasurementModel(double length, double width, double height, double cartonsQuantity)
        {
            Length = length;
            Width = width;
            Height = height;
            CartonsQuantity = cartonsQuantity;
        }
    }
}
