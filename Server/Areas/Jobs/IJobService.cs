using Occumetric.Shared;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.Jobs
{
    public interface IJobService
    {
        List<JobViewModel> GetJobs(int tenantId);

        int Create(CreateJobDto createJobDto);
    }
}
