using System.Collections.Generic;

namespace Occumetric.Shared
{
    public class JobViewModel
    {
        public int Id { get; set; }
        public int TenantId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        private List<JobTaskViewModel> _jobTaskViewModels;

        public virtual List<JobTaskViewModel> JobTaskViewModels
        {
            get => _jobTaskViewModels ?? (_jobTaskViewModels = new List<JobTaskViewModel>());
            set => _jobTaskViewModels = value;
        }
    }
}
