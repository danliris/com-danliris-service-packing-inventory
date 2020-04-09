using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class DyeingPrintingAreaMovementDataUtil : BaseDataUtil<DyeingPrintingAreaMovementRepository, DyeingPrintingAreaMovementModel>
    {
        public DyeingPrintingAreaMovementDataUtil(DyeingPrintingAreaMovementRepository repository) : base(repository)
        {
        }

        public override DyeingPrintingAreaMovementModel GetModel()
        {
            return new DyeingPrintingAreaMovementModel("area", "no", DateTimeOffset.UtcNow, "shift", 1, "code", "no", 1, "type", "buyer", "inst", "1-2",
                1, "code", "name", 1, "code", "name", "1", 1, "code", "name", "color", "mtf", "awal", 1, "MTR", 1, "OK", "A", "area");
        }


        public DyeingPrintingAreaMovementModel GetYardModel()
        {
            return new DyeingPrintingAreaMovementModel("area", "no", DateTimeOffset.UtcNow, "shift", 1, "code", "no", 1, "type", "buyer", "inst", "1-2",
                1, "code", "name", 1, "code", "name", "1", 1, "code", "name", "color", "mtf", "awal", 1, "YDS", 1, "OK", "A", "area");
        }
    }
}
