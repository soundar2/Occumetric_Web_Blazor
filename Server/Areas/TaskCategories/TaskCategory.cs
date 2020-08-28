using Occumetric.Server.Areas.MasterTasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Server.Areas.TaskCategories
{
    [Table("task_categories")]
    public class TaskCategory
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("category_name")]
        public string Name { get; set; }

        #region Navigation

        private List<TaskCategoryMap> _taskCategoryMaps;

        public virtual List<TaskCategoryMap> TaskCategoryMaps
        {
            get => _taskCategoryMaps ?? (_taskCategoryMaps = new List<TaskCategoryMap>());
            set => _taskCategoryMaps = value;
        }

        #endregion Navigation
    }
}
