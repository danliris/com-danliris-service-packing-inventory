using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils
{
    public class DyeingPrintingAreaOutputAvalItemDataUtil : BaseDataUtil<DyeingPrintingAreaOutputAvalItemRepository, DyeingPrintingAreaOutputAvalItemModel>
    {
        public DyeingPrintingAreaOutputAvalItemDataUtil(DyeingPrintingAreaOutputAvalItemRepository repository) : base(repository)
        {
        }

        public override DyeingPrintingAreaOutputAvalItemModel GetModel()
        {
            var model =  new DyeingPrintingAreaOutputAvalItemModel("type", 1);
            model.DyeingPrintingAreaOutputProductionOrder = new DyeingPrintingAreaOutputProductionOrderModel();

            return model;
        }

        public override DyeingPrintingAreaOutputAvalItemModel GetEmptyModel()
        {
            var model = new DyeingPrintingAreaOutputAvalItemModel(null, 0);
            model.DyeingPrintingAreaOutputProductionOrder = new DyeingPrintingAreaOutputProductionOrderModel();

            return model;
        }
    }
}
