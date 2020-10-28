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
		ListResult<GarmentPackingListViewModel> ReadNotUsed(int page, int size, string filter, string order, string keyword);

		Task<int> Update(int id, GarmentPackingListViewModel viewModel);
        Task<int> Delete(int id);
        Task<ExcelResult> ReadPdfById(int id);
        Task<GarmentPackingListViewModel> ReadByInvoiceNo(string no);
        Task SetPost(List<int> ids);
        Task SetUnpost(int id);
        Task SetApproveMd(int id, GarmentPackingListViewModel viewModel);
        Task SetApproveShipping(int id, GarmentPackingListViewModel viewModel);
        Task SetStatus(int id, GarmentPackingListStatusEnum status, string remark = null);
    }
}
