using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.LogHistory;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.LogHistory
{
    public class LogHistoryService : ILogHistoryService
    {
        private readonly ILogHistoryRepository _repos;

        public LogHistoryService(IServiceProvider serviceProvider)
        {
            _repos = serviceProvider.GetService<ILogHistoryRepository>();
        }

        public async Task<List<LogHistoryViewModel>> GetData(DateTime DateFrom, DateTime DateTo)
        {
            var query = await _repos.ReadAll()
                .Where(x => x.CreatedDate.AddHours(7).Date >= DateFrom.Date && x.CreatedDate.AddHours(7).Date <= DateTo.Date)
                .Select(x => new LogHistoryViewModel
                {
                    Activity = x.Activity,
                    Division = x.Division,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate.AddHours(7)
                }).ToListAsync();

            return query;
        }
    }
}
