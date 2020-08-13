using System.ComponentModel.DataAnnotations;

namespace Occumetric.Shared
{
    public class UpdateTenantDto
    {
        //
        //we don't have industry id here
        //because we cannot reassign to a different
        //industry
        //
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 4)]
        public string Name { get; set; }
    }
}
