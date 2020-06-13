using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.AmendLetterOfCredit
{
    public interface IGarmentAmendLetterOfCreditService
    {
        Task<int> Create(GarmentAmendLetterOfCreditViewModel viewModel);
        Task<GarmentAmendLetterOfCreditViewModel> ReadById(int id);
        ListResult<GarmentAmendLetterOfCreditViewModel> Read(int page, int size, string filter, string order, string keyword);
        Task<int> Update(int id, GarmentAmendLetterOfCreditViewModel viewModel);
        Task<int> Delete(int id);
    }
}
