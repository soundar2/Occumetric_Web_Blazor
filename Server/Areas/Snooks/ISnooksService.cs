using System.Linq;
using System;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.Snooks
{
    public interface ISnooksService
    {
        public (string, string) ComputeSnooks(int int_from_height, int int_to_height, int weight);
    }
}