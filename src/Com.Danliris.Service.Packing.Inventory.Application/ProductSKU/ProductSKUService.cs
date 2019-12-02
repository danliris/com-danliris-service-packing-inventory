using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.ProductSKU;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.Application.ProductSKU
{
    public class ProductSKUService : IProductSKUService
    {
        private readonly IProductSKURepository _repository;

        public ProductSKUService(IProductSKURepository repository)
        {
            _repository = repository;
        }

        public Task<int> Create(CreateProductSKUViewModel viewModel)
        {
            var model = new ProductSKUModel(
                null,
                viewModel.Composition,
                viewModel.Construction,
                JsonConvert.SerializeObject(viewModel.Currency),
                viewModel.Currency.Id.Value,
                viewModel.Description,
                viewModel.Design,
                viewModel.Grade,
                viewModel.Lot,
                viewModel.Name,
                viewModel.Price.GetValueOrDefault(),
                viewModel.ProductType,
                null,
                null,
                viewModel.Tags,
                viewModel.UOM.Id.Value,
                JsonConvert.SerializeObject(viewModel.UOM),
                viewModel.Width,
                viewModel.WovenType,
                viewModel.YarnType1,
                viewModel.YarnType2
            );

            var query = _repository.ReadAll();
            var latestData = query.OrderByDescending(entity => entity.SKUCode).FirstOrDefault();
            model.SKUCode = DocumentCodeGenerator.ProductSKU(latestData?.SKUCode);
            return _repository.InsertAsync(model);
        }

        public Task<int> Delete(int id)
        {
            return _repository.DeleteAsync(id);
        }

        public Task<ProductSKUModel> ReadById(int id)
        {
            return _repository.ReadByIdAsync(id);
        }

        public async Task<ListResult<ProductSKUModel>> ReadByQuery(int page, int size, string keyword)
        {
            var query = _repository.ReadAll();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(entity => entity.Code.Contains(keyword) || entity.SKUCode.Contains(keyword));
            }

            var total = await query.CountAsync();
            query = query.Skip(page -1).Take(size);

            return new ListResult<ProductSKUModel>(query.ToList(), page, size, total);
        }
    }
}