﻿using Occumetric.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Occumetric.Client.Helpers
{
    public class GlobalsService : IGlobalsService
    {
        private ICommonHttpService _httpService { get; set; }

        public int IndustryId { get; set; }

        public GlobalsService(ICommonHttpService httpService)
        {
            _httpService = httpService;
        }

        private List<EffortTypeViewModel> _effortTypeViewModels = new List<EffortTypeViewModel>();

        //------------------------
        public async Task<List<EffortTypeViewModel>> GetEffortTypes()
        {
            if (_effortTypeViewModels.Count == 0)
            {
                _effortTypeViewModels = await _httpService.GetEffortTypes();
            }
            return _effortTypeViewModels;
        }

        //------------------------
        private List<IndustryViewModel> _industryViewModels = new List<IndustryViewModel>();

        public async Task<List<IndustryViewModel>> GetIndustrys()
        {
            if (_industryViewModels.Count == 0)
            {
                _industryViewModels = await _httpService.GetAllIndustries();
            }
            return _industryViewModels;
        }

        //------------------------
    }
}
