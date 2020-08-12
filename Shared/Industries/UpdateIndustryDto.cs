using System.ComponentModel.DataAnnotations;

namespace Occumetric.Shared
{
    public class UpdateIndustryDto
    {
        [Required]
        public string Guid { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 4)]
        public string Name { get; set; }
    }
}
