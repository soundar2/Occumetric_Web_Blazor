using AutoMapper;
using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Data;
using Occumetric.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.TaskCategories
{
    public class TaskCategoryService : OccumetricServiceBase, ITaskCategoryService
    {
        public TaskCategoryService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public List<TaskCategoryViewModel> Index(int IndustryId)
        {
            if (IndustryId == 0)
            {
                return _mapper.Map<List<TaskCategoryViewModel>>((from tc in _context.TaskCategories orderby tc.Industry.Name, tc.Name select tc).ToList());
            }
            else
            {
                return _mapper.Map<List<TaskCategoryViewModel>>((from tc in _context.TaskCategories where tc.IndustryId == IndustryId orderby tc.Name select tc).ToList());
            }
        }

        public TaskCategoryViewModel Get(int id)
        {
            var tc = _context.TaskCategories
                .Find(id);
            var name = tc.Industry.Name + "";
            var vm = _mapper.Map<TaskCategoryViewModel>(tc);
            vm.IndustryViewModel = _mapper.Map<IndustryViewModel>(tc.Industry);
            return vm;
        }

        public async Task<bool> Create(CreateTaskCategoryDto dto)
        {
            foreach (var id in dto.IndustryIds)
            {
                var ind = await _context.Industries.FindAsync(id);
                ind.TaskCategories.Add(new TaskCategory
                {
                    Name = dto.Name
                });
            }
            _context.SaveChanges();
            return true;
        }

        public void Update(UpdateTaskCategoryDto dto)
        {
            var tc = _context.TaskCategories.Find(dto.Id);
            tc.Name = dto.Name;
            _context.SaveChanges();
        }
    }
}
