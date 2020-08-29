using System.Collections.Generic;

namespace Occumetric.Shared
{
    public class TenantSummary
    {
        public List<JobCountViewModel> JobStats { get; set; }
    }

    public class JobCountViewModel
    {
        public JobCountViewModel()
        {
            TaskCountViewModels = new List<TaskCountViewModel>();
        }

        public int TenantId { get; set; }
        public int JobCount { get; set; }
        public List<TaskCountViewModel> TaskCountViewModels { get; set; }
    }

    public class TaskCountViewModel
    {
        public int JobId { get; set; }
        public int TaskCount { get; set; }
    }
}
