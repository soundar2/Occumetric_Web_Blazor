using Occumetric.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Transactions;

namespace Occumetric.Server.Areas.Models
{
    [Table("effort_types")]
    public class EffortType : BaseEntity
    {
        public string effort_type { get; set; }
        public int sort_order { get; set; }
    }

    [Table("lift_duration_types")]
    public class LiftDurationType : BaseEntity
    {
        public string lift_duration_type { get; set; }
        public int sort_order { get; set; }
    }

    [Table("lift_frequency_types")]
    public class LiftFrequencyType : BaseEntity
    {
        public string lift_frequency_type { get; set; }
        public float lifts_per_min { get; set; }
        public int sort_order { get; set; }
    }

    [Table("lift_origin_types")]
    public class LiftOriginType : BaseEntity
    {
        public string lift_origin_type { get; set; }
        public int sort_order { get; set; }
    }

    [Table("frequency_multipliers")]
    public class FrequencyMultiplier : BaseEntity
    {
        public double lifts_per_minute { get; set; }
        public double lifts_per_15_minutes { get; set; }
        public string lift_origin_type { get; set; }
        public string lift_duration_type { get; set; }
        public string lift_frequency_type { get; set; }
        public double multiplier { get; set; }
    }

    [Table("snooks_query_view")]
    public class SnooksQueryView
    {
        public string sex { get; set; }
        public int distance_min { get; set; }
        public int distance_max { get; set; }
        public int weight { get; set; }
        public string percentage { get; set; }
    }
}