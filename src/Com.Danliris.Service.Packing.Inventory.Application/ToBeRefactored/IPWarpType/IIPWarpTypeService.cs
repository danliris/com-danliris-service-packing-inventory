using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPWarpType
{
    public interface IIPWarpTypeService
    {
        Task<int> Create(IPWarpTypeViewModel model);
        ListResult<IPWarpTypeViewModel> ReadAll();
        ListResult<IPWarpTypeViewModel> ReadByPage(string keyword, string order, int page, int size);
        Task<IPWarpTypeViewModel> ReadById(int id);
        Task<int> Update(int id, IPWarpTypeViewModel model);
        Task<int> Delete(int id);
    }
}
