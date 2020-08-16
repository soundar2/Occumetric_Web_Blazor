using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Occumetric.Server.Areas.Industries;
using Occumetric.Server.Areas.Jobs;
using Occumetric.Server.Areas.MasterTasks;
using Occumetric.Server.Areas.Models;
using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Areas.TaskCategories;
using Occumetric.Server.Areas.Tasks;
using Occumetric.Server.Areas.Tenants;
using Occumetric.Server.Areas.User;
using Occumetric.Server.Models;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Occumetric.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationDbContext(
            DbContextOptions options,
            ICurrentUserService currentUserService,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<MasterTask> MasterTasks { get; set; }
        public DbSet<TaskCategory> TaskCategories { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobTask> JobTasks { get; set; }
        public DbSet<TaskCategoryMasterTaskMapping> TaskCategoryMasterTaskMappings { get; set; }
        public DbSet<FrequencyMultiplier> FrequencyMultipliers { get; set; }
        public DbSet<SnooksQueryView> SnooksPercentages { get; set; }
        public DbSet<LiftOriginType> LiftOriginTypes { get; set; }
        public DbSet<LiftFrequencyType> LiftFrequencyTypes { get; set; }
        public DbSet<LiftDurationType> LiftDurationTypes { get; set; }
        public DbSet<Industry> Industries { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:

                        //entry.Entity.created_at = _currentUserService.UserId;
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:

                        //entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Entity<SnooksQueryView>().HasNoKey();
            base.OnModelCreating(builder);
        }
    }
}
