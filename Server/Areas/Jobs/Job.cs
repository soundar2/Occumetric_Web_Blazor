using Occumetric.Server.Areas.Tenants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Server.Areas.Jobs
{
    [Table("jobs")]
    public class Job
    {
        public Job()
        {
            Guid = System.Guid.NewGuid().ToString();
        }

        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [Column("Guid")]
        public string Guid { get; set; }

        [Column("job_name")]
        public string Name { get; set; }

        [Column("job_description")]
        public string Description { get; set; }

        [ForeignKey("TenantId")]
        public virtual Tenant Tenant { get; set; }

        private List<JobTask> _jobTasks;

        public virtual List<JobTask> JobTasks
        {
            get => _jobTasks ?? (_jobTasks = new List<JobTask>());
            set => _jobTasks = value;
        }
    }
}
