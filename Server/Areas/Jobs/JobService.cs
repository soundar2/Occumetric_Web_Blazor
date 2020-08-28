using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Occumetric.Server.Areas.Common;
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
        public JobService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public List<JobViewModel> GetJobs(int tenantId)
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

        public JobViewModel Details(int jobId)
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
                JobTask jobTask = _mapper.Map<JobTask>(mt);
                jobTask.Id = 0;
                taskList.Add(jobTask);

                //job.JobTasks.Add(jobTask);
            }
            job.JobTasks = taskList;
            _context.Jobs.Add(job);
            _context.SaveChanges();
            return job.Id;
        }
    }
}
