using AutoMapper;
using Occumetric.Server.Areas.Industries;
using Occumetric.Server.Areas.Tasks;
using Occumetric.Server.Areas.Tenants;
using Occumetric.Shared;

namespace Occumetric.Server.Areas.Shared
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //createmap (source, destination)
            CreateMap<UpdateTenantDto, Tenant>();
            CreateMap<CreateTenantDto, Tenant>();
            CreateMap<UpdateTaskDto, JobTask>();
            CreateMap<Industry, IndustryViewModel>();
        }
    }
}
