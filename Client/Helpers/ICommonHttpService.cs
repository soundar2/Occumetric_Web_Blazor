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

        Task<List<MasterTaskViewModel>> SearchForMasterTasks(int industryId, string searchFor);

        Task<List<MasterTaskViewModel>> GetMasterTasksForCategory(int industryId, int CategoryId);

        Task<List<StateViewModel>> GetStates();

        Task<List<EffortTypeViewModel>> GetEffortTypes();

        Task<List<LiftFrequencyTypeViewModel>> GetLiftFrequencyTypes();

        Task<List<LiftDurationTypeViewModel>> GetLiftDurationTypes();

        Task<double> GetNioshIndex(NioshCalculateDto dto);

        Task<SnooksViewModel> GetSnooksValues(SnooksCalculateDto dto);

        Task<T> GetGeneric<T>(string url) where T : class;

        Task<TOutput> PostGeneric<TInput, TOutput>(string url, TInput input) where TInput : class where TOutput : class;

        Task<(bool result, string message)> PostGeneric<TInput>(string url, TInput input)
                 where TInput : class;

        Task<(bool result, string message)> PutGeneric<TInput>(string url, TInput input)
           where TInput : class;
    }
}
