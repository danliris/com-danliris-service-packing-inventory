using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.MaterialDeliveryNote
{
    public interface IMaterialDeliveryNoteService
    {
        Task Create(MaterialDeliveryNoteViewModel viewModel);
        Task Update(int id, MaterialDeliveryNoteViewModel viewModel);
        Task Delete(int id);
        Task<MaterialDeliveryNoteViewModel> ReadById(int id);
        ListResult<MaterialDeliveryNoteViewModel> ReadByKeyword(string keyword, string order, int page, int size, string filter);
    }
}