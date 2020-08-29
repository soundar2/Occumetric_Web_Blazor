using System.ComponentModel.DataAnnotations;

namespace Occumetric.Shared
{
    public class UpdateJobDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
