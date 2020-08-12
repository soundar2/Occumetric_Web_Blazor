using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Occumetric.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.Tenants
{
    public class GetTenantsDto : IRequest<List<TenantViewModel>>
    {
    }

    public class GetTenantsDtoHandler : IRequestHandler<GetTenantsDto, List<TenantViewModel>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTenantsDtoHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TenantViewModel>> Handle(GetTenantsDto request, CancellationToken cancellationToken)
        {
            var ret = await _context.Tenants
                .ProjectTo<TenantViewModel>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.name)
                .ToListAsync();
            return ret;
        }
    }
}