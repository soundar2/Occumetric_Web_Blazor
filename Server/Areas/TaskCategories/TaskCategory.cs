using Occumetric.Server.Areas.Industries;
using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Server.Areas.TaskCategories
{
    [Table("task_categories")]
    public class TaskCategory
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("category_name")]
        public string Name { get; set; }

        public int IndustryId { get; set; }

        [ForeignKey("IndustryId")]
        public virtual Industry Industry { get; set; }
    }
}
