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
            int colorWayId,
            int constructionTypeId,
            int gradeId,
            int processTypeId,
            int uomId,
            int warpThreadId,
            int weftThreadId,
            int widthId,
            int wovenTypeId,
            int productSKUId
            )
        {
            Code = code;
            ColorWayId = colorWayId;
            ConstructionTypeId = constructionTypeId;
            GradeId = gradeId;
            ProcessTypeId = processTypeId;
            UOMId = uomId;
            WarpThreadId = warpThreadId;
            WeftThreadId = weftThreadId;
            WidthId = widthId;
            WovenTypeId = wovenTypeId;
            ProductSKUID = productSKUId;
        }

        [MaxLength(64)]
        public string Code { get; private set; }
        public int ColorWayId { get; private set; }
        public int ConstructionTypeId { get; private set; }
        public int GradeId { get; private set; }
        public int ProcessTypeId { get; private set; }
        public int UOMId { get; private set; }
        public int WarpThreadId { get; private set; }
        public int WeftThreadId { get; private set; }
        public int WidthId { get; private set; }
        public int WovenTypeId { get; private set; }
        public int ProductSKUID { get; private set; }
    }
}
