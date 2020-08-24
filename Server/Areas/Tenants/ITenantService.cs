using Occumetric.Shared;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.Tenants
{
    public interface ITenantService
    {
        void Update(UpdateTenantDto dto);

        int Create(CreateTenantDto dto);

        TenantViewModel Get(int id);

        List<TenantViewModel> GetAllTenantsForIndustry(int IndustryId);
    }
}
