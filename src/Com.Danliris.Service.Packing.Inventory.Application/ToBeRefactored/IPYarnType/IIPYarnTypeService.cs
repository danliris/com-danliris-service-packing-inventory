using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPYarnType
{
    public interface IIPYarnTypeService
    {
        Task<int> Create(IPYarnTypeViewModel model);
        ListResult<IPYarnTypeViewModel> ReadAll();
        ListResult<IPYarnTypeViewModel> ReadByPage(string keyword, string order, int page, int size);
        Task<IPYarnTypeViewModel> ReadById(int id);
        Task<int> Update(int id, IPYarnTypeViewModel model);
        Task<int> Delete(int id);
    }
}
