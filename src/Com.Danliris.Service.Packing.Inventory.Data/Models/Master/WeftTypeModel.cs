using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Master
{
    public class WeftTypeModel : StandardEntity
    {
        public string Type { get; private set; }
        public string Code { get; private set; }

        public WeftTypeModel()
        {

        }

        public WeftTypeModel(string type, string code) : this()
        {
            Type = type;
            Code = code;
        }

        public void SetType(string newType, string user, string agent)
        {
            if(newType != Type)
            {
                Type = newType;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetCode(string newCode, string user, string agent)
        {
            if (newCode != Code)
            {
                Code = newCode;
                this.FlagForUpdate(user, agent);
            }
        }
    }
}
