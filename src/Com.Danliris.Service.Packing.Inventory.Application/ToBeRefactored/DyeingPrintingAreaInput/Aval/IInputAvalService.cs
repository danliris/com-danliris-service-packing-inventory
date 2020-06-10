using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Aval
{
    public interface IInputAvalService
    {
        Task<int> Create(InputAvalViewModel viewModel);
        Task<InputAvalViewModel> ReadById(int id);
        ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword);
        ListResult<PreAvalIndexViewModel> ReadOutputPreAval(DateTimeOffset searchDate, 
                                                            string searchShift, 
                                                            string searchGroup, 
                                                            int page, 
                                                            int size, 
                                                            string filter, 
                                                            string order, 
                                                            string keyword);
        ListResult<PreAvalIndexViewModel> ReadAllOutputPreAval(
                                                            int page,
                                                            int size,
                                                            string filter,
                                                            string order,
                                                            string keyword);
        Task<int> Reject(InputAvalViewModel viewModel);
    }
}
