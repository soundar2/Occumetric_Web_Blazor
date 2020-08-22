using Occumetric.Server.Areas.Industries;
using Occumetric.Server.Areas.TaskCategories;
using Occumetric.Server.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Occumetric.Server.Areas.MasterTasks
{
    [Table("master_tasks")]
    public class MasterTask : BaseEntity
    {
        [Column("TaskName")]
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

        public string LiftDurationType { get; set; }

        public string LiftFrequencyType { get; set; }

        #region Navigation

        public int IndustryId { get; set; }

        [ForeignKey("IndustryId")]
        public virtual Industry Industry { get; set; }

        private List<TaskCategoryMap> _taskCategoryMaps;

        public virtual List<TaskCategoryMap> TaskCategoryMaps
        {
            get => _taskCategoryMaps ?? (_taskCategoryMaps = new List<TaskCategoryMap>());
            protected set => _taskCategoryMaps = value;
        }

        #endregion Navigation

        public List<TaskCategory> TaskCategories()
        {
            return (from map in TaskCategoryMaps where map.MasterTaskId == this.Id select map.TaskCategory).ToList();
        }
    }
}
