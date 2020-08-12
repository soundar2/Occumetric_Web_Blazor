using System.Linq;
using System;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.Niosh
{
    public interface INioshTask
    {
        public string effort_type { get; set; }
        public double? weight_lb { get; set; }
        public string from_height { get; set; }
        public string to_height { get; set; }
        public int int_from_height { get; set; }
        public int int_to_height { get; set; }
        public string lift_duration_type { get; set; }
        public string lift_frequency_type { get; set; }
    }
}