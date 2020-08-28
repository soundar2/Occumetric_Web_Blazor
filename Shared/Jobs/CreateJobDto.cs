using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Occumetric.Shared
{
    public class CreateJobDto
    {
        public CreateJobDto()
        {
            MasterTaskIds = new List<int>();
        }

        public int TenantId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [MinLength(1)]
        public List<int> MasterTaskIds { get; set; }
    }
}
