using AutoMapper;
using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Data;
using Occumetric.Shared;
using System.Collections.Generic;
using System.Linq;

namespace Occumetric.Server.Areas.EffortTypes
{
    public class EffortTypeService : OccumetricServiceBase, IEffortTypeService
    {
        public EffortTypeService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public List<EffortTypeViewModel> GetEffortTypes()
        {
            return _mapper.Map<List<EffortTypeViewModel>>((from e in _context.EffortTypes orderby e.SortOrder select e).ToList());
        }
    }
}
