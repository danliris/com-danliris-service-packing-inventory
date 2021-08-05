using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalReturnNote
{
    public class ItemsReturnQuantityViewModel
    {
        public int id { get; set; }
        public int returnNoteId { get; set; }
        public int salesNoteItemId { get; set; }
        public double returnQuantity { get; set; }
    }
}
