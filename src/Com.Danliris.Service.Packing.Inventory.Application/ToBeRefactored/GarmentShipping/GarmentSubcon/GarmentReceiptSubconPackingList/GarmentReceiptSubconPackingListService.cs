using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentReceiptSubconPackingList;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNote;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentReceiptSubconPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System.Linq;
using System.Threading.Tasks;
using Com.Moonlay.Models;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using System.Net.Http;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentReceiptSubconPackingList.ViewModel;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentSubcon.GarmentReceiptSubconPackingList;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentReceiptSubconPackingList
{
    public class GarmentReceiptSubconPackingListService : IGarmentReceiptSubconPackingListService
    {
        private const string UserAgent = "GarmentReceiptSubconPackingListService";

        protected readonly IGarmentReceiptSubconPackingListRepository _packingListRepository;
        protected readonly IGarmentShippingLocalSalesNoteRepository _localSalesNoteRepository;
        protected readonly IIdentityProvider _identityProvider;
        private readonly IServiceProvider serviceProvider;
        protected readonly IHttpClientService _http;

        private readonly PackingInventoryDbContext dbContext;

        public GarmentReceiptSubconPackingListService(IServiceProvider serviceProvider, PackingInventoryDbContext dbContext)
        {
            _packingListRepository = serviceProvider.GetService<IGarmentReceiptSubconPackingListRepository>();
            _localSalesNoteRepository = serviceProvider.GetService<IGarmentShippingLocalSalesNoteRepository>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            this.serviceProvider = serviceProvider;
            _http = serviceProvider.GetService<IHttpClientService>();
            this.dbContext = dbContext;
        }

        public GarmentReceiptSubconPackingListViewModel MapToViewModel(GarmentReceiptSubconPackingListModel model)
        {
            var vm = new GarmentReceiptSubconPackingListViewModel()
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

                LocalSalesNoteId = model.LocalSalesNoteId,
                LocalSalesNoteNo = model.LocalSalesNoteNo,
                LocalSalesNoteDate = model.LocalSalesNoteDate,

                LocalSalesContractId = model.LocalSalesContractId,
                LocalSalesContractNo = model.LocalSalesContractNo,

                TransactionType = new TransactionType
                {
                    id = model.TransactionTypeId,
                    code = model.TransactionTypeCode,
                    name = model.TransactionTypeName
                    
                },

                Buyer = new Buyer
                {
                    Id = model.BuyerId,
                    Code = model.BuyerCode,
                    Name = model.BuyerName,
                    npwp = model.BuyerNPWP
                },
                PaymentTerm = model.PaymentTerm,

                Omzet = model.Omzet,
                Accounting = model.Accounting,
                GrossWeight = model.GrossWeight,
                NettWeight = model.NettWeight,
                NetNetWeight = model.NetNetWeight,
                TotalCartons = model.TotalCartons,
                IsUsed = model.IsUsed,
                IsApproved = model.IsApproved,
                InvoiceNo = model.InvoiceNo,
                InvoiceDate = model.InvoiceDate,
                Items = (model.Items ?? new List<GarmentReceiptSubconPackingListItemModel>()).Select(i => new GarmentReceiptSubconPackingListItemViewModel
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
                    PackingOutNo = i.PackingOutNo,
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
                    MarketingName = i.MarketingName,
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
                    TotalQuantityPackingOut = i.TotalQuantityPackingOut,

                    Details = (i.Details ?? new List<GarmentReceiptSubconPackingListDetailModel>()).Select(d => new GarmentReceiptSubconPackingListDetailViewModel
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

                        Sizes = (d.Sizes ?? new List<GarmentReceiptSubconPackingListDetailSizeModel>()).Select(s => new GarmentReceiptSubconPackingListDetailSizeViewModel
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
                            Color = s.Color,
                            Size = new SizeViewModel
                            {
                                Id = s.SizeId,
                                Size = s.Size,
                                SizeIdx = s.SizeIdx,
                            },
                            Quantity = s.Quantity,
                            PackingOutItemId = s.PackingOutItemId
                        }).ToList()

                    }).ToList(),
                }).ToList(),

            };
            return vm;
        }

        protected GarmentReceiptSubconPackingListModel MapToModel(GarmentReceiptSubconPackingListViewModel viewModel)
        {
            var items = (viewModel.Items ?? new List<GarmentReceiptSubconPackingListItemViewModel>()).Select(i =>
            {
                var details = (i.Details ?? new List<GarmentReceiptSubconPackingListDetailViewModel>()).Select(d =>
                {
                    var sizes = (d.Sizes ?? new List<GarmentReceiptSubconPackingListDetailSizeViewModel>()).Select(s =>
                    {
                        s.Size = s.Size ?? new SizeViewModel();
                        return new GarmentReceiptSubconPackingListDetailSizeModel(s.Size.Id, s.Size.Size, s.Size.SizeIdx, s.Quantity,s.Color,s.PackingOutItemId) { Id = s.Id };
                    }).ToList();

                    return new GarmentReceiptSubconPackingListDetailModel(d.Carton1, d.Carton2, d.Style, d.CartonQuantity, d.QuantityPCS, d.TotalQuantity, d.Length, d.Width, d.Height, d.GrossWeight, d.NetWeight, d.NetNetWeight, sizes, d.Index) { Id = d.Id };
                }).ToList();

                i.BuyerBrand = i.BuyerBrand ?? new Buyer();
                i.Uom = i.Uom ?? new UnitOfMeasurement();
                i.Unit = i.Unit ?? new Unit();
                i.Comodity = i.Comodity ?? new Comodity();
                return new GarmentReceiptSubconPackingListItemModel(i.RONo, i.SCNo, i.BuyerBrand.Id, i.BuyerBrand.Name, i.Comodity.Id, i.Comodity.Code, i.Comodity.Name, i.ComodityDescription, i.MarketingName, i.Quantity, i.Uom.Id.GetValueOrDefault(), i.Uom.Unit, i.PriceRO, i.Price, i.PriceFOB, i.PriceCMT, i.Amount, i.Valas, i.Unit.Id, i.Unit.Code, i.Article, i.OrderNo, i.Description, i.DescriptionMd, i.Remarks,i.PackingOutNo, details,i.TotalQuantityPackingOut)
                {
                    Id = i.Id
                };
            }).ToList();

            viewModel.Buyer = viewModel.Buyer ?? new Buyer();
            viewModel.TransactionType = viewModel.TransactionType ?? new TransactionType();

            GarmentReceiptSubconPackingListModel garmentPackingListModel = new GarmentReceiptSubconPackingListModel
                (viewModel.LocalSalesNoteId,
                viewModel.LocalSalesNoteNo,
                viewModel.LocalSalesNoteDate,
                viewModel.LocalSalesContractId,
                viewModel.LocalSalesContractNo,
                viewModel.TransactionType.id,
                viewModel.TransactionType.code,
                viewModel.TransactionType.name,
                viewModel.Buyer.Id,
                viewModel.Buyer.Code,
                viewModel.Buyer.Name,
                viewModel.Buyer.npwp,
                viewModel.PaymentTerm,
                viewModel.Omzet,
                viewModel.Accounting,
                items,
                viewModel.GrossWeight,
                viewModel.NettWeight,
                viewModel.NetNetWeight,
                viewModel.TotalCartons,
                viewModel.IsApproved,
                viewModel.IsUsed,
                viewModel.InvoiceNo ?? GenerateInvoiceNo(viewModel),
                viewModel.InvoiceDate
                );

            return garmentPackingListModel;
        }

        private string GenerateInvoiceNo(GarmentReceiptSubconPackingListViewModel viewModel)
        {
            var year = DateTime.Now.ToString("yy");

            var prefix = $"LJS/{year}";

            var lastInvoiceNo = _packingListRepository.ReadAll().Where(w => w.InvoiceNo.StartsWith(prefix))
                .OrderByDescending(o => o.InvoiceNo)
                .Select(s => int.Parse(s.InvoiceNo.Replace(prefix, "")))
                .FirstOrDefault();
            var invoiceNo = $"{prefix}{(lastInvoiceNo + 1).ToString("D4")}";

            return invoiceNo;
        }

        public virtual async Task<string> Create(GarmentReceiptSubconPackingListViewModel viewModel)
        {
            GarmentReceiptSubconPackingListModel garmentPackingListModel = MapToModel(viewModel);

            using (var transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    //var localSalesNote = await _localSalesNoteRepository.ReadByIdAsync(garmentPackingListModel.LocalSalesNoteId);

                    //localSalesNote.SetIsSubconPackingList(true, _identityProvider.Username, UserAgent);

                    List<string> packingOutNos = new List<string>();
                    foreach (var item in garmentPackingListModel.Items)
                    {
                        packingOutNos.Add(item.PackingOutNo);

                        foreach (var detail in item.Details)
                        {
                            detail.SetNetNetWeight(detail.NetNetWeight == 0 ? 0.9 * detail.NetWeight : detail.NetNetWeight, _identityProvider.Username, UserAgent);

                        }
                    }

                    var totalNnw = garmentPackingListModel.Items
                                   .SelectMany(i => i.Details.Where(d => d.IsDeleted == false).Select(d => new { d.Carton1, d.Carton2, totalNetNetWeight = d.CartonQuantity * d.NetNetWeight }))
                                   .GroupBy(g => new { g.Carton1, g.Carton2 }, (key, value) => value.First().totalNetNetWeight).Sum();

                    garmentPackingListModel.SetNetNetWeight(totalNnw, _identityProvider.Username, UserAgent);

                    await _packingListRepository.InsertAsync(garmentPackingListModel);

                    //Update IsPL GarmentPackingOut
                    if (packingOutNos.Count() > 0)
                    {
                        await PutIsPackingListGarmentPackingOut(packingOutNos, true, garmentPackingListModel.InvoiceNo, garmentPackingListModel.Id);
                    }

                    await _packingListRepository.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return garmentPackingListModel.LocalSalesNoteNo;


        }

        public virtual async Task<GarmentReceiptSubconPackingListViewModel> ReadById(int id)
        {
            var data = await _packingListRepository.ReadByIdAsync(id);

            var viewModel = MapToViewModel(data);
            viewModel.Items = viewModel.Items.OrderBy(o => o.ComodityDescription).ToList();

            foreach (var items in viewModel.Items)
            {
                items.Details = items.Details.OrderBy(o => o.Carton1).ThenBy(o => o.Carton2).ToList();
            }


            return viewModel;
        }

        public ListResult<GarmentReceiptSubconPackingListViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var query = _packingListRepository.ReadAll();

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<GarmentReceiptSubconPackingListModel>.Filter(query, FilterDictionary);

            //List<string> SearchAttributes = new List<string>()
            //{
            //    "InvoiceNo", "InvoiceType", "PackingListType", "SectionCode", "Destination", "BuyerAgentName"
            //};
            //query = QueryHelper<GarmentPackingListModel>.Search(query, SearchAttributes, keyword);
            if (keyword != null)
            {
                query = query.Where(q => q.Items.Any(i => i.RONo.Contains(keyword)) ||
                    q.InvoiceNo.Contains(keyword) ||
                    //q.InvoiceType.Contains(keyword) ||
                    //q.PackingListType.Contains(keyword) ||
                    //q.SectionCode.Contains(keyword) ||
                    //q.Destination.Contains(keyword) ||
                    q.BuyerName.Contains(keyword));
            }

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<GarmentReceiptSubconPackingListModel>.Order(query, OrderDictionary);

            var data = query
                .Skip((page - 1) * size)
                .Take(size)
                .ToList()
                .Select(model => MapToViewModel(model))
                .ToList();

            return new ListResult<GarmentReceiptSubconPackingListViewModel>(data, page, size, query.Count());
        }

        public async Task<int> Delete(int id)
        {
            var deleted = 0;
            using (var transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    var data = await _packingListRepository.ReadByIdAsync(id);
                    List<string> packingOutNos = new List<string>();
                    foreach (var item in data.Items)
                    {
                        packingOutNos.Add(item.PackingOutNo);
                    }

                    if (packingOutNos.Count() > 0)
                    {
                        await PutIsPackingListGarmentPackingOut(packingOutNos, false,null,0);
                    }
                    //var localSalesNote = await _localSalesNoteRepository.ReadByIdAsync(data.LocalSalesNoteId);

                    //localSalesNote.SetIsSubconPackingList(false, _identityProvider.Username, UserAgent);

                    await _packingListRepository.SaveChanges();

                    deleted =  await _packingListRepository.DeleteAsync(id);

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return deleted;  
        }
        public async Task<int> Update(int id, GarmentReceiptSubconPackingListViewModel viewModel)
        {
            var updated = 0;
            using (var transaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    GarmentReceiptSubconPackingListModel newModel = MapToModel(viewModel);

                    var modelToUpdate = await _packingListRepository.ReadByIdAsync(id);

                    List<string> SetTruePackingOutNos = new List<string>();
                    List<string> SetFalsePackingOutNos = new List<string>();
                    foreach (var itemToUpdate in modelToUpdate.Items)
                    {
                        var item = newModel.Items.FirstOrDefault(i => i.Id == itemToUpdate.Id);

                        if (item != null)
                        {
                            itemToUpdate.SetComodityDescription(item.ComodityDescription, _identityProvider.Username, UserAgent);
                            itemToUpdate.SetQuantity(item.Quantity, _identityProvider.Username, UserAgent);
                            itemToUpdate.SetUomId(item.UomId, _identityProvider.Username, UserAgent);
                            itemToUpdate.SetUomUnit(item.UomUnit, _identityProvider.Username, UserAgent);
                            itemToUpdate.SetUnitId(item.UnitId, _identityProvider.Username, UserAgent);
                            itemToUpdate.SetUnitCode(item.UnitCode, _identityProvider.Username, UserAgent);
         
                            foreach (var detailToUpdate in itemToUpdate.Details)
                            {
                                var detail = item.Details.FirstOrDefault(d => d.Id == detailToUpdate.Id);
                                if (detail != null)
                                {
                                    detailToUpdate.SetCarton1(detail.Carton1, _identityProvider.Username, UserAgent);
                                    detailToUpdate.SetCarton2(detail.Carton2, _identityProvider.Username, UserAgent);
                                    detailToUpdate.SetStyle(detail.Style, _identityProvider.Username, UserAgent);

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
                                        if (size == null)
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
                                    detailToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);
                                    foreach (var size in detailToUpdate.Sizes)
                                    {
                                        size.FlagForDelete(_identityProvider.Username, UserAgent);
                                    }
                                }
                            }
                        }
                        else
                        {
                            var items = modelToUpdate.Items.FirstOrDefault(x => x.Id == itemToUpdate.Id);
                            foreach (var detail in items.Details)
                            {
                                var detailToDelete = items.Details.FirstOrDefault(d => d.Id == detail.Id);
                                if (detailToDelete != null)
                                {

                                    detailToDelete.FlagForDelete(_identityProvider.Username, UserAgent);
                                    foreach (var size in detailToDelete.Sizes)
                                    {
                                        size.FlagForDelete(_identityProvider.Username, UserAgent);
                                    }
                                }
                            }
                            SetFalsePackingOutNos.Add(itemToUpdate.PackingOutNo);
                            itemToUpdate.FlagForDelete(_identityProvider.Username, UserAgent);

                        }
                    }

                    foreach (var item in newModel.Items.Where(w => w.Id == 0))
                    {
                        SetTruePackingOutNos.Add(item.PackingOutNo);
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
                    modelToUpdate.SetInvoiceDate(newModel.InvoiceDate, _identityProvider.Username, UserAgent);
                    if (SetTruePackingOutNos.Count() > 0)
                    {
                        var distictNos = SetTruePackingOutNos.Distinct().ToList();
                        await PutIsPackingListGarmentPackingOut(distictNos, true, modelToUpdate.InvoiceNo, modelToUpdate.Id);
                    }

                    if (SetFalsePackingOutNos.Count() > 0)
                    {
                        var distictNos = SetFalsePackingOutNos.Distinct().ToList();
                        await PutIsPackingListGarmentPackingOut(distictNos, false, null, 0);
                    }

                    updated =  await _packingListRepository.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return updated;
        }

        public async Task<int> UpdateIsApproved(UpdateIsApprovedPackingListViewModel viewModel)
        {
            foreach(var id in viewModel.ids)
            {
                var model = await _packingListRepository.ReadByIdAsync(id);
                model.SetIsApproved(viewModel.Approved, _identityProvider.Username, UserAgent);
            }

            return await _packingListRepository.SaveChanges();
        }

        protected async Task<string> PutIsPackingListGarmentPackingOut(List<string> nos,bool isReceived,string invoiceNo,int packingListId)
        {
            var garmentUnitExpenditureNoteUri = ApplicationSetting.ProductionEndpoint + $"subcon-packing-outs/update-received";
            var garmentUnitExpenditureNoteResponse = await _http.PutAsync(garmentUnitExpenditureNoteUri, _identityProvider.Token, new StringContent(JsonConvert.SerializeObject(new { nos = nos, isReceived = isReceived, invoiceNo = invoiceNo, packingListId = packingListId }), Encoding.UTF8, "application/json"));

            return garmentUnitExpenditureNoteResponse.EnsureSuccessStatusCode().ToString();
        }

        public virtual async Task<MemoryStreamResult> ReadPdfFilterCarton(int id)
        {
            var data = await _packingListRepository.ReadByIdAsync(id);

            var PdfTemplate = new GarmentReceiptSubconPackingListPdfByCartonTemplate(_identityProvider);

            var viewModel = MapToViewModel(data);

            var stream = PdfTemplate.GeneratePdfTemplate(viewModel);

            return new MemoryStreamResult(stream, "Packing List Terima Subcon - " + data.InvoiceNo + ".pdf");
        }

        public async Task<List<GarmentReceiptSubconPackingListModel>>ReadByIds(List<int> ids)
        {
            var data = _packingListRepository.ReadAll().Where(x => ids.Contains(x.Id)).ToList();

            return data;
        }

    }
}
