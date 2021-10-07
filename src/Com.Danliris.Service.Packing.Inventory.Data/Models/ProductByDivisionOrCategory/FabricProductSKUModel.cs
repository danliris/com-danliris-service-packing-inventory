using Com.Moonlay.Models;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory
{
    public class FabricProductSKUModel : StandardEntity
    {
        public FabricProductSKUModel()
        {

        }

        public FabricProductSKUModel(
            string code,
            int productSKUId,
            int wovenTypeId,
            int constructionId,
            int widthId,
            int warpId,
            int weftId,
            int processTypeId,
            int yarnTypeId,
            int gradeId,
            int uomId
            )
        {
            Code = code;
            ProductSKUId = productSKUId;
            WovenTypeId = wovenTypeId;
            ConstructionId = constructionId;
            WidthId = widthId;
            WarpId = warpId;
            WeftId = weftId;
            ProcessTypeId = processTypeId;
            YarnTypeId = yarnTypeId;
            GradeId = gradeId;
            UOMId = uomId;
        }

        public FabricProductSKUModel(
            string code,
            int productSKUId,
            int wovenTypeId,
            int constructionId,
            int widthId,
            int warpId,
            int weftId,
            int processTypeId,
            int yarnTypeId,
            int gradeId,
            int uomId,
            int materialId,
            string materialName,
            int materialConstructionId,
            string materialConstructionName,
            int yarnMaterialId,
            string yarnMaterialName,
            string productionOrderNo,
            string uomUnit,
            string motif,
            string color,
            string grade,
            string width
            )
        {
            Code = code;
            ProductSKUId = productSKUId;
            WovenTypeId = wovenTypeId;
            ConstructionId = constructionId;
            WidthId = widthId;
            WarpId = warpId;
            WeftId = weftId;
            ProcessTypeId = processTypeId;
            YarnTypeId = yarnTypeId;
            GradeId = gradeId;
            UOMId = uomId;
            MaterialId = materialId;
            MaterialName = materialName;
            MaterialConstructionId = materialConstructionId;
            MaterialConstructionName = materialConstructionName;
            YarnMaterialId = yarnMaterialId;
            YarnMaterialName = yarnMaterialName;
            ProductionOrderNo = productionOrderNo;
            UomUnit = uomUnit;
            Motif = motif;
            Color = color;
            Grade = grade;
            Width = width;
        }

        [MaxLength(64)]
        public string Code { get; private set; }
        public int YarnTypeId { get; private set; }
        public int ConstructionId { get; private set; }
        public int GradeId { get; private set; }
        public int ProcessTypeId { get; private set; }
        public int UOMId { get; private set; }
        public int WarpId { get; private set; }
        public int WeftId { get; private set; }
        public int WidthId { get; private set; }
        public int WovenTypeId { get; private set; }
        public int ProductSKUId { get; private set; }
        public int MaterialId { get; set; }
        [MaxLength(225)]
        public string MaterialName { get; set; }
        public int MaterialConstructionId { get; set; }
        [MaxLength(225)]
        public string MaterialConstructionName { get; set; }
        public int YarnMaterialId { get; set; }
        [MaxLength(225)]
        public string YarnMaterialName { get; set; }
        [MaxLength(64)]
        public string ProductionOrderNo { get; set; }
        [MaxLength(64)]
        public string UomUnit { get; set; }
        [MaxLength(64)]
        public string Motif { get; set; }
        [MaxLength(64)]
        public string Color { get; set; }
        [MaxLength(64)]
        public string Grade { get; set; }
        [MaxLength(32)]
        public string Width { get; set; }
    }
}
