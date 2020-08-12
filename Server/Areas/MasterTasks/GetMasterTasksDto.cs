using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Occumetric.Server.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.MasterTasks
{
    public class GetMasterTasksDto : IRequest<List<MasterTaskViewModel>>
    {
    }

    public class GetMasterTasksDtoHandler : IRequestHandler<GetMasterTasksDto, List<MasterTaskViewModel>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetMasterTasksDtoHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MasterTaskViewModel>> Handle(GetMasterTasksDto request, CancellationToken cancellationToken)
        {
            var result = await _context.MasterTasks
                .OrderBy(t => t.task_name)
                .Select(t => _mapper.Map<MasterTaskViewModel>(t))
                .ToListAsync();
            foreach (var item in result)
            {
                item.TaskCategories = await (from m in _context.TaskCategoryMasterTaskMappings
                                             where m.master_task_id == item.id
                                             select m.TaskCategory).ToListAsync();
            }

            return result;
        }
    }
}