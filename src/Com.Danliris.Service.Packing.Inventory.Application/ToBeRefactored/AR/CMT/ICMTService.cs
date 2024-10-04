using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.AR.CMT;
using om.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.CMT;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.CMT
{
    public interface ICMTService
    {
        ReadResponse<CMTViewModel> Read(int page, int size, string order, List<string> select, string keyword, string filter);
        Task<int> CreateAsync(CMTModel model);
        Task<int> UploadExcelAsync(List<CMTModel> listData);
    }
}
