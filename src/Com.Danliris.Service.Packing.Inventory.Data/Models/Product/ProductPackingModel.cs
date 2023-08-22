using Com.Moonlay.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Product
{
    public class ProductPackingModel : StandardEntity
    {
        public ProductPackingModel()
        {

        }

        // UOM is packing type
        public ProductPackingModel(int productSKUId, int uomId, double packingSize, string code, string name, string description)
        {
            ProductSKUId = productSKUId;
            UOMId = uomId;
            PackingSize = packingSize;
            Code = code;
            Name = name;
            Description = description;
        }
        //for SO
        public ProductPackingModel(int productSKUId, int uomId, double packingSize, string code, string name, string description, string packingType, bool afterStockOpname,
                                    int materialConstructionId, string materialConstructionName, int materialId, string materialName, int yarnMaterialId, string yarnMaterialName, string finishWidth)
        {
            ProductSKUId = productSKUId;
            UOMId = uomId;
            PackingSize = packingSize;
            Code = code;
            Name = name;
            Description = description;
            PackingType = packingType;
            AfterStockOpname = afterStockOpname;
            MaterialConstructionId = materialConstructionId;
            MaterialConstructionName = materialConstructionName;
            MaterialId = materialId;
            MaterialName = materialName;
            YarnMaterialId = yarnMaterialId;
            YarnMaterialName = yarnMaterialName;
            FinishWidth = finishWidth;
        }

        public int ProductSKUId { get; private set; }
        public int UOMId { get; private set; }
        public double PackingSize { get; private set; }
        [MaxLength(64)]
        public string Code { get; private set; }
        [MaxLength(512)]
        public string Name { get; private set; }
        public string Description { get; private set; }

        [MaxLength(64)]
        public string PackingType { get; private set; }
        public bool AfterStockOpname { get; private set; }

        #region Construction
        public int MaterialConstructionId { get; set; }
        public string MaterialConstructionName { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public int YarnMaterialId { get; set; }
        public string YarnMaterialName { get; set;  }
        public string FinishWidth { get; set; }
        #endregion

        public void SetCode(string code)
        {
            Code = code;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetProductSKU(int productSKUId)
        {
            ProductSKUId = productSKUId;
        }

        public void SetPackingSize(double packingSize)
        {
            PackingSize = packingSize;
        }

        public void SetUOM(int uomId)
        {
            UOMId = uomId;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }
    }
}
