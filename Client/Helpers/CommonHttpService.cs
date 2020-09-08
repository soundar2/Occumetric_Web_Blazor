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
        public static readonly string BaseUrl = "/api/v1";

        public CommonHttpService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<List<IndustryViewModel>> GetAllIndustries()
        {
            var result = await _httpClient.GetFromJsonAsync<List<IndustryViewModel>>(BaseUrl + "/industries");
            return result;
        }

        public async Task<List<TaskCategoryViewModel>> GetTaskCategories()
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskCategoryViewModel>>(BaseUrl + $"/taskCategories");
            return result;
        }

        public async Task<List<MasterTaskViewModel>> GetMasterTasksForIndustry(int industryId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<MasterTaskViewModel>>(BaseUrl + $"/masterTasks/industry/{industryId}");
            return result;
        }

        public async Task<List<MasterTaskViewModel>> GetMasterTasksForCategory(int industryId, int CategoryId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<MasterTaskViewModel>>(BaseUrl + $"/masterTasks/industry/{industryId}/category/{CategoryId}");
            return result;
        }

        public async Task<List<MasterTaskViewModel>> SearchForMasterTasks(int industryId, string searchFor)
        {
            var result = await _httpClient.GetFromJsonAsync<List<MasterTaskViewModel>>(BaseUrl + $"/masterTasks/industry/{industryId}/search/{searchFor}");
            return result;
        }

        public async Task<List<StateViewModel>> GetStates()
        {
            var result = await _httpClient.GetFromJsonAsync<List<StateViewModel>>(BaseUrl + "/helpers/states");
            return result;
        }

        public async Task<List<EffortTypeViewModel>> GetEffortTypes()
        {
            var result = await _httpClient.GetFromJsonAsync<List<EffortTypeViewModel>>(BaseUrl + "/helpers/effortTypes");
            return result;
        }

        public async Task<List<LiftDurationTypeViewModel>> GetLiftDurationTypes()
        {
            var result = await _httpClient.GetFromJsonAsync<List<LiftDurationTypeViewModel>>(BaseUrl + "/helpers/liftDurationTypes");
            return result;
        }

        public async Task<List<LiftFrequencyTypeViewModel>> GetLiftFrequencyTypes()
        {
            var result = await _httpClient.GetFromJsonAsync<List<LiftFrequencyTypeViewModel>>(BaseUrl + "/helpers/liftFrequencyTypes");
            return result;
        }

        public async Task<double> GetNioshIndex(NioshCalculateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync<NioshCalculateDto>(BaseUrl + "/helpers/nioshIndex", dto);
            var nioshIndex = response.Content.ReadFromJsonAsync<double>().Result;
            return nioshIndex;
        }

        public async Task<SnooksViewModel> GetSnooksValues(SnooksCalculateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync<SnooksCalculateDto>(BaseUrl + "/helpers/snooks", dto);
            var viewModel = response.Content.ReadFromJsonAsync<SnooksViewModel>().Result;
            return viewModel;
        }

        #region GenericGetAndPost

        public async Task<T> GetGeneric<T>(string url) where T : class
        {
            var result = await _httpClient.GetFromJsonAsync<T>(BaseUrl + url);
            return result;
        }

        public async Task<TOutput> PostGeneric<TInput, TOutput>(string url, TInput input)
            where TInput : class
            where TOutput : class
        {
            var response = await _httpClient.PostAsJsonAsync<TInput>(BaseUrl + url, input);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadFromJsonAsync<TOutput>().Result;
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<(bool result, string message)> PostGeneric<TInput>(string url, TInput input)
                 where TInput : class
        {
            var response = await _httpClient.PostAsJsonAsync<TInput>(BaseUrl + url, input);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadFromJsonAsync<StringResult>().Result;
                return (true, result.Result);
            }
            else
            {
                var errResult = response.Content.ReadFromJsonAsync<ErrorResult>().Result;
                return (false, errResult.Result);
            }
        }

        public async Task<(bool result, string message)> PutGeneric<TInput>(string url, TInput input)
         where TInput : class
        {
            var response = await _httpClient.PutAsJsonAsync<TInput>(BaseUrl + url, input);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadFromJsonAsync<StringResult>().Result;
                return (true, result.Result);
            }
            else
            {
                var errResult = response.Content.ReadFromJsonAsync<ErrorResult>().Result;
                return (false, errResult.Result);
            }
        }

        #endregion GenericGetAndPost
    }
}
