using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Packing.Inventory.Application.InspectionDocumentReport
{
    public class InspectionDocumentReportService : IInspectionDocumentReportService
    {
        
        public List<InspectionDocumentReportViewModel> GetAll()
        {
            string dummyStringJson = @"[{'Index':'1','DateReport':'2020-03-16','GroupText':'SIANG','UnitText':'P','KeluarKe':'TRANSIT','NoSPP':'P/2020/0261','NoKereta':'1-1-9','Material':'CD 74 56 44','Keterangan':'NOT OK','Status':'PERBAIKAN','Lebar':'44','Motif':'PRJ200','Warna':'HIJAU','Mtr':'247.00','Yds':'270.22'},{'Index':'2','DateReport':'2020-03-16','GroupText':'SIANG','UnitText':'P','KeluarKe':'TRANSIT','NoSPP':'P/2020/0556','NoKereta':'1-1-6','Material':'CD 94 72 44','Keterangan':'NOT OK','Status':'PERBAIKAN','Lebar':'44','Motif':'TW 1069','Warna':'A','Mtr':'221.00','Yds':'241.77'},{'Index':'3','DateReport':'2020-03-16','GroupText':'SIANG','UnitText':'P','KeluarKe':'TRANSIT','NoSPP':'P/2020/0787','NoKereta':'1-1-10','Material':'CD 94 72 44','Keterangan':'NOT OK','Status':'PERBAIKAN','Lebar':'44','Motif':'Bp 763','Warna':'0','Mtr':'225.00','Yds':'246.15'}]";
            List<InspectionDocumentReportViewModel> jsonResult = JsonConvert.DeserializeObject<List<InspectionDocumentReportViewModel>>(dummyStringJson);
            return jsonResult;
        }
    }
}
