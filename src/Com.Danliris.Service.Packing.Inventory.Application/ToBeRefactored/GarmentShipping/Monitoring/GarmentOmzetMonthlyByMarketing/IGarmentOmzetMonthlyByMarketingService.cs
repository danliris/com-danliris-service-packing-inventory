﻿using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.Monitoring.GarmentOmzetMonthlyByMarketing
{
    public interface IGarmentOmzetMonthlyByMarketingService
    {
        List<GarmentOmzetMonthlyByMarketingViewModel> GetReportData(string marketingName, DateTime? dateFrom, DateTime? dateTo, int offset);
        MemoryStream GenerateExcel(string marketingName, DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
