using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentDraftPackingListItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentDraftPackingListItem
{
    public interface IGarmentDraftPackingListItemRepository : IRepository<GarmentDraftPackingListItemModel>
    {
        IQueryable<GarmentDraftPackingListItemModel> Query { get; }
        Task<int> SaveChanges();
    }
}
