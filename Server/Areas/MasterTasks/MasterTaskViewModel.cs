using Occumetric.Server.Areas.Niosh;
using Occumetric.Server.Areas.Shared.Mappings;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.MasterTasks
{
    public class MasterTaskViewModel : IMapFrom<MasterTask>, INioshTask
    {
        public int id { get; set; }
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

        public List<TaskCategory> TaskCategories { get; set; }
    }
}