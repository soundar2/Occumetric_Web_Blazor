using AutoMapper;
using Occumetric.Server.Data;

namespace Occumetric.Server.Areas.Shared
{
    public class BaseService
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IMapper _mapper;

        public BaseService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
