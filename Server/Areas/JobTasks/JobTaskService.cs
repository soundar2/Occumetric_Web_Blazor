using AutoMapper;
using Occumetric.Server.Areas.Common;
using Occumetric.Server.Areas.Helpers;
using Occumetric.Server.Areas.Jobs;
using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Data;
using Occumetric.Shared;
using System.Linq;

namespace Occumetric.Server.Areas.JobTasks
{
    public class JobTaskService : OccumetricServiceBase, IJobTaskService
    {
        private readonly IHelperService _helperService;

        public JobTaskService(ApplicationDbContext context, IMapper mapper, IHelperService helperService) : base(context, mapper)
        {
            _helperService = helperService;
        }

        public JobTaskViewModel ViewGet(int id)
        {
            var task = _context.JobTasks.Find(id);
            return _mapper.Map<JobTaskViewModel>(task);
        }

        public UpdateJobTaskDto UpdateGet(int jobTaskId)
        {
            var updatedTask = _context.JobTasks.Find(jobTaskId);

            return _mapper.Map<UpdateJobTaskDto>(updatedTask);
        }

        public bool Update(UpdateJobTaskDto dto)
        {
            var dbJobTask = _context.JobTasks.Find(dto.Id);

            //
            //make sure this task name is unique for this
            //jobTask
            //
            bool exists = (from item in _context.JobTasks
                           where item.Id != dbJobTask.Id
                            && item.Name == dto.Name
                            && item.JobId == dbJobTask.JobId
                           select item).Any();
            if (exists)
            {
                throw new OccumetricException("This task name is already taken in this job description.");
            }

            //
            //calculate niosh and snooks
            //
            var snooksDto = new SnooksCalculateDto
            {
                EffortType = dto.EffortType,
                WeightLb = (int)dto.WeightLb,
                FromHeight = Utility.SanitizeStringToInteger(dto.FromHeight),
                ToHeight = Utility.SanitizeStringToInteger(dto.ToHeight),
            };
            var snooksVm = _helperService.CalculateSnooks(snooksDto);
            dbJobTask.SnooksMale = snooksVm.StrMalePercentage;
            dbJobTask.SnooksFemale = snooksVm.StrFemalePercentage;

            var nioshDto = new NioshCalculateDto
            {
                WeightLb = (int)dto.WeightLb,
                EffortType = dto.EffortType,
                FromHeight = Utility.SanitizeStringToInteger(dto.FromHeight),
                ToHeight = Utility.SanitizeStringToInteger(dto.ToHeight),
                LiftDurationType = dto.LiftDurationType,
                LiftFrequencyType = dto.LiftFrequencyType
            };
            dbJobTask.LiftingIndex = _helperService.GetNioshIndex(nioshDto);
            _mapper.Map<UpdateJobTaskDto, JobTask>(dto, dbJobTask);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int jobTaskId)
        {
            var dbJobTask = _context.JobTasks.Find(jobTaskId);
            _context.JobTasks.Remove(dbJobTask);
            _context.SaveChanges();
            return true;
        }
    }
}
