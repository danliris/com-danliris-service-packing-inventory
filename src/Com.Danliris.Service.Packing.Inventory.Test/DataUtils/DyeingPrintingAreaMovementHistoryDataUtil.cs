using Com.Danliris.Service.Packing.Inventory.Data.Models;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class DyeingPrintingAreaMovementHistoryDataUtil : BaseDataUtil<DyeingPrintingAreaMovementHistoryRepository, DyeingPrintingAreaMovementHistoryModel>
    {
        public DyeingPrintingAreaMovementHistoryDataUtil(DyeingPrintingAreaMovementHistoryRepository repository) : base(repository)
        {
        }

        public override DyeingPrintingAreaMovementHistoryModel GetModel()
        {
            return new DyeingPrintingAreaMovementHistoryModel(DateTimeOffset.UtcNow, "area", "shift", AreaEnum.IM);
        }
    }
}
