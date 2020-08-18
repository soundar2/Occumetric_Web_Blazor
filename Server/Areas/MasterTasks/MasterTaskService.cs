using AutoMapper;
using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Data;
using Occumetric.Shared;
using System.Collections.Generic;
using System.Linq;

namespace Occumetric.Server.Areas.MasterTasks
{
    public class MasterTaskService : OccumetricServiceBase, IMasterTaskService
    {
        public MasterTaskService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public List<MasterTaskViewModel> GetMasterTaskForIndustry(int IndustryId)
        {
            return _mapper.Map<List<MasterTaskViewModel>>((from t in _context.MasterTasks
                                                           where t.IndustryId == IndustryId
                                                           orderby t.Name
                                                           select t).ToList());
        }

        public List<MasterTaskViewModel> GetMasterTaskForCategory(int industryId, int CategoryId = 0)
        {
            var query = _context.MasterTasks.Where(x => x.IndustryId == industryId);
            if (CategoryId > 0)
            {
                query = (from mt in query from map in _context.TaskCategoryMaps where map.TaskCategoryId == CategoryId select mt);
            }

            return _mapper.Map<List<MasterTaskViewModel>>(query).ToList();
        }

        public MasterTaskViewModel Get(int id)
        {
            return _mapper.Map<MasterTaskViewModel>(_context.MasterTasks.Find(id));
        }

        public int Create(CreateMasterTaskDto dto)
        {
            var mt = _mapper.Map<MasterTask>(dto);
            foreach (var id in dto.IndustryIds)
            {
                var ind = _context.Industries.Find(id);
                ind.MasterTasks.Add(mt);
            }
            _context.SaveChanges();
            return mt.Id;
        }

        public void Update(UpdateMasterTaskDto dto)
        {
            var tenant = _context.MasterTasks.Find(dto.Id);
            tenant.Name = dto.Name;
            _context.SaveChanges();
        }
    }
}
