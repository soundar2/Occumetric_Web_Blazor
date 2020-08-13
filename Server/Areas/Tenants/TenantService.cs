using AutoMapper;
using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Data;
using Occumetric.Shared;
using System.Collections.Generic;
using System.Linq;

namespace Occumetric.Server.Areas.Tenants
{
    public class TenantService : OccumetricServiceBase, ITenantService
    {
        public TenantService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public List<TenantViewModel> Index(int industryId)
        {
            return _mapper.Map<List<TenantViewModel>>((from t in _context.Tenants
                                                       where t.IndustryId == industryId
                                                       orderby t.Name
                                                       select t).ToList());
        }

        public TenantViewModel Get(int id)
        {
            return _mapper.Map<TenantViewModel>(_context.Tenants.Find(id));
        }

        public int Create(CreateTenantDto dto)
        {
            var tenant = new Tenant
            {
                Name = dto.Name,
            };
            _context.Industries.Find(dto.IndustryId)
                .Tenants.Add(tenant);
            _context.SaveChanges();
            return tenant.Id;
        }

        public void Update(UpdateTenantDto dto)
        {
            var tenant = _context.Tenants.Find(dto.Id);
            tenant.Name = dto.Name;
            _context.SaveChanges();
        }
    }
}
