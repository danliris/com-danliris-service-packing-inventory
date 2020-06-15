using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.IO;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application
{
    public interface IMaterialDeliveryNoteWeavingService
    {

        Task Create(MaterialDeliveryNoteWeavingViewModel viewModel);
        Task Update(int id, MaterialDeliveryNoteWeavingViewModel viewModel);
        Task Delete(int id);
        Task<MaterialDeliveryNoteWeavingViewModel> ReadById(int id);

        //Task<MemoryStream> GetPdfById(int id);

        ListResult<MaterialDeliveryNoteWeavingViewModel> ReadByKeyword(string keyword, string order, int page, int size, string filter);
    }
}