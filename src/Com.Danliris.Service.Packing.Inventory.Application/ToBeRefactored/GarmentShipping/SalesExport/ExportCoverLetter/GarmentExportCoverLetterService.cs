using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.SalesExport;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.SalesExport;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.SalesExport.ExportCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.LogHistory;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.SalesExport
{
    public class GarmentExportCoverLetterService : IGarmentExportCoverLetterService
    {
        private readonly IGarmentExportCoverLetterRepository _repository;
        protected readonly ILogHistoryRepository logHistoryRepository;
        public GarmentExportCoverLetterService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentExportCoverLetterRepository>();
            logHistoryRepository = serviceProvider.GetService<ILogHistoryRepository>();
        }

        private GarmentExportCoverLetterViewModel MapToViewModel(GarmentShippingExportCoverLetterModel model)
        {
            GarmentExportCoverLetterViewModel viewModel = new GarmentExportCoverLetterViewModel
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

                exportSalesNoteId = model.ExportSalesNoteId,
                exportCoverLetterNo = model.ExportCoverLetterNo,
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

        public async Task<int> Create(GarmentExportCoverLetterViewModel viewModel)
        {
            viewModel.buyer = viewModel.buyer ?? new Buyer();
            viewModel.shippingStaff = viewModel.shippingStaff ?? new ShippingStaff();
            GarmentShippingExportCoverLetterModel model = new GarmentShippingExportCoverLetterModel(viewModel.exportSalesNoteId, viewModel.noteNo,GenerateNo(), viewModel.date.GetValueOrDefault(), viewModel.buyer.Id, viewModel.buyer.Code, viewModel.buyer.Name, viewModel.buyer.Address, viewModel.remark, viewModel.bcNo, viewModel.bcdate.GetValueOrDefault(), viewModel.truck, viewModel.plateNumber, viewModel.driver, viewModel.shippingStaff.id, viewModel.shippingStaff.name);

            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Create Surat Pengantar Export - " + model.ExportCoverLetterNo);

            return await _repository.InsertAsync(model);
        }

        private string GenerateNo()
        {
            var year = DateTime.Now.ToString("yy");

            var prefix = $"{year}/SPDL /";

            var lastInvoiceNo = _repository.ReadAll().Where(w => w.ExportCoverLetterNo.StartsWith(prefix))
                .OrderByDescending(o => o.ExportCoverLetterNo)
                .Select(s => int.Parse(s.ExportCoverLetterNo.Replace(prefix, "")))
                .FirstOrDefault();
            var invoiceNo = $"{prefix}{(lastInvoiceNo + 1).ToString("D5")}";

            return invoiceNo;
        }

        public async Task<int> Delete(int id)
        {
            var data = await _repository.ReadByIdAsync(id);

            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Delete Surat Pengantar Export - " + data.ExportCoverLetterNo);

            return await _repository.DeleteAsync(id);
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();

            List<string> SearchAttributes = new List<string>() { "NoteNo", "BuyerCode", "BuyerName","ExportCoverLetterNo" };
            query = QueryHelper<GarmentShippingExportCoverLetterModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingExportCoverLetterModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingExportCoverLetterModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => new IndexViewModel
                {
                    id = model.Id,
                    noteNo = model.NoteNo,
                    exportCoverLetterNo=model.ExportCoverLetterNo,
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

        public async Task<GarmentExportCoverLetterViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<GarmentExportCoverLetterViewModel> ReadByExportSalesNoteId(int exportsalesnoteid)
        {
            var data = await _repository.ReadByExportSalesNoteIdAsync(exportsalesnoteid);
            var viewModel = data == null ? null : MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentExportCoverLetterViewModel viewModel)
        {
            viewModel.buyer = viewModel.buyer ?? new Buyer();
            viewModel.shippingStaff = viewModel.shippingStaff ?? new ShippingStaff();
            GarmentShippingExportCoverLetterModel model = new GarmentShippingExportCoverLetterModel(viewModel.exportSalesNoteId, viewModel.noteNo, viewModel.exportCoverLetterNo, viewModel.date.GetValueOrDefault(), viewModel.buyer.Id, viewModel.buyer.Code, viewModel.buyer.Name, viewModel.buyer.Address, viewModel.remark, viewModel.bcNo, viewModel.bcdate.GetValueOrDefault(), viewModel.truck, viewModel.plateNumber, viewModel.driver, viewModel.shippingStaff.id, viewModel.shippingStaff.name);

            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Update Surat Pengantar Export - " + model.ExportCoverLetterNo);

            return await _repository.UpdateAsync(id, model);
        }
    }
}
