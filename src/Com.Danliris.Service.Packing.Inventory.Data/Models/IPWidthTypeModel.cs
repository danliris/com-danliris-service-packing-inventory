using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class IPWidthTypeModel : StandardEntity
    {
        public string Code { get; private set; }
        public string WidthType { get; private set; }
        public IPWidthTypeModel()
        {

        }

        public IPWidthTypeModel(string code, string widthType)
        {
            Code = code;
            WidthType = widthType;
        }

        public void SetCode(string newCode,string username,string agent)
        {
            Code = newCode;
            this.FlagForUpdate(username, agent);
        }

        public void SetWidthType(string newWidthType, string username, string agent)
        {
            WidthType = newWidthType;
            this.FlagForUpdate(username, agent);
        }
    }
}
