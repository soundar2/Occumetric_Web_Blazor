using System.ComponentModel.DataAnnotations;

namespace Occumetric.Shared
{
    public class CreateTenantDto
    {
        [Required]
        [StringLength(40, MinimumLength = 4)]
        public string Name { get; set; }

        [Required]
        public int IndustryId { get; set; }
    }
}
