using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Occumetric.Server.Areas.Shared.Mappings;
using Occumetric.Server.Data;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.Jobs
{
    public class GetJobsDto : IRequest<List<JobViewModel>>
    {
        public int tenantId { get; set; }
    }

    public class JobViewModel : IMapFrom<Job>
    {
        public int id { get; set; }
        public int tenant_id { get; set; }
        public string job_name { get; set; }
        public string job_description { get; set; }
    }

    public class GetJobsDtoHandler : IRequestHandler<GetJobsDto, List<JobViewModel>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetJobsDtoHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<JobViewModel>> Handle(GetJobsDto request, CancellationToken cancellationToken)
        {
            return _context.Jobs
                .ProjectTo<JobViewModel>(_mapper.ConfigurationProvider)
                .Where(j => j.tenant_id == request.tenantId)
                .ToListAsync();
        }
    }
}