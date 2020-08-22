using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Server.Areas.Helpers
{
    [Table("snooks_criteria")]
    public class SnooksCriteria
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("distance_min")]
        public int DistanceMin { get; set; }

        [Column("distance_max")]
        public int DistanceMax { get; set; }

        [Column("sex")]
        public string Sex { get; set; }

        public virtual List<SnooksPercentage> SnooksPercentages { get; set; }
    }
}
