﻿using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentSubcon.ShippingLocalSalesNoteTS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.ShippingLocalSalesNoteTS
{
    public interface IGarmentShippingLocalSalesNoteTSRepository : IRepository<GarmentShippingLocalSalesNoteTSModel>
    {
        //Task<int> ApproveShippingAsync(int id);
        //Task<int> ApproveFinanceAsync(int id);
        //Task<int> RejectFinanceAsync(int id, GarmentShippingLocalSalesNoteTSModel model);
        //Task<int> RejectShippingAsync(int id, GarmentShippingLocalSalesNoteTSModel model);

        Task<int> SetIsUsed(List<long> ids, bool isUsed);
    }
}
