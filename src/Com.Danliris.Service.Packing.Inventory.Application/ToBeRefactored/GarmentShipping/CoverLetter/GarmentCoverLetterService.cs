using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CoverLetter
{
    public class GarmentCoverLetterService : IGarmentCoverLetterService
    {
        private readonly IGarmentCoverLetterRepository _repository;

        public GarmentCoverLetterService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentCoverLetterRepository>();
        }

        private GarmentCoverLetterViewModel MapToViewModel(GarmentCoverLetterModel model)
        {
            GarmentCoverLetterViewModel viewModel = new GarmentCoverLetterViewModel
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

                invoiceNo = model.InvoiceNo,
                date = model.Date,
                name = model.Name,
                address = model.Address,
                attn = model.ATTN,
                phone = model.Phone,
                bookingDate = model.BookingDate,

                order = model.Order,
                pcsQuantity = model.PCSQuantity,
                setsQuantity = model.SETSQuantity,
                packQuantity = model.PACKQuantity,
                cartoonQuantity = model.CartoonQuantity,
                forwarder = new Forwarder
                {
                    id = model.ForwarderId,
                    code = model.ForwarderCode,
                    name = model.ForwarderName,
                },
                truck = model.Truck,
                plateNumber = model.PlateNumber,
                driver = model.Driver,
                containerNo = model.ContainerNo,
                freight = model.Freight,
                shippingSeal = model.ShippingSeal,
                dlSeal = model.DLSeal,
                emklSeal = model.EMKLSeal,
                exportEstimationDate = model.ExportEstimationDate,
                unit = model.Unit,
                shippingStaff = model.ShippingStaff,
            };

            return viewModel;
        }

        public async Task<int> Create(GarmentCoverLetterViewModel viewModel)
        {
            viewModel.forwarder = viewModel.forwarder ?? new Forwarder();
            GarmentCoverLetterModel model = new GarmentCoverLetterModel(viewModel.packingListId, viewModel.invoiceNo, viewModel.date.GetValueOrDefault(), viewModel.name, viewModel.address, viewModel.attn, viewModel.phone, viewModel.bookingDate.GetValueOrDefault(), viewModel.order, viewModel.pcsQuantity, viewModel.setsQuantity, viewModel.packQuantity, viewModel.cartoonQuantity, viewModel.forwarder.id, viewModel.forwarder.code, viewModel.forwarder.name, viewModel.truck, viewModel.plateNumber, viewModel.driver, viewModel.containerNo, viewModel.freight, viewModel.shippingSeal, viewModel.dlSeal, viewModel.emklSeal, viewModel.exportEstimationDate.GetValueOrDefault(), viewModel.unit, viewModel.shippingStaff);

            return await _repository.InsertAsync(model);
        }

        public async Task<int> Delete(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();

            List<string> SearchAttributes = new List<string>() { "InvoiceNo", "Name", "Address", "ATTN", "Phone", "Order" };
            query = QueryHelper<GarmentCoverLetterModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentCoverLetterModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentCoverLetterModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => new IndexViewModel
                {
                    id = model.Id,
                    invoiceNo = model.InvoiceNo,
                    date = model.Date,
                    name = model.Name,
                    address = model.Address,
                    attn = model.ATTN,
                    phone = model.Phone,
                    bookingDate = model.BookingDate,
                    order = model.Order,
                })
                .ToList();

            return new ListResult<IndexViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentCoverLetterViewModel> ReadById(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentCoverLetterViewModel viewModel)
        {
            viewModel.forwarder = viewModel.forwarder ?? new Forwarder();
            GarmentCoverLetterModel model = new GarmentCoverLetterModel(viewModel.packingListId, viewModel.invoiceNo, viewModel.date.GetValueOrDefault(), viewModel.name, viewModel.address, viewModel.attn, viewModel.phone, viewModel.bookingDate.GetValueOrDefault(), viewModel.order, viewModel.pcsQuantity, viewModel.setsQuantity, viewModel.packQuantity, viewModel.cartoonQuantity, viewModel.forwarder.id, viewModel.forwarder.code, viewModel.forwarder.name, viewModel.truck, viewModel.plateNumber, viewModel.driver, viewModel.containerNo, viewModel.freight, viewModel.shippingSeal, viewModel.dlSeal, viewModel.emklSeal, viewModel.exportEstimationDate.GetValueOrDefault(), viewModel.unit, viewModel.shippingStaff);

            return await _repository.UpdateAsync(id, model);
        }
    }
}
