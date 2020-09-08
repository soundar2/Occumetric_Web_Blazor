using Occumetric.Server.Areas.Tenants;
using Occumetric.Server.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Server.Areas.Sites
{
    [Table("sites")]
    public class Site : BaseEntity
    {
        public Site()
        {
            Guid = System.Guid.NewGuid().ToString();
        }

        public string Guid { get; set; }

        [Column("location")]
        public string Name { get; set; }

        [Column("address1")]
        public string Address1 { get; set; }

        [Column("address2")]
        public string Address2 { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("stateName")]
        public string State { get; set; }

        [Column("zip")]
        public string Zip { get; set; }

        [Column("tenantId")]
        public int TenantId { get; set; }

        [ForeignKey("TenantId")]
        public virtual Tenant Tenant { get; set; }
    }
}
