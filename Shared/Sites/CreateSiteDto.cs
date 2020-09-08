using System.ComponentModel.DataAnnotations;

namespace Occumetric.Shared
{
    public class CreateSiteDto
    {
        public CreateSiteDto()
        {
            State = "California";
        }

        [Required]
        public int TenantId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }
    }
}
