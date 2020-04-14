using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AreaNote.Packing
{
    public interface IPackingAreaNoteService
    {
        List<IndexViewModel> GetReport(DateTimeOffset? date, string zone, string group, string mutation, int offSet);
        MemoryStream GenerateExcel(DateTimeOffset? date, string zone, string group, string mutation, int offSet);
    }
}
