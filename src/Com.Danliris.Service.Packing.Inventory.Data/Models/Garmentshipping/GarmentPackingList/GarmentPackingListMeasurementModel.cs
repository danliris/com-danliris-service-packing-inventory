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

        public void SetLength(double length, string userName, string userAgent)
        {
            if (Length != length)
            {
                Length = length;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetWidth(double width, string userName, string userAgent)
        {
            if (Width != width)
            {
                Width = width;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetHeight(double height, string userName, string userAgent)
        {
            if (Height != height)
            {
                Height = height;
                this.FlagForUpdate(userName, userAgent);
            }
        }

        public void SetCartonsQuantity(double cartonsQuantity, string userName, string userAgent)
        {
            if (CartonsQuantity != cartonsQuantity)
            {
                CartonsQuantity = cartonsQuantity;
                this.FlagForUpdate(userName, userAgent);
            }
        }
    }
}
