using System.ComponentModel.DataAnnotations;

namespace Occumetric.Shared
{
    public class CreateTaskCategoryDto
    {
        [Required]
        [StringLength(40, MinimumLength = 4)]
        public string Name { get; set; }
    }
}
