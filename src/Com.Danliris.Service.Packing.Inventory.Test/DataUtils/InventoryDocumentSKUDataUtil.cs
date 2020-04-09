using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.InventoryDocumentSKU;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class InventoryDocumentSKUDataUtil : BaseDataUtil<InventoryDocumentSKURepository, InventoryDocumentSKUModel>
    {
        public InventoryDocumentSKUDataUtil(InventoryDocumentSKURepository repository) : base(repository)
        {
        }

        public override InventoryDocumentSKUModel GetModel()
        {
            return new InventoryDocumentSKUModel("code", DateTimeOffset.UtcNow, new List<InventoryDocumentSKUItemModel>()
            {
                new InventoryDocumentSKUItemModel(1,1,"e")
            }, "np<", "tye", "re", "st", 1, "t");
        }
    }
}
