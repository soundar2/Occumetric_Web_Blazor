using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Server.Areas.Helpers
{
    [Table("snooks_data")]
    public class SnooksPercentage
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("criteria_id")]
        public int CriteriaId { get; set; }

        [Column("weight")]
        public int Weight { get; set; }

        [Column("percentage")]
        public string Percentage { get; set; }

        [ForeignKey("CriteriaId")]
        public virtual SnooksCriteria SnooksCriteria { get; set; }
    }
}
