using AutoMapper;
using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Data;
using System.Linq;

namespace Occumetric.Server.Areas.Tasks
{
    public class TaskService : BaseService, ITaskService
    {
        public TaskService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public JobTask CloneMasterTaskToTask(int id)
        {
            var mt = _context.MasterTasks.Find(id);
            return new JobTask
            {
                original_task_id = id,
                task_name = mt.task_name,
                effort_type = mt.effort_type,
                weight_lb = mt.weight_lb,
                from_height = mt.from_height,
                to_height = mt.to_height,
                int_from_height = mt.int_from_height,
                int_to_height = mt.int_to_height,
                carry_distance = mt.carry_distance,
                short_description = mt.short_description,
                lifting_index = mt.lifting_index,
                snooks_male = mt.snooks_male,
                snooks_female = mt.snooks_female,
                lift_duration_type = _context.LiftDurationTypes.OrderBy(x => x.sort_order).Select(x => x.lift_duration_type).First(),
                lift_frequency_type = _context.LiftFrequencyTypes.OrderBy(x => x.sort_order).Select(x => x.lift_frequency_type).First()
            };
        }
    }
}