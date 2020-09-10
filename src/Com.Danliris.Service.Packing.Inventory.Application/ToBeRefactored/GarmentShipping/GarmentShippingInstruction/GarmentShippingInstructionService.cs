using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using System.Linq;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInstruction
{
    public class GarmentShippingInstructionService : IGarmentShippingInstructionService
    {
        private readonly IGarmentShippingInstructionRepository _shippingInstructionRepository;

        public GarmentShippingInstructionService(IServiceProvider serviceProvider)
        {
            _shippingInstructionRepository = serviceProvider.GetService<IGarmentShippingInstructionRepository>();
        }

        private GarmentShippingInstructionViewModel MapToViewModel(GarmentShippingInstructionModel model)
        {
            var vm = new GarmentShippingInstructionViewModel()
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

                BuyerAgent = new Buyer
                {
                    Id = model.BuyerAgentId,
                    Code = model.BuyerAgentCode,
                    Name = model.BuyerAgentName,
                },
                forwarder= new Forwarder
                {
                    id=model.ForwarderId,
                    address=model.ForwarderAddress,
                    //attn=model.ATTN,
                    code=model.ForwarderCode,
                    fax=model.Fax,
                    name=model.ForwarderName,
                    phone=model.ForwarderPhone
                },
                BankAccountId= model.BankAccountId,
                BankAccountName= model.BankAccountName,
                BuyerAgentAddress= model.BuyerAgentAddress,
                Carrier= model.Carrier,
                CartonNo= model.CartonNo,
                ATTN = model.ATTN,
                CC = model.CC,
                Date= model.Date,
                FeederVessel= model.FeederVessel,
                Flight= model.Flight,
                InvoiceNo= model.InvoiceNo,
                Notify= model.Notify,
                OceanVessel= model.OceanVessel,
                InvoiceId= model.InvoiceId,
                Phone= model.Phone,
                PlaceOfDelivery= model.PlaceOfDelivery,
                PortOfDischarge= model.PortOfDischarge,
                ShippedBy= model.ShippedBy,
                ShippingStaffId= model.ShippingStaffId,
                ShippingStaffName = model.ShippingStaffName,
                SpecialInstruction = model.SpecialInstruction,
                Transit= model.Transit,
                TruckingDate= model.TruckingDate,
                LadingDate=model.LadingDate,
                LadingBill=model.LadingBill,
                Freight=model.Freight,
                Marks=model.Marks
            };

            return vm;
        }

        private GarmentShippingInstructionModel MapToModel(GarmentShippingInstructionViewModel viewModel)
        {
            
            viewModel.forwarder = viewModel.forwarder ?? new Forwarder();
            viewModel.BuyerAgent = viewModel.BuyerAgent ?? new Buyer();
            GarmentShippingInstructionModel garmentShippingInstructionModel = new GarmentShippingInstructionModel(viewModel.InvoiceNo,viewModel.InvoiceId, 
                viewModel.Date, viewModel.forwarder.id, viewModel.forwarder.code, viewModel.forwarder.name,viewModel.forwarder.address,
                viewModel.forwarder.phone, viewModel.ATTN, viewModel.forwarder.fax,viewModel.CC, viewModel.ShippingStaffId, 
                viewModel.ShippingStaffName, viewModel.Phone, viewModel.ShippedBy, viewModel.TruckingDate, 
                viewModel.CartonNo, viewModel.PortOfDischarge, viewModel.PlaceOfDelivery, 
                viewModel.FeederVessel, viewModel.OceanVessel, viewModel.Carrier, viewModel.Flight, 
                viewModel.Transit, viewModel.BankAccountId, viewModel.BankAccountName, viewModel.BuyerAgent.Id, 
                viewModel.BuyerAgent.Code, viewModel.BuyerAgent.Name, viewModel.BuyerAgentAddress, 
                viewModel.Notify, viewModel.SpecialInstruction, viewModel.LadingDate.GetValueOrDefault(),
                viewModel.LadingBill,viewModel.Freight, viewModel.Marks);

            return garmentShippingInstructionModel;
        }
        public async Task<int> Create(GarmentShippingInstructionViewModel viewModel)
        {
            GarmentShippingInstructionModel garmentShippingInstructionModel = MapToModel(viewModel);

            int Created = await _shippingInstructionRepository.InsertAsync(garmentShippingInstructionModel);

            return Created;
        }

        public async Task<int> Delete(int id)
        {
            return await _shippingInstructionRepository.DeleteAsync(id);
        }

        public ListResult<GarmentShippingInstructionViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _shippingInstructionRepository.ReadAll();
            List<string> SearchAttributes = new List<string>()
            {
                "InvoiceNo","EMKLName","ATTN","ShippedBy", "BuyerAgentName"
            };
            query = QueryHelper<GarmentShippingInstructionModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentShippingInstructionModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentShippingInstructionModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentShippingInstructionViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentShippingInstructionViewModel> ReadById(int id)
        {
            var data = await _shippingInstructionRepository.ReadByIdAsync(id);

            var viewModel = MapToViewModel(data);

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentShippingInstructionViewModel viewModel)
        {
            GarmentShippingInstructionModel garmentShippingInstructionModel = MapToModel(viewModel);

            return await _shippingInstructionRepository.UpdateAsync(id, garmentShippingInstructionModel);
        }
    }
}
