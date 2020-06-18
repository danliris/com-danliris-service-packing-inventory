﻿using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.LocalCoverLetter
{
    public class IndexViewModel
    {
        public int id { get; set; }

        public string noteNo { get; set; }
        public DateTimeOffset? date { get; set; }
        public Buyer buyer { get; set; }
    }
}
