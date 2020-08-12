using System.ComponentModel.DataAnnotations;

namespace Occumetric.Shared
{
    public class CreateIndustryDto
    {
        [Required]
        [StringLength(40, MinimumLength = 4)]
        public string Name { get; set; }
    }
}
