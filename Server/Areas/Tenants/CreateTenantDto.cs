using AutoMapper;
using MediatR;
using Occumetric.Server.Data;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.Tenants
{
    public class CreateTenantDto : IRequest<string>
    {
        public string name { get; set; }

        public string address { get; set; }

        public string state { get; set; }

        public string agency { get; set; }
    }

    public class CreateTenantDtoHandler : IRequestHandler<CreateTenantDto, string>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateTenantDtoHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> Handle(CreateTenantDto request, CancellationToken cancellationToken)
        {
            var tenant = _mapper.Map<Tenant>(request);
            tenant.guid = System.Guid.NewGuid().ToString();
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync(cancellationToken);
            return tenant.guid;
        }
    }
}