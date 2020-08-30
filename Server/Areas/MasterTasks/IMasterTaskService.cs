using Occumetric.Shared;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.MasterTasks
{
    public interface IMasterTaskService
    {
        List<MasterTaskViewModel> Index(int IndustryId, int CategoryId = 0);

        List<MasterTaskViewModel> Search(int industryId, string needle);

        //List<MasterTaskViewModel> GetMasterTaskForCategory(int industryId, int CategoryId);

        MasterTaskViewModel ViewGet(int id);

        void Create(CreateMasterTaskDto dto);

        void Update(UpdateMasterTaskDto dto);

        UpdateMasterTaskDto UpdateGet(int id);
    }
}
