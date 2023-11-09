using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingPackingList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingPackingList
{
    public interface IGarmentShippingPackingListService
    {
        GarmentShippingPackingListViewModel MapToViewModel(GarmentShippingPackingListModel model);
        Task<string> Create(GarmentShippingPackingListViewModel viewModel);
        Task<GarmentShippingPackingListViewModel> ReadById(int id);
        ListResult<GarmentShippingPackingListViewModel> Read(int page, int size, string filter, string order, string keyword);
        ListResult<GarmentShippingPackingListViewModel> ReadNotUsed(int page, int size, string filter, string order, string keyword);
        ListResult<GarmentShippingPackingListViewModel> ReadNotUsedCostStructure(int page, int size, string filter, string order, string keyword);
        ReadResponse<dynamic> ReadSampleDelivered(int page, int size, string filter, string order, string keyword, string Select = "{}");
        //ListResult<GarmentPackingListViewModel> ReadSampleDelivered(int page, int size, string filter, string order, string keyword, string Select = "{}");
        Task<int> Update(int id, GarmentShippingPackingListViewModel viewModel);
        Task<int> Delete(int id);
        Task<MemoryStreamResult> ReadPdfById(int id);
        Task<MemoryStreamResult> ReadWHPdfById(int id);
        Task<MemoryStreamResult> ReadWHSectionDPdfById(int id);
        Task<MemoryStreamResult> ReadPdfByOrderNo(int id);
        Task<MemoryStreamResult> ReadWHPdfByOrderNo(int id);
        Task<MemoryStreamResult> ReadWHSectionDPdfByOrderNo(int id);
        //Task<MemoryStreamResult> ReadPdfFilterCarton(int id);
        //Task<MemoryStreamResult> ReadPdfFilterCartonMD(int id);
        //Task<GarmentShippingPackingListViewModel> ReadByInvoiceNo(string no);
        Task SetPost(List<int> ids);
        Task SetUnpost(int id);
		Task SetUnpostDelivered(int id);
		Task SetApproveMd(int id, GarmentShippingPackingListViewModel viewModel);
        Task SetApproveShipping(int id, GarmentShippingPackingListViewModel viewModel);
        Task SetStatus(int id, GarmentShippingPackingListStatusEnum status, string remark = null);
        Task<MemoryStreamResult> ReadExcelById(int id);
        //Task<MemoryStreamResult> ReadExcelByIdFilterCarton(int id);
        Task SetSampleDelivered(List<int> ids);
        Task SetSampleExpenditureGood(string invoiceNo, bool isSampleExpenditureGood);
    }
}
