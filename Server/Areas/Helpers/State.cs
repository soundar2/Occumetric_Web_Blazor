using Occumetric.Server.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Server.Areas.Helpers
{
    [Table("states")]
    public class State : BaseEntity
    {
        [Column("stateCode")]
        public string Code { get; set; }

        [Column("stateName")]
        public string Name { get; set; }
    }
}
