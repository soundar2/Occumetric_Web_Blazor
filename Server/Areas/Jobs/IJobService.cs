using Occumetric.Shared;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.Jobs
{
    public interface IJobService
    {
        List<JobViewModel> Index(int tenantId);

        int Create(CreateJobDto createJobDto);

        bool AddNewTasksToJob(int jobId, List<int> MasterTaskIds);

        TenantSummary GetJobCountsByTenant();
    }
}
