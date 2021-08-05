using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.FabricQualityControl
{
    public class FabricGradeTestModel : StandardEntity
    {
        public FabricGradeTestModel()
        {
            Criteria = new HashSet<CriteriaModel>();
        }

        public FabricGradeTestModel(double avalALength, double avalBLength, double avalConnectionLength, double fabricGradeTest, double finalArea, double finalGradeTest, double finalLength, double finalScore,
            string grade, double initLength, string pcsNo, double pointLimit, double pointSystem, double sampleLength, double score, string type, double width, int itemIndex,
            ICollection<CriteriaModel> criteria)
        {
            AvalALength = avalALength;
            AvalBLength = avalBLength;
            AvalConnectionLength = avalConnectionLength;
            FabricGradeTest = fabricGradeTest;
            FinalArea = finalArea;
            FinalGradeTest = finalGradeTest;
            FinalLength = finalLength;
            FinalScore = finalScore;
            Grade = grade;
            InitLength = initLength;
            PcsNo = pcsNo;
            PointLimit = pointLimit;
            PointSystem = pointSystem;
            SampleLength = sampleLength;
            Score = score;
            Type = type;
            Width = width;
            ItemIndex = itemIndex;

            Criteria = criteria;

        }

        public double AvalALength { get; private set; }
        public double AvalBLength { get; private set; }
        public double AvalConnectionLength { get; private set; }
        public double FabricGradeTest { get; private set; }
        public double FinalArea { get; private set; }
        public double FinalGradeTest { get; private set; }
        public double FinalLength { get; private set; }
        public double FinalScore { get; private set; }
        public string Grade { get; private set; }
        public double InitLength { get; private set; }
        public string PcsNo { get; private set; }
        public double PointLimit { get; private set; }
        public double PointSystem { get; private set; }
        public double SampleLength { get; private set; }
        public double Score { get; private set; }
        public string Type { get; private set; }
        public double Width { get; private set; }

        public int ItemIndex { get; private set; }

        public ICollection<CriteriaModel> Criteria { get; private set; }

        public int FabricQualityControlId { get; set; }
        public virtual FabricQualityControlModel FabricQualityControl { get; set; }

        public void SetAvalALength(double newAvalALength, string user, string agent)
        {
            if(newAvalALength != AvalALength)
            {
                AvalALength = newAvalALength;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetAvalBLength(double newAvalBLength, string user, string agent)
        {
            if (newAvalBLength != AvalBLength)
            {
                AvalBLength = newAvalBLength;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetAvalConnectionLength(double newAvalConnectionLength, string user, string agent)
        {
            if (newAvalConnectionLength != AvalConnectionLength)
            {
                AvalBLength = newAvalConnectionLength;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetFabricGradeTest(double newFabricGradeTest, string user, string agent)
        {
            if(newFabricGradeTest != FabricGradeTest)
            {
                FabricGradeTest = newFabricGradeTest;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetFinalArea(double newFinalArea, string user, string agent)
        {
            if (newFinalArea != FinalArea)
            {
                FinalArea = newFinalArea;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetFinalGradeTest(double newFinalGradeTest, string user, string agent)
        {
            if (newFinalGradeTest != FinalGradeTest)
            {
                FinalGradeTest = newFinalGradeTest;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetFinalLength(double newFinalLength, string user, string agent)
        {
            if (newFinalLength != FinalLength)
            {
                FinalLength = newFinalLength;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetFinalScore(double newFinalScore, string user, string agent)
        {
            if (newFinalScore != FinalScore)
            {
                FinalScore = newFinalScore;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetGrade(string newGrade, string user, string agent)
        {
            if (newGrade != Grade)
            {
                Grade = newGrade;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetInitLength(double newInitLength, string user, string agent)
        {
            if (newInitLength != InitLength)
            {
                InitLength = newInitLength;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetPcsNo(string newPcsNo, string user, string agent)
        {
            if (newPcsNo != PcsNo)
            {
                PcsNo = newPcsNo;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetPointLimit(double newPointLimit, string user, string agent)
        {
            if (newPointLimit != PointLimit)
            {
                PointLimit = newPointLimit;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetPointSystem(double newPointSystem, string user, string agent)
        {
            if (newPointSystem != PointSystem)
            {
                PointSystem = newPointSystem;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetSampleLength(double newSampleLength, string user, string agent)
        {
            if (newSampleLength != SampleLength)
            {
                SampleLength = newSampleLength;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetScore(double newScore, string user, string agent)
        {
            if (newScore != Score)
            {
                Score = newScore;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetType(string newType, string user, string agent)
        {
            if (newType != Type)
            {
                Type = newType;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetWidth(double newWidth, string user, string agent)
        {
            if (newWidth != Width)
            {
                Width = newWidth;
                this.FlagForUpdate(user, agent);
            }
        }

        public void SetItemIndex(int newItemIndex, string user, string agent)
        {
            if (newItemIndex != ItemIndex)
            {
                ItemIndex = newItemIndex;
                this.FlagForUpdate(user, agent);
            }
        }
    }
}
