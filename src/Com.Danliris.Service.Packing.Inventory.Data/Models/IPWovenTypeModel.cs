using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class IPWovenTypeModel : StandardEntity
    {
        public string Code { get; private set; }
        public string WovenType { get; private set; }
        public IPWovenTypeModel()
        {

        }

        public IPWovenTypeModel(string code, string wovenType)
        {
            Code = code;
            WovenType = wovenType;
        }

        public void SetCode(string newCode, string username, string agent)
        {
            Code = newCode;
            this.FlagForUpdate(username, agent);
        }

        public void SetWarpType(string newWovenType, string username , string agent)
        {
            WovenType = newWovenType;
            this.FlagForUpdate(username, agent);

        }
    }
}
