using om.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.CMT;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.AR.CashInBank;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.CashInBank
{
    public interface ICashInBankService
    {
        ReadResponse<CashInBankViewModel> Read(int page, int size, string order, List<string> select, string keyword, string filter);
        Task<int> CreateAsync(CashInBankModel model);
        Task<int> UploadExcelAsync(List<CashInBankModel> listData);
    }
}
