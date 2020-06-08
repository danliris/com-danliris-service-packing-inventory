using Com.Danliris.Service.Packing.Inventory.Data;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.Utilities
{
    public class ListResult<T>
    {
        private List<MaterialDeliveryNoteModel> data;
        private int totalRow;

        public ListResult(List<T> data, int page, int size, int totalRow)
        {
            Data = data;
            Page = page;
            Size = size;
            Total = totalRow;
        }

        public ListResult(List<MaterialDeliveryNoteModel> data, int page, int size, int totalRow)
        {
            this.data = data;
            Page = page;
            Size = size;
            this.totalRow = totalRow;
        }

        public List<T> Data { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public int Total { get; set; }
    }
}