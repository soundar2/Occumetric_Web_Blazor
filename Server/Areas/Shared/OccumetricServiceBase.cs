using AutoMapper;
using Occumetric.Server.Data;

namespace Occumetric.Server.Areas.Shared
{
    public abstract class OccumetricServiceBase
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IMapper _mapper;

        public OccumetricServiceBase(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
    }
}
