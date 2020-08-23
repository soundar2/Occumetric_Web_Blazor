using AutoMapper;
using Occumetric.Server.Areas.Helpers;
using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Data;
using Occumetric.Shared;
using System.Collections.Generic;
using System.Linq;

namespace Occumetric.Server.Areas.MasterTasks
{
    public class MasterTaskService : OccumetricServiceBase, IMasterTaskService
    {
        private readonly IHelperService _helperService;

        public MasterTaskService(ApplicationDbContext context, IMapper mapper, IHelperService helperService) : base(context, mapper)
        {
            _helperService = helperService;
        }

        public List<MasterTaskViewModel> GetMasterTasks(int IndustryId, int CategoryId = 0)
        {
            List<MasterTask> dbMasterTasks;

            if (CategoryId == 0)
            {
                dbMasterTasks = (from t in _context.MasterTasks
                                 where t.IndustryId == IndustryId
                                 orderby t.Name
                                 select t).ToList();
            }
            else
            {
                dbMasterTasks = (from map in _context.TaskCategoryMaps
                                 join mt in _context.MasterTasks on map.MasterTaskId equals mt.Id
                                 join tc in _context.TaskCategories on map.TaskCategoryId equals tc.Id
                                 where mt.IndustryId == IndustryId && tc.Id == CategoryId
                                 select mt).ToList();
            }
            var mtIds = (from item in dbMasterTasks select item.Id).ToList(); //master Task Ids
            var dbTcList = (from map in _context.TaskCategoryMaps
                            where mtIds.Contains(map.MasterTaskId)
                            from tc in _context.TaskCategories
                            where tc.Id == map.TaskCategoryId
                            orderby tc.Name
                            select new
                            {
                                MtId = map.MasterTaskId,
                                TaskCategory = tc
                            }).ToList();
            List<MasterTaskViewModel> mtViewModels = _mapper.Map<List<MasterTaskViewModel>>(dbMasterTasks);
            foreach (var mtVm in mtViewModels)
            {
                mtVm.TaskCategoryViewModels = _mapper.Map<List<TaskCategoryViewModel>>((from item in dbTcList where item.MtId == mtVm.Id select item.TaskCategory).ToList());
            }

            return mtViewModels;
        }

        //public List<MasterTaskViewModel> GetMasterTaskForCategory(int industryId, int CategoryId = 0)
        //{
        //    var query = _context.MasterTasks.Where(x => x.IndustryId == industryId);
        //    if (CategoryId > 0)
        //    {
        //        query = (from mt in query from map in _context.TaskCategoryMaps where map.TaskCategoryId == CategoryId select mt);
        //    }

        //    return _mapper.Map<List<MasterTaskViewModel>>(query).ToList();
        //}

        public MasterTaskViewModel Get(int id)
        {
            return _mapper.Map<MasterTaskViewModel>(_context.MasterTasks.Find(id));
        }

        public void Create(CreateMasterTaskDto dto)
        {
            //
            //make sure this task name is unique
            //
            if (_context.MasterTasks.Any(x => x.Name == dto.Name))
            {
                throw new System.Exception("Task name already exists.");
            }

            var mt = _mapper.Map<MasterTask>(dto);

            var intFromHeight = Utility.SanitizeStringToInteger(dto.FromHeight);
            var intToHeight = Utility.SanitizeStringToInteger(dto.ToHeight);

            var snooksValues = _helperService.CalculateSnooks(new SnooksCalculateDto
            {
                EffortType = mt.EffortType,
                FromHeight = intFromHeight,
                ToHeight = intToHeight,
                WeightLb = (int)dto.WeightLb
            });

            mt.SnooksMale = snooksValues.StrMalePercentage;
            mt.SnooksFemale = snooksValues.StrFemalePercentage;

            var nioshIndex = _helperService.GetNioshIndex(new NioshCalculateDto
            {
                EffortType = dto.EffortType,
                FromHeight = intFromHeight,
                ToHeight = intToHeight,
                WeightLb = (int)dto.WeightLb
            });

            mt.LiftingIndex = nioshIndex;
            foreach (var id in dto.IndustryIds)
            {
                //becuase we are creating multiple tasks
                //we have to clone this
                //
                var mtCloned = (MasterTask)mt.Clone();
                var ind = _context.Industries.Find(id);
                ind.MasterTasks.Add(mtCloned);
                _context.SaveChanges();
                foreach (int categoryId in dto.CategoryIds)
                {
                    mtCloned.TaskCategoryMaps.Add(new TaskCategoryMap
                    {
                        TaskCategoryId = categoryId,
                        MasterTaskId = mtCloned.Id
                    });
                }
            }
            _context.SaveChanges();
        }

        public void Update(UpdateMasterTaskDto dto)
        {
            var tenant = _context.MasterTasks.Find(dto.Id);
            tenant.Name = dto.Name;
            _context.SaveChanges();
        }
    }
}
