using Occumetric.Shared;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.Sites
{
    public interface ISiteService
    {
        List<SiteViewModel> Index(int tenantId);

        int Create(CreateSiteDto createSiteDto);

        SiteViewModel ViewGet(int siteId);

        UpdateSiteDto UpdateGet(int siteId);

        bool Update(UpdateSiteDto updateSiteDto);
    }
}
