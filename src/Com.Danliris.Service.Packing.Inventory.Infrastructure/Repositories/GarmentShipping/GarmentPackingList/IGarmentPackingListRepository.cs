using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentPackingList;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentPackingList
{
    public interface IGarmentPackingListRepository : IRepository<GarmentPackingListModel>
    {
        Task<GarmentPackingListModel> ReadByInvoiceNoAsync(string no);
    }
}
