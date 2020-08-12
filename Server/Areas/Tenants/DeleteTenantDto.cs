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
    public class DeleteTenantDto : IRequest<bool>
    {
        public string guid { get; set; }
    }

    public class DeleteTenantDtoHandler : IRequestHandler<DeleteTenantDto, bool>
    {
        private readonly ApplicationDbContext _context;

        public DeleteTenantDtoHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteTenantDto request, CancellationToken cancellationToken)
        {
            var tenant = await _context.Tenants
                .Where(t => t.guid == request.guid)
                .SingleOrDefaultAsync();
            if (tenant != null)
            {
                _context.Tenants.Remove(tenant);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            throw new KeyNotFoundException();
        }
    }
}