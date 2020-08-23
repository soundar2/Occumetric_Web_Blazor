using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Occumetric.Shared
{
    public class CreateMasterTaskDto
    {
        public CreateMasterTaskDto()
        {
            EffortType = "Lift";
            IndustryIds = new List<int>();
            CategoryIds = new List<int>(); 
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string EffortType { get; set; }

        public string ShortDescription { get; set; }

        //[Required]
        public double WeightLb { get; set; }

        public string FromHeight { get; set; }

        public string ToHeight { get; set; }

        public string CarryDistance { get; set; }

        //[Required]
        public List<int> IndustryIds { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
