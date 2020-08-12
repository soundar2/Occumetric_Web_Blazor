using AutoMapper;
using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Data;
using System;
using System.Linq;

namespace Occumetric.Server.Areas.Snooks
{
    public class SnooksService : BaseService, ISnooksService
    {
        public SnooksService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public (string, string) ComputeSnooks(int int_from_height, int int_to_height, int weight)
        {
            int distance = Math.Max(int_to_height - int_from_height, 0); //we prevent from > to
            if (distance == 0) throw new Exception("Distance must be > 0");
            weight = Math.Max(weight, 10);
            weight = Math.Min(weight, 100);

            var mfdata = (from c in _context.SnooksPercentages
                          where c.distance_max >= distance
                          && c.distance_min <= distance
                          && c.weight == weight
                          select c).ToList();
            System.Diagnostics.Trace.Assert(mfdata.Count == 2);
            var malePercent = mfdata.Where(s => s.sex == "Male").Select(s => s.percentage).Single();
            var femalePercent = mfdata.Where(s => s.sex == "Female").Select(s => s.percentage).Single();
            return (malePercent, femalePercent);
        }
    }
}