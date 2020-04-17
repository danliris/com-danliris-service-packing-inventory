using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AreaNote.Shipping
{
    public interface IShippingAreaNoteService
    {
        List<IndexViewModel> GetReport(DateTimeOffset? date, string mutation, string zone, int offSet);
        //MemoryStream GenerateExcel(DateTimeOffset? date, string mutation, string zone, int offSet);
    }
}
