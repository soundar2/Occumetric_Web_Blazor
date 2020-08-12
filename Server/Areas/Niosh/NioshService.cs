using Occumetric.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Occumetric.Server.Areas.Niosh
{
    public class NioshService : INioshService
    {
        private readonly ApplicationDbContext _context;

        public NioshService(ApplicationDbContext context)
        {
            _context = context;
        }

        public double LiftingIndex(INioshTask np)
        {
            if (!np.effort_type.Contains("Lift") || (np.weight_lb ?? 0) <= 0) return -1;
            var lc = 51;
            //var hm = 1;
            var vm = 1 - 0.0075 * Math.Abs(np.int_from_height - 30);
            var dist = Math.Abs(np.int_to_height - np.int_from_height);
            if (dist == 0) return -1;
            var dm = 0.82 + 1.8 / dist;
            //var am = 1; //assymetry
            var fm = 1.0; //frequency
            //var cm = 1; // coupling
            //
            //frequency multiplier is defined by
            // a. lifts per minute F
            // b. vertical location of hands at origin V
            // c. durtion of continuous lifting
            //
            var liftOriginType = "Low";
            if (np.int_from_height > 30)
            {
                liftOriginType = "High";
            }
            //
            //TBD: Cache Frequency Multipliers
            //
            fm = _context.FrequencyMultipliers
                        .Where(f => f.lift_duration_type == np.lift_duration_type
                        && f.lift_frequency_type == np.lift_frequency_type
                        && f.lift_origin_type == liftOriginType)
                        .Select(f => f.multiplier)
                        .DefaultIfEmpty(0)
                        .FirstOrDefault();
            var rwl = lc * vm * dm * fm;
            var liftingIndex = (np.weight_lb ?? 0) / rwl;
            return liftingIndex;
        }
    }
}