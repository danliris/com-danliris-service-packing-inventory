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
    }
}
