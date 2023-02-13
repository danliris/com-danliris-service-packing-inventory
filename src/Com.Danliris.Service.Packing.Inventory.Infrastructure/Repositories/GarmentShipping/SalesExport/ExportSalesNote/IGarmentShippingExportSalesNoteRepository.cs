using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.SalesExport;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.SalesExport
{
    public interface IGarmentShippingExportSalesNoteRepository : IRepository<GarmentShippingExportSalesNoteModel>
    {
        Task<int> ApproveShippingAsync(int id);
        Task<int> ApproveFinanceAsync(int id);
        Task<int> RejectFinanceAsync(int id, GarmentShippingExportSalesNoteModel model);
        Task<int> RejectShippingAsync(int id, GarmentShippingExportSalesNoteModel model);
    }
}
