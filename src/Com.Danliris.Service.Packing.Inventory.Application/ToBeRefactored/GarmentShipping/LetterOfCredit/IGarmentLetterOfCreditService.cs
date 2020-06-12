using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LetterOfCredit
{
    public interface IGarmentLetterOfCreditService
    {
        Task<int> Create(GarmentLetterOfCreditViewModel viewModel);
        Task<GarmentLetterOfCreditViewModel> ReadById(int id);
        ListResult<GarmentLetterOfCreditViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentLetterOfCreditViewModel viewModel);
        Task<int> Delete(int id);
    }
}
