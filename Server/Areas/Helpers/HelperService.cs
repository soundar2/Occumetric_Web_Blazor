using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Data;
using Occumetric.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Occumetric.Server.Areas.Helpers
{
    public class HelperService : OccumetricServiceBase, IHelperService
    {
        private readonly IMemoryCache _memoryCache;
        private MemoryCacheEntryOptions _cacheExpirationOptions;

        public HelperService(ApplicationDbContext context, IMapper mapper, IMemoryCache memoryCache) : base(context, mapper)
        {
            _memoryCache = memoryCache;
            _cacheExpirationOptions = new MemoryCacheEntryOptions();
            _cacheExpirationOptions.AbsoluteExpiration = DateTime.Now.AddDays(1);
        }

        public List<EffortTypeViewModel> GetEffortTypes()
        {
            return _memoryCache.GetOrCreate<List<EffortTypeViewModel>>("EffortTypes",
          cacheEntry =>
          {
              return _mapper.Map<List<EffortTypeViewModel>>((from e in _context.EffortTypes orderby e.SortOrder select e).ToList());
          });
        }

        public List<LiftDurationTypeViewModel> GetLiftDurationTypes()
        {
            return _memoryCache.GetOrCreate<List<LiftDurationTypeViewModel>>("LiftDurationTypeViewModels",
               cacheEntry =>
               {
                   return _mapper.Map<List<LiftDurationTypeViewModel>>((from t in _context.LiftDurationType orderby t.SortOrder select t).ToList());
               });
        }

        public List<LiftFrequencyTypeViewModel> GetLiftFrequencyTypes()
        {
            return _memoryCache.GetOrCreate<List<LiftFrequencyTypeViewModel>>("LiftFrequencyTypeViewModels",
               cacheEntry =>
               {
                   return _mapper.Map<List<LiftFrequencyTypeViewModel>>((from t in _context.LiftFrequencyTypes orderby t.SortOrder select t).ToList());
               });
        }

        private List<FrequencyMultiplier> FrequencyMultipliers()
        {
            return _memoryCache.GetOrCreate<List<FrequencyMultiplier>>("FrequencyMultipliers",
                cacheEntry =>
                {
                    return _context.FrequencyMultipliers.ToList();
                });
        }

        public double GetNioshIndex(NioshCalculateDto dto)
        {
            if (String.IsNullOrEmpty(dto.LiftFrequencyType))
            {
                dto.LiftFrequencyType = this.GetLiftFrequencyTypes().First().Type;
            }
            if (String.IsNullOrEmpty(dto.LiftDurationType))
            {
                dto.LiftDurationType = this.GetLiftDurationTypes().First().Type;
            }
            if (!dto.EffortType.Contains("Lift") || dto.WeightLb <= 0) return -1;
            int lc = 51;

            //var hm = 1;
            double vm = 1 - 0.0075 * Math.Abs(dto.FromHeight - 30);

            double dist = Math.Abs(dto.ToHeight - dto.FromHeight);
            if (dist == 0) return -1;
            double dm = 0.82 + 1.8 / dist;

            //var am = 1; //assymetry
            double fm = 1.0; //frequency

            //var cm = 1; // coupling
            //
            //frequency multiplier is defined by
            // a. lifts per minute F
            // b. vertical location of hands at origin V
            // c. durtion of continuous lifting
            //
            string liftOriginType = "Low";

            if (dto.FromHeight > 30)
            {
                liftOriginType = "High";
            }

            //
            //TBD: Cache Frequency Multipliers
            //
            fm = this.FrequencyMultipliers()
                        .Where(f => f.LiftDurationType == dto.LiftDurationType
                                && f.LiftFrequencyType == dto.LiftFrequencyType
                                && f.LiftOriginType == liftOriginType)
                                .Select(f => f.Multiplier)
                                .DefaultIfEmpty(0)
                                .FirstOrDefault();
            var rwl = lc * vm * dm * fm;
            var liftingIndex = dto.WeightLb / rwl;
            return liftingIndex;
        }

        public SnooksViewModel CalculateSnooks(SnooksCalculateDto dto)
        {
            var vm = new SnooksViewModel();
            int distance = Math.Abs(dto.ToHeight - dto.FromHeight);
            vm.StrMalePercentage = SnooksPercentage("Male", dto.WeightLb, distance);
            vm.IntMalePercentage = ConvertToNumber(vm.StrMalePercentage);

            vm.StrFemalePercentage = SnooksPercentage("Female", dto.WeightLb, distance);
            vm.IntFemalePercentage = ConvertToNumber(vm.StrFemalePercentage);

            return vm;
        }

        private int ConvertToNumber(string snooksPercentage)
        {
            if (snooksPercentage.Length == 0)
            {
                return 0;
            }
            else if (snooksPercentage.Contains(">90"))
            {
                return 91;
            }
            else if (snooksPercentage.Contains("<10"))
            {
                return 9;
            }
            else
            {
                string s = snooksPercentage.Replace("%", "");
                return Convert.ToInt32(s);
            }
        }

        private string SnooksPercentage(string sex, int weight, int distance)
        {
            if (weight == 0) return "";
            weight = Math.Max(weight, 10);
            weight = Math.Min(weight, 100);

            distance = Math.Max(distance, 0);
            if (distance == 0) return "";
            distance = Math.Min(distance, 100);

            return (from c in _context.SnooksCriteria
                    from p in c.SnooksPercentages
                    where c.Sex == sex
                    && c.DistanceMin <= distance
                    && c.DistanceMax >= distance
                    && p.Weight == weight
                    select p.Percentage).Single().ToString() + "%";
        }
    } // end class
}
