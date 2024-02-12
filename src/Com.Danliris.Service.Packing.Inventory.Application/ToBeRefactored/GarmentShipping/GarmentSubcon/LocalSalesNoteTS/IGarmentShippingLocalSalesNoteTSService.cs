using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNoteTS
{
    public interface IGarmentShippingLocalSalesNoteTSService
    {
        Task<int> Create(GarmentShippingLocalSalesNoteTSViewModel viewModel);
        Task<GarmentShippingLocalSalesNoteTSViewModel> ReadById(int id);
        ListResult<GarmentShippingLocalSalesNoteTSViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentShippingLocalSalesNoteTSViewModel viewModel);
        Task<int> Delete(int id);
        //Buyer GetBuyer(string buyerCode);
        List<Buyer> GetBuyer(string buyerCode);
        List<BICurrency> GetBICurrency();

        Task<int> SetIsUsed(List<long> ids, bool isUsed);
        //IQueryable<GarmentShippingLocalSalesNoteTSViewModel> ReadShippingLocalSalesNoteListNow(int month, int year);
        //IQueryable<GarmentShippingLocalSalesNoteTSViewModel> ReadLocalSalesDebtor(string type, int month, int year);
        //IQueryable<LocalSalesNoteFinanceReportViewModel> ReadSalesNoteForFinance(string type, int month, int year, string buyer);
        //Task<int> ApproveShipping(int id);
        //Task<int> ApproveFinance(int id);
        //Task<int> RejectedFinance(int id, GarmentShippingLocalSalesNoteTSViewModel viewModel);
        //Task<int> RejectedShipping(int id, GarmentShippingLocalSalesNoteTSViewModel viewModel);
    }
}
