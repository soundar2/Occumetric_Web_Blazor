using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Server.Areas.Helpers
{
    [Table("lift_frequency_types")]
    public class LiftFrequencyType
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("lift_frequency_type")]
        public string Type { get; set; }

        [Column("lifts_per_min")]
        public int LiftsPerMinute { get; set; }

        [Column("sort_order")]
        public int SortOrder { get; set; }
    }
}
