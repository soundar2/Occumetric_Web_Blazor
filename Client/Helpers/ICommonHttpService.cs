using Occumetric.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Occumetric.Client.Helpers
{
    public interface ICommonHttpService
    {
        public Task<List<IndustryViewModel>> GetAllIndustries();

        Task<List<TaskCategoryViewModel>> GetTaskCategories();

        Task<List<MasterTaskViewModel>> GetMasterTasksForIndustry(int industryId);

        Task<List<MasterTaskViewModel>> GetMasterTasksForCategory(int industryId, int CategoryId);

        Task<List<EffortTypeViewModel>> GetEffortTypes();
    }
}
