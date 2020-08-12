using System.Linq;
using System;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.Niosh
{
    public interface INioshService
    {
        public double LiftingIndex(INioshTask np);
    }
}