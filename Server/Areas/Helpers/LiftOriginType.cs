using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Server.Areas.Helpers
{
    [Table("lift_origin_types")]
    public class LiftOriginType
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("lift_origin_type")]
        public string Type { get; set; }

        [Column("sort_order")]
        public int SortOrder { get; set; }
    }
}
