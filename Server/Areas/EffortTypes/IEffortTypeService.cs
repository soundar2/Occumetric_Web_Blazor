using Occumetric.Shared;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.EffortTypes
{
    public interface IEffortTypeService
    {
        List<EffortTypeViewModel> GetEffortTypes();
    }
}
