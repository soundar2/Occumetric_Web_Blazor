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

namespace Occumetric.Server.Areas.MasterTasks
{
    public class ShowMasterTaskDto : IRequest<MasterTask>
    {
        public int id { get; set; }
    }

    public class ShowMasterTaskDtoHandler : IRequestHandler<ShowMasterTaskDto, MasterTask>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ShowMasterTaskDtoHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MasterTask> Handle(ShowMasterTaskDto request, CancellationToken cancellationToken)
        {
            var ret = (await _context.MasterTasks
                .Where(t => t.id == request.id)
                .ToListAsync())
                .SingleOrDefault();
            return ret;
        }
    }
}