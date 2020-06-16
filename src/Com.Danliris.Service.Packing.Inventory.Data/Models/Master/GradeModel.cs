using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Master
{
    public class GradeModel : StandardEntity
    {
        public string Type { get; private set; }
        public string Code { get; private set; }
        public bool IsAvalGrade { get; private set; }

        public GradeModel()
        {

        }

        public GradeModel(string type, string code, bool isAvalGrade) : this()
        {
            Type = type;
            Code = code;
            IsAvalGrade = isAvalGrade;
        }

        public void SetType(string newType, string user, string agent)
        {
            if (newType != Type)
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

        public void SetIsAvalGrade(bool newIsAvalGrade, string user, string agent)
        {
            if (newIsAvalGrade != IsAvalGrade)
            {
                IsAvalGrade = newIsAvalGrade;
                this.FlagForUpdate(user, agent);
            }
        }
    }
}
