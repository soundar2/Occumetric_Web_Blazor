using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Areas.Tenants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Server.Areas.Industries
{
    [Table("industries")]
    public class Industry : AuditableEntity
    {
        public Industry()
        {
            Guid = System.Guid.NewGuid().ToString();
        }

        public string Guid { get; set; }
        public string Name { get; set; }
        private List<Tenant> _tenants { get; set; }

        public virtual List<Tenant> Tenants
        {
            get => _tenants ?? (_tenants = new List<Tenant>());
            protected set => _tenants = value;
        }
    }
}
