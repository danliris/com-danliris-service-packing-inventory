using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.LogHistory
{
    public interface ILogHistoryService
    {
        Task<List<LogHistoryViewModel>> GetData(DateTime DateFrom, DateTime DateTo);
    }
}
