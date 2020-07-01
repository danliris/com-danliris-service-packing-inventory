using Com.Danliris.Service.Packing.Inventory.Data.Models.Product;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.Product
{
   public class UnitOfMeasurementDataUtil : BaseDataUtil<UOMRepository, UnitOfMeasurementModel>
    {
        public UnitOfMeasurementDataUtil(UOMRepository repository) : base(repository)
        {
        }

        public override UnitOfMeasurementModel GetModel()
        {
            return new UnitOfMeasurementModel("unit");
        }
    }
}
