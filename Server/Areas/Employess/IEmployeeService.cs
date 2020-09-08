using Occumetric.Shared;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.Employees
{
    public interface IEmployeeService
    {
        void Update(UpdateEmployeeDto dto);

        int Create(CreateEmployeeDto dto);

        EmployeeViewModel Get(int id);

        List<EmployeeViewModel> GetAllEmployeesForIndustry(int IndustryId);
    }
}
