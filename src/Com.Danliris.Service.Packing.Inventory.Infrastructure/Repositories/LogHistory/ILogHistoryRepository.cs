using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.LogHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.LogHistory
{
    public interface ILogHistoryRepository 
    {
        Task<int> InsertAsync(string division, string activity);
        IQueryable<LogHistoryModel> ReadAll();

        }
}
