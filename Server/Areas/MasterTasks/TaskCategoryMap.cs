using Occumetric.Server.Areas.TaskCategories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Server.Areas.MasterTasks
{
    [Table("task_category_master_tasks")]
    public class TaskCategoryMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

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
