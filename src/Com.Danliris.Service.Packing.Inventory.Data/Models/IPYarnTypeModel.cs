using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class IPYarnTypeModel : StandardEntity
    {
        public string Code { get; private set; }
        public string YarnType { get; private set; }
        public IPYarnTypeModel()
        {

        }

        public IPYarnTypeModel(string code, string yarnType)
        {
            Code = code;
            YarnType = yarnType;
        }

        public void SetCode(string newCode, string username, string agent)
        {
            Code = newCode;
            this.FlagForUpdate(username, agent);
        }

        public void SetYarnType(string newWidthType, string username, string agent)
        {
            YarnType = newWidthType;
            this.FlagForUpdate(username, agent);
        }
    }
}
