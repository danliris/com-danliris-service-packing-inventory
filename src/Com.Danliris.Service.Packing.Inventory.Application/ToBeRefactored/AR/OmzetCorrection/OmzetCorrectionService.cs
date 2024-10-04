using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.DownPayment;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.AR.OmzetCorrectionsModel;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AR.OmzetCorrectionService
{
    public class OmzetCorrectionService : IOmzetCorrectionService
    {
        private readonly PackingInventoryDbContext _dbContext;
        private readonly IIdentityProvider _identityService;
        private readonly List<string> _alphabets;
        private const string UserAgent = "finance-service";

        public OmzetCorrectionService(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _identityService = serviceProvider.GetService<IIdentityProvider>();
        }

        public ReadResponse<DownPaymentList> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            var query = _dbContext.AR_OmzetCorrections.AsQueryable();

            var searchAttributes = new List<string>()
            {
                "MemoNo",
                "InvoiceNo",
            };

            query = QueryHelper<OmzetCorrectionModel>.Search(query, searchAttributes, keyword);

            var filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<OmzetCorrectionModel>.Filter(query, filterDictionary);

            var orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<OmzetCorrectionModel>.Order(query, orderDictionary);

            var pageable = new Pageable<OmzetCorrectionModel>(query, page - 1, size);
            var data = pageable.Data.Select(entity => new DownPaymentList()
            {
                MemoNo = entity.MemoNo,
                Remark = entity.Remark,
                InvoiceNo = entity.InvoiceNo,
                BuyerCode = entity.BuyerCode,
                Amount =  entity.Amount,
                Kurs = entity.Kurs,
                TotalAmount = entity.TotalAmount

            }).ToList();

            int totalData = pageable.TotalCount;

            return new ReadResponse<DownPaymentList>(data, totalData, page, size);
        }

        public Task<int> CreateAsync(OmzetCorrectionModel model)
        {
            //model.DocumentNo = GetDownPaymentNo(model);

            model.FlagForCreate(_identityService.Username, UserAgent);

            _dbContext.AR_OmzetCorrections.Add(model);

            return _dbContext.SaveChangesAsync();
        }

        public async Task<int> UploadExcelAsync(List<OmzetCorrectionModel> listData)
        {
            int created = 0;
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var omzetCorrection in listData)
                    {
                        omzetCorrection.FlagForCreate(_identityService.Username, UserAgent);
                        _dbContext.AR_OmzetCorrections.Add(omzetCorrection);
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
