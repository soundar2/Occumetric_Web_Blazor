using System.Linq;
using System;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.Snooks
{
    public interface ISnooksTask
    {
        public double? weight_lb { get; set; }
        public string from_height { get; set; }
        public string to_height { get; set; }
        public int int_from_height { get; set; }
        public int int_to_height { get; set; }
    }
}