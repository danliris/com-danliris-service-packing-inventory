using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.SalesExport.ExportCoverLetter;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.SalesExport
{
    public interface IGarmentExportCoverLetterRepository : IRepository<GarmentShippingExportCoverLetterModel>
    {
        Task<GarmentShippingExportCoverLetterModel> ReadByExportSalesNoteIdAsync(int localsalesnoteid);
    }
}
