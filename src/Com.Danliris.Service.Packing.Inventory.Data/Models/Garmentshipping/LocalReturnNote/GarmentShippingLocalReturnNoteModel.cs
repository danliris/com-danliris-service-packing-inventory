using Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.ShippingLocalSalesNote;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Garmentshipping.LocalReturnNote
{
    public class GarmentShippingLocalReturnNoteModel : StandardEntity
    {

        public string ReturnNoteNo { get; private set; }
        public int SalesNoteId { get; private set; }
        public DateTimeOffset ReturnDate { get; private set; }
        public string Description { get; private set; }
        public GarmentShippingLocalSalesNoteModel SalesNote { get; private set; }
        public ICollection<GarmentShippingLocalReturnNoteItemModel> Items { get; private set; }

        public GarmentShippingLocalReturnNoteModel(string returnNoteNo, int salesNoteId, DateTimeOffset returnDate, string description, GarmentShippingLocalSalesNoteModel salesNote, ICollection<GarmentShippingLocalReturnNoteItemModel> items)
        {
            ReturnNoteNo = returnNoteNo;
            SalesNoteId = salesNoteId;
            ReturnDate = returnDate;
            Description = description;
            SalesNote = salesNote;
            Items = items;
        }

        public GarmentShippingLocalReturnNoteModel()
        {
        }
    }
}
