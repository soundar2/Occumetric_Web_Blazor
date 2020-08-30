using System;

namespace Occumetric.Shared
{
    public class JobTaskViewModel : ICloneable
    {
        public int Id { get; set; }

        public int JobId { get; set; }

        public string Guid { get; set; }

        public string Name { get; set; }

        public string EffortType { get; set; }

        public double WeightLb { get; set; }

        public string FromHeight { get; set; }

        public string ToHeight { get; set; }

        public int IntFromHeight { get; set; }

        public int IntToHeight { get; set; }

        public string CarryDistance { get; set; }

        public string ShortDescription { get; set; }

        public double LiftingIndex { get; set; }

        public string SnooksMale { get; set; }

        public string SnooksFemale { get; set; }

        public int? OriginalTaskId { get; set; }

        public string LiftDurationType { get; set; }

        public string LiftFrequencyType { get; set; }

        public string OrgToHeight { get; set; }

        public string OrgFromHeight { get; set; }

        public bool IsModifiedForBatteryOfTests { get; set; }
        public bool IsSplitForBatteryOfTests { get; set; }
        public bool IsNewForBatteryOfTests { get; set; }

        //
        //for reports
        //we use bucket to sort weights
        //
        public int BucketNo
        {
            get
            {
                return (int)this.WeightLb / 10;
            }
        }

        public int HeightRange
        {
            get
            {
                return Math.Abs((this.IntToHeight - this.IntFromHeight));
            }
        }

        public bool BracketsAnotherLift(JobTaskViewModel that)
        {
            //
            //different weight but
            //within the same bucket
            //
            //
            //   |-----------------------------------| source
            //        |----------------------------| target
            //
            return (this.Id != that.Id &&
                this.WeightLb >= that.WeightLb &&
                this.IntFromHeight <= that.IntFromHeight &&
                this.IntToHeight >= that.IntToHeight &&
                this.BucketNo == that.BucketNo);
        }

        public object Clone()
        {
            return (JobTaskViewModel)this.MemberwiseClone();
        }

        public bool OverlapsAnotherLift(JobTaskViewModel that)
        {
            //
            //same weight
            //
            return (this.Id != that.Id &&
                this.WeightLb == that.WeightLb &&
                this.IntFromHeight <= that.IntFromHeight &&
                this.IntToHeight >= that.IntToHeight &&
                this.BucketNo == that.BucketNo);
        }
    }
}
