using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Areas.Tasks;
using Occumetric.Server.Areas.Tenants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Server.Areas.Jobs
{
    [Table("jobs")]
    public class Job : AuditableEntity
    {
        public Job()
        {
            Tasks = new List<JobTask>();
        }

        public int tenant_id { get; set; }
        public string job_name { get; set; }
        public string job_description { get; set; }

        [ForeignKey("tenant_id")]
        public virtual Tenant Tenant { get; set; }

        public virtual List<JobTask> Tasks { get; set; }
    }
}