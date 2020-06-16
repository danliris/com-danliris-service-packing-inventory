using Com.Danliris.Service.Packing.Inventory.Application.Master.Category;
using Com.Danliris.Service.Packing.Inventory.Application.Master.ProductPacking;
using Com.Danliris.Service.Packing.Inventory.Application.Master.ProductSKU;
using Com.Danliris.Service.Packing.Inventory.Application.Master.UOM;
using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
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

        public FabricProductService(IServiceProvider serviceProvider)
        {
            _categoryService = serviceProvider.GetService<ICategoryService>();
            _uomService = serviceProvider.GetService<IUOMService>();
            _productSKUService = serviceProvider.GetService<IProductSKUService>();
            _productPackingService = serviceProvider.GetService<IProductPackingService>();
            _fabricProductSKURepository = serviceProvider.GetService<IRepository<FabricProductSKUModel>>();
            _fabricProductPackingRepository = serviceProvider.GetService<IRepository<FabricProductPackingModel>>();
        }

        public async Task<BarcodeInfo> CreateProductFabric(FabricFormDto form)
        {
            var categoryId = await _categoryService.Upsert(new Category.FormDto() { Name = "FABRIC" });
            var uomId = await _uomService.Upsert(new UOM.FormDto() { Unit = form.UOMUnit });

            var productSKUDto = new ProductSKU.FormDto()
            {
                CategoryId = categoryId,
                UOMId = uomId,
                Name = GetProductName(form)
            };

            var productSKUId = await _productSKUService.Create(productSKUDto);
            var product = await _productSKUService.GetById(productSKUId);

            var packingUomId = await _uomService.Upsert(new UOM.FormDto() { Unit = form.PackingType });
            var productPackingDto = new ProductPacking.FormDto()
            {
                PackingSize = form.PackingSize,
                ProductSKUId = productSKUId,
                UOMId = packingUomId
            };
            var productPackingId = await _productPackingService.Create(productPackingDto);
            var productPacking = await _productPackingService.GetById(productPackingId);

            var model = new FabricProductSKUModel(product.Code, form.Composition, form.Construction, form.Width, form.Design, form.Grade, form.UOMUnit, productSKUId);
            var id = await _fabricProductSKURepository.InsertAsync(model);

            var modelPacking = new FabricProductPackingModel(productPacking.Code, id, productSKUId, productPackingId, form.PackingType, form.PackingSize.GetValueOrDefault());
            await _fabricProductPackingRepository.InsertAsync(modelPacking);

            return new BarcodeInfo(product, productPacking);
        }

        private string GetProductName(FabricFormDto form)
        {
            return $"{form.Composition} {form.Construction} {form.Width} {form.Design} {form.Grade}";
        }

        public async Task<BarcodeInfo> GetBarcodeByPackingCode(string code)
        {
            var model = _fabricProductPackingRepository.ReadAll().FirstOrDefault(entity => entity.Code.ToLower() == code.ToLower());
            var product = await _productSKUService.GetById(model.ProductSKUId);
            var productPacking = await _productPackingService.GetById(model.ProductPackingId);
            return new BarcodeInfo(product, productPacking);
        }
    }
}
