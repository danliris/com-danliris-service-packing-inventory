using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice
{
	public interface IGarmentShippingInvoiceService
	{
		Task<int> Create(GarmentShippingInvoiceViewModel viewModel);
		Task<GarmentShippingInvoiceViewModel> ReadById(int id);
		ListResult<GarmentShippingInvoiceViewModel> Read(int page, int size, string filter, string order, string keyword);
		Task<int> Update(int id, GarmentShippingInvoiceViewModel viewModel);
		Task<int> Delete(int id);
        Buyer GetBuyer(int id);
        BankAccount GetBank(int id);
    }
}
