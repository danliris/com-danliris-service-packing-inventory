using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalCoverLetterTS;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalCoverLetterTS;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.LogHistory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetterTS
{
    public class GarmentLocalCoverLetterTSService : IGarmentLocalCoverLetterTSService
    {
        private readonly IGarmentLocalCoverLetterTSRepository _repository;
        protected readonly ILogHistoryRepository logHistoryRepository;

        private readonly PackingInventoryDbContext dbContext;
        public GarmentLocalCoverLetterTSService(IServiceProvider serviceProvider, PackingInventoryDbContext dbContext)
        {
            _repository = serviceProvider.GetService<IGarmentLocalCoverLetterTSRepository>();
            logHistoryRepository = serviceProvider.GetService<ILogHistoryRepository>();

            this.dbContext = dbContext;
        }

        private GarmentLocalCoverLetterTSViewModel MapToViewModel(GarmentShippingLocalCoverLetterTSModel model)
        {
            GarmentLocalCoverLetterTSViewModel viewModel = new GarmentLocalCoverLetterTSViewModel
            {
                Active = model.Active,
                Id = model.Id,
                CreatedAgent = model.CreatedAgent,
                CreatedBy = model.CreatedBy,
                CreatedUtc = model.CreatedUtc,
                DeletedAgent = model.DeletedAgent,
                DeletedBy = model.DeletedBy,
                DeletedUtc = model.DeletedUtc,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedUtc = model.LastModifiedUtc,

                localSalesNoteId = model.LocalSalesNoteId,
                localCoverLetterNo = model.LocalCoverLetterNo,
                noteNo = model.NoteNo,
                date = model.Date,
                buyer = new Buyer
                {
                    Id = model.BuyerId,
                    Code = model.BuyerCode,
                    Name = model.BuyerName,
                    Address = model.BuyerAdddress
                },
                remark = model.Remark,
                bcNo = model.BCNo,
                bcdate = model.BCDate,
                truck = model.Truck,
                plateNumber = model.PlateNumber,
                driver = model.Driver,
                shippingStaff = new ShippingStaff
                {
                    id = model.ShippingStaffId,
                    name = model.ShippingStaffName
                }
            };

            return viewModel;
        }

        public async Task<int> Create(GarmentLocalCoverLetterTSViewModel viewModel)
        {
            var Created = 0;
            using (var transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    viewModel.buyer = viewModel.buyer ?? new Buyer();
                    viewModel.shippingStaff = viewModel.shippingStaff ?? new ShippingStaff();
                    GarmentShippingLocalCoverLetterTSModel model = new GarmentShippingLocalCoverLetterTSModel(viewModel.localSalesNoteId, viewModel.noteNo, GenerateNo(), viewModel.date.GetValueOrDefault(), viewModel.buyer.Id, viewModel.buyer.Code, viewModel.buyer.Name, viewModel.buyer.Address, viewModel.remark, viewModel.bcNo, viewModel.bcdate.GetValueOrDefault(), viewModel.truck, viewModel.plateNumber, viewModel.driver, viewModel.shippingStaff.id, viewModel.shippingStaff.name);

                    //Add Log History
                    Created += await logHistoryRepository.InsertAsync("SHIPPING", "Create Surat Pengantar Lokal - (Terima Subkon) - " + model.LocalCoverLetterNo);

                    Created += await _repository.InsertAsync(model);
                    transaction.Commit();

                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return Created;
        }

        private string GenerateNo()
        {
            var year = DateTime.Now.ToString("yy");

            var prefix = $"{year}/SPDL /";

            var lastInvoiceNo = _repository.ReadAll().Where(w => w.LocalCoverLetterNo.StartsWith(prefix))
                .OrderByDescending(o => o.LocalCoverLetterNo)
                .Select(s => int.Parse(s.LocalCoverLetterNo.Replace(prefix, "")))
                .FirstOrDefault();
            var invoiceNo = $"{prefix}{(lastInvoiceNo + 1).ToString("D5")}";

            return invoiceNo;
        }

        public async Task<int> Delete(int id)
        {
            var Deleted = 0;
            using (var transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    var data = await _repository.ReadByIdAsync(id);
                    //Add Log History
                    Deleted += await logHistoryRepository.InsertAsync("SHIPPING", "Delete Surat Pengantar Lokal - (Terima Subkon)  - " + data.LocalCoverLetterNo);

                    Deleted += await _repository.DeleteAsync(id);

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return Deleted;
                
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();

            List<string> SearchAttributes = new List<string>() { "NoteNo", "BuyerCode", "BuyerName","LocalCoverLetterNo" };
            query = QueryHelper<GarmentShippingLocalCoverLetterTSModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingLocalCoverLetterTSModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingLocalCoverLetterTSModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => new IndexViewModel
                {
                    id = model.Id,
                    noteNo = model.NoteNo,
                    localCoverLetterNo=model.LocalCoverLetterNo,
                    date = model.Date,
                    buyer = new Buyer
                    {
                        Code = model.BuyerCode,
                        Name = model.BuyerName
                    },
                    bcNo = model.BCNo,
                    bcDate = model.BCDate
                })
                .ToList();

            return new ListResult<IndexViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentLocalCoverLetterTSViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<GarmentLocalCoverLetterTSViewModel> ReadByLocalSalesNoteId(int localsalesnoteid)
        {
            var data = await _repository.ReadByLocalSalesNoteIdAsync(localsalesnoteid);
            var viewModel = data == null ? null : MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentLocalCoverLetterTSViewModel viewModel)
        {
            var Updated = 0;
            using (var transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    viewModel.buyer = viewModel.buyer ?? new Buyer();
                    viewModel.shippingStaff = viewModel.shippingStaff ?? new ShippingStaff();
                    GarmentShippingLocalCoverLetterTSModel model = new GarmentShippingLocalCoverLetterTSModel(viewModel.localSalesNoteId, viewModel.noteNo, viewModel.localCoverLetterNo, viewModel.date.GetValueOrDefault(), viewModel.buyer.Id, viewModel.buyer.Code, viewModel.buyer.Name, viewModel.buyer.Address, viewModel.remark, viewModel.bcNo, viewModel.bcdate.GetValueOrDefault(), viewModel.truck, viewModel.plateNumber, viewModel.driver, viewModel.shippingStaff.id, viewModel.shippingStaff.name);

                    //Add Log History
                    Updated += await logHistoryRepository.InsertAsync("SHIPPING", "Update Surat Pengantar Lokal - (Terima Subkon)  - " + model.LocalCoverLetterNo);

                    Updated += await _repository.UpdateAsync(id, model);

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return Updated;
           
        }
    }
}
