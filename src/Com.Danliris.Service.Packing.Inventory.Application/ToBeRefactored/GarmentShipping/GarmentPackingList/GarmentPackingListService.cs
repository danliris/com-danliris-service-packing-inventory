using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListService : IGarmentPackingListService
    {
        private GarmentPackingListViewModel MapToViewModel(GarmentPackingListModel model)
        {
            var vm =  new GarmentPackingListViewModel()
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

                InvoiceNo = model.InvoiceNo,
                InvoiceType = model.InvoiceType,
                Section = new Section
                {
                    Id = model.SectionId,
                    Code = model.SectionCode
                },
                Date = model.Date,
                PriceType = model.PriceType,
                LCNo = model.LCNo,
                IssuedBy = model.IssuedBy,
                Comodity = model.Comodity,
                Destination = model.Destination,
                TruckingDate = model.TruckingDate,
                ExportEstimationDate = model.ExportEstimationDate,
                Omzet = model.Omzet,
                Accounting = model.Accounting,
                Items = model.Items.Select(i => new GarmentPackingListItemViewModel
                {
                    Active = i.Active,
                    Id = i.Id,
                    CreatedAgent = i.CreatedAgent,
                    CreatedBy = i.CreatedBy,
                    CreatedUtc = i.CreatedUtc,
                    DeletedAgent = i.DeletedAgent,
                    DeletedBy = i.DeletedBy,
                    DeletedUtc = i.DeletedUtc,
                    IsDeleted = i.IsDeleted,
                    LastModifiedAgent = i.LastModifiedAgent,
                    LastModifiedBy = i.LastModifiedBy,
                    LastModifiedUtc = i.LastModifiedUtc,

                    RONo = i.RONo,
                    SCNo = i.SCNo,
                    Buyer = new Buyer
                    {
                        Id = i.BuyerId,
                        Name = i.BuyerName
                    },
                    Quantity = i.Quantity,
                    Uom = new UnitOfMeasurement
                    {
                        Id = i.UomId,
                        Unit = i.UomUnit
                    },
                    Price = i.Price,
                    Amount = i.Amount,
                    Valas = i.Valas,
                    Unit = new Unit
                    {
                        Id = i.UnitId,
                        Code = i.UnitCode
                    },
                    OTL = i.OTL,
                    Article = i.Article,
                    OrderNo = i.OrderNo,
                    Description = i.Description,

                    Details = i.Details.Select(d => new GarmentPackingListDetailViewModel
                    {
                        Active = d.Active,
                        Id = d.Id,
                        CreatedAgent = d.CreatedAgent,
                        CreatedBy = d.CreatedBy,
                        CreatedUtc = d.CreatedUtc,
                        DeletedAgent = d.DeletedAgent,
                        DeletedBy = d.DeletedBy,
                        DeletedUtc = d.DeletedUtc,
                        IsDeleted = d.IsDeleted,
                        LastModifiedAgent = d.LastModifiedAgent,
                        LastModifiedBy = d.LastModifiedBy,
                        LastModifiedUtc = d.LastModifiedUtc,

                        Carton1 = d.Carton1,
                        Carton2 = d.Carton2,
                        Colour = d.Colour,
                        CartonQuantity = d.CartonQuantity,
                        QuantityPCS = d.QuantityPCS,
                        TotalQuantity = d.TotalQuantity,

                    }).ToList()
                }).ToList(),
                AVG_GW = model.AVG_GW,
                AVG_NW = model.AVG_NW,

                GrossWeight = model.GrossWeight,
                NettWeight = model.NettWeight,
                TotalCartons = model.TotalCartons,
                Measurements = model.Measurements.Select(m => new GarmentPackingListMeasurementViewModel
                {
                    Active = m.Active,
                    Id = m.Id,
                    CreatedAgent = m.CreatedAgent,
                    CreatedBy = m.CreatedBy,
                    CreatedUtc = m.CreatedUtc,
                    DeletedAgent = m.DeletedAgent,
                    DeletedBy = m.DeletedBy,
                    DeletedUtc = m.DeletedUtc,
                    IsDeleted = m.IsDeleted,
                    LastModifiedAgent = m.LastModifiedAgent,
                    LastModifiedBy = m.LastModifiedBy,
                    LastModifiedUtc = m.LastModifiedUtc,

                    Length = m.Length,
                    Width = m.Width,
                    Height = m.Height,
                    CartonsQuantity = m.CartonsQuantity,

                }).ToList(),

                ShippingMark = model.ShippingMark,
                SideMark = model.SideMark,
                Remark = model.Remark,
            };
            return vm;
        }

        public Task<int> Create(GarmentPackingListViewModel viewModel)
        {
            var items = new HashSet<GarmentPackingListItemModel>();
            foreach (var i in viewModel.Items ?? new List<GarmentPackingListItemViewModel>())
            {
                var details = new HashSet<GarmentPackingListDetailModel>();
                foreach (var d in i.Details ?? new List<GarmentPackingListDetailViewModel>())
                {
                    details.Add(new GarmentPackingListDetailModel(d.Carton1, d.Carton2, d.Colour, d.CartonQuantity, d.QuantityPCS, d.TotalQuantity));
                }
                i.Buyer = i.Buyer ?? new Buyer();
                i.Uom = i.Uom ?? new UnitOfMeasurement();
                i.Unit = i.Unit ?? new Unit();
                items.Add(new GarmentPackingListItemModel(i.RONo, i.SCNo, i.Buyer.Id, i.Buyer.Name, i.Quantity, i.Uom.Id.GetValueOrDefault(), i.Uom.Unit, i.Price, i.Amount, i.Valas, i.Unit.Id, i.Unit.Code, i.OTL, i.Article, i.OrderNo, i.Description, details));
            }

            var measurements = new HashSet<GarmentPackingListMeasurementModel>();
            foreach (var m in viewModel.Measurements ?? new List<GarmentPackingListMeasurementViewModel>())
            {
                measurements.Add(new GarmentPackingListMeasurementModel(m.Length, m.Width, m.Height, m.CartonsQuantity));
            }

            viewModel.Section = viewModel.Section ?? new Section();
            GarmentPackingListModel garmentPackingListModel = new GarmentPackingListModel(viewModel.InvoiceType, viewModel.Section.Id, viewModel.Section.Code, viewModel.Date.GetValueOrDefault(), viewModel.PriceType, viewModel.LCNo, viewModel.IssuedBy, viewModel.Comodity, viewModel.Destination, viewModel.TruckingDate.GetValueOrDefault(), viewModel.ExportEstimationDate.GetValueOrDefault(), viewModel.Omzet, viewModel.Accounting, items, viewModel.AVG_GW, viewModel.AVG_NW, viewModel.GrossWeight, viewModel.NettWeight, viewModel.TotalCartons, measurements, viewModel.ShippingMark, viewModel.SideMark, viewModel.Remark);

            return Task.FromResult(1);
        }

        public ListResult<GarmentPackingListViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var data = new List<GarmentPackingListViewModel>()
            {
                new GarmentPackingListViewModel()
            };

            return new ListResult<GarmentPackingListViewModel>(data, 1, 1, 1);
        }

        public Task<GarmentPackingListViewModel> ReadById(int id)
        {
            // Dummy
            var details = new HashSet<GarmentPackingListDetailModel> { new GarmentPackingListDetailModel(1, 1, "", 1, 1, 1) };
            var items = new HashSet<GarmentPackingListItemModel> { new GarmentPackingListItemModel("", "", 1, "", 1, 1, "", 1, 1, "", 1, "", "", "", "", "", details) };
            var measurements = new HashSet<GarmentPackingListMeasurementModel> { new GarmentPackingListMeasurementModel(1, 1, 1, 1) };
            var model = new GarmentPackingListModel("", 1, "", DateTimeOffset.Now, "", "", "", "", "", DateTimeOffset.Now, DateTimeOffset.Now, false, false, items, 1, 1, 1, 1, 1, measurements, "", "", "");
            // Dummy

            var viewModel = MapToViewModel(model);

            return Task.FromResult(viewModel);
        }

        public Task<int> Update(int id, GarmentPackingListViewModel viewModel)
        {
            return Task.FromResult(1);
        }

        public Task<int> Delete(int id)
        {
            return Task.FromResult(1);
        }
    }
}
