using Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric;
using Com.Danliris.Service.Packing.Inventory.Application.Master.ProductPacking;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingStockOpname;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingStockOpname.Warehouse
{
    public class StockOpnameMutationService : IStockOpnameMutationService
    {
        private const string UserAgent = "packing-inventory-service";
        private readonly IDyeingPrintingStockOpnameMutationRepository _stockOpnameMutationRepository;
        private readonly IDyeingPrintingStockOpnameMutationItemRepository _stockOpnameMutationItemsRepository;
        private readonly IFabricPackingSKUService _fabricPackingSKUService;
        private readonly IProductPackingService _productPackingService;
        private readonly IIdentityProvider _identityProvider;
        private readonly IServiceProvider _serviceProvider;


        public StockOpnameMutationService(IServiceProvider serviceProvider)
        {
            _stockOpnameMutationRepository = serviceProvider.GetService<IDyeingPrintingStockOpnameMutationRepository>();
            _stockOpnameMutationItemsRepository = serviceProvider.GetService<IDyeingPrintingStockOpnameMutationItemRepository>();
            _fabricPackingSKUService = serviceProvider.GetService<IFabricPackingSKUService>();
            _productPackingService = serviceProvider.GetService<IProductPackingService>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            //_outputProductionOrderRepository = serviceProvider.GetService<IDyeingPrintingAreaOutputProductionOrderRepository>();
            _serviceProvider = serviceProvider;
        }

        public async Task<int> Create(StockOpnameMutationViewModel viewModel)
        {
            int result = 0;

            //if (viewModel.Type == DyeingPrintingArea.STOCK_OPNAME)
            //{
                result = await CreateMutation(viewModel);
            //}

            return result;
        }

        private async Task<int> CreateMutation(StockOpnameMutationViewModel viewModel)
        {
            DateTime date = DateTime.Today;

            DyeingPrintingStockOpnameMutationModel model;
            int result = 0;
            if (viewModel.Type == "STOCK OPNAME")
            {
                model = _stockOpnameMutationRepository.GetDbSet().AsNoTracking().FirstOrDefault(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
                                                                                            s.Date.AddHours(7).ToString("dd/MM/YYYY").Equals(date.AddHours(7).ToString("dd/MM/YYYY")) &&
                                                                                            s.Type == DyeingPrintingArea.SO
                                                                                            && !s.IsDeleted);
            }
            else if (viewModel.Type == "ADJ OUT")
            {
                model = _stockOpnameMutationRepository.GetDbSet().AsNoTracking().FirstOrDefault(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
                                                                                            s.Date.AddHours(7).ToString("dd/MM/YYYY").Equals(date.AddHours(7).ToString("dd/MM/YYYY")) &&
                                                                                            s.Type == DyeingPrintingArea.ADJ_OUT
                                                                                            && !s.IsDeleted);

            }
            else {
                model = null;
            }

            var typeOut = viewModel.Type == "STOCK OPNAME" ? "SO" : "ADJ OUT";

            viewModel.DyeingPrintingStockOpnameMutationItems = viewModel.DyeingPrintingStockOpnameMutationItems.ToList();

            if (model == null) {
                int totalCurentYearData = _stockOpnameMutationRepository.ReadAllIgnoreQueryFilter()
                                                .Count(s => s.Area == DyeingPrintingArea.GUDANGJADI &&
                                                            s.CreatedUtc.Year == viewModel.Date.Year && s.Type == DyeingPrintingArea.STOCK_OPNAME);

                string bonNo = GenerateBonNo(totalCurentYearData + 1, viewModel.Date, DyeingPrintingArea.GUDANGJADI, typeOut);

                

                model = new DyeingPrintingStockOpnameMutationModel(DyeingPrintingArea.GUDANGJADI, bonNo, DateTime.Today,
                                                                    typeOut,
                                                                    viewModel.DyeingPrintingStockOpnameMutationItems.Select(s =>
                                                                        new DyeingPrintingStockOpnameMutationItemModel(
                                                                            (double)s.SendQuantity * s.PackagingLength,
                                                                            s.Color,
                                                                            s.Construction,
                                                                            s.Grade,
                                                                            s.Motif,
                                                                            s.SendQuantity,
                                                                            s.PackagingLength,
                                                                            s.PackagingType,
                                                                            s.PackagingUnit,
                                                                            s.ProductionOrder.Id,
                                                                            s.ProductionOrder.No,
                                                                            s.ProductionOrder.Type,
                                                                            s.ProductionOrder.OrderQuantity,
                                                                            s.ProcessTypeId,
                                                                            s.ProcessTypeName,
                                                                            s.Remark,
                                                                            s.Unit,
                                                                            s.UomUnit,
                                                                            s.Track.Id,
                                                                            s.Track.Type,
                                                                            s.Track.Name,
                                                                            s.ProductSKUId,
                                                                            s.FabricSKUId,
                                                                            s.ProductSKUCode,
                                                                            s.ProductPackingId,
                                                                            s.FabricPackingId,
                                                                            s.ProductPackingCode,
                                                                            typeOut
                                                                            
                                                                            )).ToList());
                result = await _stockOpnameMutationRepository.InsertAsync(model);
                await _stockOpnameMutationRepository.UpdateAsync(model.Id, model);

            }
            else
            {
                foreach (var item in viewModel.DyeingPrintingStockOpnameMutationItems )
                {
                    var modelItem = new DyeingPrintingStockOpnameMutationItemModel(
                                                                            (double)item.SendQuantity * item.PackagingLength,
                                                                            item.Color,
                                                                            item.Construction,
                                                                            item.Grade,
                                                                            item.Motif,
                                                                            item.SendQuantity,
                                                                            item.PackagingLength,
                                                                            item.PackagingType,
                                                                            item.PackagingUnit,
                                                                            item.ProductionOrder.Id,
                                                                            item.ProductionOrder.No,
                                                                            item.ProductionOrder.Type,
                                                                            item.ProductionOrder.OrderQuantity,
                                                                            item.ProcessTypeId,
                                                                            item.ProcessTypeName,
                                                                            item.Remark,
                                                                            item.Unit,
                                                                            item.UomUnit,
                                                                            item.Track.Id,
                                                                            item.Track.Type,
                                                                            item.Track.Name,
                                                                            item.ProductSKUId,
                                                                            item.FabricSKUId,
                                                                            item.ProductSKUCode,
                                                                            item.ProductPackingId,
                                                                            item.FabricPackingId,
                                                                            item.ProductPackingCode,
                                                                            typeOut
                        );

                    modelItem.DyeingPrintingStockOpnameMutationId = model.Id;
                    result += await _stockOpnameMutationItemsRepository.InsertAsync(modelItem);
                }


            }
            return result;
        }

        public string GenerateBonNo(int totalPreviousData, DateTimeOffset date, string destinationArea, string typeOut)
        {
            var bonNo = "";
            if (destinationArea == DyeingPrintingArea.GUDANGJADI)
            {
                bonNo = string.Format("{0}.{1}.{2}.{3}", DyeingPrintingArea.GJ, typeOut, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'));
            }


            return bonNo;
        }
        public ListResult<IndexViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            var validIds = _stockOpnameMutationItemsRepository.ReadAll().Select(entity => entity.DyeingPrintingStockOpnameMutationId).ToList();
            var query = _stockOpnameMutationRepository.ReadAll().Where(s => s.Area == DyeingPrintingArea.GUDANGJADI && validIds.Contains(s.Id));

            List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DyeingPrintingStockOpnameMutationModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyeingPrintingStockOpnameMutationModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyeingPrintingStockOpnameMutationModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new IndexViewModel()
            {
                Id = s.Id,
                Area = s.Area,
                Type = DyeingPrintingArea.STOCK_OPNAME,
                BonNo = s.BonNo,
                Date = s.Date,

            });

            return new ListResult<IndexViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<StockOpnameMutationViewModel> ReadById(int id)
        {
            var model = await _stockOpnameMutationRepository.ReadByIdAsync(id);
            if (model == null)
                return null;

            StockOpnameMutationViewModel vm = await MapToViewModel(model);

            return vm;
        }

        private async Task<StockOpnameMutationViewModel> MapToViewModel(DyeingPrintingStockOpnameMutationModel model)
        {
            var vm = new StockOpnameMutationViewModel();
            vm = new StockOpnameMutationViewModel
            {

                Active = model.Active,
                Id = model.Id,
                Area = model.Area,
                BonNo = model.BonNo,
                Type = model.Type,
                Bon = new IndexViewModel
                {
                    Area = model.Area,
                    BonNo = model.BonNo,
                    Date = model.Date,
                    Id = model.Id
                },
                CreatedAgent = model.CreatedAgent,
                CreatedBy = model.CreatedBy,
                CreatedUtc = model.CreatedUtc,
                Date = model.Date,
                DeletedAgent = model.DeletedAgent,
                DeletedBy = model.DeletedBy,
                DeletedUtc = model.DeletedUtc,
                IsDeleted = model.IsDeleted,
                LastModifiedAgent = model.LastModifiedAgent,
                LastModifiedBy = model.LastModifiedBy,
                LastModifiedUtc = model.LastModifiedUtc,
                DyeingPrintingStockOpnameMutationItems = model.DyeingPrintingStockOpnameMutationItems.Where(x => !x.IsDeleted).Select(s => new StockOpnameMutationItemViewModel() 
                {
                    Active = s.Active,
                    LastModifiedUtc = s.LastModifiedUtc,
                    
                    Color = s.Color,
                    Construction = s.Construction,
                    CreatedAgent = s.CreatedAgent,
                    CreatedBy = s.CreatedBy,
                    CreatedUtc = s.CreatedUtc,
                    DeletedAgent = s.DeletedAgent,
                    DeletedBy = s.DeletedBy,
                    DeletedUtc = s.DeletedUtc,
                    Balance = s.Balance,
                    Grade = s.Grade,
                    Motif = s.Motif,
                    PackagingQty = s.PackagingQty,
                    PackagingLength = s.PackagingLength,
                    PackagingType = s.PackagingType,
                    PackagingUnit = s.PackagingUnit,
                    ProductionOrder = new ProductionOrder()
                    { 
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        Type = s.ProductionOrderType,
                        OrderQuantity = s.ProductionOrderOrderQuantity
                    },
                    Remark = s.Remark,
                    ProcessTypeId = s.ProcessTypeId,
                    ProcessTypeName = s.ProcessTypeName,
                    Unit = s.Unit,
                    UomUnit = s.UomUnit,
                    Track = new Track()
                    { 
                        Id = s.TrackId,
                        Name = s.TrackName,
                        Type = s.TrackType
                    },
                    ProductSKUId = s.ProductSKUId,
                    FabricSKUId = s.FabricSKUId,
                    ProductSKUCode = s.ProductSKUCode,
                    ProductPackingId = s.ProductPackingId,
                    FabricPackingId = s.FabricPackingId,
                    ProductPackingCode = s.ProductPackingCode,
                    TypeOut = s.TypeOut,
                    SendQuantity = s.PackagingQty
                }).ToList()    
            };

            return vm;

        }
        
    
    
    }



    
}
