using AutoMapper;
using MediatR;
using Occumetric.Server.Areas.Niosh;
using Occumetric.Server.Areas.Snooks;
using Occumetric.Server.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.Tasks
{
    public class UpdateTaskDto : IRequest
    {
        public int task_id { get; set; }
        public string task_name { get; set; }
        public string effort_type { get; set; }
        public double? weight_lb { get; set; }
        public string from_height { get; set; }
        public string to_height { get; set; }
        public string carry_distance { get; set; }
        public string short_description { get; set; }
        public string lift_duration_type { get; set; }
        public string lift_frequency_type { get; set; }
    }

    public class UpdateTaskDtoHandler : IRequestHandler<UpdateTaskDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITaskService _taskService;
        private readonly ISnooksService _snooksService;
        private readonly INioshService _nioshService;

        public UpdateTaskDtoHandler(ApplicationDbContext context, IMapper mapper, ITaskService taskService,
            ISnooksService snooksService,
            INioshService nioshService)
        {
            _context = context;
            _mapper = mapper;
            _taskService = taskService;
            _snooksService = snooksService;
            _nioshService = nioshService;
        }

        public async Task<Unit> Handle(UpdateTaskDto updateTaskDto, CancellationToken cancellationToken)
        {
            //var dbTask = await _context.JobTasks.FindAsync(updateTaskDto.task_id);
            //bool taskNameExists = await _context.JobTasks.Where(t => t.task_name == updateTaskDto.task_name && t.id != updateTaskDto.task_id && dbTask.job_id == t.job_id).AnyAsync();
            //if (taskNameExists)
            //{
            //    throw new Exception("Task name exists already");
            //}
            ////
            ////do any of the new task names conflict with
            ////exising task names for this job?
            ////
            //dbTask = _mapper.Map<JobTask>(updateTaskDto);
            //if (dbTask.effort_type.Contains("Lift"))
            //{
            //    var snooks = _snooksService.ComputeSnooks(
            //    PdaUtility.SanitizeString(dbTask.from_height),
            //    PdaUtility.SanitizeString(dbTask.to_height),
            //    Convert.ToInt32(dbTask.weight_lb ?? 0));
            //    dbTask.snooks_male = snooks.Item1;
            //    dbTask.snooks_female = snooks.Item2;
            //    dbTask.lifting_index = _nioshService.LiftingIndex(dbTask);
            //}
            //await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }


    }
}
