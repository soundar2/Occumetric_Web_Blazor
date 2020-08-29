using Occumetric.Shared;

namespace Occumetric.Server.Areas.JobTasks
{
    public interface IJobTaskService
    {
        JobTaskViewModel ViewGet(int Id);

        bool Update(UpdateJobTaskDto dto);

        UpdateJobTaskDto UpdateGet(int id);
    }
}
