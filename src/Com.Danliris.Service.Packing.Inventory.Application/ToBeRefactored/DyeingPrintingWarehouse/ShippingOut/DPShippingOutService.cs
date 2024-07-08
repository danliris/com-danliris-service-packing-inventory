using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.ShippingIN.ViewModel;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingWarehouse.DPShipping;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.ShippingOut.ViewModel;
using Com.Moonlay.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingWarehouse.ShippingOut
{
    public class DPShippingOutService : IDPShippingOutService
    {

        private readonly DbSet<DPShippingInputModel> _dbSetShippingIn;
        private readonly DbSet<DPShippingInputItemModel> _dbSetItemShippingInItem;
        private readonly DbSet<ProductRFIDModel> _dbSetProductRFID;
        private readonly PackingInventoryDbContext _dbContext;
        private readonly DbSet<DPShippingOutputModel> _dbSet;
        private readonly DbSet<DPShippingOutputItemModel> _dbSetItem;
        private readonly IIdentityProvider _identityProvider;
        private const string UserAgent = "Repository";
        private readonly DbSet<DPShippingMovementModel> _dbSetShippingMovement;
        public DPShippingOutService(PackingInventoryDbContext dbContext, IServiceProvider serviceProvider)
        {

            _dbSetShippingIn = dbContext.Set<DPShippingInputModel>();
            _dbSetItemShippingInItem = dbContext.Set<DPShippingInputItemModel>();
            _dbSetProductRFID = dbContext.Set<ProductRFIDModel>();
            _dbContext = dbContext;
            _dbSet = dbContext.Set<DPShippingOutputModel>();
            _dbSetItem = dbContext.Set<DPShippingOutputItemModel>();
            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
            _dbSetShippingMovement = dbContext.Set<DPShippingMovementModel>();

        }

        

        public List<InputShippingItemViewModel> GetInputByDeliveryOrder(long deliveryOrderId)
        {
            //var data = _dbSetItemShippingInItem.AsNoTracking().Where(x => x.DeliveryOrderSalesId == deliveryOrderId);
            IQueryable<DPShippingInputItemModel> inputShippingItem;

            if (deliveryOrderId == 0)
            {
                inputShippingItem = _dbSetItemShippingInItem.AsNoTracking().Where(x => x.DeliveryOrderSalesId == deliveryOrderId && !x.HasOutputArea).Take(100) ;
            }
            else
            {
                inputShippingItem = _dbSetItemShippingInItem.AsNoTracking().Where(x => x.DeliveryOrderSalesId == deliveryOrderId && !x.HasOutputArea);
            }

            var data = inputShippingItem.Select(s => new InputShippingItemViewModel
            {
                Id = s.Id,
                DeliveryOrder = new DeliveryOrderSales()
                {
                    Id = s.DeliveryOrderSalesId,
                    No = s.DeliveryOrderSalesNo,
                    Name = s.DestinationBuyerName
                },
                ProductionOrder = new ProductionOrder()
                {
                    Id = s.ProductionOrderId,
                    No = s.ProductionOrderNo,
                    OrderQuantity = Convert.ToDouble(s.ProductionOrderOrderQuantity),
                    Type = s.ProductionOrderType
                },
                MaterialWidth = s.MaterialWidth,
                MaterialOrigin = s.MaterialOrigin,
                FinishWidth = s.FinishWidth,
                Material = new Material()
                {
                    Id = s.MaterialId,
                    Name = s.MaterialName
                },
                MaterialConstruction = new MaterialConstruction()
                {
                    Name = s.MaterialConstructionName,
                    Id = s.MaterialConstructionId
                },
                ProcessType = new CommonViewModelObjectProperties.ProcessType()
                {
                    Id = s.ProcessTypeId,
                    Name = s.ProcessTypeName
                },
                YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                {
                    Id = s.YarnMaterialId,
                    Name = s.YarnMaterialName
                },
                Construction = s.Construction,
                Unit = s.Unit,
                BuyerId = s.BuyerId,
                Buyer = s.BuyerName,
                Color = s.Color,
                Motif = s.Motif,
                UomUnit = s.UomUnit,
                Grade = s.Grade,
                PackagingUnit = s.PackagingUnit,
                PackagingQty = s.PackagingQty,
                Balance = s.Balance,
                PackingType = s.PackagingType,
                PackingInstruction = s.PackingInstruction,
                Remark = s.Remark,
                PackingLength = s.PackagingLength,
                ProductSKUId = s.ProductSKUId,
                ProductSKUCode = s.ProductSKUCode,
                FabricPackingId = s.FabricPackingId,
                ProductPackingCode = s.ProductPackingCode,
                ProductPackingId = s.ProductPackingId,
                FabricSKUId = s.FabricSKUId,
                
                DeliveryOrderSalesType = s.DeliveryOrderSalesType,
                Description = s.Description,
                DestinationBuyerName = s.DestinationBuyerName,
                CreatedUtcOrderNo = s.CreatedUtcOrderNo,
                RFIDList = _dbSetProductRFID.Where(x => x.ProductPackingId == s.ProductPackingId && x.DOId == s.DeliveryOrderSalesId && x.CurrentArea == DyeingPrintingArea.SHIPPING).Select(p => new RFIDViewModel()
                {
                    RFID = p.RFID

                }).ToList()



            });

            return data.ToList();
        }

        public async Task<int> Create(OutputShippingViewModel viewModel)
        {
            int Created = 0;
            using (var transaction = this._dbContext.Database.BeginTransaction()) 
            {
                try
                {
                    DPShippingOutputModel model = null;
                    if (viewModel.DestinationArea == DyeingPrintingArea.PENJUALAN)
                    {
                        //model = _repository.GetDbSet().AsNoTracking()
                        //.FirstOrDefault(s => s.Area == DyeingPrintingArea.SHIPPING && s.DestinationArea == viewModel.DestinationArea
                        //&& s.Date.Date == viewModel.Date.Date & s.Shift == viewModel.Shift && s.DeliveryOrderSalesId == viewModel.DeliveryOrder.Id && s.Type == DyeingPrintingArea.OUT && s.ShippingCode == viewModel.ShippingCode);

                        if (viewModel.ShippingItems.First().ShippingGrade == "BQ")
                        {

                            model = _dbSet.AsNoTracking()
                            .FirstOrDefault(s =>  s.DestinationArea == viewModel.DestinationArea
                            && s.Date.Date == viewModel.Date.Date && s.DeliveryOrderSalesId == viewModel.DeliveryOrder.Id && s.ShippingCode == viewModel.ShippingCode
                            && s.BonNo.Contains("BQ"));
                        }
                        else
                        {
                            model = _dbSet.AsNoTracking()
                            .FirstOrDefault(s => s.DestinationArea == viewModel.DestinationArea
                            && s.Date.Date == viewModel.Date.Date && s.DeliveryOrderSalesId == viewModel.DeliveryOrder.Id && s.ShippingCode == viewModel.ShippingCode
                            && s.BonNo.Contains("BS"));
                        }
                    }
                    else
                    {
                        if (viewModel.DestinationArea == DyeingPrintingArea.BUYER)
                        {
                            if (viewModel.ShippingItems.First().ShippingGrade == "BQ")
                            {
                                model = _dbSet.AsNoTracking().FirstOrDefault(s => s.DestinationArea == viewModel.DestinationArea
                                    && s.Date.Date == viewModel.Date.Date && s.DeliveryOrderSalesId == viewModel.DeliveryOrder.Id && s.ShippingCode == viewModel.ShippingCode
                                    && s.BonNo.Contains("BQ"));
                            }
                            else
                            {
                                model = _dbSet.AsNoTracking().FirstOrDefault(s => s.DestinationArea == viewModel.DestinationArea
                                && s.Date.Date == viewModel.Date.Date && s.DeliveryOrderSalesId == viewModel.DeliveryOrder.Id && s.ShippingCode == viewModel.ShippingCode
                                && s.BonNo.Contains("BS"));
                            }

                        }


                    }

                    viewModel.ShippingItems = viewModel.ShippingItems.Where(s => s.IsSave).ToList();
                    #region If First Data in Header
                    if (model == null)
                    {
                        if (viewModel.DestinationArea != DyeingPrintingArea.BUYER)
                        {
                            int totalCurrentYearData = 0;
                            string bonNo = "";
                            totalCurrentYearData = _dbSet.Count(s => s.DestinationArea == viewModel.DestinationArea && s.CreatedUtc.Year == viewModel.Date.Year && s.ShippingCode == viewModel.ShippingCode);

                            bonNo = GenerateBonNoPenjualan(totalCurrentYearData + 1, viewModel.Date, viewModel.ShippingCode, viewModel.ShippingItems.First().ShippingGrade);

                            model = new DPShippingOutputModel(
                                    viewModel.Date,  bonNo, false, viewModel.DestinationArea, Convert.ToInt32(viewModel.DeliveryOrder.Id),
                                    viewModel.DeliveryOrder.No, viewModel.DestinationBuyerName, viewModel.ShippingCode, viewModel.UpdateBySales,
                                    viewModel.ShippingItems.Select( s => new DPShippingOutputItemModel( DyeingPrintingArea.SHIPPING,
                                        viewModel.DestinationArea, false, Convert.ToInt32(viewModel.DeliveryOrder.Id), viewModel.DeliveryOrder.No, Convert.ToInt32(s.ProductionOrder.Id), s.ProductionOrder.No,
                                        s.ProductionOrder.Type, Convert.ToString( s.ProductionOrder.OrderQuantity), s.BuyerId, s.Buyer, s.Construction, s.Unit, s.Color, s.Motif,s.Grade,
                                        s.UomUnit, s.Balance, s.DPAreaInputProductionOrderId, s.PackagingUnit, s.PackingType, s.PackagingQty, s.ShippingGrade, s.ShippingRemark,
                                        s.Weight, s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.Remark, s.ProcessType.Id,
                                        s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.ProductPackingId, s.FabricPackingId,
                                        s.ProductPackingCode, s.PackingLength, s.FinishWidth, s.DestinationBuyerName, s.MaterialOrigin, s.DeliveryOrderSalesType, s.PackingInstruction, s.Id

                                        )).ToList());

                            model.FlagForCreate(_identityProvider.Username, UserAgent);

                            _dbSet.Add(model);

                            foreach (var item in model.DPShippingOutputItems)
                            {
                                item.FlagForCreate(_identityProvider.Username, UserAgent);


                            }

                            foreach (var s in viewModel.ShippingItems) {
                                //update data shipping input
                                var modelInput = _dbSetItemShippingInItem.FirstOrDefault(x => x.Id == s.Id);
                                modelInput.HasOutputArea = true;
                                EntityExtension.FlagForUpdate(modelInput, _identityProvider.Username, UserAgent);

                                //update Data RFID

                                var productRFID = _dbSetProductRFID.Where(x => x.ProductPackingId == s.ProductPackingId && x.DOId == s.DeliveryOrder.Id && x.CurrentArea == DyeingPrintingArea.SHIPPING);
                                foreach (var i in productRFID)
                                {
                                    var itemRFID = _dbSetProductRFID.FirstOrDefault(x => x.Id == i.Id);

                                    itemRFID.CurrentArea = DyeingPrintingArea.PENJUALAN;
                                    EntityExtension.FlagForUpdate(itemRFID, _identityProvider.Username, UserAgent);

                                }
                            }
                                



                            Created = await _dbContext.SaveChangesAsync();

                            await createMovement(model.DPShippingOutputItems.ToList());
                        }
                        else { // Buyer
                            string DOType = viewModel.ShippingItems.Select(s => s.DeliveryOrder.Type).First();
                            var dataDO = (from a in _dbSet
                                          join b in _dbSetItem on a.Id equals b.DPShippingOutputId
                                          where
                                          
                                           a.DestinationArea == viewModel.DestinationArea
                                           && a.CreatedUtc.Year == viewModel.Date.Year
                                          
                                           && b.DeliveryOrderSalesType == DOType
                                          select new
                                          {
                                              DeliveryOrderSalesType = b.DeliveryOrderSalesType,
                                              PackingListNo = a.PackingListNo

                                          }).GroupBy(x => new { x.PackingListNo, x.DeliveryOrderSalesType }).Select(d => new
                                          {
                                              DeliveryOrderSalesType = d.Key.DeliveryOrderSalesType,
                                              PackingListNo = d.Key.PackingListNo

                                          });


                            var totalPackingListNo = dataDO.Count();

                            var packingListNo = GeneratePackingListNo(DOType, totalPackingListNo, viewModel.Date);

                            model = new DPShippingOutputModel(
                                    viewModel.Date, viewModel.BonNo, false, viewModel.DestinationArea, Convert.ToInt32(viewModel.DeliveryOrder.Id),
                                    viewModel.DeliveryOrder.No, viewModel.DestinationBuyerName, viewModel.ShippingCode, viewModel.UpdateBySales,
                                    packingListNo, viewModel.PackingType, viewModel.PackingListRemark, viewModel.PackingListAuthorized, viewModel.PackingListLCNumber,
                                    viewModel.PackingListIssuedBy, viewModel.PackingListDescription,
                                    viewModel.ShippingItems.Select(s => new DPShippingOutputItemModel(DyeingPrintingArea.PENJUALAN,
                                       viewModel.DestinationArea, false, Convert.ToInt32(viewModel.DeliveryOrder.Id), viewModel.DeliveryOrder.No, Convert.ToInt32(s.ProductionOrder.Id), s.ProductionOrder.No,
                                       s.ProductionOrder.Type, Convert.ToString(s.ProductionOrder.OrderQuantity), s.BuyerId, s.Buyer, s.Construction, s.Unit, s.Color, s.Motif, s.Grade,
                                       s.UomUnit, s.Balance, s.DPAreaInputProductionOrderId, s.PackagingUnit, s.PackingType, s.PackagingQty, s.ShippingGrade, s.ShippingRemark,
                                       s.Weight, s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.Remark, s.ProcessType.Id,
                                       s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.ProductPackingId, s.FabricPackingId,
                                       s.ProductPackingCode, s.PackingLength, s.FinishWidth, s.DestinationBuyerName, s.MaterialOrigin, s.DeliveryOrderSalesType, s.PackingInstruction, s.Id,
                                       s.PackingListNet, s.PackingListGross, s.PackingListBaleNo, s.DeliveryNote

                                       )).ToList());

                            model.FlagForCreate(_identityProvider.Username, UserAgent);

                            _dbSet.Add(model);

                            foreach (var item in viewModel.ShippingItems)
                            {
                                //update data shipping output penjualan
                                var modelInput = _dbSetItem.FirstOrDefault(x => x.Id == item.Id);
                                modelInput.HasNextAreaDocument = true;
                                EntityExtension.FlagForUpdate(modelInput, _identityProvider.Username, UserAgent);
                            }

                                //var modelShippingInput = _dbSet.FirstOrDefault(x => x.Id == viewModel.Id);
                                //modelShippingInput.HasOuputArea = true;
                                //EntityExtension.FlagForUpdate(modelShippingInput, _identityProvider.Username, UserAgent);
                            foreach (var item in model.DPShippingOutputItems)
                            {
                                
                           
                                item.FlagForCreate(_identityProvider.Username, UserAgent);
                            }
                            Created = await _dbContext.SaveChangesAsync();

                            await createMovement(model.DPShippingOutputItems.ToList());
                        }

                      
                    }
                    #endregion

                    #region If not first data
                    else
                    {
                        var listItem = new List<DPShippingOutputItemModel>();
                        if (viewModel.DestinationArea != DyeingPrintingArea.BUYER) //penjualan
                        {
                            foreach (var s in viewModel.ShippingItems)
                            {
                                var modelItem = new DPShippingOutputItemModel(
                                         DyeingPrintingArea.SHIPPING,
                                        viewModel.DestinationArea,
                                        false,
                                        Convert.ToInt32(viewModel.DeliveryOrder.Id),
                                        viewModel.DeliveryOrder.No,
                                        Convert.ToInt32(s.ProductionOrder.Id),
                                        s.ProductionOrder.No,
                                        s.ProductionOrder.Type,
                                        Convert.ToString(s.ProductionOrder.OrderQuantity),
                                        s.BuyerId,
                                        s.Buyer,
                                        s.Construction,
                                        s.Unit,
                                        s.Color,
                                        s.Motif,
                                        s.Grade,
                                        s.UomUnit,
                                        s.Balance,
                                        model.Id,
                                        s.PackagingUnit,
                                        s.PackingType,
                                        s.PackagingQty,
                                        s.ShippingGrade,
                                        s.ShippingRemark,
                                        s.Weight,
                                        s.Material.Id,
                                        s.Material.Name,
                                        s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.Remark, s.ProcessType.Id,
                                        s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.ProductPackingId, s.FabricPackingId,
                                        s.ProductPackingCode, s.PackingLength, s.FinishWidth, s.DestinationBuyerName, s.MaterialOrigin, s.DeliveryOrderSalesType, s.PackingInstruction, s.Id);

                                modelItem.FlagForCreate(_identityProvider.Username, UserAgent);
                                listItem.Add(modelItem);
                                _dbSetItem.Add(modelItem);


                                //update data shipping input
                                var modelInput = _dbSetItemShippingInItem.FirstOrDefault(x => x.Id == s.Id);
                                modelInput.HasOutputArea = true;
                                EntityExtension.FlagForUpdate(modelInput, _identityProvider.Username, UserAgent);

                                //update Data RFID

                                var productRFID = _dbSetProductRFID.Where(x => x.ProductPackingId == s.ProductPackingId && x.DOId == s.DeliveryOrder.Id && x.CurrentArea == DyeingPrintingArea.SHIPPING);
                                foreach (var i in productRFID)
                                {
                                    var itemRFID = _dbSetProductRFID.FirstOrDefault(x => x.Id == i.Id);

                                    itemRFID.CurrentArea = DyeingPrintingArea.PENJUALAN;
                                    EntityExtension.FlagForUpdate(itemRFID, _identityProvider.Username, UserAgent);

                                }

                            }

                            //Created = await _dbContext.SaveChangesAsync();
                            //await createMovement(listItem);
                        }
                        else {

                            
                            foreach (var s in viewModel.ShippingItems)
                            {
                                var modelItem = new DPShippingOutputItemModel(
                                    DyeingPrintingArea.PENJUALAN,
                                       viewModel.DestinationArea, false, Convert.ToInt32(viewModel.DeliveryOrder.Id), viewModel.DeliveryOrder.No, Convert.ToInt32(s.ProductionOrder.Id), s.ProductionOrder.No,
                                       s.ProductionOrder.Type, Convert.ToString(s.ProductionOrder.OrderQuantity), s.BuyerId, s.Buyer, s.Construction, s.Unit, s.Color, s.Motif, s.Grade,
                                       s.UomUnit, s.Balance, s.DPAreaInputProductionOrderId, s.PackagingUnit, s.PackingType, s.PackagingQty, s.ShippingGrade, s.ShippingRemark,
                                       s.Weight, s.Material.Id, s.Material.Name, s.MaterialConstruction.Id, s.MaterialConstruction.Name, s.MaterialWidth, s.Remark, s.ProcessType.Id,
                                       s.ProcessType.Name, s.YarnMaterial.Id, s.YarnMaterial.Name, s.ProductSKUId, s.FabricSKUId, s.ProductSKUCode, s.ProductPackingId, s.FabricPackingId,
                                       s.ProductPackingCode, s.PackingLength, s.FinishWidth, s.DestinationBuyerName, s.MaterialOrigin, s.DeliveryOrderSalesType, s.PackingInstruction, s.Id,
                                       s.PackingListNet, s.PackingListGross, s.PackingListBaleNo, s.DeliveryNote

                                    );

                                modelItem.FlagForCreate(_identityProvider.Username, UserAgent);
                                listItem.Add(modelItem);
                                _dbSetItem.Add(modelItem);


                                //update data shipping output penjualan
                                var modelInput = _dbSetItem.FirstOrDefault(x => x.Id == s.Id);
                                modelInput.HasNextAreaDocument = true;
                                EntityExtension.FlagForUpdate(modelInput, _identityProvider.Username, UserAgent);

                                //update Data RFID

                                var productRFID = _dbSetProductRFID.Where(x => x.ProductPackingId == s.ProductPackingId && x.DOId == s.DeliveryOrder.Id && x.CurrentArea == DyeingPrintingArea.SHIPPING);
                                foreach (var i in productRFID)
                                {
                                    var itemRFID = _dbSetProductRFID.FirstOrDefault(x => x.Id == i.Id);

                                    itemRFID.CurrentArea = DyeingPrintingArea.PENJUALAN;
                                    EntityExtension.FlagForUpdate(itemRFID, _identityProvider.Username, UserAgent);

                                }

                            }
                            //Created = await _dbContext.SaveChangesAsync();
                            //await createMovement(listItem);



                        }
                        Created = await _dbContext.SaveChangesAsync();
                        await createMovement(listItem);

                    }
                    #endregion

                    transaction.Commit();

                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return Created;
        }

        private string GeneratePackingListNo(string DOtype, int totalPackingListNo, DateTimeOffset date)
        {
            if (DOtype == "Lokal")
            {
                return string.Format("{0}{1}{2}", "LKL", date.ToString("yy"), totalPackingListNo.ToString().PadLeft(4, '0'));

            }

            else
            {
                return string.Format("{0}{1}{2}", "EXP", date.ToString("yy"), totalPackingListNo.ToString().PadLeft(4, '0'));
            }

        }

        private string GenerateBonNoPenjualan(int totalPreviousData, DateTimeOffset date, string shippingCode, string shippingGrade)
        {
            return string.Format("{0}.{1}.{2}.{3}", shippingCode, date.ToString("yy"), totalPreviousData.ToString().PadLeft(4, '0'), shippingGrade);
        }

        private async Task<int> createMovement(List<DPShippingOutputItemModel> modelItem)
        {
            int count = 0;

            foreach (var item in modelItem)
            {
                var movement = new DPShippingMovementModel(
                        item.CreatedUtc.Date,
                        item.Area,
                        item.DestinationArea,
                        DyeingPrintingArea.OUT,
                        item.DPShippingInputItemId,
                        item.Id,
                        item.DPShippingOutputId,
                        item.ProductionOrderId,
                        item.ProductionOrderNo,
                        item.BuyerId,
                        item.BuyerName,
                        item.Construction,
                        item.Unit,
                        item.Color,
                        item.Motif,
                        item.UomUnit,
                        item.Balance,
                        item.Grade,
                        item.ProductionOrderType,
                        item.Remark,
                        item.Description,
                        item.PackagingType,
                        item.PackagingLength,
                        item.PackagingQty,
                        item.PackagingUnit,
                        item.MaterialOrigin,
                        item.ProductPackingCode,
                        item.ProductPackingId,
                        0,
                        "",
                        ""
                    );
                movement.FlagForCreate(_identityProvider.Username, UserAgent);

                _dbSetShippingMovement.Add(movement);

            }

            count = await _dbContext.SaveChangesAsync();
            return count;
        }

        public ListResult<IndexOutViewModel> Read(int page, int size, string filter, string order, string keyword)
        {
            //var query = _repository.ReadAll().Where(s => s.Area == SHIPPING &&
            //(((s.Type == OUT || s.Type == null) && s.DyeingPrintingAreaOutputProductionOrders.Any(d => !d.HasNextAreaDocument)) || (s.Type != OUT && s.Type != null)));
            var query = _dbSet.AsNoTracking();
        List<string> SearchAttributes = new List<string>()
            {
                "BonNo"
            };

            query = QueryHelper<DPShippingOutputModel>.Search(query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DPShippingOutputModel>.Filter(query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DPShippingOutputModel>.Order(query, OrderDictionary);
            var data = query.Skip((page - 1) * size).Take(size).Select(s => new IndexOutViewModel()
            {
                
                BonNo = s.BonNo,
                Date = s.Date,
                Id = s.Id,
                
                ShippingCode = s.ShippingCode,
                //Type = s.Type == null || s.Type == DyeingPrintingArea.OUT ? DyeingPrintingArea.OUT : DyeingPrintingArea.ADJ,
                
                DestinationArea = s.DestinationArea,
                DeliveryOrder = new DeliveryOrderSales()
                {
                    Id = s.DeliveryOrderSalesId,
                    No = s.DeliveryOrderSalesNo
                },
                
                UpdateBySales = s.UpdateBySales,
                ShippingItems = s.DPShippingOutputItems.Select(d => new OutputShippingItemViewModel()
                {
                    Balance = d.Balance,
                    BalanceRemains = d.Balance,
                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = d.DeliveryOrderSalesId,
                        No = d.DeliveryOrderSalesNo,
                        Type = d.DeliveryOrderSalesType
                    },
                    MaterialWidth = d.MaterialWidth,
                    MaterialOrigin = d.MaterialOrigin,
                    FinishWidth = d.FinishWidth,
                    Material = new Material()
                    {
                        Id = d.MaterialId,
                        Name = d.MaterialName
                    },
                    MaterialConstruction = new MaterialConstruction()
                    {
                        Name = d.MaterialConstructionName,
                        Id = d.MaterialConstructionId
                    },
                    YarnMaterial = new CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = d.YarnMaterialId,
                        Name = d.YarnMaterialName
                    },
                    ProcessType = new CommonViewModelObjectProperties.ProcessType()
                    {
                        Id = d.ProcessTypeId,
                        Name = d.ProcessTypeName
                    },
                    BuyerId = d.BuyerId,
                    Buyer = d.BuyerName,
                    Color = d.Color,
                    Construction = d.Construction,
                    Motif = d.Motif,
                    ProductionOrder = new ProductionOrder()
                    {
                        Id = d.ProductionOrderId,
                        No = d.ProductionOrderNo,
                        Type = d.ProductionOrderType
                    },
                    
                    Id = d.Id,
                    Unit = d.Unit,
                    Grade = d.Grade,
                    Remark = d.Remark,
                    
                    PackingLength = d.PackagingLength,
                    PackingType = d.PackagingType,
                    //QtyPacking = d.PackagingQty,
                    PackagingQty = d.PackagingQty,
                    Packing = d.PackagingUnit,
                    PackagingUnit = d.PackagingUnit,
                    ShippingGrade = d.ShippingGrade,
                    ShippingRemark = d.ShippingRemark,
                    
                    UomUnit = d.UomUnit,
                    
                    Qty = d.Balance,
                   
                    ProductSKUId = d.ProductSKUId,
                    FabricSKUId = d.FabricSKUId,
                    ProductSKUCode = d.ProductSKUCode,
                    
                    ProductPackingId = d.ProductPackingId,
                    FabricPackingId = d.FabricPackingId,
                    ProductPackingCode = d.ProductPackingCode,
                    
                    DestinationBuyerName = d.DestinationBuyerName,
                    PackingInstruction = d.PackingInstruction,
                    DeliveryOrderSalesType = d.DeliveryOrderSalesType



                }).ToList()
            });

            return new ListResult<IndexOutViewModel>(data.ToList(), page, size, query.Count());
        }

        public async Task<OutputShippingViewModel> ReadById(int id)
        {
            var model = await _dbSet.Include(s => s.DPShippingOutputItems).FirstOrDefaultAsync(s => s.Id == id);
            if (model == null)
                return null;

            var vm = MapToViewModel(model);

            return vm;


        }

        private OutputShippingViewModel MapToViewModel(DPShippingOutputModel model) 
        {
            var vm = new OutputShippingViewModel() 
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
                DestinationArea = model.DestinationArea,
                Date = model.Date,
                BonNo = model.BonNo,
                DeliveryOrder = new DeliveryOrderSales()
                { 
                    Id = model.DeliveryOrderSalesId,
                    No = model.DeliveryOrderSalesNo,
                },
                DestinationBuyerName = model.DestinationBuyerName,
                ShippingCode = model.ShippingCode,
                PackingListAuthorized = model.PackingListAuthorized,
                PackingListNo = model.PackingListNo,
                PackingListRemark = model.PackingListRemark,
                PackingType = model.PackingType,
                PackingListDescription = model.PackingListDescription,
                PackingListIssuedBy = model.PackingListIssuedBy,
                PackingListLCNumber = model.PackingListLCNumber,
                UpdateBySales = model.UpdateBySales,
                ShippingItems = model.DPShippingOutputItems.Select( s => new OutputShippingItemViewModel() 
                {
                    Id = s.Id,
                    Active = s.Active,
                    LastModifiedUtc = s.LastModifiedUtc,
                    CreatedAgent = s.CreatedAgent,
                    CreatedBy = s.CreatedBy,
                    CreatedUtc = s.CreatedUtc,
                    DeletedAgent = s.DeletedAgent,
                    DeletedBy = s.DeletedBy,
                    DeletedUtc = s.DeletedUtc,
                    DeliveryOrder = new DeliveryOrderSales()
                    {
                        Id = s.DeliveryOrderSalesId,
                        No = s.DeliveryOrderSalesNo,
                        Type = s.DeliveryOrderSalesType
                    },
                    ProductionOrder = new ProductionOrder()
                    { 
                        Id = s.ProductionOrderId,
                        No = s.ProductionOrderNo,
                        OrderQuantity =Convert.ToDouble(s.ProductionOrderOrderQuantity),
                        Type = s.ProductionOrderType
                    },
                    Material = new Material()
                    { 
                        Id = s.MaterialId,
                        Name = s.MaterialName,
                        
                    },
                    MaterialConstruction = new MaterialConstruction()
                    { 
                        Id = s.MaterialConstructionId,
                        Name = s.MaterialConstructionName,
                    },
                    ProcessType = new ProcessType()
                    { 
                        Id = s.ProcessTypeId,
                        Name = s.ProcessTypeName
                    },
                    YarnMaterial = new YarnMaterial()
                    { 
                        Id  = s.YarnMaterialId,
                        Name = s.YarnMaterialName
                    },
                    MaterialWidth = s.MaterialWidth,
                    MaterialOrigin = s.MaterialOrigin,
                    FinishWidth = s.FinishWidth,
                    Construction = s.Construction,
                    Unit = s.Unit,
                    BuyerId = s.BuyerId,
                    Buyer = s.BuyerName,
                    Color = s.Color,
                    Motif = s.Motif,
                    UomUnit = s.UomUnit,
                    Grade = s.Grade,
                    PackagingUnit = s.PackagingUnit,
                    PackagingQty = s.PackagingQty,
                    Balance = s.Balance,
                    PackingType = s.PackagingType,
                    Remark = s.Remark,
                    DPAreaInputProductionOrderId = s.DPShippingInputItemId,
                    ShippingGrade = s.ShippingGrade,
                    ShippingRemark = s.ShippingRemark,
                    PackingLength    = s.PackagingLength,
                    ProductSKUId = s.ProductSKUId,
                    FabricSKUId = s.FabricSKUId,
                    ProductSKUCode = s.ProductSKUCode,
                    ProductPackingId = s.ProductPackingId,
                    FabricPackingId = s.FabricPackingId,
                    ProductPackingCode = s.ProductPackingCode,
                    PackingListBaleNo = s.PackingListBaleNo,
                    PackingListNet = s.PackingListNet,
                    PackingListGross = s.PackingListGross,
                    DeliveryOrderSalesType = s.DeliveryOrderSalesType,
                    DeliveryOrderSalesNo = s.DeliveryOrderSalesNo,
                    DestinationBuyerName = s.DestinationBuyerName,
                    PackingInstruction = s.PackingInstruction,
                    DeliveryNote = s.DeliveryNote


                }).ToList()

            };

            return vm;


        }

        public async Task<int> Update(int Id, OutputShippingViewModel viewModel)
        {
            int Update = 0;
            using (var transaction = this._dbContext.Database.BeginTransaction())
            {
                try
                {
                    var model = _dbSet.FirstOrDefault(x => x.Id == Id);
                    model.PackingListDescription = viewModel.PackingListDescription;
                    model.PackingListAuthorized = viewModel.PackingListAuthorized;
                    model.PackingListIssuedBy = viewModel.PackingListIssuedBy;
                    model.PackingListLCNumber = viewModel.PackingListLCNumber;
                    model.PackingListRemark = viewModel.PackingListRemark;
                    model.UpdateBySales = true;
                    EntityExtension.FlagForUpdate(model, _identityProvider.Username, UserAgent);

                    foreach (var i in viewModel.ShippingItems)
                    {
                        var modelItem = _dbSetItem.FirstOrDefault(x => x.Id == i.Id);

                        if (modelItem != null)
                        {
                            modelItem.DeliveryNote = i.DeliveryNote;
                            modelItem.PackingListBaleNo = i.PackingListBaleNo;
                            modelItem.PackingListGross = i.PackingListGross;
                            modelItem.PackingListNet = i.PackingListNet;
                        }
                        else {
                            continue;
                        }
                        
                    
                    }

                    Update = await _dbContext.SaveChangesAsync();

                    transaction.Commit();

                }
                catch (Exception e){
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }

            
            }

               



            return Update;
        
        }

    }
}
