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
            var result = await _httpClient.GetFromJsonAsync<List<IndustryViewModel>>("api/v1/industry");
            return result;
        }

        public async Task<List<TaskCategoryViewModel>> GetTaskCategories(int IndustryId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskCategoryViewModel>>($"api/v1/taskCategory/industry/{IndustryId}");
            return result;
        }
    }
}
