using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Server.Areas.EffortTypes
{
    [Table("effort_types")]
    public class EffortType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("effort_type")]
        public string Type { get; set; }

        [Column("sort_order")]
        public int SortOrder { get; set; }
    }
}
