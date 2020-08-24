using Occumetric.Shared;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.MasterTasks
{
    public interface IMasterTaskService
    {
        List<MasterTaskViewModel> GetMasterTasks(int IndustryId, int CategoryId = 0);

        //List<MasterTaskViewModel> GetMasterTaskForCategory(int industryId, int CategoryId);

        MasterTaskViewModel Get(int id);

        void Create(CreateMasterTaskDto dto);

        void Update(UpdateMasterTaskDto dto);

        UpdateMasterTaskDto GetForUpdate(int id);
    }
}
