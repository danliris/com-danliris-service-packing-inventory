using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.Utilities
{
    public class ReadResponse<T>
    {
        public ReadResponse(List<T> data, int totalRow, int page, int size)
        {
            Data = data;
            Page = page;
            Size = size;
            Total = totalRow;
        }

        public List<T> Data { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public int Total { get; set; }
       

       
    }
}
