using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Occumetric.Server.Areas.Helpers;
using Occumetric.Server.Areas.Industries;
using Occumetric.Server.Areas.Jobs;
using Occumetric.Server.Areas.JobTasks;
using Occumetric.Server.Areas.MasterTasks;
using Occumetric.Server.Areas.TaskCategories;
using Occumetric.Server.Areas.Tenants;
using System.Reflection;

namespace Occumetric.Server.Areas.Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IIndustryService, IndustryService>();
            services.AddTransient<ITenantService, TenantService>();
            services.AddTransient<ITaskCategoryService, TaskCategoryService>();
            services.AddTransient<IMasterTaskService, MasterTaskService>();
            services.AddTransient<IHelperService, HelperService>();
            services.AddTransient<IJobService, JobService>();
            services.AddTransient<IJobTaskService, JobTaskService>();

            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            return services;
        }
    }
}
