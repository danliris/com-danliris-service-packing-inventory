using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.AR_ReportMutation
{
    public interface IAR_ReportMutationService
    {
        Task<MemoryStream> GetExcel();
    }
}
