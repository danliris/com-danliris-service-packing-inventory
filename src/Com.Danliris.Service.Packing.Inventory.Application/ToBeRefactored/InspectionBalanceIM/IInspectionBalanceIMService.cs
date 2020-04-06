using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.InspectionBalanceIM
{
    public interface IInspectionBalanceIMService
    {
        List<IndexViewModel> GetReport(DateTimeOffset? dateReport, string shift, string unit,  int timeOffset);
        MemoryStream GenerateExcel(DateTimeOffset? dateReport, string shift, string unit, int timeOffset);
    }
}
