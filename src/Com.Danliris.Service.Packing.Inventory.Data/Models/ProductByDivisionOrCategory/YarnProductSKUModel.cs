using Com.Moonlay.Models;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.ProductByDivisionOrCategory
{
    public class YarnProductSKUModel : StandardEntity
    {
        public YarnProductSKUModel()
        {

        }

        public YarnProductSKUModel(
            string code,
            string yarnType,
            string lotNo,
            string uomUnit,
            int productSKUId
            )
        {
            Code = code;
            YarnType = yarnType;
            LotNo = lotNo;
            UOMUnit = uomUnit;
            ProductSKUId = productSKUId;
        }

        [MaxLength(64)]
        public string Code { get; private set; }
        [MaxLength(128)]
        public string YarnType { get; private set; }
        [MaxLength(128)]
        public string LotNo { get; private set; }
        [MaxLength(64)]
        public string UOMUnit { get; private set; }
        public int ProductSKUId { get; private set; }
    }
}
