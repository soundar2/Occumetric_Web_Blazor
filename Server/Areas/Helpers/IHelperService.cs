using Occumetric.Shared;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.Helpers
{
    public interface IHelperService
    {
        List<EffortTypeViewModel> GetEffortTypes();

        List<LiftFrequencyTypeViewModel> GetLiftFrequencyTypes();

        List<LiftDurationTypeViewModel> GetLiftDurationTypes();

        double GetNioshIndex(NioshCalculateDto dto);

        SnooksViewModel CalculateSnooks(SnooksCalculateDto dto);
    } // end class
}
