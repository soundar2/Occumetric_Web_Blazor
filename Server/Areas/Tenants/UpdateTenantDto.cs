using AutoMapper;
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
    public class UpdateTenantDto : IRequest<bool>
    {
        public string guid { get; set; }

        public string name { get; set; }

        public string address { get; set; }

        public string state { get; set; }

        public string agency { get; set; }
    }

    public class UpdateTenantDtoHandler : IRequestHandler<UpdateTenantDto, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateTenantDtoHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateTenantDto request, CancellationToken cancellationToken)
        {
            var dbTenant = await _context.Tenants
                .Where(t => t.guid == request.guid)
                .SingleOrDefaultAsync();
            if (dbTenant != null)
            {
                dbTenant = _mapper.Map<UpdateTenantDto, Tenant>(request, dbTenant);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            throw new KeyNotFoundException("Tenant not found");
        }
    }
}