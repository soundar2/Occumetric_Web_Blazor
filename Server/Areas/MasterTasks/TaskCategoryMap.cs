using Occumetric.Server.Areas.TaskCategories;
using Occumetric.Server.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Server.Areas.MasterTasks
{
    public class TaskCategoryMap : BaseEntity
    {
        [Column("task_category_id")]
        public int TaskCategoryId { get; set; }

        [Column("master_task_id")]
        public int MasterTaskId { get; set; }

        [ForeignKey("MasterTaskId")]
        public virtual MasterTask MasterTask { get; set; }

        [ForeignKey("TaskCategoryId")]
        public virtual TaskCategory TaskCategory { get; set; }
    }
}
