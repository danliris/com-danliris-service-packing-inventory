using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNoteWeaving;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNoteWeaving;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.MaterialDeliveryNoteWeaving
{
    public class MaterialDeliveryNoteWeavingDataUtil : BaseDataUtil<MaterialDeliveryNoteWeavingRepository, MaterialDeliveryNoteWeavingModel>
    {
        public MaterialDeliveryNoteWeavingDataUtil(MaterialDeliveryNoteWeavingRepository repository) : base(repository)
        {

        }

        public override MaterialDeliveryNoteWeavingModel GetModel()
        {
            return new MaterialDeliveryNoteWeavingModel("code", DateTimeOffset.UtcNow, 1, "aaa", "aaa", 1, "aaa", 1, "aaa", "aaa", "aaa", 1, "aaa", "aaa", "aaa",
                new List<ItemsMaterialDeliveryNoteWeavingModel>()
                    {
                        new ItemsMaterialDeliveryNoteWeavingModel("code", "grp","abc","name","aaa",1,1,1,1)
                    });
        }

        public override MaterialDeliveryNoteWeavingModel GetEmptyModel()
        {
            return new MaterialDeliveryNoteWeavingModel("a", DateTimeOffset.UtcNow.AddSeconds(3),0, null, null, 0, null, 0, null, null, null, 0, null,null,null, new List<ItemsMaterialDeliveryNoteWeavingModel>()
                    {
                        new ItemsMaterialDeliveryNoteWeavingModel("code", "grp","abc","name","aaa",1,1,1,1)
                    });
        }
    }
}