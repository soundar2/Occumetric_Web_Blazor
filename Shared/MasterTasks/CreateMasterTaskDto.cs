﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Occumetric.Shared
{
    public class CreateMasterTaskDto
    {
        public CreateMasterTaskDto()
        {
            EffortType = "Lift";
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string EffortType { get; set; }

        public string ShortDescription { get; set; }

        public double? WeightLb { get; set; }

        public string FromHeight { get; set; }

        public string ToHeight { get; set; }

        public string CarryDistance { get; set; }

        [Required]
        public List<int> IndustryIds { get; set; }
    }
}
