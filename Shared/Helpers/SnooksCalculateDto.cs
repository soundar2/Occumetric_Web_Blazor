using System.ComponentModel.DataAnnotations;

namespace Occumetric.Shared
{
    public class SnooksCalculateDto
    {
        [Required]
        public string EffortType { get; set; }

        [Required]
        public int WeightLb { get; set; }

        [Required]
        public int FromHeight { get; set; }

        [Required]
        public int ToHeight { get; set; }
    }
}
