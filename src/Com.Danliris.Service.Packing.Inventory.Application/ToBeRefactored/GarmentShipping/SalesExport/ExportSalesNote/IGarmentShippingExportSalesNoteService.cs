using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.SalesExport;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.SalesExport
{
    public interface IGarmentShippingExportSalesNoteService
    {
        Task<int> Create(GarmentShippingExportSalesNoteViewModel viewModel);
        Task<GarmentShippingExportSalesNoteViewModel> ReadById(int id);
        ListResult<GarmentShippingExportSalesNoteViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentShippingExportSalesNoteViewModel viewModel);
        Task<int> Delete(int id);
        Buyer GetBuyer(int id);
        IQueryable<GarmentShippingExportSalesNoteViewModel> ReadShippingExportSalesNoteListNow(int month, int year);
        IQueryable<GarmentShippingExportSalesNoteViewModel> ReadExportSalesDebtor(string type, int month, int year);
        IQueryable<ExportSalesNoteFinanceReportViewModel> ReadSalesNoteForFinance(string type, int month, int year, string buyer);
        Task<int> ApproveShipping(int id);
        Task<int> ApproveFinance(int id);
        Task<int> RejectedFinance(int id, GarmentShippingExportSalesNoteViewModel viewModel);
        Task<int> RejectedShipping(int id, GarmentShippingExportSalesNoteViewModel viewModel);
    }
}
