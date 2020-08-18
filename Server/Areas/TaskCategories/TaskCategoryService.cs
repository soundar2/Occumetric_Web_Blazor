using AutoMapper;
using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Data;
using Occumetric.Shared;
using System.Collections.Generic;
using System.Linq;

namespace Occumetric.Server.Areas.TaskCategories
{
    public class TaskCategoryService : OccumetricServiceBase, ITaskCategoryService
    {
        public TaskCategoryService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public List<TaskCategoryViewModel> Index()
        {
            return _mapper.Map<List<TaskCategoryViewModel>>((from tc in _context.TaskCategories orderby tc.Name select tc).ToList());
        }

        public TaskCategoryViewModel Get(int id)
        {
            return _mapper.Map<TaskCategoryViewModel>(_context.TaskCategories
                .Find(id));
        }

        public void Create(CreateTaskCategoryDto dto)
        {
            _context.TaskCategories.Add(new TaskCategory
            {
                Name = dto.Name
            });
            _context.SaveChanges();
        }

        public void Update(UpdateTaskCategoryDto dto)
        {
            var tc = _context.TaskCategories.Find(dto.Id);
            tc.Name = dto.Name;
            _context.SaveChanges();
        }
    }
}
