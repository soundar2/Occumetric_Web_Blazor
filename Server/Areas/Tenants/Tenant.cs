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

        public virtual List<Job> Jobs { get; set; }
    }
}
