using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Master.WeftType
{
    public interface IWeftTypeService
    {
        Task<int> Delete(int id);
        Task<int> Update(int id, WeftTypeViewModel viewModel);
        Task<int> Create(WeftTypeViewModel viewModel);
        Task<WeftTypeViewModel> ReadById(int id);
        ListResult<WeftTypeViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Upload(IEnumerable<WeftTypeViewModel> data);
        Tuple<bool, List<object>> UploadValidate(IEnumerable<WeftTypeViewModel> data);
        bool ValidateHeader(IEnumerable<string> headers);
        MemoryStream DownloadTemplate();
    }
}
