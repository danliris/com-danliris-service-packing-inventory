using om.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.CMT;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.AR.CMT;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.CMT
{
    public class CMTService : ICMTService
    {
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityService;
        private const string UserAgent = "finance-service";

        public CMTService(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _identityService = serviceProvider.GetService<IIdentityProvider>();
        }

        public ReadResponse<CMTViewModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            var query = _dbContext.AR_CMTs.AsQueryable();

            var searchAttributes = new List<string>()
            {
                "InvoiceNo",
                "ExpenditureGoodNo",
                "RONo",
            };

            query = QueryHelper<CMTModel>.Search(query, searchAttributes, keyword);

            var filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<CMTModel>.Filter(query, filterDictionary);

            var orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<CMTModel>.Order(query, orderDictionary);

            var pageable = new Pageable<CMTModel>(query, page - 1, size);
            var data = pageable.Data.Select(entity => new CMTViewModel()
            {
                InvoiceNo = entity.InvoiceNo,
                TruckingDate = entity.TruckingDate,
                PEBDate = entity.PEBDate,
                ExpenditureGoodNo = entity.ExpenditureGoodNo,
                RONo = entity.RONo,
                Quantity = entity.Quantity,
                Amount = entity.Amount,
                Kurs = entity.Kurs,
                TotalAmount = entity.TotalAmount,
                Month = entity.Month

            }).ToList();

            int totalData = pageable.TotalCount;

            return new ReadResponse<CMTViewModel>(data, totalData, page,size);
        }

        public Task<int> CreateAsync(CMTModel model)
        {

            model.FlagForCreate(_identityService.Username, UserAgent);

            _dbContext.AR_CMTs.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public async Task<int> UploadExcelAsync(List<CMTModel> listData)
        {
            int created = 0;
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var data in listData)
                    {
                        data.FlagForCreate(_identityService.Username, UserAgent);
                        _dbContext.AR_CMTs.Add(data);
                        created += await _dbContext.SaveChangesAsync();
                    }

                    transaction.Commit();
                    return created;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }

        }
    }
}
