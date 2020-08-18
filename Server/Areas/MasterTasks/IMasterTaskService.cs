using Occumetric.Shared;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.MasterTasks
{
    public interface IMasterTaskService
    {
        List<MasterTaskViewModel> GetMasterTaskForIndustry(int IndustryId);

        List<MasterTaskViewModel> GetMasterTaskForCategory(int industryId, int CategoryId);

        public MasterTaskViewModel Get(int id);

        public int Create(CreateMasterTaskDto dto);

        public void Update(UpdateMasterTaskDto dto);
    }
}
