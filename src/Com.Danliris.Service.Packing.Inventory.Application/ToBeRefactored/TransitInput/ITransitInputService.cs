using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.TransitInput
{
    public interface ITransitInputService
    {
        Task<int> Create(TransitInputViewModel viewModel);
        Task<int> Update(int id, TransitInputViewModel viewModel);
        Task<int> Delete(int id);
        Task<TransitInputViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
    }
}
