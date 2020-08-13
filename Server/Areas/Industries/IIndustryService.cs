using Occumetric.Shared;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.Industries
{
    public interface IIndustryService
    {
        List<IndustryViewModel> Index();

        IndustryViewModel Get(int Id);

        int Create(CreateIndustryDto dto);

        void Update(UpdateIndustryDto dto);
    }
}
