using AutoMapper;
using Occumetric.Server.Areas.Common;
using Occumetric.Server.Areas.Helpers;
using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Data;
using Occumetric.Shared;
using System.Collections.Generic;
using System.Linq;

namespace Occumetric.Server.Areas.Sites
{
    public class SiteService : OccumetricServiceBase, ISiteService
    {
        private IHelperService _helperService { get; set; }

        public SiteService(ApplicationDbContext context, IMapper mapper, IHelperService helperService) : base(context, mapper)
        {
            _helperService = helperService;
        }

        public List<SiteViewModel> Index(int tenantId)
        {
            List<Site> dbSites = _context.Sites
                .Where(x => x.TenantId == tenantId)
                .OrderBy(x => x.Name)
                .Select(x => x)
                .ToList();

            List<SiteViewModel> jvms = new List<SiteViewModel>();
            foreach (var item in dbSites)
            {
                var jvm = _mapper.Map<SiteViewModel>(item);
                jvms.Add(jvm);
            }
            return jvms;
        }

        public SiteViewModel ViewGet(int siteId)
        {
            Site dbSite = _context.Sites
                .Where(x => x.Id == siteId)
                .Single();

            var jvm = _mapper.Map<SiteViewModel>(dbSite);
            return jvm;
        }

        public UpdateSiteDto UpdateGet(int siteId)
        {
            Site dbSite = _context.Sites
                .Where(x => x.Id == siteId)
                .Single();

            var jvm = _mapper.Map<UpdateSiteDto>(dbSite);
            return jvm;
        }

        public int Create(CreateSiteDto createSiteDto)
        {
            //
            //if the same task name exists for this tenant
            //not allowed
            //
            if (_context.Sites.Where(x => x.Name == createSiteDto.Name && x.TenantId == createSiteDto.TenantId).Any())
            {
                throw new OccumetricException("Site name already exists");
            }
            var site = _mapper.Map<Site>(createSiteDto);
            _context.Sites.Add(site);
            _context.SaveChanges();
            return site.Id;
        }

        public bool Update(UpdateSiteDto updateSiteDto)
        {
            var dbSite = _context.Sites.Find(updateSiteDto.Id);
            dbSite = _mapper.Map<UpdateSiteDto, Site>(updateSiteDto, dbSite);
            _context.SaveChanges();
            return true;
        }
    }
}
