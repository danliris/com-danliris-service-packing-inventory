using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.CoverLetter;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.LogHistory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
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
        private const string UserAgent = "GarmentCoverLetterService";

        private readonly IGarmentCoverLetterRepository _repository;
        private readonly IGarmentPackingListRepository _packingListrepository;

        private readonly IIdentityProvider _identityProvider;
        protected readonly ILogHistoryRepository logHistoryRepository;
        public GarmentCoverLetterService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetService<IGarmentCoverLetterRepository>();
            _packingListrepository = serviceProvider.GetService<IGarmentPackingListRepository>();

            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            logHistoryRepository = serviceProvider.GetService<ILogHistoryRepository>();
        }

        private GarmentCoverLetterViewModel MapToViewModel(GarmentShippingCoverLetterModel model)
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

                packingListId = model.PackingListId,
                invoiceId = model.InvoiceId,
                invoiceNo = model.InvoiceNo,
                date = model.Date,
                emkl=new EMKL
                {
                    Name=model.Name,
                    Id=model.EMKLId,
                    Code=model.EMKLCode,
                    //attn=model.ATTN,
                    //address=model.Address,
                    //phone=model.Phone
                },
                destination = model.Destination,
                address = model.Address,
                pic = model.PIC,
                attn = model.ATTN,
                phone = model.Phone,
                bookingDate = model.BookingDate,

                order = new Buyer
                {
                    Id = model.OrderId,
                    Code = model.OrderCode,
                    Name = model.OrderName
                },
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
                shippingStaff = new ShippingStaff
                {
                    id = model.ShippingStaffId,
                    name = model.ShippingStaffName
                }
            };

            return viewModel;
        }

        public async Task<int> Create(GarmentCoverLetterViewModel viewModel)
        {
            viewModel.order = viewModel.order ?? new Buyer();
            viewModel.forwarder = viewModel.forwarder ?? new Forwarder();
            viewModel.shippingStaff = viewModel.shippingStaff ?? new ShippingStaff();
            viewModel.emkl = viewModel.emkl ?? new EMKL();
            GarmentShippingCoverLetterModel model = new GarmentShippingCoverLetterModel(viewModel.packingListId, viewModel.invoiceId, viewModel.invoiceNo, viewModel.date.GetValueOrDefault(),viewModel.emkl.Id, viewModel.emkl.Code, viewModel.emkl.Name, viewModel.destination, viewModel.address, viewModel.pic, viewModel.attn, viewModel.phone, viewModel.bookingDate.GetValueOrDefault(), viewModel.order.Id, viewModel.order.Code, viewModel.order.Name, viewModel.pcsQuantity, viewModel.setsQuantity, viewModel.packQuantity, viewModel.cartoonQuantity, viewModel.forwarder.id, viewModel.forwarder.code, viewModel.forwarder.name, viewModel.truck, viewModel.plateNumber, viewModel.driver, viewModel.containerNo, viewModel.freight, viewModel.shippingSeal, viewModel.dlSeal, viewModel.emklSeal, viewModel.exportEstimationDate.GetValueOrDefault(), viewModel.unit, viewModel.shippingStaff.id, viewModel.shippingStaff.name);

            var packingList = _packingListrepository.Query.SingleOrDefault(s => s.Id == model.PackingListId);
            var status = GarmentPackingListStatusEnum.DELIVERED;
            //&& packingList.Status != status
            if (packingList != null )
            {
                packingList.SetStatus(status, _identityProvider.Username, UserAgent);
                packingList.StatusActivities.Add(new GarmentPackingListStatusActivityModel(_identityProvider.Username, UserAgent, status.ToString()));

                //Add Log History
                await logHistoryRepository.InsertAsync("SHIPPING", "Create Surat Pengantar - " + model.InvoiceNo);
            }
            else
            {
                throw new Exception("Packing List " + model.PackingListId + " not found");
            }

            return await _repository.InsertAsync(model);
        }

        public async Task<int> Delete(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            var packingList = _packingListrepository.Query.Include(i => i.StatusActivities).SingleOrDefault(s => s.Id == data.PackingListId);
            if (packingList != null)
            {
                var usedCount = _repository.ReadAll().Count(w => w.Id != id && w.PackingListId == packingList.Id);
                if (usedCount == 0)
                {
                    var statusActivity = packingList.StatusActivities.LastOrDefault(s => s.Status != GarmentPackingListStatusEnum.DELIVERED.ToString());
                    Enum.TryParse(statusActivity.Status, true, out GarmentPackingListStatusEnum statusParsed);
                    var status = statusActivity != null ? statusParsed : GarmentPackingListStatusEnum.CREATED;
                    packingList.SetStatus(status, _identityProvider.Username, UserAgent);
                    packingList.StatusActivities.Add(new GarmentPackingListStatusActivityModel(_identityProvider.Username, UserAgent, status.ToString()));
                }

                //Add Log History
                await logHistoryRepository.InsertAsync("SHIPPING", "Delete Surat Pengantar - " + data.InvoiceNo);
            }
            else
            {
                throw new Exception("Packing List " + data.PackingListId + " not found");
            }

            return await _repository.DeleteAsync(id);
        }

        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _repository.ReadAll();

            List<string> SearchAttributes = new List<string>() { "InvoiceNo", "Name", "Address", "ATTN", "Phone", "OrderName" };
            query = QueryHelper<GarmentShippingCoverLetterModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingCoverLetterModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingCoverLetterModel>.Order(query, OrderDictionary);

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
                    orderName = model.OrderName,
                    destination=model.Destination,
                    cartoonQuantity=model.CartoonQuantity
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

        public async Task<GarmentCoverLetterViewModel> ReadByInvoiceId(int invoiceId)
        {
            var data = await _repository.ReadByInvoiceIdAsync(invoiceId);
            if (data != null)
            {
                return MapToViewModel(data);
            }
            else
            {
                return new GarmentCoverLetterViewModel();
            }
        }

        public async Task<int> Update(int id, GarmentCoverLetterViewModel viewModel)
        {
            viewModel.order = viewModel.order ?? new Buyer();
            viewModel.forwarder = viewModel.forwarder ?? new Forwarder();
            viewModel.shippingStaff = viewModel.shippingStaff ?? new ShippingStaff();
            viewModel.emkl = viewModel.emkl ?? new EMKL();
            GarmentShippingCoverLetterModel model = new GarmentShippingCoverLetterModel(viewModel.packingListId, viewModel.invoiceId, viewModel.invoiceNo, viewModel.date.GetValueOrDefault(), viewModel.emkl.Id, viewModel.emkl.Code, viewModel.emkl.Name, viewModel.destination, viewModel.address, viewModel.pic, viewModel.attn, viewModel.phone, viewModel.bookingDate.GetValueOrDefault(), viewModel.order.Id, viewModel.order.Code, viewModel.order.Name, viewModel.pcsQuantity, viewModel.setsQuantity, viewModel.packQuantity, viewModel.cartoonQuantity, viewModel.forwarder.id, viewModel.forwarder.code, viewModel.forwarder.name, viewModel.truck, viewModel.plateNumber, viewModel.driver, viewModel.containerNo, viewModel.freight, viewModel.shippingSeal, viewModel.dlSeal, viewModel.emklSeal, viewModel.exportEstimationDate.GetValueOrDefault(), viewModel.unit, viewModel.shippingStaff.id, viewModel.shippingStaff.name);

            //Add Log History
            await logHistoryRepository.InsertAsync("SHIPPING", "Update Surat Pengantar - " + model.InvoiceNo);

            return await _repository.UpdateAsync(id, model);
        }
    }
}
