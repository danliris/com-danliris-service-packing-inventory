using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPWovenType
{
    public interface IIPWovenTypeService
    {
        Task<int> Create(IPWovenTypeViewModel model);
        ListResult<IPWovenTypeViewModel> ReadAll();
        ListResult<IPWovenTypeViewModel> ReadByPage(string keyword, string order, int page, int size);
        Task<IPWovenTypeViewModel> ReadById(int id);
        Task<int> Update(int id, IPWovenTypeViewModel model);
        Task<int> Delete(int id);
    }
}
