using System.ComponentModel.DataAnnotations;

namespace Occumetric.Shared
{
    public class UpdateMasterTaskDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string EffortType { get; set; }

        public double WeightLb { get; set; }

        public string FromHeight { get; set; }

        public string ToHeight { get; set; }

        public string CarryDistance { get; set; }
    }
}
