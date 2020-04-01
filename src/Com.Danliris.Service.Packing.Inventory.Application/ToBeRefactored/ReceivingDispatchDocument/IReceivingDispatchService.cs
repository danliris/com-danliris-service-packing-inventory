using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ReceivingDispatchDocument
{
    public interface IReceivingDispatchService
    {
        Task Receive(CreateReceivingDispatchDocumentViewModel viewModel);
        Task Dispatch(CreateReceivingDispatchDocumentViewModel viewModel);
    }
}
