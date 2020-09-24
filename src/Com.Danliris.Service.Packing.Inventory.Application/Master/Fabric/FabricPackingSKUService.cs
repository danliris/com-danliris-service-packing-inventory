using Com.Danliris.Service.Packing.Inventory.Application.Helper;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory;
using Com.Danliris.Service.Packing.Inventory.Infrastructure;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Utilities;
using Com.Moonlay.Models;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Application.Master.Fabric
{
    public class FabricPackingSKUService : IFabricPackingSKUService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PackingInventoryDbContext _dbContext;
        private const string TYPE = "FABRIC";

        public FabricPackingSKUService(IServiceProvider serviceProvider, PackingInventoryDbContext dbContext)
        {
            _unitOfWork = serviceProvider.GetService<IUnitOfWork>();
            _dbContext = dbContext;
        }
        public int CreateSKU(FabricSKUFormDto form)
        {
            var code = GenerateCode(form);

            if (_dbContext.ProductSKUs.Any(entity => entity.Code == code))
                return 0;

            var category = _dbContext.IPCategories.FirstOrDefault(entity => entity.Name == TYPE);

            var product = new ProductSKUModel(code, code, form.UOMId.GetValueOrDefault(), category.Id, "");
            _unitOfWork.ProductSKUs.Insert(product);
            _unitOfWork.Commit();

            var fabric = new FabricProductSKUModel(code, product.Id, form.WovenTypeId.GetValueOrDefault(), form.ConstructionId.GetValueOrDefault(), form.WidthId.GetValueOrDefault(), form.WarpId.GetValueOrDefault(), form.WeftId.GetValueOrDefault(), form.ProcessTypeId.GetValueOrDefault(), form.YarnTypeId.GetValueOrDefault(), form.GradeId.GetValueOrDefault(), form.UOMId.GetValueOrDefault());
            _unitOfWork.FabricSKUProducts.Insert(fabric);
            _unitOfWork.Commit();

            return fabric.Id;
        }

        private string GenerateCode(FabricSKUFormDto form)
        {
            var wovenType = _dbContext.IPWovenType.FirstOrDefault(entity => entity.Id == form.WovenTypeId.GetValueOrDefault());
            var wovenCode = "000";
            if (wovenType != null)
                wovenCode = wovenType.Code.PadLeft(3, '0');

            var construction = _dbContext.IPMaterialConstructions.FirstOrDefault(entity => entity.Id == form.ConstructionId.GetValueOrDefault());
            var constructionCode = "000";
            if (construction != null)
                constructionCode = construction.Code.PadLeft(3, '0');

            var width = _dbContext.IPWidthType.FirstOrDefault(entity => entity.Id == form.WidthId.GetValueOrDefault());
            var widthCode = "00";
            if (width != null)
                widthCode = width.Code.PadLeft(2, '0');

            var warp = _dbContext.IPWarpTypes.FirstOrDefault(entity => entity.Id == form.WarpId.GetValueOrDefault());
            var warpCode = "00";
            if (warp != null)
                warpCode = warp.Code.PadLeft(2, '0');

            var weft = _dbContext.IPWeftTypes.FirstOrDefault(entity => entity.Id == form.WeftId.GetValueOrDefault());
            var weftCode = "00";
            if (weft != null)
                weftCode = weft.Code.PadLeft(2, '0');

            var processType = _dbContext.IPProcessType.FirstOrDefault(entity => entity.Id == form.ProcessTypeId.GetValueOrDefault());
            var processTypeCode = "0";
            if (processType != null)
                processTypeCode = processType.Code;

            var yarnType = _dbContext.IPYarnType.FirstOrDefault(entity => entity.Id == form.YarnTypeId.GetValueOrDefault());
            var yarnTypeCode = "000";
            if (yarnType != null)
                yarnTypeCode = yarnType.Code.PadLeft(3, '0');

            var grade = _dbContext.IPGrades.FirstOrDefault(entity => entity.Id == form.GradeId.GetValueOrDefault());
            var gradeCode = "0";
            if (grade != null)
                gradeCode = grade.Code;

            var uom = _dbContext.IPUnitOfMeasurements.FirstOrDefault(entity => entity.Id == form.UOMId.GetValueOrDefault());
            var uomCode = "0";
            var uomMeterString = new List<string>() { "MTR", "METER" };
            var uomYardString = new List<string>() { "YDS", "YRD", "YARD" };
            if (grade != null)
                if (uomMeterString.Contains(uom.Unit.ToUpper()))
                    uomCode = "1";
                else if (uomYardString.Contains(uom.Unit.ToUpper()))
                    uomCode = "2";

            var monthCode = DateTime.Now.Month.ToString("d2");
            var yearCode = DateTime.Now.Year.ToString().Substring(2);

            return string.Concat(wovenCode, constructionCode, widthCode, warpCode, weftCode, processTypeCode, yarnTypeCode, gradeCode, uomCode, monthCode, yearCode);
        }

        public int DeleteSKU(int id)
        {
            _unitOfWork.FabricSKUProducts.Delete(id);
            _unitOfWork.Commit();
            return id;
        }

        public FabricSKUDto GetById(int id)
        {
            var fabric = _unitOfWork.FabricSKUProducts.GetByID(id);
            var wovenType = _dbContext.IPWovenType.FirstOrDefault(entity => entity.Id == fabric.WovenTypeId);
            var construction = _dbContext.IPMaterialConstructions.FirstOrDefault(entity => entity.Id == fabric.ConstructionId);
            var width = _dbContext.IPWidthType.FirstOrDefault(entity => entity.Id == fabric.WidthId);
            var warp = _dbContext.IPWarpTypes.FirstOrDefault(entity => entity.Id == fabric.WarpId);
            var weft = _dbContext.IPWeftTypes.FirstOrDefault(entity => entity.Id == fabric.WeftId);
            var processType = _dbContext.IPProcessType.FirstOrDefault(entity => entity.Id == fabric.ProcessTypeId);
            var yarnType = _dbContext.IPYarnType.FirstOrDefault(entity => entity.Id == fabric.YarnTypeId);
            var grade = _dbContext.IPGrades.FirstOrDefault(entity => entity.Id == fabric.GradeId);
            var uom = _dbContext.IPUnitOfMeasurements.FirstOrDefault(entity => entity.Id == fabric.UOMId);

            return new FabricSKUDto()
            {
                Id = fabric.Id,
                ProductSKUId = fabric.ProductSKUId,
                Code = fabric.Code,
                WovenType = wovenType,
                Construction = construction,
                Width = width,
                Warp = warp,
                Weft = weft,
                ProcessType = processType,
                YarnType = yarnType,
                Grade = grade,
                UOM = uom
            };
        }

        public async Task<FabricSKUIndex> GetIndex(IndexQueryParam queryParam)
        {
            if (string.IsNullOrWhiteSpace(queryParam.order))
                queryParam.order = "{}";

            var searchAttributes = new List<string>() { "Code" };
            var order = JsonConvert.DeserializeObject<Dictionary<string, string>>(queryParam.order);

            var fabrics = _dbContext.FabricProductSKUs;
            var wovenTypes = _dbContext.IPWovenType;
            var constructions = _dbContext.IPMaterialConstructions;
            var widths = _dbContext.IPWidthType;
            var warpTypes = _dbContext.IPWarpTypes;
            var weftTypes = _dbContext.IPWeftTypes;
            var processTypes = _dbContext.IPProcessType;
            var yarnTypes = _dbContext.IPYarnType;
            var grades = _dbContext.IPGrades;
            var uoms = _dbContext.IPUnitOfMeasurements;

            var query = from product in fabrics

                        join wovenType in wovenTypes on product.WovenTypeId equals wovenType.Id into wovenTypeProducts
                        from wovenTypeProduct in wovenTypeProducts.DefaultIfEmpty()

                        join construction in constructions on product.ConstructionId equals construction.Id into constructionProducts
                        from constructionProduct in constructionProducts.DefaultIfEmpty()

                        join width in widths on product.WidthId equals width.Id into widthProducts
                        from widthProduct in widthProducts.DefaultIfEmpty()

                        join warpType in warpTypes on product.WarpId equals warpType.Id into warpTypeProducts
                        from warpTypeProduct in warpTypeProducts.DefaultIfEmpty()

                        join weftType in weftTypes on product.WeftId equals weftType.Id into weftTypeProducts
                        from weftTypeProduct in weftTypeProducts.DefaultIfEmpty()

                        join processType in processTypes on product.ProcessTypeId equals processType.Id into processTypeProducts
                        from processTypeProduct in processTypeProducts.DefaultIfEmpty()

                        join yarnType in yarnTypes on product.YarnTypeId equals yarnType.Id into yarnTypeProducts
                        from yarnTypeProduct in yarnTypeProducts.DefaultIfEmpty()

                        join grade in grades on product.GradeId equals grade.Id into gradeProducts
                        from gradeProduct in gradeProducts.DefaultIfEmpty()

                        join uom in uoms on product.UOMId equals uom.Id into uomProducts
                        from uomProduct in uomProducts.DefaultIfEmpty()

                        select new FabricSKUIndexInfo()
                        {
                            Id = product.Id,
                            LastModifiedUtc = product.LastModifiedUtc,
                            Code = product.Code,
                            WovenType = wovenTypeProduct.WovenType,
                            Construction = constructionProduct.Type,
                            Width = widthProduct.WidthType,
                            Warp = warpTypeProduct.Type,
                            Weft = weftTypeProduct.Type,
                            ProcessType = processTypeProduct.ProcessType,
                            YarnType = yarnTypeProduct.YarnType,
                            Grade = gradeProduct.Type,
                            UOM = uomProduct.Unit
                        };

            query = QueryHelper<FabricSKUIndexInfo>.Search(query, searchAttributes, queryParam.keyword, true);
            query = QueryHelper<FabricSKUIndexInfo>.Order(query, order);

            var total = await query.CountAsync();

            if (queryParam.page <= 0)
                queryParam.page = 1;

            var data = query.Skip(queryParam.size * (queryParam.page - 1)).Take(queryParam.size).ToList();
            return new FabricSKUIndex(data, total, queryParam.page, queryParam.size);
        }

        public Task<int> UpdateSKU(int id, FabricSKUFormDto form)
        {
            throw new NotImplementedException();
        }

        public FabricSKUIdCodeDto AutoCreateSKU(FabricSKUAutoCreateFormDto form)
        {
            var code = "";
            var processType = _dbContext.IPProcessType.FirstOrDefault(entity => entity.ProcessType == form.ProcessType);
            var processTypeId = 0;
            if (processType != null)
            {
                code += processType.Code;
                processTypeId = processType.Id;
            }
            else
            {
                code += "00";
                processTypeId = 0;
            }

            var yearCode = CodeConstructionHelper.GetYearCode(DateTime.Now.Year);
            code += yearCode;

            var sppNo = form.ProductionOrderNo.Substring(form.ProductionOrderNo.Length - 4);
            code += sppNo;

            var grade = _dbContext.IPGrades.FirstOrDefault(entity => entity.Type == form.Grade);
            var gradeId = 0;
            if (grade != null)
            {
                code += grade.Code;
                gradeId = grade.Id;
            }
            else
            {
                code += "0";
                gradeId = 0;
            }

            var uom = _dbContext.IPUnitOfMeasurements.FirstOrDefault(entity => entity.Unit == form.UOM);
            var category = _dbContext.IPCategories.FirstOrDefault(entity => entity.Name == "FABRIC");

            var model = new ProductSKUModel();
            var productFabricSKU = new FabricProductSKUModel();
            if (uom != null && category != null)
            {
                model = new ProductSKUModel(code, code, uom.Id, category.Id, "");
                _unitOfWork.ProductSKUs.Insert(model);
                _unitOfWork.Commit();

                productFabricSKU = new FabricProductSKUModel(code, model.Id, 0, 0, 0, 0, 0, processTypeId, 0, gradeId, uom.Id);
                _unitOfWork.FabricSKUProducts.Insert(productFabricSKU);
                _unitOfWork.Commit();
            }
            return new FabricSKUIdCodeDto() { FabricSKUId = productFabricSKU.Id, ProductSKUCode = code, ProductSKUId = model.Id };
        }

        public FabricPackingIdCodeDto AutoCreatePacking(FabricPackingAutoCreateFormDto form)
        {
            var fabric = _dbContext.FabricProductSKUs.FirstOrDefault(entity => entity.Id == form.FabricSKUId);

            if (fabric != null)
            {
                var productSKU = _dbContext.ProductSKUs.FirstOrDefault(entity => entity.Id == fabric.ProductSKUId);
                var latestProductPacking = _dbContext.ProductPackings.Where(entity => entity.Code.Contains(productSKU.Code)).OrderByDescending(entity => entity.Code).FirstOrDefault();

                var i = 1;
                if (latestProductPacking != null)
                {
                    var rollNumber = latestProductPacking.Code.Substring(latestProductPacking.Code.Count() - 4);
                    i = int.Parse(rollNumber) + 1;
                }

                var packingModel = new ProductPackingModel();
                var fabricPackingProduct = new FabricProductPackingModel();
                var packingCodes = new List<string>();
                var limit = (i - 1) + form.Quantity;
                for (; i <= limit; i++)
                {
                    var code = productSKU.Code + i.ToString().PadLeft(4, '0');
                    var uom = _dbContext.IPUnitOfMeasurements.FirstOrDefault(entity => entity.Unit == form.PackingType);
                    packingModel = new ProductPackingModel(productSKU.Id, uom.Id, form.Length, code, code, "");
                    _unitOfWork.ProductPackings.Insert(packingModel);
                    _unitOfWork.Commit();
                    packingCodes.Add(code);

                    fabricPackingProduct = new FabricProductPackingModel(code, fabric.Id, productSKU.Id, packingModel.Id, uom.Id, form.Length);
                    _dbContext.FabricProductPackings.Add(fabricPackingProduct);
                }

                _dbContext.SaveChanges();


                return new FabricPackingIdCodeDto() { FabricPackingId = fabricPackingProduct.Id, ProductPackingCode = packingModel.Code, ProductPackingId = packingModel.Id, FabricSKUId = fabric.Id, ProductSKUCode = productSKU.Code, ProductSKUId = productSKU.Id, ProductPackingCodes = packingCodes };
            }
            else
            {
                return new FabricPackingIdCodeDto();
            }
        }

        public FabricSKUIdCodeDto AutoCreateSKU(NewFabricSKUAutoCreateFormDto form)
        {
            return new FabricSKUIdCodeDto() { FabricSKUId = 1, ProductSKUCode = "code", ProductSKUId = 1 };
        }
    }
}
