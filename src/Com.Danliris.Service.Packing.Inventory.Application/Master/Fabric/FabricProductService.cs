using Com.Danliris.Service.Packing.Inventory.Application.Master.Category;
using Com.Danliris.Service.Packing.Inventory.Application.Master.ProductPacking;
using Com.Danliris.Service.Packing.Inventory.Application.Master.ProductSKU;
using Com.Danliris.Service.Packing.Inventory.Application.Master.UOM;
using Com.Danliris.Service.Packing.Inventory.Application.Master.UpsertMaster;
using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Moonlay.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric
{
    public class FabricProductService : IFabricProductService
    {
        private readonly ICategoryService _categoryService;
        private readonly IUOMService _uomService;
        private readonly IProductSKUService _productSKUService;
        private readonly IProductPackingService _productPackingService;
        private readonly IRepository<FabricProductSKUModel> _fabricProductSKURepository;
        private readonly IRepository<FabricProductPackingModel> _fabricProductPackingRepository;
        private readonly IRepository<ProductPackingModel> _productPackingRepository;
        private readonly IUpsertMasterService _upsertMaster;
        private readonly IIdentityProvider _identityProvider;

        public FabricProductService(IServiceProvider serviceProvider)
        {
            _categoryService = serviceProvider.GetService<ICategoryService>();
            _uomService = serviceProvider.GetService<IUOMService>();
            _productSKUService = serviceProvider.GetService<IProductSKUService>();
            _productPackingService = serviceProvider.GetService<IProductPackingService>();

            _fabricProductSKURepository = serviceProvider.GetService<IRepository<FabricProductSKUModel>>();
            _fabricProductPackingRepository = serviceProvider.GetService<IRepository<FabricProductPackingModel>>();
            _productPackingRepository = serviceProvider.GetService<IRepository<ProductPackingModel>>();

            _upsertMaster = serviceProvider.GetService<IUpsertMasterService>();

            _identityProvider = serviceProvider.GetService<IIdentityProvider>();
        }

        public async Task<string> GenerateProductPackingCodeByCompositeId(FabricProductPackingCompositeIdFormDto form)
        {
            var fabricSKU = await _fabricProductSKURepository.ReadByIdAsync(form.SKUId.GetValueOrDefault());
            var uom = await _uomService.GetById(form.PackingUOMId.GetValueOrDefault());

            if (fabricSKU != null && uom != null)
            {
                var clientNow = DateTime.Now.AddHours(_identityProvider.TimezoneOffset);
                var packingSizeRoundedString = Math.Round(form.PackingSize.GetValueOrDefault(), 2).ToString().Replace(".", "").Replace(",", "");
                var code = fabricSKU.Code + packingSizeRoundedString + uom.Unit;

                if (!_productPackingRepository.ReadAll().Any(entity => entity.Code == code))
                {
                    //var packingModel = new ProductPackingModel(fabricSKU.ProductSKUID, uom.Id, form.PackingSize.GetValueOrDefault(), code, code);

                    //var packingId = await _productPackingRepository.InsertAsync(packingModel);
                    //var fabricPacking = new FabricProductPackingModel(code, fabricSKU.Id, fabricSKU.ProductSKUID, packingId, uom.Id, form.PackingSize.GetValueOrDefault());
                    //await _fabricProductPackingRepository.InsertAsync(fabricPacking);
                }

                return code;
            }

            return string.Empty;
        }

        public async Task<string> GenerateProductPackingCodeByCompositeString(FabricProductPackingCompositeStringFormDto form)
        {
            var fabricSKU = _fabricProductSKURepository.ReadAll().Where(entity => entity.Code == form.SKUCode).FirstOrDefault();

            if (fabricSKU != null)
            {
                var sku = await _productPackingService.GetById(fabricSKU.ProductSKUID);
                var uom = await _upsertMaster.UpsertUOM(form.PackingUOM);
                var packingSizeRoundedString = Math.Round(form.PackingSize.GetValueOrDefault(), 2).ToString().Replace(".", "").Replace(",", "");

                //var code = AppendMonthAndYear(fabricSKU.Code + uom.Unit + packingSizeRoundedString + uom.Unit);

                //if (!_productPackingRepository.ReadAll().Any(entity => entity.Code == code))
                //{
                //    //var name = $"{sku.Name} {}"
                //    var packingModel = new ProductPackingModel(fabricSKU.ProductSKUID, uom.Id, form.PackingSize.GetValueOrDefault(), code, code);

                //    var packingId = await _productPackingRepository.InsertAsync(packingModel);
                //    var fabricPacking = new FabricProductPackingModel(code, fabricSKU.Id, fabricSKU.ProductSKUID, packingId, uom.Id, form.PackingSize.GetValueOrDefault());
                //    await _fabricProductPackingRepository.InsertAsync(fabricPacking);
                //}

                //return code;
            }

            return string.Empty;
        }

        public Task<string> GenerateProductSKUCodeByCompositeId(FabricProductSKUCompositeIdFormDto form)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateProductSKUCodeByCompositeString(FabricProductSKUCompositeStringFormDto form)
        {
            throw new NotImplementedException();
        }

        public Task<PackingAndSKUCode> UpsertPackingSKU(FabricProductCompositeStringDto form)
        {
            throw new NotImplementedException();
        }
    }
}
