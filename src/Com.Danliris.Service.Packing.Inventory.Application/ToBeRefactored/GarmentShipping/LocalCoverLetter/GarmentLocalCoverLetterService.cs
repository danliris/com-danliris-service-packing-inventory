using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalCoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetter
{
    public class GarmentLocalCoverLetterService : IGarmentLocalCoverLetterService
    {
        private readonly IGarmentLocalCoverLetterRepository _repository;

        public GarmentLocalCoverLetterService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentLocalCoverLetterRepository>();
        }

        private GarmentLocalCoverLetterViewModel MapToViewModel(GarmentShippingLocalCoverLetterModel model)
        {
            GarmentLocalCoverLetterViewModel viewModel = new GarmentLocalCoverLetterViewModel
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

        public async Task<int> Create(GarmentLocalCoverLetterViewModel viewModel)
        {
            viewModel.buyer = viewModel.buyer ?? new Buyer();
            viewModel.shippingStaff = viewModel.shippingStaff ?? new ShippingStaff();
            GarmentShippingLocalCoverLetterModel model = new GarmentShippingLocalCoverLetterModel(viewModel.localSalesNoteId, viewModel.noteNo,GenerateNo(), viewModel.date.GetValueOrDefault(), viewModel.buyer.Id, viewModel.buyer.Code, viewModel.buyer.Name, viewModel.buyer.Address, viewModel.remark, viewModel.bcNo, viewModel.bcdate.GetValueOrDefault(), viewModel.truck, viewModel.plateNumber, viewModel.driver, viewModel.shippingStaff.id, viewModel.shippingStaff.name);

            return await _repository.InsertAsync(model);
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
            return await _repository.DeleteAsync(id);
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();

            List<string> SearchAttributes = new List<string>() { "NoteNo", "BuyerCode", "BuyerName","LocalCoverLetterNo" };
            query = QueryHelper<GarmentShippingLocalCoverLetterModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingLocalCoverLetterModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingLocalCoverLetterModel>.Order(query, OrderDictionary);

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
                    }
                })
                .ToList();

            return new ListResult<IndexViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentLocalCoverLetterViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<GarmentLocalCoverLetterViewModel> ReadByLocalSalesNoteId(int localsalesnoteid)
        {
            var data = await _repository.ReadByLocalSalesNoteIdAsync(localsalesnoteid);
            var viewModel = data == null ? null : MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentLocalCoverLetterViewModel viewModel)
        {
            viewModel.buyer = viewModel.buyer ?? new Buyer();
            viewModel.shippingStaff = viewModel.shippingStaff ?? new ShippingStaff();
            GarmentShippingLocalCoverLetterModel model = new GarmentShippingLocalCoverLetterModel(viewModel.localSalesNoteId, viewModel.noteNo, viewModel.localCoverLetterNo, viewModel.date.GetValueOrDefault(), viewModel.buyer.Id, viewModel.buyer.Code, viewModel.buyer.Name, viewModel.buyer.Address, viewModel.remark, viewModel.bcNo, viewModel.bcdate.GetValueOrDefault(), viewModel.truck, viewModel.plateNumber, viewModel.driver, viewModel.shippingStaff.id, viewModel.shippingStaff.name);

            return await _repository.UpdateAsync(id, model);
        }
    }
}
