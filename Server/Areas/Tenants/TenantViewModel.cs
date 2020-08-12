using System.Linq;
using System;
using System.Collections.Generic;
using Occumetric.Server.Areas.Shared.Mappings;

namespace Occumetric.Server.Areas.Tenants
{
    public class TenantViewModel : IMapFrom<Tenant>
    {
        public string guid { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string agency { get; set; }
    }
}