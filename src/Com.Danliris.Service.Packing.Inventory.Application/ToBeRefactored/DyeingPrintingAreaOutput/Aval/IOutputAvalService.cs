using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System.IO;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaOutput.Aval
{
    public interface IOutputAvalService
    {
        Task<int> Create(OutputAvalViewModel viewModel);
        Task<OutputAvalViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<MemoryStream> GenerateExcel(int id);
    }
}
