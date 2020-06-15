using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.MaterialDeliveryNote;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.MaterialDeliveryNote;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.MaterialDeliveryNote
{
    public class MaterialDeliveryNoteDataUtil : BaseDataUtil<MaterialDeliveryNoteRepository, MaterialDeliveryNoteModel>
    {
        public MaterialDeliveryNoteDataUtil(MaterialDeliveryNoteRepository repository) : base(repository)
        {

        }

        public override MaterialDeliveryNoteModel GetModel()
        {
            return new MaterialDeliveryNoteModel("code", DateTimeOffset.UtcNow, "abc", DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, "aaa", "aaa", "aaa", "aaa", "aaa","aaa","aaa",
                new List<ItemsModel>()
                    {
                        new ItemsModel("code", "grp","abc",1,1,1,1,1)
                    });
        }

        public override MaterialDeliveryNoteModel GetEmptyModel()
        {
            return new MaterialDeliveryNoteModel("a", DateTimeOffset.UtcNow.AddSeconds(3), null, DateTimeOffset.UtcNow.AddSeconds(3), DateTimeOffset.UtcNow.AddSeconds(3), 
                null, null, null, null, null, null, null, new List<ItemsModel>()
                    {
                        new ItemsModel("a", null,null,null,0,0,0,0)
                    });
        }
    }
}
