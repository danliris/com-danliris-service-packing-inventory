using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.ProductRFID
{
    public class ProductRFIDService : IProductRFIDService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRepository<ProductRFIDModel> _productRFIDRepository;
        private readonly IRepository<ProductPackingModel> _productPackingRepository;
        private readonly IRepository<ProductSKUModel> _productSKURepository;
        private readonly IRepository<UnitOfMeasurementModel> _uomRepository;
        private readonly IRepository<FabricProductSKUModel> _fabricRepository;
        private readonly IRepository<FabricProductPackingModel> _fabricPackingRepository;
        private readonly PackingInventoryDbContext _dbContext;
        public ProductRFIDService(IServiceProvider serviceProvider,  PackingInventoryDbContext dbContext) 
        {
            _serviceProvider = serviceProvider;
            _dbContext = dbContext;
            _productRFIDRepository = serviceProvider.GetService<IRepository<ProductRFIDModel>>();
            _productPackingRepository = serviceProvider.GetService<IRepository<ProductPackingModel>>();
            _productSKURepository = serviceProvider.GetService<IRepository<ProductSKUModel>>();
            _uomRepository = serviceProvider.GetService<IRepository<UnitOfMeasurementModel>>();
            _fabricRepository = serviceProvider.GetService<IRepository<FabricProductSKUModel>>();
            _fabricPackingRepository = serviceProvider.GetService<IRepository<FabricProductPackingModel>>();
        }

        public async Task<ProductRFIDIndex> GetIndex(IndexQueryParam queryParam)
        {
            if (string.IsNullOrWhiteSpace(queryParam.order))
                queryParam.order = "{}";

            var searchAttributes = new List<string>() { "Name", "Code", "UOMUnit", "ProductSKUName" };
            var order = JsonConvert.DeserializeObject<Dictionary<string, string>>(queryParam.order);


            var productRFIDQuery = _productRFIDRepository.ReadAll();
            var productPackingQuery = _productPackingRepository.ReadAll();
            var productSKUQuery = _productSKURepository.ReadAll();
            var uomQuery = _uomRepository.ReadAll();


            var query = from productRFID in productRFIDQuery
                        join productPacking in productPackingQuery on productRFID.ProductPackingId equals productPacking.Id into productPackings
                        from packing in productPackings.DefaultIfEmpty()
                        join uom in uomQuery on packing.UOMId equals uom.Id into uomProducts
                        from uomProduct in uomProducts.DefaultIfEmpty()

                        join productSKU in productSKUQuery on packing.ProductSKUId equals productSKU.Id into products
                        from product in products.DefaultIfEmpty()

                        select new ProductRFIDIndexInfo()
                        {
                            LastModifiedUtc = packing.LastModifiedUtc,
                            RFID = productRFID.RFID,
                            ProductPackingCode = packing.Code,
                            UOMUnit = uomProduct.Unit,
                            ProductSKUName = product.Name,
                            PackingSize = packing.PackingSize,
                            Id = productRFID.Id
                        };

            query = QueryHelper<ProductRFIDIndexInfo>.Search(query, searchAttributes, queryParam.keyword, true);
            query = QueryHelper<ProductRFIDIndexInfo>.Order(query, order);

            var total = await query.CountAsync();
            var data = query.Skip(queryParam.size * (queryParam.page - 1)).Take(queryParam.size).ToList();
            return new ProductRFIDIndex(data, total, queryParam.page, queryParam.size);
        }

        public async Task<ProductRFIDDto> GetById(int id)
        {
            var productRFID = await _productRFIDRepository.ReadByIdAsync(id);
            if (productRFID != null)
            {
                var productPacking = await _productPackingRepository.ReadByIdAsync(productRFID.ProductPackingId);
                var uom = await _uomRepository.ReadByIdAsync(productPacking.UOMId);
                var productSKU = await _productSKURepository.ReadByIdAsync(productPacking.ProductSKUId);
                var fabricSKU =  _dbContext.FabricProductSKUs.FirstOrDefault( x => x.ProductSKUId == productSKU.Id);

                return new ProductRFIDDto(productRFID, productPacking, productSKU, uom, fabricSKU);
            }

            return null;
        }

        
        


    }
}
