using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.InspectionMaterial
{
    public interface IInspectionMaterialService
    {
        Task<int> Create(InspectionMaterialViewModel viewModel);
        Task<int> Update(int id, InspectionMaterialViewModel viewModel);
        Task<int> Delete(int id);
        Task<InspectionMaterialViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
    }
}
