using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPProcessType
{
    public interface IIPProcessTypeService
    {
        Task<int> Create(IPProcessTypeViewModel model);
        ListResult<IPProcessTypeViewModel> ReadAll();
        ListResult<IPProcessTypeViewModel> ReadByPage(string keyword, string order, int page, int size);
        Task<IPProcessTypeViewModel> ReadById(int id);
        Task<int> Update(int id, IPProcessTypeViewModel model);
        Task<int> Delete(int id);
    }
}
