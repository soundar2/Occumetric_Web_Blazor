using Occumetric.Server.Areas.Jobs;
using Occumetric.Server.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Server.Areas.Tenants
{
    [Table("tenants")]
    public class Tenant : BaseEntity
    {
        public string guid { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string agency { get; set; }

        public virtual List<Job> Jobs { get; set; }
    }
}