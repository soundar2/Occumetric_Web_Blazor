using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            return _mapper.Map<TaskCategoryViewModel>(_context.TaskCategories.Find(id));
        }

        public async Task<bool> Create(CreateTaskCategoryDto dto)
        {
            var tc = new TaskCategory
            {
                Name = dto.Name
            };

            if (dto.IndustryId == 0)
            {
                //
                //this task category applies to all indutries
                //
                await _context.Industries.ForEachAsync(x => x.TaskCategories.Add(tc));
            }
            else
            {
                var ind = await _context.Industries.FindAsync(dto.IndustryId);
                ind.TaskCategories.Add(tc);
            }
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
