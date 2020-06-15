using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNoteWeaving;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNoteWeaving;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.MaterialDeliveryNote
{
    public class ItemsMaterialDeliveryNoteWeavingDataUtil : BaseDataUtil<ItemsMaterialDeliveryNoteWeavingRepository, ItemsMaterialDeliveryNoteWeavingModel>
    {
        public ItemsMaterialDeliveryNoteWeavingDataUtil(ItemsMaterialDeliveryNoteWeavingRepository repository) : base(repository)
        {

        }

        public override ItemsMaterialDeliveryNoteWeavingModel GetModel()
        {
            return new ItemsMaterialDeliveryNoteWeavingModel("itemnosop", "itemmaterialName", "itemgrade", "itemtype", "itemcode", 1, 1, 1, 1);
        }

        public override ItemsMaterialDeliveryNoteWeavingModel GetEmptyModel()
        {
            return new ItemsMaterialDeliveryNoteWeavingModel("itemnosop", "itemmaterialName", "itemgrade", "itemtype", "itemcode", 1, 1, 1, 1);
        }
    }
}
