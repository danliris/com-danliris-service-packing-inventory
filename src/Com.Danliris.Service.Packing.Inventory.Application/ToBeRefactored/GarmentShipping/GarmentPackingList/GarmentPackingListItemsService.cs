using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList
{
    public class GarmentPackingListItemsService : GarmentPackingListService, IGarmentPackingListItemsService
    {
        private const string UserAgent = "GarmentPackingListItemsService";

        public GarmentPackingListItemsService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override ListResult<GarmentPackingListViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _packingListRepository.ReadAll();

            GarmentPackingListStatusEnum[] statusFilter = {
                GarmentPackingListStatusEnum.DRAFT,
                GarmentPackingListStatusEnum.DRAFT_POSTED,
                GarmentPackingListStatusEnum.DRAFT_REJECTED_MD,
                GarmentPackingListStatusEnum.DRAFT_APPROVED_MD,
                GarmentPackingListStatusEnum.DRAFT_REJECTED_SHIPPING,
                GarmentPackingListStatusEnum.DRAFT_APPROVED_SHIPPING,
            };
            query = query.Where(w => statusFilter.Contains(w.Status) && w.Items.Any(i => i.CreatedBy == _identityProvider.Username));

            List<string> SearchAttributes = new List<string>()
            {
                "InvoiceNo", "InvoiceType", "PackingListType", "SectionCode", "Destination", "BuyerAgentName"
            };
            query = QueryHelper<GarmentPackingListModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentPackingListModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentPackingListModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentPackingListViewModel>(data, page, size, query.Count());
        }

        public override async Task<GarmentPackingListViewModel> ReadById(int id)
        {
            var viewModel = await _packingListRepository.Query.Select(model => new GarmentPackingListViewModel
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
                Description = model.Description,
                InvoiceNo = model.InvoiceNo,
                PackingListType = model.PackingListType,
                InvoiceType = model.InvoiceType,
                Section = new Section
                {
                    Id = model.SectionId,
                    Code = model.SectionCode
                },
                Date = model.Date,
                PaymentTerm = model.PaymentTerm,
                LCNo = model.LCNo,
                LCDate = model.LCDate,
                IssuedBy = model.IssuedBy,
                BuyerAgent = new Buyer
                {
                    Id = model.BuyerAgentId,
                    Code = model.BuyerAgentCode,
                    Name = model.BuyerAgentName,
                },
                Destination = model.Destination,
                ShipmentMode = model.ShipmentMode,
                TruckingDate = model.TruckingDate,
                TruckingEstimationDate = model.TruckingEstimationDate,
                ExportEstimationDate = model.ExportEstimationDate,
                Omzet = model.Omzet,
                Accounting = model.Accounting,
                FabricCountryOrigin = model.FabricCountryOrigin,
                FabricComposition = model.FabricComposition,
                RemarkMd = model.RemarkMd,
                IsUsed = model.IsUsed,
                IsPosted = model.IsPosted,
                IsCostStructured = model.IsCostStructured,
                Items = model.Items.Where(i => i.CreatedBy == _identityProvider.Username).Select(i => new GarmentPackingListItemViewModel
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
                    BuyerBrand = new Buyer
                    {
                        Id = i.BuyerBrandId,
                        Name = i.BuyerBrandName
                    },
                    Comodity = new Comodity
                    {
                        Id = i.ComodityId,
                        Code = i.ComodityCode,
                        Name = i.ComodityName
                    },
                    ComodityDescription = i.ComodityDescription,
                    Quantity = i.Quantity,
                    Uom = new UnitOfMeasurement
                    {
                        Id = i.UomId,
                        Unit = i.UomUnit
                    },
                    PriceRO = i.PriceRO,
                    Price = i.Price,
                    PriceFOB = i.PriceFOB,
                    PriceCMT = i.PriceCMT,
                    Amount = i.Amount,
                    Valas = i.Valas,
                    Unit = new Unit
                    {
                        Id = i.UnitId,
                        Code = i.UnitCode
                    },
                    Article = i.Article,
                    OrderNo = i.OrderNo,
                    Description = i.Description,
                    DescriptionMd = i.DescriptionMd,
                    Remarks = i.Remarks,

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
                        Style = d.Style,
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

                        Sizes = d.Sizes.Select(s => new GarmentPackingListDetailSizeViewModel
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

                    }).OrderBy(x => x.Carton1).ThenBy(x => x.Carton2).ToList(),
                }).OrderBy(o => o.ComodityDescription).ToList(),

                GrossWeight = model.GrossWeight,
                NettWeight = model.NettWeight,
                NetNetWeight = model.NetNetWeight,
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
                SayUnit = model.SayUnit,
                OtherCommodity = model.OtherCommodity,

                ShippingMark = model.ShippingMark,
                SideMark = model.SideMark,
                Remark = model.Remark,

                ShippingMarkImagePath = model.ShippingMarkImagePath,
                SideMarkImagePath = model.SideMarkImagePath,
                RemarkImagePath = model.RemarkImagePath,

                ShippingStaff = new ShippingStaff
                {
                    id = model.ShippingStaffId,
                    name = model.ShippingStaffName,
                },

                Status = model.Status.ToString(),
                StatusActivities = model.StatusActivities.Select(a => new GarmentPackingListStatusActivityViewModel
                {
                    Id = a.Id,
                    CreatedDate = a.CreatedDate,
                    CreatedBy = a.CreatedBy,
                    CreatedAgent = a.CreatedAgent,
                    Status = a.Status.ToString(),
                    Remark = a.Remark
                }).ToList()
            }).SingleOrDefaultAsync(s => s.Id == id);

            viewModel.ShippingMarkImageFile = await _azureImageService.DownloadImage(IMG_DIR, viewModel.ShippingMarkImagePath);
            viewModel.SideMarkImageFile = await _azureImageService.DownloadImage(IMG_DIR, viewModel.SideMarkImagePath);
            viewModel.RemarkImageFile = await _azureImageService.DownloadImage(IMG_DIR, viewModel.RemarkImagePath);

            return viewModel;
        }

        public override Task<int> Update(int id, GarmentPackingListViewModel viewModel)
        {
            GarmentPackingListModel model = MapToModel(viewModel);

            var modelToUpdate = _packingListRepository.Query
                .Include(i => i.Items)
                    .ThenInclude(i => i.Details)
                        .ThenInclude(i => i.Sizes)
                .Include(i => i.Measurements)
                .FirstOrDefault(s => s.Id == id);

            modelToUpdate.SetSectionId(model.SectionId, _identityProvider.Username, UserAgent);
            modelToUpdate.SetSectionCode(model.SectionCode, _identityProvider.Username, UserAgent);

            var measurements = modelToUpdate.Items
                .SelectMany(i => i.Details.Select(d => new { d.Index, d.Carton1, d.Carton2, d.Length, d.Width, d.Height, d.CartonQuantity }))
                .GroupBy(m => new { m.Length, m.Width, m.Height }, (k, g) => new GarmentPackingListMeasurementModel(k.Length, k.Width, k.Height, g.Distinct().Sum(d => d.CartonQuantity)));

            foreach (var itemToUpdate in modelToUpdate.Items.Where(i => i.CreatedBy == _identityProvider.Username))
            {
                var item = model.Items.FirstOrDefault(i => i.Id == itemToUpdate.Id);
                if (item != null)
                {
                    itemToUpdate.SetRONo(item.RONo, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetSCNo(item.SCNo, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetBuyerBrandId(item.BuyerBrandId, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetBuyerBrandName(item.BuyerBrandName, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetComodityId(item.ComodityId, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetComodityCode(item.ComodityCode, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetComodityName(item.ComodityName, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetComodityDescription(item.ComodityDescription, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetQuantity(item.Quantity, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetUomId(item.UomId, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetUomUnit(item.UomUnit, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetPriceRO(item.PriceRO, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetPrice(item.Price, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetPriceFob(item.PriceFOB, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetPriceCmt(item.PriceCMT, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetAmount(item.Amount, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetValas(item.Valas, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetUnitId(item.UnitId, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetUnitCode(item.UnitCode, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetArticle(item.Article, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetOrderNo(item.OrderNo, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetDescription(item.Description, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetDescriptionMd(item.DescriptionMd, _identityProvider.Username, UserAgent);
                    itemToUpdate.SetRemarks(item.Remarks, _identityProvider.Username, UserAgent);

                    foreach (var detailToUpdate in itemToUpdate.Details)
                    {
                        var detail = item.Details.FirstOrDefault(d => d.Id == detailToUpdate.Id);
                        if (detail != null)
                        {
                            detailToUpdate.SetCarton1(detail.Carton1, _identityProvider.Username, UserAgent);
                            detailToUpdate.SetCarton2(detail.Carton2, _identityProvider.Username, UserAgent);
                            detailToUpdate.SetStyle(detail.Style, _identityProvider.Username, UserAgent);
                            detailToUpdate.SetColour(detail.Colour, _identityProvider.Username, UserAgent);
                            detailToUpdate.SetCartonQuantity(detail.CartonQuantity, _identityProvider.Username, UserAgent);
                            detailToUpdate.SetQuantityPCS(detail.QuantityPCS, _identityProvider.Username, UserAgent);
                            detailToUpdate.SetTotalQuantity(detail.TotalQuantity, _identityProvider.Username, UserAgent);

                            detailToUpdate.SetLength(detail.Length, _identityProvider.Username, UserAgent);
                            detailToUpdate.SetWidth(detail.Width, _identityProvider.Username, UserAgent);
                            detailToUpdate.SetHeight(detail.Height, _identityProvider.Username, UserAgent);

                            detailToUpdate.SetGrossWeight(detail.GrossWeight, _identityProvider.Username, UserAgent);
                            detailToUpdate.SetNetWeight(detail.NetWeight, _identityProvider.Username, UserAgent);
                            detailToUpdate.SetNetNetWeight(detail.NetNetWeight == 0 ? 0.9 * detail.NetWeight : detail.NetNetWeight, _identityProvider.Username, UserAgent);

                            detailToUpdate.SetIndex(detail.Index, _identityProvider.Username, UserAgent);

                            foreach (var sizeToUpdate in detailToUpdate.Sizes)
                            {
                                var size = detail.Sizes.FirstOrDefault(s => s.Id == sizeToUpdate.Id);
                                if (size != null)
                                {
                                    sizeToUpdate.SetSizeId(size.SizeId, _identityProvider.Username, UserAgent);
                                    sizeToUpdate.SetSize(size.Size, _identityProvider.Username, UserAgent);
                                    sizeToUpdate.SetQuantity(size.Quantity, _identityProvider.Username, UserAgent);
                                }
                                else
                                {
                                    sizeToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);
                                }
                            }

                            foreach (var size in detail.Sizes.Where(w => w.Id == 0))
                            {
                                size.FlagForCreate(_identityProvider.Username, UserAgent);
                                detailToUpdate.Sizes.Add(size);
                            }
                        }
                        else
                        {
                            /*var measurement = model.Measurements.FirstOrDefault(m => m.Length == detailToUpdate.Length && m.Width == detailToUpdate.Width && m.Height == detailToUpdate.Height);
                            var measurementDB = modelToUpdate.Measurements.FirstOrDefault(m => m.Length == detailToUpdate.Length && m.Width == detailToUpdate.Width && m.Height == detailToUpdate.Height);
                            foreach(var measurementDb in modelToUpdate.Measurements)
                            {
                                
                            }
                            if (measurement == null)
                            {
                                var sumQty = measurementDB.CartonsQuantity - detailToUpdate.CartonQuantity;
                                if (sumQty == 0)
                                {
                                    measurementDB.FlagForDelete(_identityProvider.Username, UserAgent);
                                }
                                else
                                {
                                    measurementDB.SetCartonsQuantity(sumQty, _identityProvider.Username, UserAgent);
                                }
                            }*/
                            detailToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);
                        }
                    }

                    foreach (var detail in item.Details.Where(w => w.Id == 0))
                    {
                        var netNetWeight = detail.NetNetWeight == 0 ? 0.9 * detail.NetWeight : detail.NetNetWeight;
                        detail.SetNetNetWeight(netNetWeight, _identityProvider.Username, UserAgent);
                        detail.FlagForCreate(_identityProvider.Username, UserAgent);
                        foreach (var size in detail.Sizes)
                        {
                            size.FlagForCreate(_identityProvider.Username, UserAgent);
                        }
                        itemToUpdate.Details.Add(detail);
                    }
                }
                else
                {
                    var items = modelToUpdate.Items.FirstOrDefault(x => x.Id == itemToUpdate.Id);
                    foreach (var detail in items.Details)
                    {
                        var measurement = model.Measurements.FirstOrDefault(m => m.Length == detail.Length && m.Width == detail.Width && m.Height == detail.Height);
                        var measurementDB = measurements.FirstOrDefault(m => m.Length == detail.Length && m.Width == detail.Width && m.Height == detail.Height);
                        if (measurement == null)
                        {
                            measurementDB.FlagForDelete(_identityProvider.Username, UserAgent);
                        }
                    }
                    

                    itemToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);
                }

            }

            foreach (var item in model.Items.Where(w => w.Id == 0))
            {
                item.FlagForCreate(_identityProvider.Username, UserAgent);
                foreach (var detail in item.Details)
                {
                    detail.SetNetNetWeight(detail.NetNetWeight == 0 ? 0.9 * detail.NetWeight : detail.NetNetWeight, _identityProvider.Username, UserAgent);
                    detail.FlagForCreate(_identityProvider.Username, UserAgent);
                    foreach (var size in detail.Sizes)
                    {
                        size.FlagForCreate(_identityProvider.Username, UserAgent);
                    }
                }
                modelToUpdate.Items.Add(item);
            }

            

            foreach (var measurementToUpdate in modelToUpdate.Measurements)
            {
                var measurement = model.Measurements.FirstOrDefault(m => m.Length == measurementToUpdate.Length && m.Width == measurementToUpdate.Width && m.Height == measurementToUpdate.Height);
                var measurementDB = measurements.FirstOrDefault(m => m.Length == measurementToUpdate.Length && m.Width == measurementToUpdate.Width && m.Height == measurementToUpdate.Height);
                if (measurement != null)
                {
                    double diffQty = 0;
                    double sumQty = 0;
                    if (measurementDB.CartonsQuantity > measurement.CartonsQuantity)
                    {
                        diffQty = measurementDB.CartonsQuantity - measurement.CartonsQuantity;
                        sumQty = measurementDB.CartonsQuantity - diffQty;
                    }
                    else
                    {
                        diffQty = measurement.CartonsQuantity - measurementDB.CartonsQuantity;
                        sumQty = measurement.CartonsQuantity - diffQty;
                    }
                    measurementToUpdate.SetCartonsQuantity(sumQty, _identityProvider.Username, UserAgent);
                } else
                {
                    if(measurementDB != null)
                    {
                        double diffQty = 0;
                        double sumQty = 0;
                        if (measurementDB.CartonsQuantity > measurementToUpdate.CartonsQuantity)
                        {
                            diffQty = measurementDB.CartonsQuantity - measurementToUpdate.CartonsQuantity;
                            sumQty = measurementToUpdate.CartonsQuantity + diffQty;
                        }
                        else
                        {
                            diffQty = measurementToUpdate.CartonsQuantity - measurementDB.CartonsQuantity;
                            sumQty = measurementToUpdate.CartonsQuantity - diffQty;
                        }
                        measurementToUpdate.SetCartonsQuantity(sumQty, _identityProvider.Username, UserAgent);
                    } else
                    {
                        measurementToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);
                    }
                    
                }
                /*  else
                  {
                      measurementToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);
                  }*/
            }

            foreach (var measurement in measurements)
            {
                var oldMeasurement = modelToUpdate.Measurements.FirstOrDefault(m => m.Length == measurement.Length && m.Width == measurement.Width && m.Height == measurement.Height);
                if (oldMeasurement == null)
                {
                    measurement.FlagForCreate(_identityProvider.Username, UserAgent);
                    modelToUpdate.Measurements.Add(measurement);
                }
            }

            var itemsUpdate = modelToUpdate.Items.Where(i => i.IsDeleted == false).OrderBy(o => o.ComodityDescription);

            var totalCartons = itemsUpdate
                .SelectMany(i => i.Details.Where(d => d.IsDeleted == false).Select(d => new { d.Index, d.Carton1, d.Carton2, d.CartonQuantity }))
                .Distinct().Sum(d => d.CartonQuantity);
            modelToUpdate.SetTotalCartons(totalCartons, _identityProvider.Username, UserAgent);

            var totalGw = itemsUpdate
                .SelectMany(i => i.Details.Where(d => d.IsDeleted == false).Select(d => new { d.Index, d.Carton1, d.Carton2, totalGrossWeight = d.CartonQuantity * d.GrossWeight }))
                .GroupBy(g => new { g.Index, g.Carton1, g.Carton2 }, (key, value) => value.First().totalGrossWeight).Sum();

            modelToUpdate.SetGrossWeight(totalGw, _identityProvider.Username, UserAgent);

            var totalNw = itemsUpdate
                .SelectMany(i => i.Details.Where(d => d.IsDeleted == false).Select(d => new { d.Index, d.Carton1, d.Carton2, totalNetWeight = d.CartonQuantity * d.NetWeight }))
                .GroupBy(g => new { g.Index, g.Carton1, g.Carton2 }, (key, value) => value.First().totalNetWeight).Sum();

            modelToUpdate.SetNettWeight(totalNw, _identityProvider.Username, UserAgent);

            var totalNnw = itemsUpdate
                .SelectMany(i => i.Details.Where(d => d.IsDeleted == false).Select(d => new { d.Index, d.Carton1, d.Carton2, totalNetNetWeight = d.CartonQuantity * d.NetNetWeight }))
                .GroupBy(g => new { g.Index, g.Carton1, g.Carton2 }, (key, value) => value.First().totalNetNetWeight).Sum();

            modelToUpdate.SetNetNetWeight(totalNnw, _identityProvider.Username, UserAgent);

            return _packingListRepository.SaveChanges();
        }
    }
}