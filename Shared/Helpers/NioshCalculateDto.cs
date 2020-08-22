using System.ComponentModel.DataAnnotations;

namespace Occumetric.Shared
{
    public class NioshCalculateDto
    {
        [Required]
        public string EffortType { get; set; }

        [Required]
        public int WeightLb { get; set; }

        [Required]
        public int FromHeight { get; set; }

        [Required]
        public int ToHeight { get; set; }

        public string LiftDurationType { get; set; }

        public string LiftFrequencyType { get; set; }
    }
}
