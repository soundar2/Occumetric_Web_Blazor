using AutoMapper;
using Occumetric.Server.Areas.Industries;
using Occumetric.Server.Areas.MasterTasks;
using Occumetric.Server.Areas.TaskCategories;
using Occumetric.Server.Areas.Tenants;
using Occumetric.Shared;

namespace Occumetric.Server.Areas.Shared
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //createmap (source, destination)
            CreateMap<Industry, IndustryViewModel>();
            CreateMap<Tenant, TenantViewModel>();
            CreateMap<TaskCategory, TaskCategoryViewModel>();

            //
            //master tasks
            //
            CreateMap<CreateMasterTaskDto, MasterTask>();
            CreateMap<UpdateMasterTaskDto, MasterTask>();
            CreateMap<MasterTask, MasterTaskViewModel>();
        }
    }
}
