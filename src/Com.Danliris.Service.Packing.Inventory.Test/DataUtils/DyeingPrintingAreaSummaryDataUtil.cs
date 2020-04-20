using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class DyeingPrintingAreaSummaryDataUtil : BaseDataUtil<DyeingPrintingAreaSummaryRepository, DyeingPrintingAreaSummaryModel>
    {
        public DyeingPrintingAreaSummaryDataUtil(DyeingPrintingAreaSummaryRepository repository) : base(repository)
        {
        }

        public override DyeingPrintingAreaSummaryModel GetModel()
        {
            return new DyeingPrintingAreaSummaryModel(DateTimeOffset.UtcNow, "area", "in", 1, "no", 1, "a", "a", "a", "a", "a", "a", "a", "a", 1);
        }
    }
}
