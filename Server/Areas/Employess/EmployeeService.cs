using AutoMapper;
using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Data;
using Occumetric.Shared;
using System.Collections.Generic;
using System.Linq;

namespace Occumetric.Server.Areas.Employees
{
    public class EmployeeService : OccumetricServiceBase, IEmployeeService
    {
        public EmployeeService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public List<EmployeeViewModel> GetAllEmployeesForIndustry(int IndustryId)
        {
            return _mapper.Map<List<EmployeeViewModel>>((from t in _context.Employees
                                                       where t.IndustryId == IndustryId
                                                       orderby t.Name
                                                       select t).ToList());
        }

        public EmployeeViewModel Get(int id)
        {
            return _mapper.Map<EmployeeViewModel>(_context.Employees.Find(id));
        }

        public int Create(CreateEmployeeDto dto)
        {
            var employee = new Employee
            {
                Name = dto.Name,
            };
            var parent = _context.Industries.Find(dto.IndustryId);
            parent.Employees.Add(employee);
            _context.SaveChanges();
            return employee.Id;
        }

        public void Update(UpdateEmployeeDto dto)
        {
            var employee = _context.Employees.Find(dto.Id);
            employee.Name = dto.Name;
            _context.SaveChanges();
        }
    }
}
