using Occumetric.Shared;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.TaskCategories
{
    public interface ITaskCategoryService
    {
        void Create(CreateTaskCategoryDto dto);

        void Update(UpdateTaskCategoryDto dto);

        TaskCategoryViewModel Get(int id);

        List<TaskCategoryViewModel> Index();
    }
}
