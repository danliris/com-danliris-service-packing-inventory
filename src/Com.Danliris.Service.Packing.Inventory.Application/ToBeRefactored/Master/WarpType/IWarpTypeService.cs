using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Master.WarpType
{
    public interface IWarpTypeService
    {
        Task<int> Delete(int id);
        Task<int> Update(int id, WarpTypeViewModel viewModel);
        Task<int> Create(WarpTypeViewModel viewModel);
        Task<WarpTypeViewModel> ReadById(int id);
        ListResult<WarpTypeViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Upload(IEnumerable<WarpTypeViewModel> data);
        Tuple<bool, List<object>> UploadValidate(IEnumerable<WarpTypeViewModel> data);
        bool ValidateHeader(IEnumerable<string> headers);
        MemoryStream DownloadTemplate();
    }
}
