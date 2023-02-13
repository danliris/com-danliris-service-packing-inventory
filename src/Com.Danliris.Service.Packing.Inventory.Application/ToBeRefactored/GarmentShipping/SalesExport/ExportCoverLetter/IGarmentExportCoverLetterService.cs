using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.SalesExport
{
    public interface IGarmentExportCoverLetterService
    {
        Task<int> Create(GarmentExportCoverLetterViewModel viewModel);
        Task<GarmentExportCoverLetterViewModel> ReadById(int id);
        Task<GarmentExportCoverLetterViewModel> ReadByExportSalesNoteId(int exportsalesnoteid);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentExportCoverLetterViewModel viewModel);
        Task<int> Delete(int id);
    }
}
