using Occumetric.Shared;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.MasterTasks
{
    public interface IMasterTaskService
    {
        List<MasterTaskViewModel> GetMasterTasks(int IndustryId, int CategoryId = 0);

        //List<MasterTaskViewModel> GetMasterTaskForCategory(int industryId, int CategoryId);

        public MasterTaskViewModel Get(int id);

        public void Create(CreateMasterTaskDto dto);

        public void Update(UpdateMasterTaskDto dto);
    }
}
