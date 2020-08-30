using Occumetric.Shared;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.JobTasks
{
    public interface IJobTaskService
    {
        JobTaskViewModel ViewGet(int Id);

        bool Update(UpdateJobTaskDto dto);

        UpdateJobTaskDto UpdateGet(int id);

        List<JobTaskViewModel> SimplifyListsForBotReport(int jobId);
    }
}
