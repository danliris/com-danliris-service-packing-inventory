using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.InventoryDocumentPacking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class InventoryDocumentPackingDataUtil : BaseDataUtil<InventoryDocumentPackingRepository, InventoryDocumentPackingModel>
    {
        public InventoryDocumentPackingDataUtil(InventoryDocumentPackingRepository repository) : base(repository)
        {

        }
        public override InventoryDocumentPackingModel GetModel()
        {

            return new InventoryDocumentPackingModel("code", DateTimeOffset.UtcNow, new List<InventoryDocumentPackingItemModel>()
                {
                    new InventoryDocumentPackingItemModel(1,1,1,"mtr")
                }, "no", "tupe", "re", "s", 1, "t");
            
        }
    }
}
