using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class IPWarpTypeModel : StandardEntity
    {
        public string Code { get; private set; }
        public string WarpType { get; private set; }
        public IPWarpTypeModel()
        {

        }

        public IPWarpTypeModel(string code, string warpType)
        {
            Code = code;
            WarpType = warpType;
        }

        public void SetCode(string newCode, string username, string agent)
        {
            Code = newCode;
            this.FlagForUpdate(username, agent);
        }

        public void SetWarpType(string newWarpType, string username , string agent)
        {
            WarpType = newWarpType;
            this.FlagForUpdate(username, agent);

        }
    }
}
