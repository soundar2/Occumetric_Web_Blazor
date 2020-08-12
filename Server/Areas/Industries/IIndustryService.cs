using Occumetric.Shared;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.Industries
{
    public interface IIndustryService
    {
        List<IndustryViewModel> Index();

        IndustryViewModel Get(string guid);

        string Create(CreateIndustryDto dto);

        void Update(UpdateIndustryDto dto);
    }
}
