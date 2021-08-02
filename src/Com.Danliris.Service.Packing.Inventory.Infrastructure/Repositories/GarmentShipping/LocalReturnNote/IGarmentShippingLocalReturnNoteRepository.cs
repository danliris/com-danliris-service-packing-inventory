using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalReturnNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.LocalReturnNote
{
    public interface IGarmentShippingLocalReturnNoteRepository : IRepository<GarmentShippingLocalReturnNoteModel>
    {
        IQueryable<GarmentShippingLocalReturnNoteItemModel> ReadItemAll();
    }
}
