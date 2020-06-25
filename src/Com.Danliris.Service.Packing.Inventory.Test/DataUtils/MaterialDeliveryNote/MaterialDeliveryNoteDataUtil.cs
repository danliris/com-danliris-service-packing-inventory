
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
            return new MaterialDeliveryNoteModel(
                "code",
                DateTimeOffset.Now,
                "boncode",
                 DateTimeOffset.Now.AddDays(-2),
                 DateTimeOffset.Now.AddDays(2),
                 1,
                 "donumber",
                 "fonumber",
                 1,
                 "receivercode",
                 "receivername",
                 "remark",
                 1,
                 "scnumber",
                 1,
                 "sendercode",
                 "sendername",
                 1,
                 "storagecode",
                 "storagename",
                 new List<ItemsModel>()
                 {
                     new ItemsModel(
                         1,
                         "noSPP",
                         "materialName",
                         "inputLot",
                         1,
                         "222,222",
                         "222,222",
                         1,
                         1
                         )
                 }

                );
        }

        public override MaterialDeliveryNoteModel GetEmptyModel()
        {
            return new MaterialDeliveryNoteModel(
                "code",
                DateTimeOffset.Now,
                "boncode",
                 DateTimeOffset.Now.AddDays(-2),
                 DateTimeOffset.Now.AddDays(2),
                 1,
                 "donumber",
                 "fonumber",
                 1,
                 "receivercode",
                 "receivername",
                 "remark",
                 1,
                 "scnumber",
                 1,
                 "sendercode",
                 "sendername",
                 1,
                 "storagecode",
                 "storagename",
                 new List<ItemsModel>()
                 {
                     new ItemsModel(
                         1,
                         "noSPP",
                         "materialName",
                         "inputLot",
                         1,
                         "222,222",
                         "222,222",
                         1,
                         1
                         )
                 }

                );
        }
    }
}
