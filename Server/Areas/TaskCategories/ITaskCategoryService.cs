using Occumetric.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Occumetric.Server.Areas.TaskCategories
{
    public interface ITaskCategoryService
    {
        Task<bool> Create(CreateTaskCategoryDto dto);

        void Update(UpdateTaskCategoryDto dto);

        TaskCategoryViewModel Get(int id);

        List<TaskCategoryViewModel> Index(int IndustryId);
    }
}
