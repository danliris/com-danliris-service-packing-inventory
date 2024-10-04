
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.DownPayment;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.AR.OmzetCorrectionsModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.OmzetCorrectionService
{
    public interface IOmzetCorrectionService
    {
        ReadResponse<DownPaymentList> Read(int page, int size, string order, List<string> select, string keyword, string filter);
        Task<int> CreateAsync(OmzetCorrectionModel model);
        Task<int> UploadExcelAsync(List<OmzetCorrectionModel> listData);
    }
}
