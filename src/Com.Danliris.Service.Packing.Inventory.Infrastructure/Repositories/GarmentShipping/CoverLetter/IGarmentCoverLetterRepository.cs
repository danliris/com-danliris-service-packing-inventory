using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CoverLetter;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CoverLetter
{
    public interface IGarmentCoverLetterRepository : IRepository<GarmentShippingCoverLetterModel>
    {
        Task<GarmentShippingCoverLetterModel> ReadByInvoiceIdAsync(int invoiceid);
    }
}
