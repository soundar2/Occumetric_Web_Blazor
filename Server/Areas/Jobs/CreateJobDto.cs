using AutoMapper;
using MediatR;
using Occumetric.Server.Areas.Tasks;
using Occumetric.Server.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.Jobs
{
    public class CreateJobDto : IRequest
    {
        [Required]
        public string job_name { get; set; }

        public string job_description { get; set; }

        [Required]
        public List<int> master_task_ids { get; set; }
    }

    public class CreateJobDtoHandler : IRequestHandler<CreateJobDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITaskService _taskService;

        public CreateJobDtoHandler(ApplicationDbContext context, IMapper mapper, ITaskService taskService)
        {
            _context = context;
            _mapper = mapper;
            _taskService = taskService;
        }

        public async Task<Unit> Handle(CreateJobDto request, CancellationToken cancellationToken)
        {
            //
            //create new job
            //
            var job = new Job
            {
                tenant_id = 6,
                job_description = request.job_description,
                job_name = request.job_name
            };

            foreach (int id in request.master_task_ids)
            {
                job.Tasks.Add(_taskService.CloneMasterTaskToTask(id));
            }
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }


    }
}
