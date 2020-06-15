using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Master.MaterialConstruction
{
    public interface IMaterialConstructionService
    {
        Task<int> Delete(int id);
        Task<int> Update(int id, MaterialConstructionViewModel viewModel);
        Task<int> Create(MaterialConstructionViewModel viewModel);
        Task<MaterialConstructionViewModel> ReadById(int id);
        ListResult<MaterialConstructionViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Upload(IEnumerable<MaterialConstructionViewModel> data);
        Tuple<bool, List<object>> UploadValidate(IEnumerable<MaterialConstructionViewModel> data);
        bool ValidateHeader(IEnumerable<string> headers);
        MemoryStream DownloadTemplate();
    }
}
