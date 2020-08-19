using Occumetric.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Occumetric.Client.Helpers
{
    public interface IGlobalsService
    {
        public int IndustryId { get; set; }

        Task<List<EffortTypeViewModel>> GetEffortTypes();
    }
}
