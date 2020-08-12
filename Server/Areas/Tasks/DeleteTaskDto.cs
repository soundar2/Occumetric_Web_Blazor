using MediatR;
using Occumetric.Server.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.Tasks
{
    public class DeleteTaskDto : IRequest
    {
        public int task_id { get; set; }
    }

    public class DeleteTaskDtoHandler : IRequestHandler<DeleteTaskDto>
    {
        private readonly ApplicationDbContext _context;

        public DeleteTaskDtoHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(DeleteTaskDto deleteTaskDto, CancellationToken cancellationToken)
        {
            _context.JobTasks.Remove(_context.JobTasks.Find(deleteTaskDto.task_id));
            return Task.FromResult(Unit.Value);
        }
    }
}