using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNote;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.MaterialDeliveryNote
{
    public class ItemsDataUtil : BaseDataUtil<ItemsRepository, ItemsModel>
    {
        public ItemsDataUtil(ItemsRepository repository) : base(repository)
        {

        }
        public override ItemsModel GetModel()
        {
            return new ItemsModel(
                "noSPP",
                "materialName",
                "inputLot",
                1,
                1,
                1,
                1,
                1
                );
        }

        public override ItemsModel GetEmptyModel()
        {
            return new ItemsModel(
               "noSPP",
               "materialName",
               "inputLot",
               1,
               1,
               1,
               1,
               1
               );
        }
    }

   
    
    
}
