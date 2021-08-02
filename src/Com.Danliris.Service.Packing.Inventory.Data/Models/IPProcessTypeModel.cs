using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models
{
    public class IPProcessTypeModel : StandardEntity
    {
        public string Code { get; private set; }
        public string ProcessType { get; private set; }
        public IPProcessTypeModel()
        {

        }

        public IPProcessTypeModel(string code, string processType)
        {
            Code = code;
            ProcessType = processType;
        }

        public void SetCode(string newCode, string username, string agent)
        {
            Code = newCode;
            this.FlagForUpdate(username, agent);
        }

        public void SetProcessType(string newProcessType, string username, string agent)
        {
            ProcessType = newProcessType;
            this.FlagForUpdate(username, agent);
        }
    }
}
