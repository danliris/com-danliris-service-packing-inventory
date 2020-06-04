using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;

namespace Com.Danliris.Service.Packing.Inventory.Application.DTOs
{
    public class UnitOfMeasurementDto
    {
        public UnitOfMeasurementDto(UnitOfMeasurementModel uom)
        {
            Id = uom.Id;
            Unit = uom.Unit;
        }

        public int Id { get; }
        public string Unit { get; }
    }
}