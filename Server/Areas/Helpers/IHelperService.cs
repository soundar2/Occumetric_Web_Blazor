using Occumetric.Shared;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.Helpers
{
    public interface IHelperService
    {
        List<StateViewModel> GetStates();

        List<EffortTypeViewModel> GetEffortTypes();

        List<LiftFrequencyTypeViewModel> GetLiftFrequencyTypes();

        List<LiftDurationTypeViewModel> GetLiftDurationTypes();

        double GetNioshIndex(NioshCalculateDto dto);

        SnooksViewModel CalculateSnooks(SnooksCalculateDto dto);
    } // end class
}
