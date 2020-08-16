using Occumetric.Server.Areas.Niosh;
using Occumetric.Server.Areas.Snooks;
using Occumetric.Server.Areas.TaskCategories;
using Occumetric.Server.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Server.Areas.MasterTasks
{
    [Table("master_tasks")]
    public class MasterTask : BaseEntity, INioshTask, ISnooksTask
    {
        public string task_name { get; set; }

        public string effort_type { get; set; }
        public double? weight_lb { get; set; }
        public string from_height { get; set; }
        public string to_height { get; set; }
        public int int_from_height { get; set; }
        public int int_to_height { get; set; }
        public string carry_distance { get; set; }
        public string short_description { get; set; }
        public double lifting_index { get; set; }
        public string snooks_male { get; set; }
        public string snooks_female { get; set; }

        public string lift_duration_type { get; set; }
        public string lift_frequency_type { get; set; }

        public virtual List<TaskCategoryMasterTaskMapping> TaskCategoryMasterTaskMappings { get; set; }
    }

    [Table("task_category_master_tasks")]
    public class TaskCategoryMasterTaskMapping : BaseEntity
    {
        public int master_task_id { get; set; }

        [ForeignKey("master_task_id")]
        public MasterTask MasterTask { get; set; }

        public int task_category_id { get; set; }

        [ForeignKey("task_category_id")]
        public TaskCategory TaskCategory { get; set; }
    }
}
