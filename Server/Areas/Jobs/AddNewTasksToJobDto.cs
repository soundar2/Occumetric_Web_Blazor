using AutoMapper;
using MediatR;
using Occumetric.Server.Areas.Snooks;
using Occumetric.Server.Areas.Tasks;
using Occumetric.Server.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.Jobs
{
    public class AddNewTasksToJobDto : IRequest
    {
        [Required]
        public int job_id { get; set; }

        [Required]
        public List<int> master_task_ids { get; set; }
    }

    public class AddNewTasksToJobDtoHandler : IRequestHandler<AddNewTasksToJobDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITaskService _taskService;
        private readonly ISnooksService _snooksService;

        public AddNewTasksToJobDtoHandler(ApplicationDbContext context, IMapper mapper, ITaskService taskService, ISnooksService snooksService)
        {
            _context = context;
            _mapper = mapper;
            _taskService = taskService;
            _snooksService = snooksService;
        }

        public async Task<Unit> Handle(AddNewTasksToJobDto request, CancellationToken cancellationToken)
        {
            var job = _context.Jobs.Find(request.job_id);
            var newTasks = request.master_task_ids.Select(id => _taskService.CloneMasterTaskToTask(id)).ToList();

            //
            //do any of the new task names conflict with
            //exising task names for this job?
            //
            var duplicateTaskNames = newTasks.Select(t => t.task_name)
                .Intersect(job.Tasks.Select(y => y.task_name)).ToList();
            {
                if (duplicateTaskNames.Any())
                {
                    throw new Exception("Task names already exist:" + string.Join(",", duplicateTaskNames));
                }
            }

            //
            //since we are just adding master tasks and not updating
            //no need to calculate snooks, niosh etc
            //
            job.Tasks.AddRange(newTasks);
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
