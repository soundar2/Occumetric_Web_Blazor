using Occumetric.Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Occumetric.Client.Helpers
{
    public class CommonHttpService : ICommonHttpService
    {
        private readonly HttpClient _httpClient;

        public CommonHttpService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<List<IndustryViewModel>> GetAllIndustries()
        {
            var result = await _httpClient.GetFromJsonAsync<List<IndustryViewModel>>("api/v1/industries");
            return result;
        }

        public async Task<List<TaskCategoryViewModel>> GetTaskCategories()
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskCategoryViewModel>>($"api/v1/taskCategories");
            return result;
        }

        public async Task<List<MasterTaskViewModel>> GetMasterTasksForIndustry(int industryId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<MasterTaskViewModel>>($"api/v1/masterTasks/industry/{industryId}");
            return result;
        }

        public async Task<List<MasterTaskViewModel>> GetMasterTasksForCategory(int industryId, int CategoryId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<MasterTaskViewModel>>($"api/v1/masterTasks/industry/{industryId}/category/{CategoryId}");
            return result;
        }

        public async Task<List<EffortTypeViewModel>> GetEffortTypes()
        {
            var result = await _httpClient.GetFromJsonAsync<List<EffortTypeViewModel>>("api/v1/effortTypes");
            return result;
        }
    }
}
