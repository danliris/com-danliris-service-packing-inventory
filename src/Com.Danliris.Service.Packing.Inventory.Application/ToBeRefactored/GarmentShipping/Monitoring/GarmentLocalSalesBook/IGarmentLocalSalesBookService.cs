﻿using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentLocalSalesBook
{
    public interface IGarmentLocalSalesBookService
    {
        Tuple<List<GarmentLocalSalesBookViewModel>, int> GetReportData(DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcel(DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
