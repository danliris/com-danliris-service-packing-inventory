using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
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
            return new DyeingPrintingAreaMovementModel(DateTimeOffset.UtcNow, "DL", "area", "in", 1, "no", 1, "a", "a", "a", "a", "a", "a", "a", "a", 1, 1, "type", 1, "a", "a");
        }
    }
}
