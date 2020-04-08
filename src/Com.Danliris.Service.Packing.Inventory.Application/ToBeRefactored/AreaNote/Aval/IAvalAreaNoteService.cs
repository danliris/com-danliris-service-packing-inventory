using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.AreaNote.Aval
{
    public interface IAvalAreaNoteService
    {
        List<IndexViewModel> GetReport(DateTimeOffset? date, string group, string mutation, string zone, int offSet);
        MemoryStream GenerateExcel(DateTimeOffset? date, string group, string mutation, string zone, int offSet);
    }
}
