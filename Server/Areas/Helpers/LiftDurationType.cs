using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Server.Areas.Helpers
{
    [Table("lift_duration_types")]
    public class LiftDurationType
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("lift_duration_type")]
        public string Type { get; set; }

        [Column("sort_order")]
        public int SortOrder { get; set; }
    }
}
