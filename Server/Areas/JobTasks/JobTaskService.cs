using AutoMapper;
using Occumetric.Server.Areas.Common;
using Occumetric.Server.Areas.Helpers;
using Occumetric.Server.Areas.Jobs;
using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Data;
using Occumetric.Shared;
using System.Collections.Generic;
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

        private List<JobTaskViewModel> CollectLifts(int jobId)
        {
            var lifts = (from j in _context.Jobs
                         where j.Id == jobId
                         from t in j.JobTasks
                         where t.EffortType.Contains("Lift") &&
                         t.WeightLb > 1 &&
                         t.IntFromHeight >= 0 &&
                         t.IntToHeight >= 0
                         select _mapper.Map<JobTaskViewModel>(t)).ToList();
            foreach (var item in lifts)
            {
                item.IntFromHeight = Utility.SanitizeStringToInteger(item.FromHeight);
                item.IntToHeight = Utility.SanitizeStringToInteger(item.ToHeight);
            }
            return lifts.OrderBy(x => x.BucketNo)
                         .ThenBy(x => x.WeightLb)
                         .Where(x => x.FromHeight != x.ToHeight)
                         .ToList();
        }

        private List<JobTaskViewModel> DeleteBracketingLiftsWithinBucket(List<JobTaskViewModel> lifts)
        {
            //
            //if there are 2 lifts A and B with identical weights,
            // and lift A has a range that INCLUDES range of lift B
            // remove lift B (don't have to consider it)
            //
            List<int> IdsToDelete = new List<int>();
            foreach (JobTaskViewModel source in lifts)
            {
                if (IdsToDelete.Contains(source.Id)) continue;
                foreach (JobTaskViewModel target in lifts.Where(x => x.Id != source.Id))
                {
                    if (IdsToDelete.Contains(target.Id)) continue;
                    if (source.BracketsAnotherLift(target))
                    {
                        IdsToDelete.Add(target.Id);
                    }
                }
            }
            lifts.RemoveAll(x => IdsToDelete.Contains(x.Id));
            return lifts;
        }

        private List<JobTaskViewModel> RemoveOverlappingLifts(List<JobTaskViewModel> lifts)
        {
            //
            //is there an overlap (identical weight and height range is bracketed
            // then merge
            //
            List<int> IdsToDelete = new List<int>();

            foreach (JobTaskViewModel source in lifts)
            {
                if (IdsToDelete.Contains(source.Id)) continue;
                foreach (JobTaskViewModel target in lifts.Where(x => x.Id != source.Id))
                {
                    if (IdsToDelete.Contains(target.Id)) continue;
                    if (source.OverlapsAnotherLift(target))
                    {
                        source.IsModifiedForBatteryOfTests = true;
                        IdsToDelete.Add(target.Id);
                    }
                }
            }
            lifts.RemoveAll(x => IdsToDelete.Contains(x.Id));
            return lifts;
        }

        private List<JobTaskViewModel> SplitReduntantLifts(List<JobTaskViewModel> lifts)
        {
            //
            //A: 35 lb, 12 to 28 in
            //B: 30 lb, 0 to 20 in
            //since we have already tested for 35 lb at 12 inches, no need to test
            //for 30 lb at 12 inches

            // X: 35 lb, 0 to 12"
            // Y: 30 lb, 0 to 24"
            //becomes X => 0 to 12, Y => 12 to 24"
            //
            //   |----------------| source
            //   |----------------------------| target
            //
            //          OR
            //   |----------------| source
            //        |----------------------------| target
            //
            //modify the target
            //
            foreach (JobTaskViewModel source in lifts)
            {
                foreach (JobTaskViewModel target in lifts
                    .Where(t => t.Id != source.Id &&
                    t.WeightLb < source.WeightLb &&
                    t.IntFromHeight >= source.IntFromHeight &&
                    t.IntFromHeight <= source.IntToHeight &&
                    t.IntToHeight > source.IntToHeight &&
                    t.BucketNo == source.BucketNo))
                {
                    target.OrgFromHeight = target.FromHeight;
                    target.IntFromHeight = source.IntToHeight;
                    target.FromHeight = source.ToHeight;
                    if (!target.IsModifiedForBatteryOfTests)
                    {
                        target.IsModifiedForBatteryOfTests = true;
                    }
                }
            }

            //
            //
            //               |----------------| source
            //   |----------------------------| target
            //
            //                  OR
            //               |----------------| source
            //   |--------------------| target
            //
            //modify the target
            //

            foreach (JobTaskViewModel source in lifts)
            {
                foreach (JobTaskViewModel target in lifts
                    .Where(t => t.Id != source.Id &&
                    t.WeightLb < source.WeightLb &&
                    t.IntFromHeight < source.IntFromHeight &&
                    t.IntToHeight > source.IntFromHeight &&
                    t.IntToHeight <= source.IntToHeight &&
                    t.BucketNo == source.BucketNo))
                {
                    target.OrgToHeight = target.ToHeight;
                    target.IntToHeight = source.IntFromHeight;
                    target.ToHeight = source.FromHeight;
                    if (!target.IsModifiedForBatteryOfTests)
                    {
                        target.IsModifiedForBatteryOfTests = true;
                    }
                }
            }

            //
            //
            //               |----------------| source
            //   |---------------------------------------| target
            //       <cut>                        <new>
            //add a new target in this case
            //
            List<JobTaskViewModel> newTasks = new List<JobTaskViewModel>();
            foreach (JobTaskViewModel source in lifts)
            {
                foreach (JobTaskViewModel target in lifts
                    .Where(t => t.Id != source.Id &&
                    t.WeightLb < source.WeightLb &&
                    t.IntFromHeight < source.IntFromHeight &&
                    t.IntToHeight > source.IntToHeight &&
                    t.BucketNo == source.BucketNo))
                {
                    //
                    //add a new task
                    //
                    var newTask = (JobTaskViewModel)target.Clone();
                    newTask.IntFromHeight = source.IntToHeight;
                    newTask.FromHeight = source.ToHeight;
                    newTask.IsNewForBatteryOfTests = true;
                    newTask.Id = 0;
                    newTasks.Add(newTask);

                    //--
                    target.OrgToHeight = target.ToHeight;
                    target.IntToHeight = source.IntFromHeight;
                    target.ToHeight = source.FromHeight;
                    if (!target.IsModifiedForBatteryOfTests)
                    {
                        target.IsModifiedForBatteryOfTests = true;
                        target.IsSplitForBatteryOfTests = true;
                    }
                }
            }
            lifts.AddRange(newTasks);
            return lifts;
        }

        public List<JobTaskViewModel> SimplifyListsForBotReport(int jobId)
        {
            var lifts = this.CollectLifts(jobId);
            lifts = this.DeleteBracketingLiftsWithinBucket(lifts);
            lifts = this.RemoveOverlappingLifts(lifts);
            return lifts.OrderBy(x => x.WeightLb).ThenBy(x => x.IntFromHeight).ToList();
        }

        //------------
    } // end class
} // end namespace
