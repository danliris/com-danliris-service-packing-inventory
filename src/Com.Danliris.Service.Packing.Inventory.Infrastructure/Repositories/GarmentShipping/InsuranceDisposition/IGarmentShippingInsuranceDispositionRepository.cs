using Com.Danliris.Service.Packing.Inventory.Data;
using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.InsuranceDisposition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.InsuranceDisposition
{
    public interface IGarmentShippingInsuranceDispositionRepository : IRepository<GarmentShippingInsuranceDispositionModel>
    {
        IQueryable<GarmentShippingInsuranceDispositionItemModel> ReadItemAll();
    }
}
