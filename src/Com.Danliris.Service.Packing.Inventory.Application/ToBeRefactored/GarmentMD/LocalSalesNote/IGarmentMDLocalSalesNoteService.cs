using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentMD.LocalSalesNote.ViewModel;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentMD.ShippingLocalSalesNote
{
    public interface IGarmentMDLocalSalesNoteService
    {
        Task<int> Create(GarmentMDLocalSalesNoteViewModel viewModel);
        Task<GarmentMDLocalSalesNoteViewModel> ReadById(int id);
        ListResult<GarmentMDLocalSalesNoteViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentMDLocalSalesNoteViewModel viewModel);
        Task<int> Delete(int id);
        Buyer GetBuyer(int id);
        IQueryable<GarmentMDLocalSalesNoteViewModel> ReadShippingLocalSalesNoteListNow(int month, int year);
        //IQueryable<GarmentMDLocalSalesNoteViewModel> ReadLocalSalesDebtor(string type, int month, int year);
        //IQueryable<LocalSalesNoteFinanceReportViewModel> ReadSalesNoteForFinance(string type, int month, int year, string buyer);
        //Task<int> ApproveShipping(int id);
        //Task<int> ApproveFinance(int id);
        //Task<int> RejectedFinance(int id, GarmentMDLocalSalesNoteViewModel viewModel);
        //Task<int> RejectedShipping(int id, GarmentMDLocalSalesNoteViewModel viewModel);
    }
}
