using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPWidthType
{
    public interface IIPWidthTypeService
    {
        Task<int> Create(IPWidthTypeViewModel model);
        ListResult<IndexViewModel> ReadAll();
        ListResult<IndexViewModel> ReadByPage(string keyword, string order, int page, int size);
        Task<IPWidthTypeViewModel> ReadById(int id);
        Task<int> Update(int id, IPWidthTypeViewModel model);
        Task<int> Delete(int id);
    }
}
