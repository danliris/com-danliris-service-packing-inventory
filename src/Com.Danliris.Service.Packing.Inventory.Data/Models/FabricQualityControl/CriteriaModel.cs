using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl
{
    public class CriteriaModel
    {
        public CriteriaModel()
        {

        }

        public CriteriaModel(string code, string group, int index, string name, double scoreA, double scoreB, double scoreC, double scoreD)
        {
            Code = code;
            Group = group;
            Index = index;
            Name = name;
            ScoreA = scoreA;
            ScoreB = scoreB;
            ScoreC = scoreC;
            ScoreD = scoreD;
        }

        public virtual int Id { get; set; }
        public string Code { get; private set; }
        public string Group { get; private set; }
        public int Index { get; private set; }
        public string Name { get; private set; }
        public double ScoreA { get; private set; }
        public double ScoreB { get; private set; }
        public double ScoreC { get; private set; }
        public double ScoreD { get; private set; }

        public int FabricGradeTestId { get; set; }
        public virtual FabricGradeTestModel FabricGradeTest { get; set; }

        public void SetCode(string newCode)
        {
            if(newCode != Code)
            {
                Code = newCode;
            }
        }

        public void SetGroup(string newGroup)
        {
            if (newGroup != Group)
            {
                Group = newGroup;
            }
        }

        public void SetIndex(int newIndex)
        {
            if (newIndex != Index)
            {
                Index = newIndex;
            }
        }

        public void SetName(string newName)
        {
            if (newName != Name)
            {
                Name = newName;
            }
        }

        public void SetScoreA(double newScoreA)
        {
            if (newScoreA != ScoreA)
            {
                ScoreA = newScoreA;
            }
        }

        public void SetScoreB(double newScoreB)
        {
            if (newScoreB != ScoreB)
            {
                ScoreB = newScoreB;
            }
        }

        public void SetScoreC(double newScoreC)
        {
            if (newScoreC != ScoreC)
            {
                ScoreC = newScoreC;
            }
        }

        public void SetScoreD(double newScoreD)
        {
            if (newScoreD != ScoreD)
            {
                ScoreD = newScoreD;
            }
        }
    }
}
