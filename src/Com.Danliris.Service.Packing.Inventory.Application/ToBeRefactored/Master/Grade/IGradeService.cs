using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Master.Grade
{
    public interface IGradeService
    {
        Task<int> Delete(int id);
        Task<int> Update(int id, GradeViewModel viewModel);
        Task<int> Create(GradeViewModel viewModel);
        Task<GradeViewModel> ReadById(int id);
        ListResult<GradeViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Upload(IEnumerable<GradeViewModel> data);
        Tuple<bool, List<object>> UploadValidate(IEnumerable<GradeViewModel> data);
        bool ValidateHeader(IEnumerable<string> headers);
        MemoryStream DownloadTemplate();
    }
}
