using Occumetric.Server.Areas.Jobs;
using Occumetric.Server.Areas.Niosh;
using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Areas.Snooks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Occumetric.Server.Areas.Tasks
{
    [Table("tasks")]
    public class JobTask : AuditableEntity, INioshTask, ISnooksTask
    {
        public int job_id { get; set; }
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

        public int? original_task_id { get; set; }
        public string lift_duration_type { get; set; }
        public string lift_frequency_type { get; set; }

        [ForeignKey("job_id")]
        public virtual Job Job { get; set; }
    }
}