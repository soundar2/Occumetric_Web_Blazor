using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Occumetric.Server.Areas.Common;
using Occumetric.Server.Areas.Helpers;
using Occumetric.Server.Areas.MasterTasks;
using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Data;
using Occumetric.Shared;
using System.Collections.Generic;
using System.Linq;

namespace Occumetric.Server.Areas.Jobs
{
    public class JobService : OccumetricServiceBase, IJobService
    {
        private IHelperService _helperService { get; set; }

        public JobService(ApplicationDbContext context, IMapper mapper, IHelperService helperService) : base(context, mapper)
        {
            _helperService = helperService;
        }

        public List<JobViewModel> Index(int tenantId)
        {
            List<Job> dbJobs = _context.Jobs
                .Include(j => j.JobTasks)
                .Where(x => x.TenantId == tenantId)
                .Select(x => x)
                .ToList();

            List<JobViewModel> jvms = new List<JobViewModel>();
            foreach (var item in dbJobs)
            {
                var jvm = _mapper.Map<JobViewModel>(item);
                jvm.JobTaskViewModels.AddRange(_mapper.Map<List<JobTaskViewModel>>(item.JobTasks));
                jvms.Add(jvm);
            }
            return jvms;
        }

        public JobViewModel ViewGet(int jobId)
        {
            Job dbJob = _context.Jobs
                .Include(j => j.JobTasks)
                .Where(x => x.Id == jobId)
                .Single();

            var jvm = _mapper.Map<JobViewModel>(dbJob);
            jvm.JobTaskViewModels.AddRange(_mapper.Map<List<JobTaskViewModel>>(dbJob.JobTasks));
            return jvm;
        }

        public int Create(CreateJobDto createJobDto)
        {
            //
            //if the same task name exists for this tenant
            //not allowed
            //
            if (_context.Jobs.Where(x => x.Name == createJobDto.Name && x.TenantId == createJobDto.TenantId).Any())
            {
                throw new OccumetricException("Job name already exists");
            }
            var job = _mapper.Map<Job>(createJobDto);
            var taskList = new List<JobTask>();
            foreach (int id in createJobDto.MasterTaskIds)
            {
                MasterTask mt = _context.MasterTasks
                                .Find(id);

                //
                //snooks and niosh are already calculated
                //and stored in mastertask
                //no need to recalculate  here
                //
                JobTask jobTask = _mapper.Map<JobTask>(mt);
                jobTask.Id = 0;
                taskList.Add(jobTask);
            }
            job.JobTasks = taskList;
            _context.Jobs.Add(job);
            _context.SaveChanges();
            return job.Id;
        }

        public bool Update(UpdateJobDto updateJobDto)
        {
            var dbJob = _context.Jobs.Find(updateJobDto.Id);
            dbJob = _mapper.Map<UpdateJobDto, Job>(updateJobDto, dbJob);
            _context.SaveChanges();
            return true;
        }

        public bool AddNewTasksToJob(int jobId, List<int> MasterTaskIds)
        {
            var dbJob = _context.Jobs.Find(jobId);
            foreach (int id in MasterTaskIds)
            {
                MasterTask mt = _context.MasterTasks
                                .Find(id);

                //
                //snooks and niosh are already calculated
                //and stored in mastertask
                //no need to recalculate  here
                //
                JobTask jobTask = _mapper.Map<JobTask>(mt);
                dbJob.JobTasks.Add(jobTask);
            }
            _context.SaveChanges();
            return true;
        }

        public TenantSummary GetJobCountsByTenant()
        {
            var result = new List<JobCountViewModel>();
            var tenants = _context.Tenants
                            .Include(t => t.Jobs)
                            .ThenInclude(j => j.JobTasks)
                            .Select(t => t).ToList();

            foreach (var tenant in tenants)
            {
                var vm = new JobCountViewModel
                {
                    TenantId = tenant.Id,
                    JobCount = tenant.Jobs.Count
                };
                foreach (var job in tenant.Jobs)
                {
                    vm.TaskCountViewModels.Add(new TaskCountViewModel
                    {
                        JobId = job.Id,
                        TaskCount = job.JobTasks.Count
                    });
                }
                result.Add(vm);
            };
            return new TenantSummary
            {
                JobStats = result
            };
        }
    }
}
