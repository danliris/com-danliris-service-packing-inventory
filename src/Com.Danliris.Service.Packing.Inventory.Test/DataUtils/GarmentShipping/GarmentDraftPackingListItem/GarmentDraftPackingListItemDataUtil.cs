using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.GarmentDraftPackingListItem;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.GarmentShipping.GarmentDraftPackingListItem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Test.DataUtils.GarmentShipping.GarmentDraftPackingListItem
{
    public class GarmentDraftPackingListItemDataUtil : BaseDataUtil<GarmentDraftPackingListItemRepository, GarmentDraftPackingListItemModel>
    {
        public GarmentDraftPackingListItemDataUtil(GarmentDraftPackingListItemRepository repository) : base(repository)
        {
        }

        public override GarmentDraftPackingListItemModel GetModel()
        {
            var sizes = new HashSet<GarmentDraftPackingListDetailSizeModel> { new GarmentDraftPackingListDetailSizeModel(1, "", 1) };
            var details = new HashSet<GarmentDraftPackingListDetailModel> { new GarmentDraftPackingListDetailModel(1, 1, "", "", 1, 1, 1, 1, 1, 1, 1, 1, 1, sizes, 1) };
            var model = new GarmentDraftPackingListItemModel("", "", 1, "", 1, "", "", "", 1, 1, "", 1, 1, 1, 1, 1, "", 1, "", "", "", "", "", 1, "", "", details);

            return model;
        }

        public override GarmentDraftPackingListItemModel GetEmptyModel()
        {
            var sizes = new HashSet<GarmentDraftPackingListDetailSizeModel> { new GarmentDraftPackingListDetailSizeModel(0, null, 0) };
            var details = new HashSet<GarmentDraftPackingListDetailModel> { new GarmentDraftPackingListDetailModel(0, 0, null, null, 0, 0, 0, 0, 0, 0, 0, 0, 0, sizes, 1) };
            var model = new GarmentDraftPackingListItemModel(null, null, 0, null, 0, null, null, null, 0, 0, null, 0, 0, 0, 0, 0, null, 0, null, null, null, null, null, 0, null, null, details);

            return model;
        }

        public GarmentDraftPackingListItemModel CopyModel(GarmentDraftPackingListItemModel i)
        {
            var details = new HashSet<GarmentDraftPackingListDetailModel>();
            foreach (var d in i.Details)
            {
                var sizes = new HashSet<GarmentDraftPackingListDetailSizeModel>();
                foreach (var s in d.Sizes)
                {
                    sizes.Add(new GarmentDraftPackingListDetailSizeModel(s.SizeId, s.Size, s.Quantity) { Id = s.Id });
                }
                details.Add(new GarmentDraftPackingListDetailModel(d.Carton1, d.Carton2, d.Style, d.Colour, d.CartonQuantity, d.QuantityPCS, d.TotalQuantity, d.Length, d.Width, d.Height, d.GrossWeight, d.NetWeight, d.NetNetWeight, sizes, d.Index) { Id = d.Id });
            }
            
            var model = new GarmentDraftPackingListItemModel(i.RONo, i.SCNo, i.BuyerBrandId, i.BuyerBrandName, i.ComodityId, i.ComodityCode, i.ComodityName, i.ComodityDescription, i.Quantity, i.UomId, i.UomUnit, i.PriceRO, i.Price, i.PriceFOB, i.PriceCMT, i.Amount, i.Valas, i.UnitId, i.UnitCode, i.Article, i.OrderNo, i.Description, i.DescriptionMd, i.BuyerId, i.BuyerCode, i.SectionCode, details) { Id = i.Id };

            return model;
        }
    }
}
