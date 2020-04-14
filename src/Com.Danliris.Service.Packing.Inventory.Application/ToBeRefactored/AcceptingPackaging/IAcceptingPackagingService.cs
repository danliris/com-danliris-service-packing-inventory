using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.AcceptingPackaging
{
    public interface IAcceptingPackagingService
    {
        Task<int> Create(AcceptingPackagingViewModel viewModel);
        Task<int> Update(int id, AcceptingPackagingViewModel viewModel);
        Task<int> Delete(int id);
        Task<AcceptingPackagingViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
    }
}
