using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList
{
    public interface IGarmentPackingListService
    {
        Task<string> Create(GarmentPackingListViewModel viewModel);
        Task<GarmentPackingListViewModel> ReadById(int id);
        ListResult<GarmentPackingListViewModel> Read(int page, int size, string filter, string order, string keyword);
		ListResult<GarmentPackingListViewModel> ReadPLSample(int page, int size, string filter, string order, string keyword);
		IQueryable<GarmentPLItemViewModel> ReadPLSampleRO(int page, int size, string filter, string order, string keyword);
		IQueryable<GarmentPLDetailViewModel> ReadPLSampleStyle(  string roNo);

		ListResult<GarmentPackingListViewModel> ReadNotUsed(int page, int size, string filter, string order, string keyword);
        ListResult<GarmentPackingListViewModel> ReadNotUsedCostStructure(int page, int size, string filter, string order, string keyword);
        ReadResponse<dynamic> ReadSampleDelivered(int page, int size, string filter, string order, string keyword, string Select = "{}");
        //ListResult<GarmentPackingListViewModel> ReadSampleDelivered(int page, int size, string filter, string order, string keyword, string Select = "{}");
        Task<int> Update(int id, GarmentPackingListViewModel viewModel);
        Task<int> Delete(int id);
        Task<MemoryStreamResult> ReadPdfById(int id);
        Task<MemoryStreamResult> ReadWHPdfById(int id);
        Task<MemoryStreamResult> ReadWHSectionDPdfById(int id);
        Task<MemoryStreamResult> ReadPdfByOrderNo(int id);
        Task<MemoryStreamResult> ReadWHPdfByOrderNo(int id);
        Task<MemoryStreamResult> ReadWHSectionDPdfByOrderNo(int id);
        Task<MemoryStreamResult> ReadPdfFilterCarton(int id);
        Task<MemoryStreamResult> ReadPdfFilterCartonMD(int id);
        Task<GarmentPackingListViewModel> ReadByInvoiceNo(string no);
        Task SetPost(List<int> ids);
        Task SetUnpost(int id);
		Task SetUnpostDelivered(int id);
		Task SetApproveMd(int id, GarmentPackingListViewModel viewModel);
        Task SetApproveShipping(int id, GarmentPackingListViewModel viewModel);
        Task SetStatus(int id, GarmentPackingListStatusEnum status, string remark = null);
        Task<MemoryStreamResult> ReadExcelById(int id);
        Task<MemoryStreamResult> ReadExcelByIdFilterCarton(int id);
        Task SetSampleDelivered(List<int> ids);
        Task SetSampleExpenditureGood(string invoiceNo, bool isSampleExpenditureGood);
    }
}
