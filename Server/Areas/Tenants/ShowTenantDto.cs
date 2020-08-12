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
    public class ShowTenantDto : IRequest<TenantViewModel>
    {
        public string guid { get; set; }
    }

    public class ShowTenantDtoHandler : IRequestHandler<ShowTenantDto, TenantViewModel>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ShowTenantDtoHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TenantViewModel> Handle(ShowTenantDto request, CancellationToken cancellationToken)
        {
            var ret = (await _context.Tenants
                .ProjectTo<TenantViewModel>(_mapper.ConfigurationProvider)
                .Where(t => t.guid == request.guid)
                .ToListAsync())
                .SingleOrDefault();
            return ret;
        }
    }
}