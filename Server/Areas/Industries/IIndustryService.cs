using Occumetric.Shared;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.Industries
{
    public interface IIndustryService
    {
        List<IndustryViewModel> GetIndustries();

        string CreateIndustry(CreateIndustryDto dto);
    }
}
