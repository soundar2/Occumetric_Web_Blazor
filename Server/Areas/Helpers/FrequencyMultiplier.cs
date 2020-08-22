using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Server.Areas.Helpers
{
    [Table("frequency_multipliers")]
    public class FrequencyMultiplier
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("lifts_per_minute")]
        public int LiftsPerMinute { get; set; }

        [Column("lifts_per_15_minutes")]
        public int LiftsPer15Minutes { get; set; }

        [Column("lift_origin_type")]
        public string LiftOriginType { get; set; }

        [Column("lift_duration_type")]
        public string LiftDurationType { get; set; }

        [Column("lift_frequency_type")]
        public string LiftFrequencyType { get; set; }

        [Column("multiplier")]
        public double Multiplier { get; set; }
    }
}
