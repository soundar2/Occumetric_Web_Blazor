using Occumetric.Server.Areas.Applicants;
using Occumetric.Server.Areas.Industries;
using Occumetric.Server.Areas.Jobs;
using Occumetric.Server.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Server.Areas.Tenants
{
    [Table("tenants")]
    public class Tenant : BaseEntity
    {
        public Tenant()
        {
            Guid = System.Guid.NewGuid().ToString();
        }

        public string Guid { get; set; }
        public string Name { get; set; }

        public int IndustryId { get; set; }

        [ForeignKey("IndustryId")]
        public virtual Industry Industry { get; set; }

        private List<Job> _jobs;

        public virtual List<Job> Jobs
        {
            get => _jobs ?? (_jobs = new List<Job>());
            set => _jobs = value;
        }

        private List<Applicant> _applicants;

        public virtual List<Applicant> Applicants
        {
            get => _applicants ?? (_applicants = new List<Applicant>());
            set => _applicants = value;
        }
    }
}
