using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentDraftPackingListItem;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentDraftPackingListItem;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System.Linq;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentDraftPackingListItem
{
    public class GarmentDraftPackingListItemService : IGarmentDraftPackingListItemService
    {
        private const string UserAgent = "GarmentDraftPackingListItemService";

        protected readonly IGarmentDraftPackingListItemRepository _draftPackingListItemRepository;
        protected readonly IIdentityProvider _identityProvider;
        protected readonly IAzureImageService _azureImageService;

        public GarmentDraftPackingListItemService(IServiceProvider serviceProvider)
        {
            _draftPackingListItemRepository = serviceProvider.GetService<IGarmentDraftPackingListItemRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _azureImageService = serviceProvider.GetService<IAzureImageService>();
        }

        protected GarmentDraftPackingListItemViewModel MapToViewModel(GarmentDraftPackingListItemModel model)
        {
            var vm = new GarmentDraftPackingListItemViewModel()
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
                
                RONo = model.RONo,
                SCNo = model.SCNo,
                BuyerBrand = new Buyer
                {
                    Id = model.BuyerBrandId,
                    Name = model.BuyerBrandName
                },
                Comodity = new Comodity
                {
                    Id = model.ComodityId,
                    Code = model.ComodityCode,
                    Name = model.ComodityName
                },
                ComodityDescription = model.ComodityDescription,
                Quantity = model.Quantity,
                Uom = new UnitOfMeasurement
                {
                    Id = model.UomId,
                    Unit = model.UomUnit
                },
                PriceRO = model.PriceRO,
                Price = model.Price,
                PriceFOB = model.PriceFOB,
                PriceCMT = model.PriceCMT,
                Amount = model.Amount,
                Valas = model.Valas,
                Unit = new Unit
                {
                    Id = model.UnitId,
                    Code = model.UnitCode
                },
                Article = model.Article,
                OrderNo = model.OrderNo,
                Description = model.Description,
                DescriptionMd = model.DescriptionMd,
                Section=new Section
                {
                    Code=model.SectionCode
                },
                Buyer= new Buyer
                {
                    Id=model.BuyerId,
                    Code=model.BuyerCode
                },
                Remarks = model.Remarks,

                Details = (model.Details ?? new List<GarmentDraftPackingListDetailModel>()).Select(d => new GarmentDraftPackingListDetailViewModel
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

                    PackingListItemId = d.PackingListItemId,
                    Carton1 = d.Carton1,
                    Carton2 = d.Carton2,
                    Style = d.Style,
                    Colour = d.Colour,
                    CartonQuantity = d.CartonQuantity,
                    QuantityPCS = d.QuantityPCS,
                    TotalQuantity = d.TotalQuantity,

                    Length = d.Length,
                    Width = d.Width,
                    Height = d.Height,

                    GrossWeight = d.GrossWeight,
                    NetWeight = d.NetWeight,
                    NetNetWeight = d.NetNetWeight,

                    Index = d.Index,

                    Sizes = (d.Sizes ?? new List<GarmentDraftPackingListDetailSizeModel>()).Select(s => new GarmentDraftPackingListDetailSizeViewModel
                    {
                        Active = s.Active,
                        Id = s.Id,
                        CreatedAgent = s.CreatedAgent,
                        CreatedBy = s.CreatedBy,
                        CreatedUtc = s.CreatedUtc,
                        DeletedAgent = s.DeletedAgent,
                        DeletedBy = s.DeletedBy,
                        DeletedUtc = s.DeletedUtc,
                        IsDeleted = s.IsDeleted,
                        LastModifiedAgent = s.LastModifiedAgent,
                        LastModifiedBy = s.LastModifiedBy,
                        LastModifiedUtc = s.LastModifiedUtc,

                        Size = new SizeViewModel
                        {
                            Id = s.SizeId,
                            Size = s.Size
                        },
                        Quantity = s.Quantity
                    }).ToList()

                }).ToList(),

            };
            return vm;
        }

        protected GarmentDraftPackingListItemModel MapToModel(GarmentDraftPackingListItemViewModel viewModel)
        {
            var details = (viewModel.Details ?? new List<GarmentDraftPackingListDetailViewModel>()).Select(d =>
            {
                var sizes = (d.Sizes ?? new List<GarmentDraftPackingListDetailSizeViewModel>()).Select(s =>
                {
                    s.Size = s.Size ?? new SizeViewModel();
                    return new GarmentDraftPackingListDetailSizeModel(s.Size.Id, s.Size.Size, s.Quantity) { Id = s.Id };
                }).ToList();

                return new GarmentDraftPackingListDetailModel(d.Carton1, d.Carton2, d.Style, d.Colour, d.CartonQuantity, d.QuantityPCS, d.TotalQuantity, d.Length, d.Width, d.Height, d.GrossWeight, d.NetWeight, d.NetNetWeight, sizes, d.Index) { Id = d.Id };
            }).ToList();

            viewModel.BuyerBrand = viewModel.BuyerBrand ?? new Buyer();
            viewModel.Uom = viewModel.Uom ?? new UnitOfMeasurement();
            viewModel.Unit = viewModel.Unit ?? new Unit();
            viewModel.Comodity = viewModel.Comodity ?? new Comodity();
            viewModel.Buyer = viewModel.Buyer ?? new Buyer();
            viewModel.Section = viewModel.Section ?? new Section();
            GarmentDraftPackingListItemModel GarmentDraftPackingListItemModel = new GarmentDraftPackingListItemModel(viewModel.RONo, viewModel.SCNo, viewModel.BuyerBrand.Id, viewModel.BuyerBrand.Name, viewModel.Comodity.Id, viewModel.Comodity.Code, viewModel.Comodity.Name, viewModel.ComodityDescription, viewModel.Quantity, viewModel.Uom.Id.GetValueOrDefault(), viewModel.Uom.Unit, viewModel.PriceRO, viewModel.Price, viewModel.PriceFOB, viewModel.PriceCMT, viewModel.Amount, viewModel.Valas, viewModel.Unit.Id, viewModel.Unit.Code, viewModel.Article, viewModel.OrderNo, viewModel.Description, viewModel.DescriptionMd, viewModel.Buyer.Id, viewModel.Buyer.Code, viewModel.Section.Code, details, viewModel.Remarks);
           
            return GarmentDraftPackingListItemModel;
        }

        public async Task<int> Create(List<GarmentDraftPackingListItemViewModel> viewModels)
        {
            int Created = viewModels.Count();
            int count = 0;
            foreach (var viewModel in viewModels)
            {
                GarmentDraftPackingListItemModel draftItemModel = MapToModel(viewModel);
                if (draftItemModel.Details.Count() > 0)
                {
                    foreach (var detail in draftItemModel.Details)
                    {
                        detail.SetNetNetWeight(detail.NetNetWeight == 0 ? 0.9 * detail.NetWeight : detail.NetNetWeight, _identityProvider.Username, UserAgent);
                    }
                }
                count+= await _draftPackingListItemRepository.InsertAsync(draftItemModel);
            }
            Created = Created == count ? 1 : 0;

            return Created;
        }

        public async Task<int> Delete(int id)
        {
            return await _draftPackingListItemRepository.DeleteAsync(id);
        }

        public ListResult<GarmentDraftPackingListItemViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _draftPackingListItemRepository.ReadAll();


            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentDraftPackingListItemModel>.Filter(query, FilterDictionary);

            if (keyword != null)
            {
                query = query.Where(q => q.RONo.Contains(keyword) || q.BuyerBrandName.Contains(keyword));
            }

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentDraftPackingListItemModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentDraftPackingListItemViewModel>(data, page, size, query.Count());
        }

        public async Task<GarmentDraftPackingListItemViewModel> ReadById(int id)
        {
            var data = await _draftPackingListItemRepository.ReadByIdAsync(id);

            var viewModel = MapToViewModel(data);
            viewModel.Details = viewModel.Details.Count()>0 ? viewModel.Details.OrderBy(o => o.Carton1).ThenBy(o => o.Carton2).ToList() : null;

            return viewModel;
        }

        public async Task<int> Update(int id, GarmentDraftPackingListItemViewModel viewModel)
        {
            GarmentDraftPackingListItemModel draftItemModel = MapToModel(viewModel);

            foreach (var detail in draftItemModel.Details)
            {
                detail.SetNetNetWeight(detail.NetNetWeight == 0 ? 0.9 * detail.NetWeight : detail.NetNetWeight, _identityProvider.Username, UserAgent);
            }
            
            return await _draftPackingListItemRepository.UpdateAsync(id, draftItemModel);
        }
    }
}
